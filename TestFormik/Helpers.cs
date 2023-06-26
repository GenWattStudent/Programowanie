using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace TestFormik;


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