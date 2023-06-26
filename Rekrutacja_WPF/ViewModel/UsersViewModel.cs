using System.Collections.ObjectModel;
using System.Windows.Input;
using Rekrutacja.Context;
using Rekrutacja.Models;
using Rekrutacja_WPF.Command;
using TestFormik;

namespace Rekrutacja_WPF.ViewModel;

public partial class UsersViewModel: BaseVM {
    Użytkownicy _user;
    Formik<Użytkownicy> _formik;
    Użytkownicy _selectedUser;
    ObservableCollection<Użytkownicy> _users;
    Użytkownicy _userToAdd;
    ICommand _addUserCommand;
    ICommand _selectUserToEditCommand;
    ICommand _updateUserCommand;
    ICommand _deleteUserCommand;
    RekrutacjaContext _rekrutacjaContext;

    public ICommand DeleteUserCommand { get { return _deleteUserCommand ?? (_deleteUserCommand = new DeleteUserCommand(this)); } }

    public ICommand UpdateUserCommand { get { return _updateUserCommand ?? (_updateUserCommand = new UpdateUserCommand(this)); } }

    public ICommand SelectUserToEditCommand { get { return _selectUserToEditCommand ?? (_selectUserToEditCommand = new SelectUserToEditCommand(this)); } }

    public ICommand AddUserCommand { get { return _addUserCommand ?? (_addUserCommand = new AddUserCommand(this)); } }

    public RekrutacjaContext RekrutacjaContext {
        get => _rekrutacjaContext;
        set {
            _rekrutacjaContext = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Użytkownicy> Users {
        get => _users;
        set {
            _users = value;
            OnPropertyChanged();
        }
    }

    public Użytkownicy SelectedUser {
        get => _selectedUser;
        set {
            _selectedUser = value;
            OnPropertyChanged();
        }
    }
    public Użytkownicy User {
        get => _user;
        set {
            _user = value;
            OnPropertyChanged();
        }
    }

    public Formik<Użytkownicy> Formik {
        get => _formik;
        set {
            _formik = value;
            OnPropertyChanged();
        }
    }

    public Użytkownicy UserToAdd {
        get => _userToAdd;
        set {
            _userToAdd = value;
            OnPropertyChanged();
        }
    }

    public UsersViewModel(Użytkownicy user, Formik<Użytkownicy> formik, Użytkownicy selectedUser, ObservableCollection<Użytkownicy> users, RekrutacjaContext context) {
        User = user;
        Formik = formik;
        SelectedUser = selectedUser;
        Users = users;
        RekrutacjaContext = context;
        Formik.Submit += AddUser;
    }

    public void AddUser(Użytkownicy user) {
        if (user.UżytkownikId > 0) {
            SelectedUser = user;
            UpdateUserCommand.Execute(null);
            return;
        }

        UserToAdd = user;
        AddUserCommand.Execute(null);
    }
}