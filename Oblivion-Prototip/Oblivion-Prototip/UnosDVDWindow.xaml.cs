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
    /// Interaction logic for UnosDVDWindow.xaml
    /// </summary>
    public partial class UnosDVDWindow : Window
    {
        public DVD dvd;
        bool prikaz;

        public UnosDVDWindow(DVD dvd, bool prikaz)
        {
            InitializeComponent();
            this.dvd = dvd;
            this.prikaz = prikaz;
            if (prikaz == false)
            {
                lblNaslov.Text = "Modifikacija DVD-ROM u";
                tbBtnUnos.Text = " Potvrdi promjene na DVD-ROM u";

                tbJibKomponente.Text = dvd.IDKomponente.ToString();
                tbJibKomponente.IsEnabled = false;
                tbNazivProizvodjaca.Text = dvd.NazivProizvodjaca;
                tbBrzina.Text = dvd.Brzina;
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

        private void tbBrzina_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {
            if (prikaz == false)
            {
                string cmd_string = "UPDATE `komponenta` SET `ime_proizvodjaca` = '" + tbNazivProizvodjaca.Text + "',`brzina` = '" + tbBrzina.Text + "' WHERE `idkomponenta` = " + dvd.IDKomponente;
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                dvd.NazivProizvodjaca = tbNazivProizvodjaca.Text;
                dvd.Brzina = tbBrzina.Text;

                this.DialogResult = true;
            }
            else
            {
                string cmd_string = "INSERT INTO `komponenta` (idkomponenta,ime_proizvodjaca,tip_komponente,brzina,racunar_idracunara) VALUES (" + tbJibKomponente.Text + ",'" + tbNazivProizvodjaca.Text + "','DVD-ROM','" + tbBrzina.Text + "'," + dvd.BrojRacunara + ")";
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                dvd.NazivProizvodjaca = tbNazivProizvodjaca.Text;
                dvd.IDKomponente = Convert.ToInt32(tbJibKomponente.Text);
                dvd.Brzina = tbBrzina.Text;

                this.DialogResult = true;
            }
        }
    }
}
