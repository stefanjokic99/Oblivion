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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Logika izlaza iz aplikacije i omogućavanje promjene prikaza
        /// </summary>
        #region Upravljanje prikazom i izlazom iz aplikacije
        private void LogInWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.ThreeDBorderWindow;
                lblNaslov.FontSize = 100;
                lblPodNaslov.Margin = new Thickness(0, 0, 0, 20);
            }

        }

        private void LogInWindow_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowStyle = WindowStyle.None;
                lblNaslov.FontSize = 130;
                lblPodNaslov.Margin = new Thickness(0, 0, 40, 20);
            }
        }
        #endregion

        /// <summary>
        /// Prijava na sistem 
        /// </summary>
        #region SignIn
        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {

            MySqlDataReader reader = null;
            try
            {
                string cmd_string = "SELECT * FROM `igraonica`.`radnik` WHERE `radnik`.`korisnicko_ime`='" + tbKorisnickoIme.Text + "' AND `radnik`.`lozinka`=PASSWORD('" + pbLozinka.Password + "') AND `radnik`.`zaposlen`=1;";
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Korisnik logovaniKorisnik = null;
                    string jmbg = reader["jmbg"].ToString();
                    string ime = reader["ime"].ToString();
                    string prezime = reader["prezime"].ToString();
                    DateTime dat_zaposlenja = (DateTime)reader["dat_zaposlenja"];
                    double plata = (double)reader["plata"];
                    int mjesto_ptt = (int)reader["mjesto_ptt"];
                    int igraonica_reg_broj = (int)reader["igraonica_reg_broj"];
                    bool administrator = Convert.ToBoolean(reader.GetInt32("administrator"));
                    string korisnicko_ime = reader["korisnicko_ime"].ToString();

                    reader.Close();

                    logovaniKorisnik = new Korisnik(jmbg, ime, prezime, dat_zaposlenja, plata, mjesto_ptt, igraonica_reg_broj,
                       administrator, korisnicko_ime);

                    if (logovaniKorisnik.Administrator)
                    {
                        tbKorisnickoIme.Text = "";
                        pbLozinka.Password = "";
                        lblGreska.Content = "";

                        AdminWindow adminUpravljanje = new AdminWindow(logovaniKorisnik);

                        this.Hide();
                        adminUpravljanje.ShowDialog();

                        if (adminUpravljanje.DialogResult == true)
                        {
                            this.Show();
                            tbKorisnickoIme.Focus();
                        }
                        else
                        {
                            Application.Current.Shutdown();
                        }
                    }
                    else if (!logovaniKorisnik.Administrator)
                    {
                        tbKorisnickoIme.Text = "";
                        pbLozinka.Password = "";
                        lblGreska.Content = "";

                        ZaposleniWindow zaposleniUpravljanje = new ZaposleniWindow(logovaniKorisnik);

                        this.Hide();
                        zaposleniUpravljanje.ShowDialog();

                        if (zaposleniUpravljanje.DialogResult == true)
                        {
                            this.Show();
                            tbKorisnickoIme.Focus();
                        }
                        else
                        {
                            Application.Current.Shutdown();
                        }
                    }
                }
                else
                {
                    tbKorisnickoIme.Text = "";
                    pbLozinka.Password = "";
                    lblGreska.Content = "Unijeli ste pogrešne podatke!";
                }
            }
            catch
            {
                MessageBox.Show("Neuspješno čitanje iz baze podataka", "Greška",MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        #endregion

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
