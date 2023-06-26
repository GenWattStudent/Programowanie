using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestFormik;

public class Formik<T>
{
    private readonly T _model;
    private readonly StackPanel _stackPanel;
    private bool _isCollection = false;

    public Formik(T model, StackPanel stackPanel, bool isCollection = false)
    {
        _model = model;
        _stackPanel = stackPanel;
        _isCollection = isCollection;
    }
    public void Update()
    {
        GenerateForm();
    }

    public void GenerateForm()
    {
        _stackPanel.Children.Clear();

        foreach (PropertyInfo property in typeof(T).GetProperties())
        {
            if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
            {
                AddCollectionForm(property);
            }
            else
            {
                AddField(property);
            }
        }
        
        if (!_isCollection) {
            Button submitButton = Helpers.CreateButton("Submit", (sender) => {
                T modelCopy = Helpers.CopyObject(_model);
                Submit?.Invoke(modelCopy);
            });

            _stackPanel.Children.Add(submitButton);
        }
    }

    public event Action<T> Submit;

    private void AddField(PropertyInfo property)
    {
        Label label = new Label { Content = Helpers.CamelCaseToSentence(property.Name) };
        _stackPanel.Children.Add(label);

        if (property.PropertyType == typeof(DateTime))
        {
            DateTime defaultValue = DateTime.Today;
            if (property.GetGetMethod().GetCustomAttributes(typeof(DefaultValueAttribute), true).FirstOrDefault() is DefaultValueAttribute defaultValueAttribute)
            {
                defaultValue = (DateTime)defaultValueAttribute.Value;
            }

            DatePicker datePicker = new DatePicker { SelectedDate = defaultValue };
            datePicker.SelectedDateChanged += (sender, args) =>
            {
                property.SetValue(_model, datePicker.SelectedDate);
            };
            _stackPanel.Children.Add(datePicker);
        }
        else if (property.PropertyType.IsEnum)
        {
            ComboBox comboBox = new ComboBox { ItemsSource = Enum.GetValues(property.PropertyType) };
            comboBox.SelectedItem = property.GetValue(_model);
            comboBox.SelectionChanged += (sender, args) =>
            {
                property.SetValue(_model, comboBox.SelectedItem);
            };
            _stackPanel.Children.Add(comboBox);
        }
        else
        {
            TextBox textBox = new TextBox { Text = property.GetValue(_model)?.ToString() };
            Label errorLabel = new Label();
            errorLabel.Visibility = Visibility.Collapsed;
            errorLabel.Foreground = Brushes.Red;
            textBox.TextChanged += (sender, args) =>
            {
                property.SetValue(_model, Convert.ChangeType(textBox.Text, property.PropertyType));
                List<string> validationErrors = new List<string>();
                foreach (var attribute in property.GetCustomAttributes(true))
                {
                    if (attribute is RequiredAttribute requiredAttribute && !requiredAttribute.IsValid(property.GetValue(_model)))
                    {
                        validationErrors.Add(requiredAttribute.ErrorMessage);
                    }
                    else if (attribute is StringLengthAttribute stringLengthAttribute && !stringLengthAttribute.IsValid(property.GetValue(_model)))
                    {
                        validationErrors.Add(stringLengthAttribute.ErrorMessage);
                    }
                    // Add other validation attributes here
                }

                // Display validation errors
                if (validationErrors.Any())
                {
                    // remove validation errors that are null 
                    validationErrors.RemoveAll(x => x == null);
                    
                    label.Foreground = Brushes.Red;
                    errorLabel.Content = string.Join(", ", validationErrors);
                    errorLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    label.Foreground = Brushes.Black;
                    errorLabel.Visibility = Visibility.Collapsed;
                }
            };
            _stackPanel.Children.Add(textBox);
            _stackPanel.Children.Insert(_stackPanel.Children.IndexOf(textBox) + 1, errorLabel);
        }
    }

