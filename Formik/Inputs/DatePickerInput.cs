using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace Formik.Inputs
{
    public class DatePickerInput
    {
        private readonly object _model;
        private readonly PropertyInfo property;
        private readonly Label label;
        
        public DatePickerInput(object model, PropertyInfo property, Label label)
        {
            _model = model;
            this.property = property;
            this.label = label;
        }
        public InputReturn Create() {
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

            return new InputReturn { Label = label, Control = datePicker };
        }
        
    }
}