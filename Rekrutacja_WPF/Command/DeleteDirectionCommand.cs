using System;
using System.Windows.Input;
using Rekrutacja_WPF.ViewModel;

namespace Rekrutacja_WPF.Command;

public class DeleteDirectionCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    private readonly DirectionsViewModel _directionsViewModel;

    public DeleteDirectionCommand(DirectionsViewModel directionsViewModel)
    {
        _directionsViewModel = directionsViewModel;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        _directionsViewModel.Formik.Clear();
        _directionsViewModel.RekrutacjaContext.Kierunki.Remove(_directionsViewModel.SelectedDirection);
        await _directionsViewModel.RekrutacjaContext.SaveChangesAsync();
        _directionsViewModel.Directions.Remove(_directionsViewModel.SelectedDirection);
    }
}
