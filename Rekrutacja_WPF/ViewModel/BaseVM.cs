using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Rekrutacja_WPF.ViewModel;

public class BaseVM : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName]string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}