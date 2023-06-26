using MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Window
    {
        private StringList Items;
        public App()
        {
            InitializeComponent();
            Items = new StringList(new Model.StringListModel { Items = new ObservableCollection<string>() { "C++", "Java" } });
            Items.SelectedItem = "C++";
            DataContext = Items;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Items.Items.Add(Items.NewItem);
            Items.NewItem = "";
        }
    }
}
