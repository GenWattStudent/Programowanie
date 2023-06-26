using System;
using System.Windows;
using System.Windows.Controls;

namespace WPFik
{
    
    public class Walec
    {
        public double R { get; set; }
        public double H { get; set; }

        public double Area()
        {
            return 2 * Math.PI * Math.Pow(R, 2) + 2 * Math.PI * R * H;
        }

        public double Volume()
        {
            return Math.PI * Math.Pow(R, 2) * H;
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Walec triangle = new Walec();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = triangle;
        }

       private void ObliczButton_Click(object sender, RoutedEventArgs e)
        {
            Result_Objetosc.Content = $"V = {triangle.Volume()}";
            Result_Pole_calkowite.Content = $"P = {triangle.Area()}";
        }
    }
}
