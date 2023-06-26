using MVVM.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM.ViewModel
{
    public class StringList : INotifyPropertyChanged
    {
        private string _newItem;
        private StringListModel _model;
        private string _selectedItem;

        public StringList(StringListModel model)
        {
            _model = model;
        }

        public string NewItem { get { return _newItem; } set { _newItem = value; OnPropertyChanged(); } }
        public ObservableCollection<string> Items { get { return _model.Items; } set { _model.Items = value; OnPropertyChanged(); } }
        public string SelectedItem { get { return _selectedItem; } set { _selectedItem = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