    private void AddCollectionForm(PropertyInfo property)
    {
        var prop = property.PropertyType.GetGenericArguments()[0];

        IEnumerable collection = (IEnumerable)property.GetValue(_model);
        if (collection == null)
        {
            collection = (IEnumerable)Activator.CreateInstance(property.PropertyType);
            property.SetValue(_model, collection);
        }

        if (!collection.Cast<object>().Any())
        {
            object defaultItem = null;
            if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
            {
                Type itemType = prop;
                defaultItem = Activator.CreateInstance(itemType);
            }
            else if (property.PropertyType.IsArray)
            {
                Type itemType = property.PropertyType.GetElementType();
                defaultItem = Activator.CreateInstance(itemType);
            }

            if (defaultItem != null)
            {
                collection.GetType().GetMethod("Add").Invoke(collection, new[] { defaultItem });
            }
        }
        int i = 0;
        foreach (object item in collection)
        {
            StackPanel itemPanel = new StackPanel();

            Type itemType = item.GetType();
            Type formikType = typeof(Formik<>).MakeGenericType(itemType);
            object itemFormGenerator = Activator.CreateInstance(formikType, item, itemPanel, true);
            formikType.GetMethod("GenerateForm").Invoke(itemFormGenerator, null);
            Label itemLabel = new Label { Content = $"{Helpers.CamelCaseToSentence(item.GetType().Name)} {i + 1}", FontWeight = FontWeights.Bold };

            itemPanel.Children.Insert(0, itemLabel);

            _stackPanel.Children.Add(itemPanel);
            if (i > 0) {
                Button removeButton = Helpers.CreateButton("Remove");
                removeButton.Background = Brushes.Red;
                removeButton.Foreground = Brushes.White;
                removeButton.Click += (sender, args) =>
                {
                    collection.GetType().GetMethod("Remove").Invoke(collection, new[] { item });
                    _stackPanel.Children.Remove(itemPanel);
                    _stackPanel.Children.Remove(removeButton);
                    Update();
                };
                _stackPanel.Children.Add(removeButton);
            }

            i++;
        }

        Button addButton = Helpers.CreateButton("Add");
        addButton.Click += (sender, args) =>
        {
            object newItem = Activator.CreateInstance(property.PropertyType.GetGenericArguments()[0]);
            collection.GetType().GetMethod("Add").Invoke(collection, new[] { newItem });
            StackPanel itemPanel = new StackPanel();

            int index = collection.Cast<object>().Count();
            Label itemLabel = new Label { Content = $"{newItem.GetType().Name} {index}", FontWeight = FontWeights.Bold };

             _stackPanel.Children.Insert(_stackPanel.Children.IndexOf(addButton), itemLabel);

            Type itemType = newItem.GetType();
            Type formikType = typeof(Formik<>).MakeGenericType(itemType);
            object itemFormGenerator = Activator.CreateInstance(formikType, newItem, itemPanel, true);
            formikType.GetMethod("GenerateForm").Invoke(itemFormGenerator, null);

            _stackPanel.Children.Insert(_stackPanel.Children.IndexOf(addButton), itemPanel);
            
            Button removeButton = Helpers.CreateButton("Remove");
            removeButton.Background = Brushes.Red;
            removeButton.Foreground = Brushes.White;
            removeButton.Click += (sender, args) =>
            {
                collection.GetType().GetMethod("Remove").Invoke(collection, new[] { newItem });
                _stackPanel.Children.Remove(itemPanel);
                _stackPanel.Children.Remove(removeButton);
                _stackPanel.Children.Remove(itemLabel);
            };
            _stackPanel.Children.Insert(_stackPanel.Children.IndexOf(addButton), removeButton);
        };
        _stackPanel.Children.Add(addButton);
    }

    public void Clear()
    {
        foreach (UIElement element in _stackPanel.Children)
        {
            if (element is TextBox textBox)
            {
                textBox.Text = string.Empty;
            }
            else if (element is ComboBox comboBox)
            {
                comboBox.SelectedIndex = 0;
            }
            else if (element is StackPanel subPanel)
            {
                foreach (UIElement subElement in subPanel.Children)
                {
                    if (subElement is TextBox subTextBox)
                    {
                        subTextBox.Text = string.Empty;
                    }
                    else if (subElement is ComboBox subComboBox)
                    {
                        subComboBox.SelectedIndex = 0;
                    }
                }
            }
        }

        foreach (PropertyInfo property in _model.GetType().GetProperties())
        {
            if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
            {
                // object subModel = property.GetValue(_model);
                // if (subModel != null)
                // {
                //     MethodInfo clearMethod = subModel.GetType().GetMethod("Clear");
                //     if (clearMethod != null)
                //     {
                //         clearMethod.Invoke(subModel, null);
                //     }
                // }
            }
            else if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
            {
                IList collection = (IList)property.GetValue(_model);
                if (collection.Count > 1)
                {
                    for (int i = collection.Count - 2; i > 0; i--)
                    {
                        // collection.RemoveAt(i);
                        _stackPanel.Children.RemoveAt(_stackPanel.Children.Count - 2);
                        _stackPanel.Children.RemoveAt(_stackPanel.Children.Count - 1);
                    }
                }
            }
        }
    }
}