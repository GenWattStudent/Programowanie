using System.Collections.Generic;
using System.Reflection;
using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Formik.Options;
using TestFormik;

namespace Formik.Inputs;

public class Buttons
{
    public static Button RemoveButton(Panel parent, IEnumerable collection, object item, StackPanel itemPanel) {
        Button removeButton = Helpers.CreateButton("Remove");
        removeButton.Style = (Style)Application.Current.MainWindow.FindResource("DeleteButtonStyle");
        removeButton.Click += (sender, args) =>
        {
            collection.GetType().GetMethod("Remove").Invoke(collection, new[] { item });
            parent.Children.Remove(itemPanel);
            parent.Children.Remove(removeButton);
        };

        return removeButton;
    }

    public static Button AddButton(Panel parent, IEnumerable collection, PropertyInfo property) {
        Button addButton = Helpers.CreateButton("Add");
        addButton.Click += (sender, args) =>
        {
            object newItem = Activator.CreateInstance(property.PropertyType.GetGenericArguments()[0]);
            collection.GetType().GetMethod("Add").Invoke(collection, new[] { newItem });
            StackPanel itemPanel = new StackPanel();

            int index = collection.Cast<object>().Count();
            Label itemLabel = new Label { Content = $"{newItem.GetType().Name} {index}", FontWeight = FontWeights.Bold };

            parent.Children.Insert(parent.Children.IndexOf(addButton), itemLabel);

            Type itemType = newItem.GetType();
            Type formikType = typeof(Formik<>).MakeGenericType(itemType);
            object itemFormGenerator = Activator.CreateInstance(formikType, newItem, itemPanel, new Dictionary<string, FormikOption>(), true);
            formikType.GetMethod("GenerateForm").Invoke(itemFormGenerator, null);

            parent.Children.Insert(parent.Children.IndexOf(addButton), itemPanel);

            Button removeButton = RemoveButton(parent, collection, newItem, itemPanel);
            removeButton.Click += (sender, args) =>
            {
                collection.GetType().GetMethod("Remove").Invoke(collection, new[] { newItem });
                parent.Children.Remove(itemPanel);
                parent.Children.Remove(removeButton);
                parent.Children.Remove(itemLabel);
            };

            parent.Children.Insert(parent.Children.IndexOf(addButton), removeButton);
        };

        return addButton;
    }
}
