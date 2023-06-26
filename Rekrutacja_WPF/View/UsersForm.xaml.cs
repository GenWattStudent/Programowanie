using Formik.Options;
using Microsoft.EntityFrameworkCore;
using Rekrutacja.Context;
using Rekrutacja.Models;
using Rekrutacja_WPF.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TestFormik;

namespace Rekrutacja_WPF.View
{
    /// <summary>
    /// Logika interakcji dla klasy UserControl.xaml
    /// </summary>
    public partial class UsersForm : UserControl
    {

        private UsersViewModel usersViewModel;
        private RekrutacjaContext rekrutacjaContext = new RekrutacjaContext();
        public UsersForm()
        {
            InitializeComponent();
            var user = new Użytkownicy();
            var selectedUser = new Użytkownicy();
            var users = rekrutacjaContext.Użytkownicy.Include(u => u.Adresy).ToList();
            var usersCollection = new ObservableCollection<Użytkownicy>(users);
            //  options for User Form
            var options = new Dictionary<string, FormikOption>();
            options.Add("Płeć", new FormikSelectOption { SourceItems = new List<string>() { "Mężczyzna", "Kobieta" }, SelectedItem = "Kobieta" } );
            // init formik for User Form
            var formik = new Formik<Użytkownicy>(new Użytkownicy(), userForm, options);

            usersViewModel = new UsersViewModel(user, formik, selectedUser, usersCollection, rekrutacjaContext);
            usersViewModel.Formik.GenerateForm();

            DataContext = usersViewModel;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Pracownicy" || e.PropertyName == "Adresy" || e.PropertyName == "Kandydaci")
            {
                e.Column.IsReadOnly = true;
                e.Column.Visibility = Visibility.Hidden;
            }
        }
    }
}
