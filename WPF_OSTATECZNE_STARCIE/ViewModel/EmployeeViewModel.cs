using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using WPF_OSTATECZNE_STARCIE.Model;
using System.Collections.Generic;
using System.Windows.Input;
using WPF_OSTATECZNE_STARCIE.Command;

namespace WPF_OSTATECZNE_STARCIE.ViewModel;

public partial class EmployeeViewModel : INotifyPropertyChanged {

    Employee _employee;
    ObservableCollection<Employee> _employees;
    ICommand _clickCommand;
    public EmployeeViewModel(Employee employee, ObservableCollection<Employee> employees) {
        employee.HireDate = System.DateTime.Now;
        _employee = employee;
        _employees = employees;
    }
    public ObservableCollection<Employee> Employees { get { return _employees; } set { _employees = value; OnPropertyChanged(); } }
    public Employee Employee { get { return _employee; } set { _employee = value; OnPropertyChanged(); } }
    public string FirstName { get { return _employee.FirstName; } set { _employee.FirstName = value; OnPropertyChanged(); } }
    public string LastName { get { return _employee.LastName; } set { _employee.LastName = value; OnPropertyChanged(); } }
    public string Email { get { return _employee.Email; } set { _employee.Email = value; OnPropertyChanged(); } }
    public string Phone { get { return _employee.Phone; } set { _employee.Phone = value; OnPropertyChanged(); } }
    public System.DateTime HireDate { get { return _employee.HireDate; } set { _employee.HireDate = value; OnPropertyChanged(); } }
    public int ManagerID { get { return _employee.ManagerID; } set { _employee.ManagerID = value; OnPropertyChanged(); } }
    public float Salary { get { return _employee.Salary; } set { _employee.Salary = value; OnPropertyChanged(); } }
    public int DepartmentID { get { return _employee.DepartmentID; } set { _employee.DepartmentID = value; OnPropertyChanged(); } }
    public ICollection<Address> Addresses { get { return _employee.Addresses; } set { _employee.Addresses = value; OnPropertyChanged(); } }
    public ICommand ClickCommand { get { return _clickCommand ?? (_clickCommand = new AddEmployeeCommand(this)); } }
    public event PropertyChangedEventHandler? PropertyChanged;

    public void ResetEmployee() {
        FirstName = "";
        LastName = "";
        Email = "";
        Phone = "";
        HireDate = System.DateTime.Now;
        ManagerID = 0;
        Salary = 0;
        DepartmentID = 0;
        Addresses = new List<Address>();
        Employee.EmployeeID = 0;
    }

    public void OnPropertyChanged([CallerMemberName]string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}