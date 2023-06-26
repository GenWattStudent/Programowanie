using System;
using System.Windows.Input;
using WPF_OSTATECZNE_STARCIE.Model;
using WPF_OSTATECZNE_STARCIE.Utils;
using WPF_OSTATECZNE_STARCIE.ViewModel;

namespace WPF_OSTATECZNE_STARCIE.Command;

public class AddAddressCommand: ICommand {
    private readonly AddressesViewModel _addressesViewModel;

    public AddAddressCommand(AddressesViewModel addressesViewModel) {
        _addressesViewModel = addressesViewModel;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) {
        return true;
    }

    public void Execute(object? parameter) {
        using ApplicationContext _appContext = new ApplicationContext();

        if (_addressesViewModel.SelectedAddress is not null && _addressesViewModel.SelectedEmployee is not null) {
            _addressesViewModel.SelectedAddress.EmployeeID = _addressesViewModel.SelectedEmployee.EmployeeID;
            var copy = Helpers.CopyObject<Address>(_addressesViewModel.SelectedAddress);

            _appContext.Addresses.Add(copy);
            _appContext.SaveChanges();
            _addressesViewModel.Addresses.Add(copy);
            _addressesViewModel.ResetAddress();
        }
    }
}