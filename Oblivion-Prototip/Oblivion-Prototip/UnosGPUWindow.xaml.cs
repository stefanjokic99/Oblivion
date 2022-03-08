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
    /// Interaction logic for UnosGPUWindow.xaml
    /// </summary>
    public partial class UnosGPUWindow : Window
    {
        public GPU gpu;
        bool prikaz;

        public UnosGPUWindow(GPU gpu, bool prikaz)
        {
            InitializeComponent();
            this.gpu = gpu;
            this.prikaz = prikaz;
            if (prikaz == false)
            {
                lblNaslov.Text = "Modifikacija GPU";
                tbBtnUnos.Text = " Potvrdi promjene na GPU";

                tbJibKomponente.Text = gpu.IDKomponente.ToString();
                tbJibKomponente.IsEnabled = false;
                tbNazivProizvodjaca.Text = gpu.NazivProizvodjaca;
                tbIme.Text = gpu.Ime;
                tbKolicinaMemorije.Text = gpu.KolicinaMemorije;
            }
        }

        private void tbJibKomponente_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {
            if (prikaz == false)
            {
                string cmd_string = "UPDATE `komponenta` SET `ime_proizvodjaca` = '" + tbNazivProizvodjaca.Text + "',`ime` = '" + tbIme.Text + "',`kolicina_memorije`='" + tbKolicinaMemorije.Text + "' WHERE `idkomponenta` = " + gpu.IDKomponente;
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                gpu.NazivProizvodjaca = tbNazivProizvodjaca.Text;
                gpu.Ime = tbIme.Text;
                gpu.KolicinaMemorije = tbKolicinaMemorije.Text;

                this.DialogResult = true;
            }
            else
            {
                string cmd_string = "INSERT INTO `komponenta` (idkomponenta,ime_proizvodjaca,tip_komponente,ime,kolicina_memorije,racunar_idracunara) VALUES (" + tbJibKomponente.Text + ",'" + tbNazivProizvodjaca.Text + "','Grafička kartica','" + tbIme.Text + "','" + tbKolicinaMemorije.Text + "'," + gpu.BrojRacunara + ")";
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                gpu.NazivProizvodjaca = tbNazivProizvodjaca.Text;
                gpu.IDKomponente = Convert.ToInt32(tbJibKomponente.Text);
                gpu.Ime= tbIme.Text;
                gpu.KolicinaMemorije = tbKolicinaMemorije.Text;

                this.DialogResult = true;
            }
        }
    }
}
