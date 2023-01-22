using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Projekt_2
{
    public static class GLOBALS  // MainWindow nadaje wszystkim zmiennym wartości w linijkach 21, 22 i 23
    {
        public static string connectionString { get; set; } // informacje o bazie danych MySQL - globalne
        public static MySqlConnection connection { get; set; } // konektor do bazy danych - globalny
        public static string table { get; set; } // nazwa tabeli - globalna

    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GLOBALS.table = "posilki"; //wynesione zeby nie musiec wszedzie kopiowac 
            GLOBALS.connectionString = "SERVER=localhost;DATABASE=baza_danych_projekt;UID=root;PASSWORD=123456789"; //polaczenie z konkretna baza danych baza_danych_projekt
            GLOBALS.connection = new MySqlConnection(GLOBALS.connectionString); //konektor do bazy
            dane.CanUserAddRows = false;  // to jest po to zeby zniknal pusty wiersz, defaultowo true
            OdswiezWidok();
        }

        private void OdswiezWidok()  // odświeża UI o nowe informacje wyciągnięte z bazy danych
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM {GLOBALS.table}", GLOBALS.connection); //konkretne zapytanie sql które chce wykonać; odwołanie do bazy 
           
            GLOBALS.connection.Open(); 
            DataTable dt = new DataTable(); //obsluga danych
            dt.Load(cmd.ExecuteReader()); //wczytuje do data table to co zrobi command
            dt.Columns.Add("SumaKalorii", typeof(Double));
            int indeksWiersza = 0;  // używany w pętli - wskazuje na kolejne wiersze tabeli
            double sumaWszystko = 0; // sumuje wszystkie wartości ilość*kalorie
            foreach(DataRow row in dt.Rows) // pętla po kolejnych wierszach tabeli
            {
                double iloczynIloscKalorie = Convert.ToDouble(row[1]) * Convert.ToDouble(row[2]);  // te liczby to indeksy kolumn
                dt.Rows[indeksWiersza][3] = iloczynIloscKalorie;  // przypisuje do sumy kalorii wiersza o indeksie indeksWiersza
                sumaWszystko += iloczynIloscKalorie;  // zwiększa sumę wszystkich kalorii
                indeksWiersza++; // zwiększa indeks - używane żeby pętla przechodziła po wszystkich wierszach
            }
            GLOBALS.connection.Close();
            dane.DataContext = dt; //wpisanie do komórki w okienku
            SumaSuma.Text = sumaWszystko.ToString();  // updateuje tekst całościowej sumy kalorii o wyliczoną wartość
        }

        private void DodajIlosc(object sender, RoutedEventArgs e) //dodawanie ilosci w bazie
        {
                string nazwa = (sender as Button).DataContext.ToString();
                MySqlCommand cmd = new MySqlCommand($"UPDATE {GLOBALS.table} SET ilosc = ilosc + 1 WHERE danie = '{nazwa}';", GLOBALS.connection); // zawołanie do bazy danych - zwiększa wartość o 1
                GLOBALS.connection.Open();
                cmd.ExecuteReader();
                GLOBALS.connection.Close();
                OdswiezWidok();
          
        }

        private void OdejmijIlosc(object sender, RoutedEventArgs e) //odejmowanie ilosci w bazie
            {
                string nazwa = (sender as Button).DataContext.ToString();
                MySqlCommand cmd = new MySqlCommand($"UPDATE {GLOBALS.table} SET ilosc = ilosc - 1 WHERE danie = '{nazwa}' AND ilosc > 0;", GLOBALS.connection); // zawołanie do bazy danych - zmniejsza wartość o 1
            GLOBALS.connection.Open();
                cmd.ExecuteReader();
                GLOBALS.connection.Close();
                OdswiezWidok();
        }

        private void DodajElementDoBazy(object sender, RoutedEventArgs e)
        {
            DodajElement dodajElement = new DodajElement();  // tworzy nowe okno gdzie wpisujemy wartości które mają być dodane do bazy danych
            dodajElement.ShowDialog();  // pokazuje okno
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
