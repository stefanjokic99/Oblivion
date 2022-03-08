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
    /// Interaction logic for UnosHDDWindow.xaml
    /// </summary>
    public partial class UnosHDDWindow : Window
    {
        public HDD hdd;
        bool prikaz; 

        public UnosHDDWindow(HDD hdd, bool prikaz)
        {
            InitializeComponent();
            this.hdd = hdd;
            this.prikaz = prikaz;
            if (prikaz == false)
            {
                lblNaslov.Text = "Modifikacija HDD a";
                tbBtnUnos.Text = " Potvrdi promjene na HDD u";

                tbJibKomponente.Text = hdd.IDKomponente.ToString();
                tbJibKomponente.IsEnabled = false;
                tbNazivProizvodjaca.Text = hdd.NazivProizvodjaca;
                tbKapacitet.Text = hdd.Kapacitet;
                tbBrzinaObrtaja.Text = hdd.BrzinaObrtaja;
            }
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void tbBrzinaObrtaja_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbJibKomponente_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbKapacitet_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {
            if (prikaz == false)
            {
                string cmd_string = "UPDATE `komponenta` SET `ime_proizvodjaca` = '" + tbNazivProizvodjaca.Text + "',`kapacitet` = '" + tbKapacitet.Text + "',`brzina_obrtaja`='" + tbBrzinaObrtaja.Text + "' WHERE `idkomponenta` = " + hdd.IDKomponente;
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                hdd.NazivProizvodjaca = tbNazivProizvodjaca.Text;
                hdd.Kapacitet = tbKapacitet.Text;
                hdd.BrzinaObrtaja = tbBrzinaObrtaja.Text;

                this.DialogResult = true;
            }
            else
            {
                string cmd_string = "INSERT INTO `komponenta` (idkomponenta,ime_proizvodjaca,tip_komponente,brzina_obrtaja,kapacitet,racunar_idracunara) VALUES (" + tbJibKomponente.Text + ",'" + tbNazivProizvodjaca.Text + "','Hard disk','" + tbBrzinaObrtaja.Text + "','" + tbKapacitet.Text + "'," + hdd.BrojRacunara + ")";
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                hdd.NazivProizvodjaca = tbNazivProizvodjaca.Text;
                hdd.IDKomponente = Convert.ToInt32(tbJibKomponente.Text);
                hdd.Kapacitet = tbKapacitet.Text;
                hdd.BrzinaObrtaja = tbBrzinaObrtaja.Text;

                this.DialogResult = true;
            }
        }
    }
}
