using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Shapes;

using MySql.Data.MySqlClient;

namespace Oblivion_Prototip
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public Korisnik admin;

        public AdminWindow(Korisnik korisnik)
        {
            InitializeComponent();
            this.admin = korisnik;
            ucitavanjeTabeleZaposlenik();
            ucitavanjeTabeleRacunara();
            ucitavanjeTabeleRacun();
            ucitavanjeTabeleUsluge();
        }

        /// <summary>
        /// Logika izlaza iz aplikacije i omogućavanje promjene prikaza
        /// </summary>
        #region Upravljanje prikazom i izlazom iz aplikacije
        private void AdminWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.ThreeDBorderWindow;
            }

        }

        private void AdminWindow_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowStyle = WindowStyle.None;
            }
        }
        #endregion

        #region zaposlenici
        public void ucitavanjeTabeleZaposlenik()
        {
            string cmd_string = "SELECT jmbg,ime,prezime,dat_zaposlenja,plata,`mjesto`.`naziv` as \"mjesto\",administrator,`mjesto_ptt` FROM `radnik` " +
                "JOIN `mjesto` ON (`radnik`.`mjesto_ptt`=`mjesto`.`ptt`) WHERE `radnik`.`igraonica_reg_broj` = " + admin.RegBrojIgraonice + " AND `radnik`.`jmbg` <> " + admin.JMBG + " AND `radnik`.`zaposlen`=1";
            MySqlCommand cmd = new MySqlCommand(cmd_string,Connection.GetConnection());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            dataZaposleni.ItemsSource = null;
            dataZaposleni.ItemsSource = dt.DefaultView;
        }

        private void dataZaposleni_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            dataZaposleni.UnselectAllCells();
        }

        private void btnBrisanje_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            string JMBG = dataRowView[0].ToString();
            string ime = dataRowView[1].ToString();
            string prezime = dataRowView[2].ToString();
            DateTime datumZaposlenja = (DateTime)dataRowView[3];
            double plata = Double.Parse(dataRowView[4].ToString());
            bool administrator = Convert.ToBoolean(Int32.Parse(dataRowView[6].ToString()));
            int mjestoPtt = Int32.Parse(dataRowView[7].ToString());

            if (MessageBox.Show("Da li ste sigurni da želite da obrišete zaposlenika :" + ime + " " + prezime, "Brisanje zaposlenika", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Korisnik korisnikZaBrisanje = new Korisnik(JMBG, ime, prezime, datumZaposlenja, plata, mjestoPtt, 0, administrator, "");

                string cmd_string = "UPDATE `radnik` SET zaposlen=0,korisnicko_ime='',lozinka=''  WHERE `radnik`.JMBG = '" + JMBG + "'";
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());
               
                korisnikZaBrisanje.StampanjeIzvjestaja();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Zaposlenik: " + korisnikZaBrisanje.Ime + " " + korisnikZaBrisanje.Prezime + " je obrisan. Svi računi koje je radnik isplatio nalaze se \"Evidenciji obrisanih zaposlenika\".", "Brisanje zaposlenika", MessageBoxButton.OK, MessageBoxImage.Information);
                ucitavanjeTabeleZaposlenik();
            }
        }

        private void btnDodajNovogZaposlenika_Click(object sender, RoutedEventArgs e)
        {
            UcUnosZaposlenika unos = new UcUnosZaposlenika(this, "", true);

            ciscenjeSPa();
            spDodavanjeZaposlenika.Children.Add(unos);
            btnDodajNovogZaposlenika.Visibility = Visibility.Hidden;
        }

        public void ciscenjeSPa()
        {
            spDodavanjeZaposlenika.Children.Clear();
        }

        private void btnOdjava_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnPromjena_Click(object sender, RoutedEventArgs e)
        {
            UcPromjenaPodataka promjena = new UcPromjenaPodataka(this);

            ciscenjeSPa();
            spDodavanjeZaposlenika.Children.Add(promjena);
            btnDodajNovogZaposlenika.Visibility = Visibility.Hidden;
        }

        private void btnModifikacija_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            string JMBG = dataRowView[0].ToString();

            UcUnosZaposlenika promjenaPodatakaZaposlenika = new UcUnosZaposlenika(this, JMBG, false);

            ciscenjeSPa();
            spDodavanjeZaposlenika.Children.Add(promjenaPodatakaZaposlenika);
            btnDodajNovogZaposlenika.Visibility = Visibility.Hidden;
        }
        #endregion

        #region racunari
        private void ucitavanjeTabeleRacunara()
        {
            string cmd_string = "SELECT broj_racunara, mrezno_ime FROM `racunar` WHERE `igraonica_reg_broj` = " + admin.RegBrojIgraonice;
            MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            dataRacunari.ItemsSource = null;
            dataRacunari.ItemsSource = dt.DefaultView;

            dataRacunari.Items.SortDescriptions.Clear();
            dataRacunari.Items.SortDescriptions.Add(new SortDescription("mrezno_ime", ListSortDirection.Ascending));
            dataRacunari.Items.Refresh();
        }
        private void dataRacunari_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            dataRacunari.UnselectAllCells();
        }
        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            int jib_racunara = Convert.ToInt32(dataRowView[0].ToString());
            string mrezno_ime = dataRowView[1].ToString();

            if (MessageBox.Show("Da li ste sigurni da želite da obrišete računar: " + mrezno_ime , "Brisanje računara", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string cmd_string = "DELETE FROM `instalirano` WHERE racunar_broj_racunara = " + jib_racunara;
                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                cmd_string = "DELETE FROM `komponenta` WHERE racunar_idracunara = " + jib_racunara;
                cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();


                cmd_string = "DELETE FROM `igra` WHERE racunar_idracunara = " + jib_racunara;
                cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                cmd_string = "DELETE FROM `racunar` WHERE broj_racunara = " + jib_racunara;
                cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                MessageBox.Show("Računar: " + mrezno_ime + " je obrisan.", "Brisanje računara", MessageBoxButton.OK, MessageBoxImage.Information);
                ucitavanjeTabeleRacunara();
            }
        }
        private void btnDodavanjeNovogRacunara_Click(object sender, RoutedEventArgs e)
        {
            UnosRacunaraWindow unos = new UnosRacunaraWindow(admin);

            if (unos.ShowDialog() == true)
            {
                ucitavanjeTabeleRacunara();
                MessageBox.Show("Računar: " + unos.MreznoIme + " je kreiran. Molimo ispunite podatke o komponentama.", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
        }
        private void btnMonitor_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            int jib_racunara = Convert.ToInt32(dataRowView[0].ToString());
            string mrezno_ime = dataRowView[1].ToString();
            UcPrikazKomponenti prikaz = new UcPrikazKomponenti(this, "monitor", jib_racunara);


            string cmd_string = "SELECT * FROM `komponenta` WHERE racunar_idracunara = " + jib_racunara + " AND tip_komponente = 'Monitor'";
            MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

            MySqlDataReader reader = cmd.ExecuteReader();

            ciscenjeSPaRacunar();
            prikaz.lvPrikazKomponenti.Items.Clear();

            while (reader.Read())
            {
                int IDKomponente = reader.GetInt32("idkomponenta");
                string NazivProizvodjaca = reader["ime_proizvodjaca"].ToString();
                string Dimenzija = reader["dimenzija"].ToString();

                prikaz.lvPrikazKomponenti.Items.Add(new Monitor(IDKomponente, NazivProizvodjaca, Dimenzija, jib_racunara));
            }
            reader.Close();

            if (prikaz.lvPrikazKomponenti.Items.Count == 0)
            {
                 UnosMonitorWindow unos = new UnosMonitorWindow(new Monitor(0,"","",jib_racunara), true);

                 if (unos.ShowDialog() == true)
                 {
                        prikaz.lvPrikazKomponenti.Items.Add(unos.monitor);
                        prikaz.lvPrikazKomponenti.Items.Refresh();
                 }
            
            }

            spRacunar.Children.Add(prikaz);
            btnDodavanjeNovogRacunara.Visibility = Visibility.Hidden;
        }

        private void btnProcesor_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            int jib_racunara = Convert.ToInt32(dataRowView[0].ToString());
            string mrezno_ime = dataRowView[1].ToString();
            UcPrikazKomponenti prikaz = new UcPrikazKomponenti(this, "procesor", jib_racunara);


            string cmd_string = "SELECT * FROM `komponenta` WHERE racunar_idracunara = " + jib_racunara + " AND tip_komponente = 'Procesor'";
            MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

            MySqlDataReader reader = cmd.ExecuteReader();

            ciscenjeSPaRacunar();
            prikaz.lvPrikazKomponenti.Items.Clear();

            while (reader.Read())
            {
                int IDKomponente = reader.GetInt32("idkomponenta");
                string NazivProizvodjaca = reader["ime_proizvodjaca"].ToString();
                string Ime = reader["ime"].ToString();
                string BrojJezgara = reader["broj_jezgara"].ToString();
                string Frekvencija = reader["frekvencija"].ToString();

                prikaz.lvPrikazKomponenti.Items.Add(new Procesor(IDKomponente, NazivProizvodjaca, Ime, Frekvencija, BrojJezgara, jib_racunara));
            }
            reader.Close();

            if (prikaz.lvPrikazKomponenti.Items.Count == 0)
            {
                UnosProcesorWindow unos = new UnosProcesorWindow(new Procesor(0, "", "", "", "", jib_racunara), true);

                if (unos.ShowDialog() == true)
                {
                    prikaz.lvPrikazKomponenti.Items.Add(unos.procesor);
                    prikaz.lvPrikazKomponenti.Items.Refresh();
                }
            }
            if (prikaz.lvPrikazKomponenti.Items.Count == 1)
            {
                prikaz.btnUnosNoveKomponente.IsEnabled = false;
            }

            spRacunar.Children.Add(prikaz);
            btnDodavanjeNovogRacunara.Visibility = Visibility.Hidden;
        }
        private void btnMaticnaPloca_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            int jib_racunara = Convert.ToInt32(dataRowView[0].ToString());
            string mrezno_ime = dataRowView[1].ToString();
            UcPrikazKomponenti prikaz = new UcPrikazKomponenti(this, "maticna", jib_racunara);


            string cmd_string = "SELECT * FROM `komponenta` WHERE racunar_idracunara = " + jib_racunara + " AND tip_komponente = 'Matična ploča'";
            MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

            MySqlDataReader reader = cmd.ExecuteReader();

            ciscenjeSPaRacunar();
            prikaz.lvPrikazKomponenti.Items.Clear();

            while (reader.Read())
            {
                int IDKomponente = reader.GetInt32("idkomponenta");
                string NazivProizvodjaca = reader["ime_proizvodjaca"].ToString();
                string cipset = reader["cipset"].ToString();

                prikaz.lvPrikazKomponenti.Items.Add(new MaticnaPloca(IDKomponente, NazivProizvodjaca, cipset, jib_racunara));
            }
            reader.Close();

            if (prikaz.lvPrikazKomponenti.Items.Count == 0)
            {
                UnosMaticnaWindow unos = new UnosMaticnaWindow(new MaticnaPloca(0, "", "", jib_racunara), true);

                if (unos.ShowDialog() == true)
                {
                    prikaz.lvPrikazKomponenti.Items.Add(unos.maticna);
                    prikaz.lvPrikazKomponenti.Items.Refresh();
                }
            }
            if (prikaz.lvPrikazKomponenti.Items.Count == 1)
            {
                prikaz.btnUnosNoveKomponente.IsEnabled = false;
            }

            spRacunar.Children.Add(prikaz);
            btnDodavanjeNovogRacunara.Visibility = Visibility.Hidden;
        }
        private void btnRAM_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            int jib_racunara = Convert.ToInt32(dataRowView[0].ToString());
            string mrezno_ime = dataRowView[1].ToString();
            UcPrikazKomponenti prikaz = new UcPrikazKomponenti(this, "ram", jib_racunara);


            string cmd_string = "SELECT * FROM `komponenta` WHERE racunar_idracunara = " + jib_racunara + " AND tip_komponente = 'RAM'";
            MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

            MySqlDataReader reader = cmd.ExecuteReader();

            ciscenjeSPaRacunar();
            prikaz.lvPrikazKomponenti.Items.Clear();

            while (reader.Read())
            {
                int IDKomponente = reader.GetInt32("idkomponenta");
                string NazivProizvodjaca = reader["ime_proizvodjaca"].ToString();
                string tip = reader["tip"].ToString();
                string kapacitet = reader["kapacitet"].ToString();

                prikaz.lvPrikazKomponenti.Items.Add(new RAM(IDKomponente, NazivProizvodjaca, tip, kapacitet, jib_racunara));
            }
            reader.Close();

            if (prikaz.lvPrikazKomponenti.Items.Count == 0)
            {
                UnosRAMWindow unos = new UnosRAMWindow(new RAM(0, "", "", "", jib_racunara), true);

                if (unos.ShowDialog() == true)
                {
                    prikaz.lvPrikazKomponenti.Items.Add(unos.ram);
                    prikaz.lvPrikazKomponenti.Items.Refresh();
                }
            }
          
            spRacunar.Children.Add(prikaz);
            btnDodavanjeNovogRacunara.Visibility = Visibility.Hidden;
        }
        private void btnHDD_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            int jib_racunara = Convert.ToInt32(dataRowView[0].ToString());
            string mrezno_ime = dataRowView[1].ToString();
            UcPrikazKomponenti prikaz = new UcPrikazKomponenti(this, "hdd", jib_racunara);


            string cmd_string = "SELECT * FROM `komponenta` WHERE racunar_idracunara = " + jib_racunara + " AND tip_komponente = 'Hard disk'";
            MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

            MySqlDataReader reader = cmd.ExecuteReader();

            ciscenjeSPaRacunar();
            prikaz.lvPrikazKomponenti.Items.Clear();

            while (reader.Read())
            {
                int IDKomponente = reader.GetInt32("idkomponenta");
                string NazivProizvodjaca = reader["ime_proizvodjaca"].ToString();
                string kapacitet = reader["kapacitet"].ToString();
                string brzina_obrtaja = reader["brzina_obrtaja"].ToString();

                prikaz.lvPrikazKomponenti.Items.Add(new HDD(IDKomponente, NazivProizvodjaca, kapacitet, brzina_obrtaja, jib_racunara));
            }
            reader.Close();

            if (prikaz.lvPrikazKomponenti.Items.Count == 0)
            {
                UnosHDDWindow unos = new UnosHDDWindow(new HDD(0, "", "", "", jib_racunara), true);

                if (unos.ShowDialog() == true)
                {
                    prikaz.lvPrikazKomponenti.Items.Add(unos.hdd);
                    prikaz.lvPrikazKomponenti.Items.Refresh();
                }
            }

            spRacunar.Children.Add(prikaz);
            btnDodavanjeNovogRacunara.Visibility = Visibility.Hidden;
        }

        private void btnGPU_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            int jib_racunara = Convert.ToInt32(dataRowView[0].ToString());
            string mrezno_ime = dataRowView[1].ToString();
            UcPrikazKomponenti prikaz = new UcPrikazKomponenti(this, "gpu", jib_racunara);


            string cmd_string = "SELECT * FROM `komponenta` WHERE racunar_idracunara = " + jib_racunara + " AND tip_komponente = 'Grafička kartica'";
            MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

            MySqlDataReader reader = cmd.ExecuteReader();

            ciscenjeSPaRacunar();
            prikaz.lvPrikazKomponenti.Items.Clear();

            while (reader.Read())
            {
                int IDKomponente = reader.GetInt32("idkomponenta");
                string NazivProizvodjaca = reader["ime_proizvodjaca"].ToString();
                string ime = reader["ime"].ToString();
                string kolicina_memorije = reader["kolicina_memorije"].ToString();

                prikaz.lvPrikazKomponenti.Items.Add(new GPU(IDKomponente, NazivProizvodjaca, ime, kolicina_memorije, jib_racunara));
            }
            reader.Close();

            if (prikaz.lvPrikazKomponenti.Items.Count == 0)
            {
                UnosGPUWindow unos = new UnosGPUWindow(new GPU(0, "", "", "", jib_racunara), true);

                if (unos.ShowDialog() == true)
                {
                    prikaz.lvPrikazKomponenti.Items.Add(unos.gpu);
                    prikaz.lvPrikazKomponenti.Items.Refresh();
                }
            }

            spRacunar.Children.Add(prikaz);
            btnDodavanjeNovogRacunara.Visibility = Visibility.Hidden;
        }
        private void btnDVD_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            int jib_racunara = Convert.ToInt32(dataRowView[0].ToString());
            string mrezno_ime = dataRowView[1].ToString();
            UcPrikazKomponenti prikaz = new UcPrikazKomponenti(this, "dvd", jib_racunara);


            string cmd_string = "SELECT * FROM `komponenta` WHERE racunar_idracunara = " + jib_racunara + " AND tip_komponente = 'DVD-ROM'";
            MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

            MySqlDataReader reader = cmd.ExecuteReader();

            ciscenjeSPaRacunar();
            prikaz.lvPrikazKomponenti.Items.Clear();

            while (reader.Read())
            {
                int IDKomponente = reader.GetInt32("idkomponenta");
                string NazivProizvodjaca = reader["ime_proizvodjaca"].ToString();
                string brzina = reader["brzina"].ToString();

                prikaz.lvPrikazKomponenti.Items.Add(new DVD(IDKomponente, NazivProizvodjaca, brzina, jib_racunara));
            }
            reader.Close();

            if (prikaz.lvPrikazKomponenti.Items.Count == 0)
            {
                UnosDVDWindow unos = new UnosDVDWindow(new DVD(0, "", "", jib_racunara), true);

                if (unos.ShowDialog() == true)
                {
                    prikaz.lvPrikazKomponenti.Items.Add(unos.dvd);
                    prikaz.lvPrikazKomponenti.Items.Refresh();
                }
            }

            spRacunar.Children.Add(prikaz);
            btnDodavanjeNovogRacunara.Visibility = Visibility.Hidden;
        }
        public void ciscenjeSPaRacunar()
        {
            spRacunar.Children.Clear();
        }
        #endregion

        #region racuni
        private void dataRacun_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            dataRacun.UnselectAllCells();
        }
        private void ucitavanjeTabeleRacun()
        {
            string cmd_string = "select radnik.`prezime`, radnik.`ime`, radnik.`jmbg`, racun.`idracun`, racun.`ukupna_cijena` from `racun` join `radnik` on (racun.`radnik_idradnika` = radnik.`jmbg`) " +
                "where radnik.`jmbg` in (select `jmbg` from `radnik` where igraonica_reg_broj = " + admin.RegBrojIgraonice + ")";
            MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            dataRacun.ItemsSource = null;
            dataRacun.ItemsSource = dt.DefaultView;
        }





        #endregion

        #region usluge
        private void dataUsluge_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            dataUsluge.UnselectAllCells();
        }
        public void ucitavanjeTabeleUsluge()
        {
            string cmd_string = "SELECT idusluga,vrsta,cijena FROM `usluga`";
            MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            dataUsluge.ItemsSource = null;
            dataUsluge.ItemsSource = dt.DefaultView;
        }

        private void tbCijenaUsluge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbCijenaUsluge_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)((TextBox)e.Source).DataContext;
                string id_usluge = dataRowView[0].ToString();
                string cmd_string = "UPDATE `usluga` SET cijena = '" + ((TextBox)e.Source).Text + "' WHERE idusluga = " + id_usluge;

                MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                cmd.ExecuteNonQuery();

                ucitavanjeTabeleUsluge();
            }
            catch { }
        }
        #endregion

        #region info o igraonici
        private void btnIgraonica_Click(object sender, RoutedEventArgs e)
        {
            PrikazInformacijaIgraonica prikaz_info = new PrikazInformacijaIgraonica(admin);

            prikaz_info.Show();
        }
        #endregion
    }
}
