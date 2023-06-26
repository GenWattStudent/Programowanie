using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Rekrutacja.Models;
using Rekrutacja_WPF.ViewModel;

namespace Rekrutacja_WPF;

public class UpdateDirectionCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    private readonly DirectionsViewModel _directionsViewModel;
    public UpdateDirectionCommand(DirectionsViewModel directionsViewModel)
    {
        _directionsViewModel = directionsViewModel;
    }
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        var existingDirection = await _directionsViewModel.RekrutacjaContext.Kierunki.FindAsync(_directionsViewModel.SelectedDirection.KierunekId);

        if (existingDirection != null)
        {
            // Get the properties of the existing user object
            var existingUserProperties = existingDirection.GetType().GetProperties();

            // Loop through the properties of the updated user object
            foreach (var updatedUserProperty in _directionsViewModel.SelectedDirection.GetType().GetProperties())
            {
                // Find the corresponding property in the existing user object
                var existingUserProperty = existingUserProperties.FirstOrDefault(p => p.Name == updatedUserProperty.Name);

                // If the property exists, update its value
                if (existingUserProperty != null && existingUserProperty.CanWrite)
                {
                    existingUserProperty.SetValue(existingDirection, updatedUserProperty.GetValue(_directionsViewModel.SelectedDirection));
                }
            }

            await _directionsViewModel.RekrutacjaContext.SaveChangesAsync();
        }

        _directionsViewModel.Directions = new ObservableCollection<Kierunki>(await _directionsViewModel.RekrutacjaContext.Kierunki.ToListAsync());
        _directionsViewModel.SelectedDirection = null;
        _directionsViewModel.Formik.Clear();
    }
}
