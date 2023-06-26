using Rekrutacja.Context;
using Rekrutacja.Models;
using Rekrutacja_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TestFormik;

namespace Rekrutacja_WPF.View
{
    /// <summary>
    /// Logika interakcji dla klasy DirectionForm.xaml
    /// </summary>
    public partial class DirectionForm : UserControl
    {
        private DirectionsViewModel directionsViewModel;
        private RekrutacjaContext rekrutacjaContext = new RekrutacjaContext();

        public DirectionForm()
        {
            InitializeComponent();
            var directions = rekrutacjaContext.Kierunki.ToList();
            var directionsCollection = new ObservableCollection<Kierunki>(directions);

            directionsViewModel = new DirectionsViewModel(new Kierunki(), new Formik<Kierunki>(new Kierunki(), directionForm), new Kierunki(), directionsCollection, rekrutacjaContext);
        
            DataContext = directionsViewModel;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            directionsViewModel.Formik.GenerateForm();
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
