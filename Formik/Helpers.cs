using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Formik;

public class Helpers {

    public static T? CopyObject<T>(T obj) {
        T copy = (T)Activator.CreateInstance(typeof(T));
        PropertyInfo[] properties = obj.GetType().GetProperties();

        foreach (PropertyInfo property in properties) {
            if (property.CanWrite) {
                property.SetValue(copy, property.GetValue(obj, null), null);
            }
        }
        
        return copy;
    }

    public static void ClearPanel(Panel panel, object model) {
        foreach (UIElement element in panel.Children)
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

        foreach (PropertyInfo property in model.GetType().GetProperties())
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
                IList collection = (IList)property.GetValue(model);
                if (collection.Count > 1)
                {
                    for (int i = collection.Count - 2; i > 0; i--)
                    {
                        panel.Children.RemoveAt(panel.Children.Count - 2);
                        panel.Children.RemoveAt(panel.Children.Count - 1);
                    }
                }
            }
        }
    }

    public static string CamelCaseToSentence(string camelCase) {
        string sentence = "";
        foreach (char c in camelCase) {
            if (char.IsUpper(c)) {
                sentence += " ";
            }
            sentence += c;
        }

        return sentence;
    }

    public static Button CreateButton(string content, Action<object>? submitAction = null) {
        Button submitButton = new Button { Content = content, Margin = new Thickness(0, 10, 0, 10)} ;
        if (submitAction != null) {
            submitButton.Click += (sender, args) => {
                submitAction(sender);
            };
        }
        return submitButton;
    }

    public static bool IsPropString(PropertyInfo property) {
        return property.PropertyType == typeof(string);
    }

    public static bool IsPropInt(PropertyInfo property) {
        return property.PropertyType == typeof(int);
    }

    public static bool IsPropDateTime(PropertyInfo property) {
        return property.PropertyType == typeof(DateTime);
    }

    public static bool IsPropBool(PropertyInfo property) {
        return property.PropertyType == typeof(bool);
    }


    public static bool IsPropEnum(PropertyInfo property) {
        return property.PropertyType.IsEnum;
    }

    public static bool IsPropNullable(PropertyInfo property) {
        return property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    public static bool IsPropList(PropertyInfo property) {
        return property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>);
    }

    public static bool HasPropKey(PropertyInfo property) {
        return property.GetCustomAttribute(typeof(KeyAttribute)) != null;
    }

    public static bool HasIdInName(PropertyInfo property) {
        return property.Name.ToLower().Contains("id");
    }
}