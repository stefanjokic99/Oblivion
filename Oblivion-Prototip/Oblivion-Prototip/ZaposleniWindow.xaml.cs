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
using System.Configuration;

namespace Oblivion_Prototip
{
    /// <summary>
    /// Interaction logic for ZaposleniWindow.xaml
    /// </summary>
    public partial class ZaposleniWindow : Window
    {
        public Korisnik zaposlenik;
        public  static List<UcIgrica> igrice;
        public List<UcRacunar> racunari;
        public static Korisnik zaposleni;
        public static StackPanel sp;

        public ZaposleniWindow(Korisnik korisnik)
        {
            InitializeComponent();

            this.zaposlenik = korisnik;
            zaposleni = zaposlenik;
            igrice = new List<UcIgrica>();
            racunari = new List<UcRacunar>();
            UcitajRacunare();
            UcitajIgrice();
            acSearchBox.FilterMode = AutoCompleteFilterMode.Contains;
            sp = spPodaci;
        }

        /// <summary>
        /// Logika izlaza iz aplikacije i omogućavanje promjene prikaza
        /// </summary>
        #region Upravljanje prikazom i izlazom iz aplikacije
        private void ZaposleniWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.ThreeDBorderWindow;
            }

        }

        private void ZaposleniWindow_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowStyle = WindowStyle.None;
            }
        }
        #endregion

        public void UcitajRacunare()
        {
            string mrezno_ime = "";
            int broj_racunara = 0;
            string upit = "select * from racunar where igraonica_reg_broj = " + zaposlenik.RegBrojIgraonice;
            MySqlCommand cmd = new MySqlCommand(upit, Connection.GetConnection());

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                broj_racunara = Int32.Parse( reader["broj_racunara"].ToString());
                mrezno_ime = reader["mrezno_ime"].ToString();

                int pozicija = mrezno_ime.IndexOf("_", 0);
                String mreznoIme = mrezno_ime.Insert(pozicija, "_");

                racunari.Add(new UcRacunar(mreznoIme,spPodaci,broj_racunara));
                wrapRacunari.Children.Add(racunari[racunari.Count - 1]);
            }

            reader.Close();
        }
        public void UcitajIgrice()
        {
            string upit = "select * from igrica";
            MySqlCommand cmd = new MySqlCommand(upit, Connection.GetConnection());
            List<string> nazivi = new List<string>();
            MySqlDataReader reader = cmd.ExecuteReader();

            wrapIgrice.ItemHeight = 250;
            wrapIgrice.ItemWidth = 200;

            while (reader.Read())
            {
                MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
                connection.Open();

                int idigrica = reader.GetInt32("idigrica");
                string naziv = reader["naziv"].ToString();
                string slika = reader["slika"].ToString();
                string vrsta = reader["vrsta"].ToString();
                List<UcRacunar> racunari_igrica = new List<UcRacunar>();

                MySqlCommand cmd_instalirano = new MySqlCommand("select * from instalirano where igrica_idigrica = " + idigrica, connection);

                MySqlDataReader reader2 = cmd_instalirano.ExecuteReader();

                while (reader2.Read())
                {
                    int brojRacunara = reader2.GetInt32("racunar_broj_racunara");

                    List<UcRacunar> racunar = racunari.Where(r => r.BrojRacunara == brojRacunara).ToList<UcRacunar>();


                    if (racunar.Count != 0)
                    {
                        racunari_igrica.Add(racunar[0]);
                    }
                }

                reader2.Close();
                connection.Close();
                nazivi.Add(naziv);
                igrice.Add(new UcIgrica(naziv, slika, vrsta, racunari_igrica));

                wrapIgrice.Children.Add(igrice[igrice.Count - 1]);
            }

            acSearchBox.ItemsSource = nazivi;
            reader.Close();

        }
        private void btnIgraonica_Click(object sender, RoutedEventArgs e)
        {
            PrikazInformacijaIgraonica prikaz_info = new PrikazInformacijaIgraonica(zaposlenik);

            prikaz_info.Show();
        }

        private void btnOdjava_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnPromjena_Click(object sender, RoutedEventArgs e)
        {
            PromjenaPodatakaWindow promjenaPodataka = new PromjenaPodatakaWindow(this);

            promjenaPodataka.ShowDialog();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search_text = acSearchBox.Text;

            wrapIgrice.Children.Clear();

            foreach (UcIgrica igrica in igrice)
            {
                if (igrica.Ime.Contains(search_text))
                {
                    wrapIgrice.Children.Add(igrica);
                }
            }
        }
    }
}
