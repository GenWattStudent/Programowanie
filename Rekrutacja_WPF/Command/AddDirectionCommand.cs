using System;
using System.Windows.Input;
using Rekrutacja_WPF.ViewModel;

namespace Rekrutacja_WPF.Command;


public class AddDirectionCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    private readonly DirectionsViewModel _directionsViewModel;

    public AddDirectionCommand(DirectionsViewModel directionsViewModel)
    {
        _directionsViewModel = directionsViewModel;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        if (_directionsViewModel.Formik.Validate() == false)
        {
            return;
        }

        try {
            _directionsViewModel.RekrutacjaContext.Kierunki.Add(_directionsViewModel.DirectionToAdd);
            await _directionsViewModel.RekrutacjaContext.SaveChangesAsync();
            _directionsViewModel.Directions.Add(_directionsViewModel.DirectionToAdd);
            _directionsViewModel.Formik.Clear();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}