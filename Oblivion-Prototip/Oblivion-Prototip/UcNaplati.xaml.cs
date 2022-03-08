using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for UcNaplati.xaml
    /// </summary>
    public partial class UcNaplati : UserControl
    {
        double ukupnoSati = 0;
        int ukupnoKafa = 0;
        int ukupnoVoda = 0;
        int ukupnoKokaKola = 0;
        int ukupnoFanta = 0;
        int igrac_id;
        string sati;
        string voda;
        string kafa;
        string kola;
        string fanta;
        double cijena_sat_igranja;
        double cijena_koka_kola;
        double cijena_fanta;
        double cijena_kafa;
        double cijena_voda;

        UcRacunar racunar;

        public UcNaplati(double broj_sati,int ukupnoKokaKola,int ukupnoFanta,int ukupnoKafa,int ukupnoVoda,int igrac_id, UcRacunar racunar)
        {
            InitializeComponent();

            ukupnoSati = broj_sati;
            txtUkupnoSati.IsReadOnly = true;
            txtUkupnoSati.Text = ukupnoSati.ToString();

            this.ukupnoKokaKola = ukupnoKokaKola;
            txtKolicinaKokaKola.IsReadOnly = true;
            txtKolicinaKokaKola.Text = ukupnoKokaKola.ToString();

            this.ukupnoVoda = ukupnoVoda;
            txtKolicinaVoda.IsReadOnly = true;
            txtKolicinaVoda.Text = ukupnoVoda.ToString();

            this.ukupnoFanta = ukupnoFanta;
            txtKolicinaFanta.IsReadOnly = true;
            txtKolicinaFanta.Text = ukupnoFanta.ToString();

            this.ukupnoKafa = ukupnoKafa;
            txtKolicinaKafa.IsReadOnly = true;
            txtKolicinaKafa.Text = ukupnoKafa.ToString();
            
            this.igrac_id = igrac_id;
            this.racunar = racunar;

            lblNaziv.Content = racunar.MreznoIme;
            procitajCjene();
            racun();
        }
        private void btn30min_Click(object sender, RoutedEventArgs e)
        {
            ukupnoSati += 0.5;
            txtUkupnoSati.Text = ukupnoSati.ToString("0.0#");
            racunar.vrijeme_zauzeca += TimeSpan.FromMinutes(30);
            racunar.ukupnoVrijeme += TimeSpan.FromMinutes(30);
            racunar.lblUplatio.Content = racunar.ukupnoVrijeme.ToString("c");

            racun();
        }

        private void btn1Sat_Click(object sender, RoutedEventArgs e)
        {
            ukupnoSati += 1;
            txtUkupnoSati.Text = ukupnoSati.ToString("0.0#");
            racunar.vrijeme_zauzeca += TimeSpan.FromMinutes(60);
            racunar.ukupnoVrijeme += TimeSpan.FromMinutes(60);
            racunar.lblUplatio.Content = racunar.ukupnoVrijeme.ToString("c");
            racun();
        }

        private void btn2Sata_Click(object sender, RoutedEventArgs e)
        {
            ukupnoSati += 2;
            txtUkupnoSati.Text = ukupnoSati.ToString("0.0#");
            racunar.vrijeme_zauzeca += TimeSpan.FromMinutes(120);
            racunar.ukupnoVrijeme += TimeSpan.FromMinutes(120);
            racunar.lblUplatio.Content = racunar.ukupnoVrijeme.ToString("c");
            racun();
        }

        private void btn3sata_Click(object sender, RoutedEventArgs e)
        {
            ukupnoSati += 3;
            txtUkupnoSati.Text = ukupnoSati.ToString("0.0#");
            racunar.vrijeme_zauzeca += TimeSpan.FromMinutes(180);
            racunar.ukupnoVrijeme += TimeSpan.FromMinutes(180);
            racunar.lblUplatio.Content = racunar.ukupnoVrijeme.ToString("c");
            racun();
        }

        private void btnKafa_Click(object sender, RoutedEventArgs e)
        {
            ukupnoKafa += 1;
            txtKolicinaKafa.Text = ukupnoKafa.ToString();
            racun();
        }

        private void btnVoda_Click(object sender, RoutedEventArgs e)
        {
            ukupnoVoda += 1;
            txtKolicinaVoda.Text = ukupnoVoda.ToString();
            racun();
        }

        private void btnKokaKola_Click(object sender, RoutedEventArgs e)
        {
            ukupnoKokaKola += 1;
            txtKolicinaKokaKola.Text = ukupnoKokaKola.ToString();
            racun();
        }

        private void btnFanta_Click(object sender, RoutedEventArgs e)
        {
            ukupnoFanta += 1;
            txtKolicinaFanta.Text = ukupnoFanta.ToString();
            racun();
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
        }

        private void txtKolicinaVoda_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtKolicinaVoda.Text == "")
            {
                ukupnoVoda = 0;
            }
        }

        private void txtKolicinaKokaKola_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtKolicinaKokaKola.Text == "")
            {
                ukupnoKokaKola = 0;
            }
        }

        private void txtKolicinaFanta_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtKolicinaFanta.Text == "")
            {
                ukupnoFanta = 0;
            }
        }

        private void txtUkupnoSati_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void racun()
        {
            lblRacun.Content = "";
            sati = "sat igranja x" + ukupnoSati.ToString("0.#0") + "................................................" + ukupnoSati*cijena_sat_igranja + " KM\n";
            lblRacun.Content += sati;
            if (ukupnoKokaKola != 0)
            {
                kola = "koka kola x" + ukupnoKokaKola.ToString("0.#0") + "..................................................." + ukupnoKokaKola*cijena_koka_kola + " KM\n";
                lblRacun.Content += kola;
            }
            if (ukupnoVoda != 0)
            {
                voda = "voda x" + ukupnoVoda.ToString("0.#0") + "..........................................................." + ukupnoVoda*cijena_voda + " KM\n";
                lblRacun.Content += voda;
            }
            if (ukupnoFanta != 0)
            {
                fanta = "fanta x" + ukupnoFanta.ToString("0.#0") + "..........................................................." + ukupnoFanta*cijena_fanta + " KM\n";
                lblRacun.Content += fanta;
            }
            if (ukupnoKafa != 0)
            {
                kafa = "kafa x" + ukupnoKafa.ToString("0.#0") + "............................................................" + ukupnoKafa*cijena_kafa + " KM\n";
                lblRacun.Content += kafa;
            }

            txtUkupnoCijena.Text = (ukupnoSati * cijena_sat_igranja + ukupnoKokaKola * cijena_koka_kola + ukupnoVoda * cijena_voda + ukupnoFanta * cijena_fanta + ukupnoKafa * cijena_kafa).ToString();
        }

        public void upisRacuna()
        {
            Random random = new Random();
            int id_racuna = random.Next();

            string cmd_string = "insert into racun values (" + id_racuna + "," + ZaposleniWindow.zaposleni.JMBG + ",'" + txtUkupnoCijena.Text + "')";

            MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

            cmd.ExecuteNonQuery();

            cmd.CommandText = "insert into naplacuje values (" + id_racuna + ",1," + igrac_id + ")";
            cmd.ExecuteNonQuery();

            if (ukupnoKokaKola != 0)
            {
                cmd.CommandText = "insert into naplacuje values (" + id_racuna + ",5," + igrac_id + ")";
                cmd.ExecuteNonQuery();
            }
            if (ukupnoVoda != 0)
            {
                cmd.CommandText = "insert into naplacuje values (" + id_racuna + ",8," + igrac_id + ")";
                cmd.ExecuteNonQuery();
            }
            if (ukupnoFanta != 0)
            {
                cmd.CommandText = "insert into naplacuje values (" + id_racuna + ",10," + igrac_id + ")";
                cmd.ExecuteNonQuery();
            }
            if (ukupnoKafa != 0)
            {
                cmd.CommandText = "insert into naplacuje values (" + id_racuna + ",9," + igrac_id + ")";
                cmd.ExecuteNonQuery();
            }

            string path = System.IO.Path.Combine(
              Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
              "Računi");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllText(System.IO.Path.Combine(path, racunar.MreznoIme + " " + id_racuna + " " + ZaposleniWindow.zaposleni.JMBG + ".txt"), lblRacun.Content + "\n\t\t\t\t\t\t\t\t" + txtUkupnoCijena.Text + " KM");
        }

        private void procitajCjene()
        {
            string upit = "SELECT * FROM `usluga`";
            MySqlCommand cmd = new MySqlCommand(upit, Connection.GetConnection());

            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();
            cijena_sat_igranja = Double.Parse(reader["cijena"].ToString());

            reader.Read();
            reader.Read();
            reader.Read();
            reader.Read();
            cijena_koka_kola = Double.Parse(reader["cijena"].ToString());

            reader.Read();
            reader.Read();
            reader.Read();
            cijena_voda = Double.Parse(reader["cijena"].ToString());

            reader.Read();
            cijena_kafa = Double.Parse(reader["cijena"].ToString());

            reader.Read();
            cijena_fanta = Double.Parse(reader["cijena"].ToString());

            reader.Close();
        }
    }
}
