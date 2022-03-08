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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Oblivion_Prototip
{
    /// <summary>
    /// Interaction logic for UcZauzmi.xaml
    /// </summary>
    public partial class UcZauzmi : UserControl
    {
        double ukupnoSati = 0;
        int ukupnoKafa = 0;
        int ukupnoVoda = 0;
        int ukupnoKokaKola = 0;
        int ukupnoFanta = 0;

        int brojRacunara = 0;
        UcRacunar racunar;

        public UcZauzmi(UcRacunar racunar, int brojRacunara, string naziv)
        {
            InitializeComponent();
            this.brojRacunara = brojRacunara;
            lblNaziv.Content = naziv;
            this.racunar = racunar;
            napuniIgrice();
        }

        private void napuniIgrice()
        {
            string upit = "SELECT * FROM `igrica` join instalirano on (instalirano.igrica_idigrica = igrica.idigrica) where racunar_broj_racunara = " + brojRacunara;
            MySqlCommand cmd = new MySqlCommand(upit, Connection.GetConnection());

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string naziv = reader["naziv"].ToString();
                cmbIgrice.Items.Add(naziv);
            }

            reader.Close();
        }

        private void btn30min_Click(object sender, RoutedEventArgs e)
        {
            ukupnoSati += 0.5;
            txtUkupnoSati.Text = ukupnoSati.ToString("0.0#");
        }

        private void btn1Sat_Click(object sender, RoutedEventArgs e)
        {
            ukupnoSati += 1;
            txtUkupnoSati.Text = ukupnoSati.ToString("0.0#");
        }

        private void btn2Sata_Click(object sender, RoutedEventArgs e)
        {
            ukupnoSati += 2;
            txtUkupnoSati.Text = ukupnoSati.ToString("0.0#");
        }

        private void btn3sata_Click(object sender, RoutedEventArgs e)
        {
            ukupnoSati += 3;
            txtUkupnoSati.Text = ukupnoSati.ToString("0.0#");
        }

        private void txtUkupnoSati_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtUkupnoSati.Text == "")
            {
                ukupnoSati = 0;
            }
            else
            {
                ukupnoSati = Double.Parse(txtUkupnoSati.Text);
            }
        }

        private void btnKafa_Click(object sender, RoutedEventArgs e)
        {
            ukupnoKafa += 1;
            txtKolicinaKafa.Text = ukupnoKafa.ToString();
        }

        private void btnVoda_Click(object sender, RoutedEventArgs e)
        {
            ukupnoVoda += 1;
            txtKolicinaVoda.Text = ukupnoVoda.ToString();
        }

        private void btnKokaKola_Click(object sender, RoutedEventArgs e)
        {
            ukupnoKokaKola += 1;
            txtKolicinaKokaKola.Text = ukupnoKokaKola.ToString();
        }

        private void btnFanta_Click(object sender, RoutedEventArgs e)
        {
            ukupnoFanta += 1;
            txtKolicinaFanta.Text = ukupnoFanta.ToString();
        }

        private void txtKolicinaKafa_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtKolicinaVoda_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtKolicinaKokaKola_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtKolicinaFanta_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtKolicinaKafa_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtKolicinaKafa.Text == "")
            {
                ukupnoKafa = 0;
            }
            else
            {
                ukupnoKafa = int.Parse(txtKolicinaKafa.Text);
            }
        }

        private void txtKolicinaVoda_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtKolicinaVoda.Text == "")
            {
                ukupnoVoda = 0;
            }
            else
            {
                ukupnoVoda = int.Parse(txtKolicinaVoda.Text);
            }
        }

        private void txtKolicinaKokaKola_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtKolicinaKokaKola.Text == "")
            {
                ukupnoKokaKola = 0;
            }
            else
            {
                ukupnoKokaKola = int.Parse(txtKolicinaKokaKola.Text);
            }
        }

        private void txtKolicinaFanta_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtKolicinaFanta.Text == "")
            {
                ukupnoFanta = 0;
            }
            else
            {
                ukupnoFanta = int.Parse(txtKolicinaFanta.Text);
            }
        }

        private void txtUkupnoSati_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnZauzmi_Click(object sender, RoutedEventArgs e)
        {
            if (cmbIgrice.SelectedIndex != -1)
            {
                //zapisati na kontrolu racunar vrijeme zaueto, poceti tajmer i trenutno sistemsko vrijeme
                int igrac_id = 0;

                if (txtID.Text == "" && txtImeiPrezime.Text != "")
                {
                    Random Rand = new Random();
                    int rand = Rand.Next();
                    string[] imeIPrezime = txtImeiPrezime.Text.Split(' ');
                    string upit_igrac = "insert into igrac (idigrac,ime,prezime,redovnost,reg_broj_igraonica) values (" + rand + ",'" + imeIPrezime[0] + "','" + imeIPrezime[1] + "','Mjesečni posjetilac'," + ZaposleniWindow.zaposleni.RegBrojIgraonice + ")";
                    MySqlCommand cmd_igrac = new MySqlCommand(upit_igrac, Connection.GetConnection());

                    cmd_igrac.ExecuteNonQuery();
                    igrac_id = rand;
                }
                else
                {
                    igrac_id = int.Parse(txtID.Text);
                }
                //u bazu upisati igraca ako je novi 

                string upit = "insert into igra VALUES ((SELECT idigrica FROM igrica WHERE naziv = '" + cmbIgrice.SelectedValue.ToString() + "')," + igrac_id + "," + racunar.BrojRacunara + ")";
                MySqlCommand cmd = new MySqlCommand(upit, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                racunar.zauzmi(ukupnoSati, ukupnoKokaKola, ukupnoFanta, ukupnoKafa, ukupnoVoda, igrac_id);

                //u bazu upisati igraca koji je zauzeo racunar 
                ZaposleniWindow.sp.Children.Clear();
                //unjeti podatke u tabelu naplacuje

            }
        }


    }
}
