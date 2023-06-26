using System;
using System.Windows.Input;
using WPF_OSTATECZNE_STARCIE.Model;
using WPF_OSTATECZNE_STARCIE.ViewModel;

namespace WPF_OSTATECZNE_STARCIE.Command;

public class UpdateEmployeesCollectionCommand : ICommand {
    private readonly Employee _employee;
    private readonly AddressesViewModel _addressesViewModel;

    public UpdateEmployeesCollectionCommand(Employee employee, AddressesViewModel addressesViewModel) {
        _employee = employee;
        _addressesViewModel = addressesViewModel;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) {
        return true;
    }

    public void Execute(object? parameter) {
        _addressesViewModel.Employees.Add(_employee);
    }
}