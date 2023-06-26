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

namespace WPF_OSTATECZNE_STARCIE;
// public class Context {
//     public EmployeeViewModel employeeViewModel { get; set; }
//     public AddressesViewModel addressesViewModel { get; set; }
// }
public partial class MainWindow : Window
{
    // ApplicationContext appContext = new ApplicationContext();
    // Context context = new Context();

    public MainWindow()
    {
        InitializeComponent();
        // context.employeeViewModel = new EmployeeViewModel(new Employee());
        // context.addressesViewModel = new AddressesViewModel();
        // DataContext = context;

    }
}

