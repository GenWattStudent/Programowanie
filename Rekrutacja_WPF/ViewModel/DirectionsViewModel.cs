using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Rekrutacja.Context;
using Rekrutacja.Models;
using Rekrutacja_WPF.Command;
using TestFormik;

namespace Rekrutacja_WPF.ViewModel;


public class DirectionsViewModel : BaseVM
{
    Kierunki _direction;
    Formik<Kierunki> _formik;
    Kierunki _selectedDirection;
    ObservableCollection<Kierunki> _directions;
    RekrutacjaContext _rekrutacjaContext;
    Kierunki _directionToAdd;
    ICommand _addDirectionCommand;
    ICommand _deleteDirectionCommand;
    ICommand _selectDirectionToEditCommand;
    ICommand _updateDirectionCommand;

    public ICommand AddDirectionCommand { get { return _addDirectionCommand ?? (_addDirectionCommand = new AddDirectionCommand(this)); } }

    public ICommand DeleteDirectionCommand { get { return _deleteDirectionCommand ?? (_deleteDirectionCommand = new DeleteDirectionCommand(this)); } }

    public ICommand SelectDirectionToEditCommand { get { return _selectDirectionToEditCommand ?? (_selectDirectionToEditCommand = new SelectedDirectionToEditCommand(this)); } }

    public ICommand UpdateDirectionCommand { get { return _updateDirectionCommand ?? (_updateDirectionCommand = new UpdateDirectionCommand(this)); } }
    [Required(ErrorMessage = "Nazwa jest wymagana")]
    public string Nazwa { get; set; }

    public Kierunki DirectionToAdd
    {
        get => _directionToAdd;
        set
        {
            _directionToAdd = value;
            OnPropertyChanged();
        }
    }

    public RekrutacjaContext RekrutacjaContext
    {
        get => _rekrutacjaContext;
        set
        {
            _rekrutacjaContext = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Kierunki> Directions
    {
        get => _directions;
        set
        {
            _directions = value;
            OnPropertyChanged();
        }
    }

    public Kierunki SelectedDirection
    {
        get => _selectedDirection;
        set
        {
            _selectedDirection = value;
            OnPropertyChanged();
        }
    }
    public Kierunki Direction
    {
        get => _direction;
        set
        {
            _direction = value;
            OnPropertyChanged();
        }
    }

    public Formik<Kierunki> Formik
    {
        get => _formik;
        set
        {
            _formik = value;
            OnPropertyChanged();
        }
    }

    public DirectionsViewModel(Kierunki direction, Formik<Kierunki> formik, Kierunki selectedDirection, ObservableCollection<Kierunki> directions, RekrutacjaContext rekrutacjaContext)
    {
        Direction = direction;
        Formik = formik;
        SelectedDirection = selectedDirection;
        Directions = directions;
        _rekrutacjaContext = rekrutacjaContext;
        Formik.Submit += AddOrUpdateDirection;
    }

    public async void AddOrUpdateDirection(Kierunki direction)
    {
        if (direction.KierunekId > 0)
        {
            SelectedDirection = direction;
            UpdateDirectionCommand.Execute(null);
            return;
        }

        DirectionToAdd = direction;
        AddDirectionCommand.Execute(null);
    }

}