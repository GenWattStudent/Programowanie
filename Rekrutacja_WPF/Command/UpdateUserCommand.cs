using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Rekrutacja.Models;
using Rekrutacja_WPF.ViewModel;

namespace Rekrutacja_WPF.Command;

public class UpdateUserCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private UsersViewModel _usersViewModel { get; set; }

    public UpdateUserCommand(UsersViewModel usersViewModel)
    {
        _usersViewModel = usersViewModel;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        // try
        // {
    
        var existingUser = await _usersViewModel.RekrutacjaContext.Użytkownicy.FindAsync(_usersViewModel.SelectedUser.UżytkownikId);

        if (existingUser != null)
        {
            // Get the properties of the existing user object
            var existingUserProperties = existingUser.GetType().GetProperties();

            // Loop through the properties of the updated user object
            foreach (var updatedUserProperty in _usersViewModel.SelectedUser.GetType().GetProperties())
            {
                // Find the corresponding property in the existing user object
                var existingUserProperty = existingUserProperties.FirstOrDefault(p => p.Name == updatedUserProperty.Name);

                // If the property exists, update its value
                if (existingUserProperty != null && existingUserProperty.CanWrite)
                {
                    existingUserProperty.SetValue(existingUser, updatedUserProperty.GetValue(_usersViewModel.SelectedUser));
                }
            }

            await _usersViewModel.RekrutacjaContext.SaveChangesAsync();
        }

        _usersViewModel.Users = new ObservableCollection<Użytkownicy>(await _usersViewModel.RekrutacjaContext.Użytkownicy.Include(u => u.Adresy).ToListAsync());
        _usersViewModel.SelectedUser = null;
        _usersViewModel.Formik.Clear();
        // }
        // catch (Exception)
        // {
        //     MessageBox.Show("Can not update");
        // }
    }
}