using System;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Rekrutacja.Context;
using Rekrutacja_WPF.ViewModel;

namespace Rekrutacja_WPF.Command;

public class SelectUserToEditCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    private readonly UsersViewModel _usersViewModel;

    public SelectUserToEditCommand(UsersViewModel usersViewModel)
    {   
        _usersViewModel = usersViewModel;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        var rekrutacjaContext = new RekrutacjaContext();
        var user = await rekrutacjaContext.Użytkownicy.Include(x => x.Adresy).FirstOrDefaultAsync(x => x.UżytkownikId == _usersViewModel.SelectedUser.UżytkownikId);

        if (user is not null)
        {
            _usersViewModel.SelectedUser = user;
            _usersViewModel.Formik.Update(user);
        }
    }
}