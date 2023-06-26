using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Formik.Options
{
    public partial class FormikOption
    {
        
    }

    public partial class FormikTextBoxOption : FormikOption
    {
        public string Text { get; set; } = "";
    }

    public partial class FormikNumericOption : FormikTextBoxOption
    {
        public double Min { get; set; } = 0;
        public double Max { get; set; } = 100;
    }

    public partial class FormikDatePickerOption : FormikOption
    {
        public DateTime Date { get; set; } = DateTime.Now;
    }

    public partial class FormikSelectOption : FormikOption
    {
        public List<string> SourceItems { get; set; } = new List<string>();
        public string SelectedItem { get; set; } = "";
    }

    public partial class FormikCollectionOption : FormikOption
    {
        public Dictionary<string, FormikOption> Options { get; set; } = new Dictionary<string, FormikOption>();
        public PropertyInfo PropInfo { get; set; }
        public Type CollectionType { get; set; } = typeof(object);    
    }
}