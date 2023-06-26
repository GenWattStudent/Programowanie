using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Rekrutacja.Models;
using Rekrutacja_WPF.ViewModel;

namespace Rekrutacja_WPF.Command;

public class DeleteUserCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private readonly UsersViewModel _usersViewModel;

    public DeleteUserCommand(UsersViewModel usersViewModel)
    {
        _usersViewModel = usersViewModel;  
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        _usersViewModel.Formik.Clear();
        var userToRemove = await _usersViewModel.RekrutacjaContext.Użytkownicy.Include(u => u.Adresy).FirstOrDefaultAsync(u => u.UżytkownikId == _usersViewModel.SelectedUser.UżytkownikId);

        if (userToRemove is null) return;

        _usersViewModel.RekrutacjaContext.Użytkownicy.Remove(userToRemove);
        await _usersViewModel.RekrutacjaContext.SaveChangesAsync();
        _usersViewModel.Users = new ObservableCollection<Użytkownicy>(_usersViewModel.Users.Where(u => u.UżytkownikId != userToRemove.UżytkownikId));
    }
}