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

using System.Text.RegularExpressions;

namespace Oblivion_Prototip
{
    /// <summary>
    /// Interaction logic for UcUnosZaposlenika.xaml
    /// </summary>
    public partial class UcUnosZaposlenika : UserControl
    {
        AdminWindow parentWindow;
        string JMBG;
        bool prikaz;

        public UcUnosZaposlenika(AdminWindow parentWindow, string JMBG, bool prikaz)
        {
            InitializeComponent();
            this.parentWindow = parentWindow;
            this.JMBG = JMBG;
            this.prikaz = prikaz;

            upisMjesta();

            if (!prikaz)
            {
                ucitavanjePodatakaZaModifikaciju();
            }
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.ciscenjeSPa();
            parentWindow.btnDodajNovogZaposlenika.Visibility = Visibility.Visible;
        }

        private void upisMjesta()
        {
            string cmd_string = "SELECT * FROM `mjesto`";
            MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cmbMjesta.Items.Add(new Mjesto(reader["naziv"].ToString(), (int)reader["ptt"]));
            }

            reader.Close();
        }

        private void tbJMBG_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbPlata_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ucitavanjePodatakaZaModifikaciju()
        {
            MySqlDataReader reader = null;
            try
            {
                string cmd_string = "SELECT ime, prezime, lozinka, plata, administrator, mjesto_ptt, korisnicko_ime FROM `igraonica`.`radnik` WHERE `radnik`.`jmbg`='" + JMBG + "'";
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string ime = reader["ime"].ToString();
                    string prezime = reader["prezime"].ToString();
                    double plata = (double)reader["plata"];
                    int mjesto_ptt = (int)reader["mjesto_ptt"];
                    bool administrator = Convert.ToBoolean(reader.GetInt32("administrator"));
                    string korisnicko_ime = reader["korisnicko_ime"].ToString();
                    string lozinka = reader["lozinka"].ToString();

                    reader.Close();

                    lblNaslov.Text = "IZMJENA PODATAKA O ZAPOSLENOM";

                    tbIme.Text = ime;
                    tbIme.IsEnabled = false;
                    tbPrezime.Text = prezime;
                    tbPrezime.IsEnabled = false;
                    tbKorisnickoIme.Text = korisnicko_ime;
                    tbKorisnickoIme.IsEnabled = false;
                    pbLozinka.Password = lozinka;
                    pbLozinka.IsEnabled = false;
                    pbAutorizacija.Password = lozinka;
                    pbAutorizacija.IsEnabled = false;
                    cbAdministrator.IsChecked = administrator;
                    foreach (Mjesto mjesto in cmbMjesta.Items)
                    {
                        if (mjesto.PostanskiBroj == mjesto_ptt)
                        {
                            cmbMjesta.SelectedItem = mjesto;
                            break;
                        }
                    }
                    tbJMBG.Text = JMBG;
                    tbJMBG.IsEnabled = false;
                    tbPlata.Text = plata.ToString("0.#0");

                    myPackIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.AccountEditOutline;
                    btnPotvrdaTextBlock.Text = " POTVRDI NOVE PODATKE ZAPOSLENIKA";
                }
            }
            catch
            {
                MessageBox.Show("Neuspješno čitanje iz baze podataka", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {
            if (prikaz)
            {
                if (tbJMBG.Text.Length == 13)
                {
                    string jmbg = tbJMBG.Text;
                    string ime = tbIme.Text;
                    string prezime = tbPrezime.Text;
                    string dat_zaposlenja = DateTime.Now.ToString("yyyy-MM-dd");
                    double plata = 0.0;
                    if (tbPlata.Text != "")
                    {
                        plata = Double.Parse(tbPlata.Text);
                    }
                    else
                    {
                        MessageBox.Show("Nisu popunjeni traženi podaci.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    Mjesto mjesto = (Mjesto)cmbMjesta.SelectedItem;
                    int igraonica_reg_broj = parentWindow.admin.RegBrojIgraonice;
                    bool administrator = (bool)cbAdministrator.IsChecked;
                    string korisnickoIme = tbKorisnickoIme.Text;
                    string lozinka = pbLozinka.Password;

                    // Provjera da li korisnicko ime postoji !!! 
                    if (korisnickoIme != "" && cmbMjesta.SelectedIndex != -1)
                    {
                        string cmd_string_postoji = "SELECT COUNT(*) as \"postoji\" FROM `radnik` WHERE `korisnicko_ime`='" + korisnickoIme + "'";
                        MySqlCommand cmd_postoji = new MySqlCommand(cmd_string_postoji, Connection.GetConnection());

                        MySqlDataReader reader = cmd_postoji.ExecuteReader();

                        reader.Read();
                        int postoji = reader.GetInt32("postoji");

                        reader.Close();

                        if (postoji == 0)
                        {
                            if (pbLozinka.Password == pbAutorizacija.Password)
                            {
                                if (pbLozinka.Password.Length > 3)
                                {
                                    try
                                    {
                                        string cmd_string = "INSERT INTO `radnik` (`jmbg`,`ime`,`prezime`,`dat_zaposlenja`,`plata`,`mjesto_ptt`,`igraonica_reg_broj`,`administrator`,`korisnicko_ime`,`lozinka`,`zaposlen`) " +
                                            "VALUES ('" + jmbg + "','" + ime + "','" + prezime + "','" + dat_zaposlenja + "'," + plata + "," + mjesto.PostanskiBroj + "," + igraonica_reg_broj + "," + administrator + ",'" + korisnickoIme + "'," +
                                            "PASSWORD('" + lozinka + "'),1)";

                                        MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                                        cmd.ExecuteNonQuery();

                                        parentWindow.ucitavanjeTabeleZaposlenik();
                                        parentWindow.ciscenjeSPa();
                                        parentWindow.btnDodajNovogZaposlenika.Visibility = Visibility.Visible;
                                    }
                                    catch
                                    {
                                        MessageBox.Show("Nisu uneseni validni podaci.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Lozinka mora imati više od 3 karaktera.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Lozinka nije potvrđena.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Korisničko ime je zauzeto.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nisu popunjeni traženi podaci.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("JMBG mora imati 13 cifara.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                if (cmbMjesta.SelectedIndex != -1 && tbPlata.Text != "")
                {
                    string cmd_string = "UPDATE `radnik` SET `administrator`=" + (bool)cbAdministrator.IsChecked + ", `mjesto_ptt`=" + ((Mjesto)cmbMjesta.SelectedItem).PostanskiBroj + ",`plata`=" + Double.Parse(tbPlata.Text)
                    + " WHERE `jmbg`='" + JMBG + "'";

                    MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());
                    cmd.ExecuteNonQuery();

                    parentWindow.ucitavanjeTabeleZaposlenik();
                    parentWindow.ciscenjeSPa();
                    parentWindow.btnDodajNovogZaposlenika.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Nisu popunjeni traženi podaci.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

    }
}
