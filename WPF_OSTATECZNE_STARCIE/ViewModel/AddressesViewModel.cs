using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPF_OSTATECZNE_STARCIE.Command;
using WPF_OSTATECZNE_STARCIE.Model;

namespace WPF_OSTATECZNE_STARCIE.ViewModel;

public partial class AddressesViewModel : INotifyPropertyChanged {
    ObservableCollection<Address> _addresses;
    Address _selecetedAddress;
    ObservableCollection<Employee> _employees;
    Employee _selectedEmployee;
    ICommand _clickCommand;
    public string Type { get { return _selecetedAddress.Type; } set { _selecetedAddress.Type = value; OnPropertyChanged(); } }
    public string Addres { get { return _selecetedAddress.Addres; } set { _selecetedAddress.Addres = value; OnPropertyChanged(); } }
    public ObservableCollection<Address> Addresses { get { return _addresses; } set { _addresses = value; OnPropertyChanged(); } }
    public Address SelectedAddress { get { return _selecetedAddress; } set { _selecetedAddress = value; OnPropertyChanged(); } }
    public Employee SelectedEmployee { get { return _selectedEmployee; } set { _selectedEmployee = value; OnPropertyChanged(); } }
    public ObservableCollection<Employee> Employees { get { return _employees; } set { _employees = value; OnPropertyChanged(); } }
    public AddressesViewModel(Address selectedAddress, ObservableCollection<Address> addresses, ObservableCollection<Employee> employees) {
        _selecetedAddress = selectedAddress;
        _addresses = addresses;
        _employees = employees;
        _selectedEmployee = _employees.Any() ? _employees.FirstOrDefault() : null;
        Mediator<Employee>.Instance.Subscribe((employee) =>  new UpdateEmployeesCollectionCommand(employee, this).Execute(null));
    }
    public ICommand ClickCommand { get { return _clickCommand ?? (_clickCommand = new AddAddressCommand(this)); } }
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName]string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void ResetAddress() {
        Addres = "";
        _selecetedAddress.AddressID = 0;
        _selecetedAddress.EmployeeID = 0;
        Type = "";
    }
}