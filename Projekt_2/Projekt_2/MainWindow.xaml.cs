using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Projekt_2
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 
    public static class GLOBALS
    {
        public static string connectionString { get; set; } 
        public static MySqlConnection connection { get; set; }
        public static string table { get; set; }

    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GLOBALS.table = "posilki"; //wynesione zeby nie musiec wszedzie kopiowac 
            GLOBALS.connectionString = "SERVER=localhost;DATABASE=baza_danych_projekt;UID=root;PASSWORD=123456789"; //polaczenie z konkretna baza danych data_test
            GLOBALS.connection = new MySqlConnection(GLOBALS.connectionString); //konektor do bazy
            dane.CanUserAddRows = false;  // to jest po to zeby zniknal pusty wiersz, defaultowo true
            OdswiezWidok();
        }

        private void OdswiezWidok()
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM {GLOBALS.table}", GLOBALS.connection); //konkretne zapytanie sql które chce wykonać; odwołanie do bazy 
           
            GLOBALS.connection.Open(); 
            DataTable dt = new DataTable(); //obsluga danych
            dt.Load(cmd.ExecuteReader()); //wczytuje do data table to co zrobi command
            dt.Columns.Add("SumaKalorii", typeof(Double));
            int indeksWiersza = 0;
            double sumaWszystko = 0;
            foreach(DataRow row in dt.Rows)
            {
                double iloczynIloscKalorie = Convert.ToDouble(row[1]) * Convert.ToDouble(row[2]);
                dt.Rows[indeksWiersza][3] = iloczynIloscKalorie;
                sumaWszystko += iloczynIloscKalorie;
                indeksWiersza++;
            }
            GLOBALS.connection.Close();
            dane.DataContext = dt; //wpisanie do komórki w okienku
            SumaSuma.Text = sumaWszystko.ToString();
        }

        private void DodajIlosc(object sender, RoutedEventArgs e) //dodawanie ilosci w bazie
        {
                string nazwa = (sender as Button).DataContext.ToString();
                MySqlCommand cmd = new MySqlCommand($"UPDATE {GLOBALS.table} SET ilosc = ilosc + 1 WHERE danie = '{nazwa}';", GLOBALS.connection);
                GLOBALS.connection.Open();
                cmd.ExecuteReader();
                GLOBALS.connection.Close();
                OdswiezWidok();
          
        }

        private void OdejmijIlosc(object sender, RoutedEventArgs e) //odejmowanie ilosci w bazie
            {
                string nazwa = (sender as Button).DataContext.ToString();
                MySqlCommand cmd = new MySqlCommand($"UPDATE {GLOBALS.table} SET ilosc = ilosc - 1 WHERE danie = '{nazwa}' AND ilosc > 0;", GLOBALS.connection);
                GLOBALS.connection.Open();
                cmd.ExecuteReader();
                GLOBALS.connection.Close();
                OdswiezWidok();
        }

        private void DodajElementDoBazy(object sender, RoutedEventArgs e)
        {
            DodajElement dodajElement = new DodajElement();
            dodajElement.ShowDialog();
            string danie = dodajElement.danie;
            string kalorie = dodajElement.kalorie;

            if (!String.IsNullOrEmpty(danie) & !String.IsNullOrEmpty(kalorie)) // jeśli danie i kalorie nie są puste
            {
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO {GLOBALS.table} VALUE('{danie}',{kalorie},0);", GLOBALS.connection);
                GLOBALS.connection.Open();
                cmd.ExecuteReader();
                GLOBALS.connection.Close();
                OdswiezWidok();
            }
        }
    }
}
