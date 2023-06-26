using System;
using System.Windows.Input;
using Rekrutacja.Context;
using Rekrutacja.Models;
using Rekrutacja_WPF.ViewModel;

namespace Rekrutacja_WPF.Command;

public class AddUserCommand : ICommand
{
    public event EventHandler CanExecuteChanged;
    public UsersViewModel UsersViewModel { get; set; }

    public AddUserCommand(UsersViewModel usersViewModel)
    {
        UsersViewModel = usersViewModel;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        var context = new RekrutacjaContext();
        try
        {
            context.Użytkownicy.Add(UsersViewModel.UserToAdd);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        UsersViewModel.Users.Add(UsersViewModel.UserToAdd);
        UsersViewModel.Formik.Clear();
    }
}