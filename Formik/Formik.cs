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
using Formik;
using Formik.Inputs;
using Formik.Options;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace TestFormik;

public class FormikInput
{
    public PropertyInfo Property { get; set; }
    public Control Control { get; set; }
    public Label Label { get; set; }
    public Label ErrorLabel { get; set; }
    public Dictionary<string, ValidationAttribute> Validations { get; set; } = new Dictionary<string, ValidationAttribute>();
}

public class Formik<T>
{
    private T _model;
    private readonly Panel _stackPanel;
    private bool _isCollection = false;
    private bool isUpdate = false;
    private readonly List<FormikInput> _formikInputs = new List<FormikInput>();
    private readonly Dictionary<string, FormikOption> _options = new Dictionary<string, FormikOption>();
    private readonly OptionCreator _optionCreator = new OptionCreator();

    public Formik(T model, Panel stackPanel, Dictionary<string, FormikOption>? options = null, bool isCollection = false)
    {
        _model = model;
        _stackPanel = stackPanel;
        _isCollection = isCollection;
        _options = options ?? new Dictionary<string, FormikOption>();
    }
    public void Update(T newModel)
    {
        isUpdate = true;
        // Update the model
        foreach (PropertyInfo property in typeof(T).GetProperties())
        {
            if (property.CanWrite)
            {
                property.SetValue(_model, property.GetValue(newModel));
            }
        }
        GenerateForm();
    }
    public void GenerateForm()
    {
        _stackPanel.Children.Clear();

        foreach (PropertyInfo property in typeof(T).GetProperties())
        {
            if ((Helpers.IsPropList(property) || Helpers.IsPropDateTime(property) || Helpers.IsPropInt(property) || Helpers.IsPropString(property) && !Helpers.HasPropKey(property) || Helpers.IsPropEnum(property)) && !Helpers.HasIdInName(property))
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
        }

        if (!_isCollection)
        {
            var buttonStack = new StackPanel { Orientation = Orientation.Horizontal };
            Button submitButton = Helpers.CreateButton("Submit", (sender) =>
            {
                T modelCopy = Helpers.CopyObject(_model);
                Submit?.Invoke(modelCopy);
                _model = (T)Activator.CreateInstance(typeof(T));
                GenerateForm();
                isUpdate = false;
            });

            Button cancelButton = Helpers.CreateButton("Cancel", (sender) =>
            {
                Clear();
                isUpdate = false;
            });

            submitButton.Style = (Style)Application.Current.MainWindow.FindResource("SubmitButtonStyle");
            submitButton.HorizontalAlignment = HorizontalAlignment.Left;
            cancelButton.Style = (Style)Application.Current.MainWindow.FindResource("CancelButtonStyle");
            cancelButton.Margin = new Thickness(10, 0, 0, 0);
            buttonStack.Children.Add(submitButton);
            buttonStack.Children.Add(cancelButton);
            _stackPanel.Children.Add(buttonStack);
        }
    }

    public event Action<T> Submit;

    private void AddField(PropertyInfo property)
    {
        Label label = new Label { Content = Helpers.CamelCaseToSentence(property.Name) };
        _stackPanel.Children.Add(label);

        var option = _optionCreator.CreateOption(property, _model, _options);
        var inputFactory = new InputFactory();

        var inputReturn = inputFactory.CreateInput(option, _model, _stackPanel, property, label);

        var formikInput = new FormikInput
        {
            Property = property,
            Control = inputReturn.Control,
            Label = label,
            ErrorLabel = inputReturn.ErrorLabel
        };

        var validations = property.GetCustomAttributes(typeof(ValidationAttribute), true)
            .Cast<ValidationAttribute>()
            .ToDictionary(v => v.GetType().Name, v => v);

        formikInput.Validations = validations;

        _formikInputs.Add(formikInput);
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

        int i = 0;

        foreach (object item in collection)
        {
            StackPanel itemPanel = new StackPanel();

            Type itemType = item.GetType();
            Type formikType = typeof(Formik<>).MakeGenericType(itemType);
            object itemFormGenerator = Activator.CreateInstance(formikType, item, itemPanel, new Dictionary<string, FormikOption>(), true);
            formikType.GetMethod("GenerateForm").Invoke(itemFormGenerator, null);
            Label itemLabel = new Label { Content = $"{Helpers.CamelCaseToSentence(item.GetType().Name)} {i + 1}", FontWeight = FontWeights.Bold };

            itemPanel.Children.Insert(0, itemLabel);

            _stackPanel.Children.Add(itemPanel);
            if (i > 0)
            {
                var removeButton = Buttons.RemoveButton(_stackPanel, collection, item, itemPanel);
                removeButton.HorizontalAlignment = HorizontalAlignment.Left;
                _stackPanel.Children.Add(removeButton);
            }
            i++;
        }

        var addButton = Buttons.AddButton(_stackPanel, collection, property);
        addButton.HorizontalAlignment = HorizontalAlignment.Left;
        _stackPanel.Children.Add(addButton);
    }

    public void Clear()
    {
        isUpdate = false;
        Helpers.ClearPanel(_stackPanel, _model);
        _model = (T)Activator.CreateInstance(typeof(T));
        GenerateForm();
    }

    public bool Validate()
    {
        bool isValid = true;

        foreach (var formikInput in _formikInputs)
        {
            var property = formikInput.Property;
            var control = formikInput.Control;
            var label = formikInput.Label;
            var errorLabel = formikInput.ErrorLabel;
            var validations = formikInput.Validations;

            errorLabel.Content = string.Empty;

            foreach (var validation in validations.Values)
            {
                var context = new ValidationContext(_model) { MemberName = property.Name };
                var validationResult = validation.GetValidationResult(property.GetValue(_model), context);

                if (validationResult != ValidationResult.Success)
                {
                    isValid = false;
                    errorLabel.Content = validationResult.ErrorMessage;
                    break;
                }
            }
        }

        return isValid;
    }
}