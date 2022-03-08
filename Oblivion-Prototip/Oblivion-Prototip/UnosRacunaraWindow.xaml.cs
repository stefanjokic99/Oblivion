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
    /// Interaction logic for UnosRacunaraWindow.xaml
    /// </summary>
    public partial class UnosRacunaraWindow : Window
    {
        Korisnik korisnik;
        public string MreznoIme { get; set; }

        public UnosRacunaraWindow(Korisnik korisnik)
        {
            InitializeComponent();
            this.korisnik = korisnik;
        }

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {
            if (tbJibRacunara.Text != "" && tbMreznoIme.Text != "")
            {
                try
                {
                    string cmd_string = "SELECT COUNT(*) as \"mreznoIme\" FROM `racunar` WHERE `mrezno_ime` = '" + tbMreznoIme.Text + "' AND igraonica_reg_broj = " + korisnik.RegBrojIgraonice;
                    MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    
                    int brojMreznihImena = reader.GetInt32("mreznoIme");

                    reader.Close();

                    if (brojMreznihImena == 0)
                    {
                        cmd_string = "INSERT INTO `racunar` VALUES(" + tbJibRacunara.Text + ",'" + tbMreznoIme.Text + "'," + korisnik.RegBrojIgraonice + ")";
                        cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                        cmd.ExecuteNonQuery();
                        MreznoIme = tbMreznoIme.Text;
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Unijeto mrezno ime postoji", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch
                {
                    MessageBox.Show("Nisu uneseni validni podaci.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Nisu uneseni validni podaci.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void tbJibRacunara_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
