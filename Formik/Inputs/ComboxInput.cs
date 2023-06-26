using System;
using System.Reflection;
using System.Windows.Controls;
using Formik.Options;

namespace Formik.Inputs
{
    public class ComboxInput
    {
        private readonly object _model;
        private readonly Label label;
        private readonly PropertyInfo property;
        private readonly FormikSelectOption option;

        public ComboxInput(object model, PropertyInfo property, Label label, FormikSelectOption option)
        {
            _model = model;
            this.property = property;
            this.label = label;
            this.option = option;
        }
        public InputReturn Create() {
            ComboBox comboBox = new ComboBox();

            if (option.SourceItems is not null)
            {
                comboBox.ItemsSource = option.SourceItems;
            }
            else {
                comboBox.ItemsSource = Enum.GetValues(property.PropertyType);
            }

            if (option.SelectedItem is not null) {
                comboBox.SelectedItem = option.SelectedItem;
                property.SetValue(_model, option.SelectedItem);
            }
            else {
                if (property.GetValue(_model) is not null)
                {
                    comboBox.SelectedItem = property.GetValue(_model);
                }
            }

            comboBox.SelectionChanged += (sender, args) =>
            {
                property.SetValue(_model, comboBox.SelectedItem);
            };

            return new InputReturn
            {
                Control = comboBox,
                Label = label
            };
        }
    }
}