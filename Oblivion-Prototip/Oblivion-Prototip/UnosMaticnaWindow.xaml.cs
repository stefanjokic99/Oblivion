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
    /// Interaction logic for UnosMaticnaWindow.xaml
    /// </summary>
    public partial class UnosMaticnaWindow : Window
    {
        public MaticnaPloca maticna;
        bool prikaz;

        public UnosMaticnaWindow(MaticnaPloca maticna, bool prikaz)
        {
            InitializeComponent();
            this.maticna = maticna;
            this.prikaz = prikaz;
            if (prikaz == false)
            {
                lblNaslov.Text = "Modifikacija procesora";
                tbBtnUnos.Text = " Potvrdi promjene na matičnoj ploči";

                tbJibKomponente.Text = maticna.IDKomponente.ToString();
                tbJibKomponente.IsEnabled = false;
                tbNazivProizvodjaca.Text = maticna.NazivProizvodjaca;
                tbCipset.Text = maticna.Cipset;
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

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {
            if (prikaz == false)
            {
                string cmd_string = "UPDATE `komponenta` SET `ime_proizvodjaca` = '" + tbNazivProizvodjaca.Text + "',`cipset` = '" + maticna.Cipset + "' WHERE `idkomponenta` = " + maticna.IDKomponente;
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                maticna.NazivProizvodjaca = tbNazivProizvodjaca.Text;
                maticna.Cipset = tbCipset.Text;

                this.DialogResult = true;
            }
            else
            {
                string cmd_string = "INSERT INTO `komponenta` (idkomponenta,ime_proizvodjaca,tip_komponente,cipset,racunar_idracunara) VALUES (" + tbJibKomponente.Text + ",'" + tbNazivProizvodjaca.Text + "','Procesor','" + tbCipset.Text + "'," + maticna.BrojRacunara + ")";
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                maticna.IDKomponente = Convert.ToInt32(tbJibKomponente.Text);
                maticna.NazivProizvodjaca = tbNazivProizvodjaca.Text;
                maticna.Cipset = tbCipset.Text;

                this.DialogResult = true;
            }
        }
    }
}
