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
    /// Logika interakcji dla klasy EmployeeControl.xaml
    /// </summary>
    public partial class EmployeeControl : UserControl
    {
        ApplicationContext appContext = new ApplicationContext();
        EmployeeViewModel context;

        public EmployeeControl()
        {
            InitializeComponent();
            var employee = new Employee();
            var employees = appContext.Employees.ToList();
            var employeesCollection = new ObservableCollection<Employee>(employees);
            context = new EmployeeViewModel(employee, employeesCollection);
            DataContext = context;
        }
    }
}
