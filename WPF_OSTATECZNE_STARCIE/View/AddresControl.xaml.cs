using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_OSTATECZNE_STARCIE.Model;
using WPF_OSTATECZNE_STARCIE.ViewModel;

namespace WPF_OSTATECZNE_STARCIE.View
{
    /// <summary>
    /// Logika interakcji dla klasy AddresControl.xaml
    /// </summary>
    public partial class AddresControl : UserControl
    {
        ApplicationContext appContext = new ApplicationContext();
        AddressesViewModel context;
        
        public AddresControl()
        {
            InitializeComponent();
            var employees = appContext.Employees.ToList();
            var addresses = appContext.Addresses.ToList();

            var employeesCollection = new ObservableCollection<Employee>(employees);
            var addressesCollection = new ObservableCollection<Address>(addresses);
            var address = new Address();

            context = new AddressesViewModel(address, addressesCollection, employeesCollection);

            DataContext = context;
        }
    }
}
