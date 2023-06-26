using System;
using System.Windows.Input;
using Rekrutacja_WPF.ViewModel;

namespace Rekrutacja_WPF;

public class SelectedDirectionToEditCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    private readonly DirectionsViewModel _directionsViewModel;

    public SelectedDirectionToEditCommand(DirectionsViewModel directionsViewModel)
    {
        _directionsViewModel = directionsViewModel;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _directionsViewModel.Formik.Update(_directionsViewModel.SelectedDirection);
    }
}