using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;

namespace Formik.Options
{
    public partial class OptionFactory
    {
        public FormikOption CreateOption<T>(PropertyInfo propInfo, T entity)
        {

            if (propInfo.PropertyType == typeof(string))
            {
                return new FormikTextBoxOption();
            }
            else if (propInfo.PropertyType == typeof(DateTime))
            {
                return new FormikDatePickerOption();
            }
            else if (propInfo.PropertyType.IsEnum)
            {
                return new FormikSelectOption { SourceItems = new List<string>(Enum.GetNames(propInfo.PropertyType)) };
            }
            // if prop is numeric 
            else if (propInfo.PropertyType == typeof(int) || propInfo.PropertyType == typeof(double) || propInfo.PropertyType == typeof(float))
            {
                return new FormikNumericOption();
            }
            else if (typeof(IEnumerable).IsAssignableFrom(propInfo.PropertyType) && propInfo.PropertyType != typeof(string))
            {   
                var collectionType = propInfo.PropertyType.GetGenericArguments()[0];
                return new FormikCollectionOption { PropInfo = propInfo, CollectionType = collectionType };
            }

            return new FormikOption();
        }
    }
}