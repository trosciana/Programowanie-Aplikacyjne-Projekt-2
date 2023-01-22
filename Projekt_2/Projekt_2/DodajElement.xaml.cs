using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Projekt_2
{
    /// <summary>
    /// Logika interakcji dla klasy DodajElement.xaml
    /// </summary>
    public partial class DodajElement : Window
    {
        public DodajElement()
        {
            InitializeComponent();
        }

        public string danie
        {
            get { return DanieText.Text; }
        }
        public string kalorie
        {
            get { return KalorieText.Text; } //skopiowane, z okna jestem w stanie zwrócić informacje z jednego okna do drugiego - zwraca do linii 80 i 81
        }

        public void dodajDoBazy(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) //skopiowane, tylko cyfry w polu kalorie
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }


}
