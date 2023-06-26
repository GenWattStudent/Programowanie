using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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

namespace TestFormik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public enum Gender
    {
        Male,
        Female
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    public class Employee
    {
        [StringLength(10, ErrorMessage = "The {0} field must be a string with a maximum length of {1}.")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public List<Address> Addresses { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class Context2
    {
        public ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        public ObservableCollection<Address> addresses= new ObservableCollection<Address>();
    }
    public partial class MainWindow : Window
    {
        private Formik<Employee> employeeForm;
        private Context2 context = new Context2();
        Employee employee = new Employee();
 
        public MainWindow()
        {
            InitializeComponent();

            employeeForm = new Formik<Employee>(employee, stackPanel);
            employeeForm.GenerateForm();
            employeeForm.Submit += OnSubmit;
            DataContext = context;
        }

         private void OnSubmit(Employee employee)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"First Name: {employee.FirstName}");
            sb.AppendLine($"Last Name: {employee.LastName}");
            sb.AppendLine($"Gender: {employee.Gender}");

            sb.AppendLine("Addresses:");
            foreach (Address address in employee.Addresses)
            {
                sb.AppendLine($"Street: {address.Street}");
                sb.AppendLine($"City: {address.City}");
                sb.AppendLine($"State: {address.State}");
                sb.AppendLine($"Zip Code: {address.ZipCode}");
                sb.AppendLine();
                context.addresses.Add(address);
            }

            MessageBox.Show(sb.ToString(), "Form Data");

            context.employees.Add(employee);
            employeeForm.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            employee.FirstName = "John";
            employee.Addresses = new List<Address> { new Address { Street = "Street 1", City = "City 1", State = "State 1", ZipCode = "ZipCode 1" }, new Address { Street = "Street 2", City = "City 2", State = "State 2", ZipCode = "ZipCode 2" } };
            employeeForm.Update();
        }
    }
}
