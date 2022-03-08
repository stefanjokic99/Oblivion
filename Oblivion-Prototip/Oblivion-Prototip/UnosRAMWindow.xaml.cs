using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

using MySql.Data.MySqlClient;

using System.Text.RegularExpressions;

namespace Oblivion_Prototip
{
    /// <summary>
    /// Interaction logic for UnosRAMWindow.xaml
    /// </summary>
    public partial class UnosRAMWindow : Window
    {
        public RAM ram;
        bool prikaz;

        public UnosRAMWindow(RAM ram, bool prikaz)
        {
            InitializeComponent();
            this.ram = ram;
            this.prikaz = prikaz;
            if (prikaz == false)
            {
                lblNaslov.Text = "Modifikacija RAM memorija";
                tbBtnUnos.Text = " Potvrdi promjene na RAM memoriji ";

                tbJibKomponente.Text = ram.IDKomponente.ToString();
                tbJibKomponente.IsEnabled = false;
                tbNazivProizvodjaca.Text = ram.NazivProizvodjaca;
                tbKapacitet.Text = ram.Kapacitet;
                tbTip.Text = ram.Tip;
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

        private void tbKapacitet_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {
            if (prikaz == false)
            {
                string cmd_string = "UPDATE `komponenta` SET `ime_proizvodjaca` = '" + tbNazivProizvodjaca.Text + "',`kapacitet` = '" + tbKapacitet.Text + "',`tip`='" + tbTip.Text + "' WHERE `idkomponenta` = " + ram.IDKomponente;
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                ram.NazivProizvodjaca = tbNazivProizvodjaca.Text;
                ram.Kapacitet = tbKapacitet.Text;
                ram.Tip = tbTip.Text;

                this.DialogResult = true;
            }
            else
            {
                string cmd_string = "INSERT INTO `komponenta` (idkomponenta,ime_proizvodjaca,tip_komponente,tip,kapacitet,racunar_idracunara) VALUES (" + tbJibKomponente.Text + ",'" + tbNazivProizvodjaca.Text + "','RAM','" + tbTip.Text + "','" + tbKapacitet.Text + "'," + ram.BrojRacunara + ")";
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                ram.NazivProizvodjaca = tbNazivProizvodjaca.Text;
                ram.IDKomponente = Convert.ToInt32(tbJibKomponente.Text);
                ram.Kapacitet = tbKapacitet.Text;
                ram.Tip = tbTip.Text;

                this.DialogResult = true;
            }
        }
    }
}
