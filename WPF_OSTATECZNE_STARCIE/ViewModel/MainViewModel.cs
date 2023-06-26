using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_OSTATECZNE_STARCIE.ViewModel;
public partial class MainViewModel : INotifyPropertyChanged {
    EmployeeViewModel _employeeViewModel;
    AddressesViewModel _addressesViewModel;

    public EmployeeViewModel EmployeeViewModel { get { return _employeeViewModel; } set { _employeeViewModel = value; OnPropertyChanged(); } }

    public AddressesViewModel AddressesViewModel { get { return _addressesViewModel; } set { _addressesViewModel = value; OnPropertyChanged(); } }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName]string propertyName = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}