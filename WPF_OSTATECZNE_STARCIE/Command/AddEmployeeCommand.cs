using System;
using System.Windows.Input;
using WPF_OSTATECZNE_STARCIE.Model;
using WPF_OSTATECZNE_STARCIE.Utils;
using WPF_OSTATECZNE_STARCIE.ViewModel;

namespace WPF_OSTATECZNE_STARCIE.Command;

public class AddEmployeeCommand : ICommand {
    private readonly EmployeeViewModel _employeeViewModel;

    public AddEmployeeCommand(EmployeeViewModel employeeViewModel) {
        _employeeViewModel = employeeViewModel;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) {
        return true;
    }

    public void Execute(object? parameter) {
        ApplicationContext _appContext = new ApplicationContext();
        var copy = Helpers.CopyObject<Employee>(_employeeViewModel.Employee);

        _appContext.Employees.Add(copy);
        _appContext.SaveChanges();
        _employeeViewModel.Employees.Add(copy);
        Mediator<Employee>.Instance.Notify(copy);
        _employeeViewModel.ResetEmployee();
    }
}