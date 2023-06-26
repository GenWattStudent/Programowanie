

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Formik.Inputs
{
    public class InputReturn {
        public Label Label { get; set; }
        public Control Control { get; set; }
        public Label ErrorLabel { get; set; }
    }
    public class TextboxInput
    {
        private readonly object _model;
        private readonly PropertyInfo property;
        private readonly Label label;
        
        public TextboxInput(object model, PropertyInfo property, Label label)
        {
            _model = model;
            this.property = property;
            this.label = label;
        }

        public InputReturn Create() {
            TextBox textBox = new TextBox { Text = property.GetValue(_model)?.ToString() };
            Label errorLabel = new Label();
            errorLabel.Visibility = Visibility.Collapsed;
            errorLabel.Foreground = Brushes.Red;
            textBox.TextChanged += (sender, args) =>
            {
                object value;
                if (property.PropertyType == typeof(int))
                {
                    var result = int.TryParse(textBox.Text, out int intValue);
                    if (result)
                    {
                        value = intValue;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (property.PropertyType == typeof(double))
                {
                    if (textBox.Text is string)
                    {
                        return;
                    }
                    value = double.Parse(textBox.Text);
                }
                else
                {
                    value = Convert.ChangeType(textBox.Text, property.PropertyType);
                }

                property.SetValue(_model, value);
                List<string> validationErrors = new List<string>();
                var validationContext = new ValidationContext(_model);
                var validationResults = new List<ValidationResult>();

                if (!Validator.TryValidateObject(_model, validationContext, validationResults, true))
                {
                    foreach (var validationResult in validationResults)
                    {
                        validationErrors.Add(validationResult.ErrorMessage);
                    }
                }

                validationErrors.RemoveAll(x => x == null);

                if (validationErrors.Any())
                {
                    errorLabel.Content = string.Join(", ", validationErrors);
                    errorLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    errorLabel.Visibility = Visibility.Collapsed;
                }
            };

            return new InputReturn { Label = label, Control = textBox, ErrorLabel = errorLabel };
        }
    }
}