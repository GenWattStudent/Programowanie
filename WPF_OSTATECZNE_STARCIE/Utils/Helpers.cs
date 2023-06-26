using System;
using System.Reflection;

namespace WPF_OSTATECZNE_STARCIE.Utils;

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
}