
using System.Reflection;
using System.Windows.Controls;
using Formik.Options;

namespace Formik.Inputs
{
    public class InputFactory
    {
        public InputReturn CreateInput<T>(FormikOption option, T model, Panel panel, PropertyInfo property, Label label)
        {
            InputReturn inputReturn = new InputReturn();

            if (option is FormikDatePickerOption)
            {
                inputReturn = new DatePickerInput(model, property, label).Create();
                inputReturn.Control.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                panel.Children.Add(inputReturn.Control);
            }
            else if (option is FormikSelectOption selectOption)
            {
                inputReturn = new ComboxInput(model, property, label, selectOption).Create();
                inputReturn.Control.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                panel.Children.Add(inputReturn.Control);
            }
            else
            {
                inputReturn = new TextboxInput(model, property, label).Create();
                panel.Children.Add(inputReturn.Control);
                panel.Children.Insert(panel.Children.IndexOf(inputReturn.Control) + 1, inputReturn.ErrorLabel);
            }

            return inputReturn;
        }
    }
}