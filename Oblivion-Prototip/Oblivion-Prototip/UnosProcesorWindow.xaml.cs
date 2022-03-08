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
    /// Interaction logic for UnosProcesorWindow.xaml
    /// </summary>
    public partial class UnosProcesorWindow : Window
    {
        public Procesor procesor;
        bool prikaz;

        public UnosProcesorWindow(Procesor procesor, bool prikaz)
        {
            InitializeComponent();
            this.procesor = procesor;
            this.prikaz = prikaz;
            if (prikaz == false)
            {
                lblNaslov.Text = "Modifikacija procesora";
                tbBtnUnos.Text = " Potvrdi promjene na procesoru";

                tbJibKomponente.Text = procesor.IDKomponente.ToString();
                tbJibKomponente.IsEnabled = false;
                tbNazivProizvodjaca.Text = procesor.NazivProizvodjaca;
                tbImeKomponente.Text = procesor.Ime;
                tbFrekvencija.Text = procesor.Frekvencija;
                tbBrojJezgara.Text = procesor.BrojJezgara;
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

        private void tbFrekvencija_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {
            if (prikaz == false)
            {
                string cmd_string = "UPDATE `komponenta` SET `ime_proizvodjaca` = '" + tbNazivProizvodjaca.Text + "',`frekvencija` = '" + tbFrekvencija.Text + "', `ime` = '" + tbImeKomponente.Text + "', `broj_jezgara = '" + tbBrojJezgara.Text + "' WHERE `idkomponenta` = " + procesor.IDKomponente;
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                procesor.NazivProizvodjaca = tbNazivProizvodjaca.Text;
                procesor.Ime = tbImeKomponente.Text;
                procesor.BrojJezgara = tbBrojJezgara.Text;
                procesor.Frekvencija = tbFrekvencija.Text;

                this.DialogResult = true;
            }
          else
            {
                string cmd_string = "INSERT INTO `komponenta` (idkomponenta,ime_proizvodjaca,tip_komponente,ime,frekvencija,broj_jezgara,racunar_idracunara) VALUES (" + tbJibKomponente.Text + ",'" + tbNazivProizvodjaca.Text + "','Procesor','" + tbImeKomponente.Text + "','" + tbFrekvencija.Text + "','" + tbBrojJezgara.Text + "'," + procesor.BrojRacunara + ")";
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                procesor.IDKomponente = Convert.ToInt32(tbJibKomponente.Text);
                procesor.NazivProizvodjaca = tbNazivProizvodjaca.Text;
                procesor.Ime = tbImeKomponente.Text;
                procesor.BrojJezgara = tbBrojJezgara.Text;
                procesor.Frekvencija = tbFrekvencija.Text;

                this.DialogResult = true;
            }
          
        }
    }
}
