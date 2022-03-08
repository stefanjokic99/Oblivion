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
using System.Windows.Navigation;
using System.Windows.Shapes;

using MySql.Data.MySqlClient;

namespace Oblivion_Prototip
{
    /// <summary>
    /// Interaction logic for UcRacunar.xaml
    /// </summary>
    public partial class UcRacunar : UserControl
    {
        StackPanel podaci;
        public int BrojRacunara { get; set; }
        public string MreznoIme { get; set; }
        public bool zauzet { get; set; }
        UcNaplati naplata { get; set; }
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public TimeSpan vrijeme_zauzeca;
        public TimeSpan ukupnoVrijeme;

        public UcRacunar(string mrezno_ime,StackPanel podaci,int brojRacunara)
        {
            InitializeComponent();
            lblNaziv.Content = mrezno_ime;
            this.podaci = podaci;
            this.BrojRacunara = brojRacunara;
            this.MreznoIme = mrezno_ime;
            this.zauzet = false;
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!zauzet)
            {
                podaci.Children.Clear();
                UcZauzmi zauzmi = new UcZauzmi(this, BrojRacunara, lblNaziv.Content.ToString());
                podaci.Children.Add(zauzmi);
            }
            else
            {
                podaci.Children.Clear();
                podaci.Children.Add(naplata);
            }
        }

        public void zauzmi(double broj_sati, int ukupnoKokaKola, int ukupnoFanta, int ukupnoKafa, int ukupnoVoda, int igrac_id)
        {

            vrijeme_zauzeca = TimeSpan.FromHours(broj_sati);
            ukupnoVrijeme = TimeSpan.FromHours(broj_sati); ;
            dispatcherTimer.Start();
            lblDolazak.Content = DateTime.Now.ToString("HH:mm:ss");

            lblUplatio.Content = ukupnoVrijeme.ToString("c");
            lblPreostalo.Content = vrijeme_zauzeca.ToString("c");
            lblPreostalo.Foreground = Brushes.Red;
            zauzet = true;

            naplata = new UcNaplati(broj_sati,ukupnoKokaKola,ukupnoFanta,ukupnoKafa,ukupnoVoda,igrac_id, this);

            //Identifikacija za igricu
            foreach (UcIgrica igrica in ZaposleniWindow.igrice)
            {
                foreach (TextBlock naziv in igrica.spRacunari.Children)
                {
                    if (naziv.Text == this.MreznoIme)
                    {
                        naziv.Foreground = Brushes.Red;
                    }
                }
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (TimeSpan.Compare(vrijeme_zauzeca, TimeSpan.Zero) != 0)
            {
                vrijeme_zauzeca = vrijeme_zauzeca - TimeSpan.FromSeconds(1);
                lblPreostalo.Content = vrijeme_zauzeca.ToString("c");
            }
            else
            {
                zauzet = false;
                lblDolazak.Content = "00:00:00";
                lblUplatio.Content = "00:00:00";
                lblPreostalo.Content = "00:00:00";

                MessageBox.Show("Naplatite račun", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);

                podaci.Children.Clear();
                podaci.Children.Add(naplata);
                dispatcherTimer.Stop();
                lblPreostalo.Foreground = Brushes.Black;
                naplata.btn1Sat.IsEnabled = false;
                naplata.btn30min.IsEnabled = false;
                naplata.btn2Sata.IsEnabled = false;
                naplata.btn3sata.IsEnabled = false;
                naplata.btnKafa.IsEnabled = false;
                naplata.btnKokaKola.IsEnabled = false;
                naplata.btnFanta.IsEnabled = false;
                naplata.btnVoda.IsEnabled = false;

                naplata.upisRacuna();
                //Identifikacija za igricu 
                foreach (UcIgrica igrica in ZaposleniWindow.igrice)
                {
                    foreach (TextBlock naziv in igrica.spRacunari.Children)
                    {
                        if (naziv.Text == this.MreznoIme)
                        {
                            naziv.Foreground = Brushes.WhiteSmoke;
                        }
                    }
                }
            }
        }
    }
}
