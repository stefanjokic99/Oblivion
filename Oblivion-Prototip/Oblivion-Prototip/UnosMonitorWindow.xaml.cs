using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using MySql.Data.MySqlClient;

namespace Oblivion_Prototip
{
    /// <summary>
    /// Interaction logic for UnosKomponenteWindow.xaml
    /// </summary>
    public partial class UnosMonitorWindow : Window
    {
        public Monitor monitor;
        bool prikaz;

        public UnosMonitorWindow(Monitor monitor, bool prikaz)
        {
            InitializeComponent();
            this.monitor = monitor;
            this.prikaz = prikaz;
            if (prikaz == false)
            {
                lblNaslov.Text = "Modifikacija monitora";
                tbBtnUnos.Text = " Potvrdi promjene na monitora ";

                tbJibKomponente.Text = monitor.IDKomponente.ToString();
                tbJibKomponente.IsEnabled = false;
                tbNazivProizvodjaca.Text = monitor.NazivProizvodjaca;
                tbDimenzija.Text = monitor.Dimenzija;
            }
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void tbJibKomponente_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbDimenzija_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {
            if (prikaz == false)
            {
                string cmd_string = "UPDATE `komponenta` SET `ime_proizvodjaca` = '" + tbNazivProizvodjaca.Text + "',`dimenzija` = '" + tbDimenzija.Text + "' WHERE `idkomponenta` = " + monitor.IDKomponente;
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                monitor.NazivProizvodjaca = tbNazivProizvodjaca.Text;
                monitor.Dimenzija = tbDimenzija.Text;

                this.DialogResult = true;
            }
            else
            {
                string cmd_string = "INSERT INTO `komponenta` (idkomponenta,ime_proizvodjaca,tip_komponente,dimenzija,racunar_idracunara) VALUES (" + tbJibKomponente.Text + ",'" + tbNazivProizvodjaca.Text + "','Monitor','" + tbDimenzija.Text + "'," + monitor.BrojRacunara + ")";
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                monitor.NazivProizvodjaca = tbNazivProizvodjaca.Text;
                monitor.IDKomponente = Convert.ToInt32(tbJibKomponente.Text);
                monitor.Dimenzija = tbDimenzija.Text;

                this.DialogResult = true;
            }
        }
    }
}
