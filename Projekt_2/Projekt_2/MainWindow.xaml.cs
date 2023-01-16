using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            GLOBALS.table = "posilki";
            GLOBALS.connectionString = "SERVER=localhost;DATABASE=data_test;UID=root;PASSWORD=123456789";
            dane.CanUserAddRows = false;
            OdswiezWidok();
        }

        private void OdswiezWidok()
        {
            GLOBALS.connection = new MySqlConnection(GLOBALS.connectionString); //konektor do bazy
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM {GLOBALS.table}", GLOBALS.connection); //konkretne zapytanie sql które chce wykonać; odwołanie do bazy 
           
            GLOBALS.connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            GLOBALS.connection.Close();
            dane.DataContext = dt;
        }

        private void DodajIlosc(object sender, RoutedEventArgs e) //dodawanie ilosci w bazie
        {
            if ((sender as Button).DataContext != null)
            {
                string nazwa = (sender as Button).DataContext.ToString();
                MySqlCommand cmd = new MySqlCommand($"UPDATE {GLOBALS.table} SET ilosc = ilosc + 1 WHERE danie = '{nazwa}';", GLOBALS.connection);
                GLOBALS.connection.Open();
                cmd.ExecuteReader();
                GLOBALS.connection.Close();
                OdswiezWidok();
            }
        }

        private void OdejmijIlosc(object sender, RoutedEventArgs e) //odejmowanie ilosci w bazie
        {
            if ((sender as Button).DataContext != null)
            {
                string nazwa = (sender as Button).DataContext.ToString();
                MySqlCommand cmd = new MySqlCommand($"UPDATE {GLOBALS.table} SET ilosc = ilosc - 1 WHERE danie = '{nazwa}';", GLOBALS.connection);
                GLOBALS.connection.Open();
                cmd.ExecuteReader();
                GLOBALS.connection.Close();
                OdswiezWidok();
            }
        }

      
    }

}
