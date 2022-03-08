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
    /// Interaction logic for UcPrikazKomponenti.xaml
    /// </summary>
    public partial class UcPrikazKomponenti : UserControl
    {
        AdminWindow parentWindow;
        string tip;
        int jib_racunara;

        public UcPrikazKomponenti(AdminWindow parentWindow, string tip, int jib_racunara)
        {
            InitializeComponent();
            this.parentWindow = parentWindow;
            this.tip = tip;
            this.jib_racunara = jib_racunara;

            if (this.tip == "monitor")
            {
                myPackIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Monitor;
                lblNaslov.Text = "MONITORI";
                btnNaslovUnos.Text = " UNOS NOVOG MONITORA";
                btnNaslovModifikacija.Text = " MODIFIKACIJA MONITORA";
                btnNaslovBrisanje.Text = " OBRIŠI OZNAČENI MONITOR";
            }
            if (this.tip == "procesor")
            {
                myPackIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Cpu64Bit;
                lblNaslov.Text = "PROCESOR";
                btnNaslovUnos.Text = " UNOS NOVOG PROCESORA";
                btnNaslovModifikacija.Text = " MODIFIKACIJA PROCESORA";
                btnNaslovBrisanje.Text = " OBRIŠI OZNAČENI PROCESOR";
            }
            if (this.tip == "maticna")
            {
                myPackIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Chip;
                lblNaslov.Text = "MATIČNA PLOČA";
                btnNaslovUnos.Text = " UNOS NOVE MATIČNE PLOČE";
                btnNaslovModifikacija.Text = " MODIFIKACIJA MATIČNE PLOČE";
                btnNaslovBrisanje.Text = " OBRIŠI OZNAČENU MATIČNU";
            }
            if (this.tip == "ram")
            {
                myPackIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Memory;
                lblNaslov.Text = "RAM memorije";
                btnNaslovUnos.Text = " UNOS NOVE RAM MEMORIJE";
                btnNaslovModifikacija.Text = " MODIFIKACIJA RAM MEMORIJE";
                btnNaslovBrisanje.Text = " OBRIŠI OZNAČENU RAM MEMORIJU";
            }
            if (this.tip == "hdd")
            {
                myPackIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Harddisk;
                lblNaslov.Text = "HDD";
                btnNaslovUnos.Text = " UNOS NOVOG HDD a";
                btnNaslovModifikacija.Text = " MODIFIKACIJA HDD a";
                btnNaslovBrisanje.Text = " OBRIŠI OZNAČENI HDD";
            }
            if (this.tip == "gpu")
            {
                myPackIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.ExpansionCard;
                lblNaslov.Text = "GPU";
                btnNaslovUnos.Text = " UNOS NOVE GPU";
                btnNaslovModifikacija.Text = " MODIFIKACIJA GPU";
                btnNaslovBrisanje.Text = " OBRIŠI OZNAČENU GPU";
            }
            if (this.tip == "dvd")
            {
                myPackIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.RecordCircle;
                lblNaslov.Text = "DVD-ROM";
                btnNaslovUnos.Text = " UNOS NOVOG DVD-ROM a";
                btnNaslovModifikacija.Text = " MODIFIKACIJA DVD-ROM a";
                btnNaslovBrisanje.Text = " OBRIŠI OZNAČEN DVD-ROM";
            }
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.ciscenjeSPaRacunar();
            parentWindow.btnDodavanjeNovogRacunara.Visibility = Visibility.Visible;
        }

        private void btnModifikacijaKomponente_Click(object sender, RoutedEventArgs e)
        {
            if (lvPrikazKomponenti.SelectedItem != null)
            {
                if (tip == "monitor")
                {
                    UnosMonitorWindow unos = new UnosMonitorWindow((Monitor)lvPrikazKomponenti.SelectedItem, false);

                    if (unos.ShowDialog() == true)
                    {
                        lvPrikazKomponenti.SelectedItem = unos.monitor;
                        lvPrikazKomponenti.Items.Refresh();
                    }
                }
                if (tip == "procesor")
                {
                    UnosProcesorWindow unos = new UnosProcesorWindow((Procesor)lvPrikazKomponenti.SelectedItem, false);

                    if (unos.ShowDialog() == true)
                    {
                        lvPrikazKomponenti.SelectedItem = unos.procesor;
                        lvPrikazKomponenti.Items.Refresh();
                    }
                }
                if (tip == "maticna")
                {
                    UnosMaticnaWindow unos = new UnosMaticnaWindow((MaticnaPloca)lvPrikazKomponenti.SelectedItem, false);

                    if (unos.ShowDialog() == true)
                    {
                        lvPrikazKomponenti.SelectedItem = unos.maticna;
                        lvPrikazKomponenti.Items.Refresh();
                    }
                }
                if (tip == "ram")
                {
                    UnosRAMWindow unos = new UnosRAMWindow((RAM)lvPrikazKomponenti.SelectedItem, false);

                    if (unos.ShowDialog() == true)
                    {
                        lvPrikazKomponenti.SelectedItem = unos.ram;
                        lvPrikazKomponenti.Items.Refresh();
                    }
                }
                if (tip == "hdd")
                {
                    UnosHDDWindow unos = new UnosHDDWindow((HDD)lvPrikazKomponenti.SelectedItem, false);

                    if (unos.ShowDialog() == true)
                    {
                        lvPrikazKomponenti.SelectedItem = unos.hdd;
                        lvPrikazKomponenti.Items.Refresh();
                    }
                }
                if (tip == "gpu")
                {
                    UnosGPUWindow unos = new UnosGPUWindow((GPU)lvPrikazKomponenti.SelectedItem, false);

                    if (unos.ShowDialog() == true)
                    {
                        lvPrikazKomponenti.SelectedItem = unos.gpu;
                        lvPrikazKomponenti.Items.Refresh();
                    }
                }
                if (tip == "dvd")
                {
                    UnosDVDWindow unos = new UnosDVDWindow((DVD)lvPrikazKomponenti.SelectedItem, false);

                    if (unos.ShowDialog() == true)
                    {
                        lvPrikazKomponenti.SelectedItem = unos.dvd;
                        lvPrikazKomponenti.Items.Refresh();
                    }
                }
            }
        }

        private void btnUnosNoveKomponente_Click(object sender, RoutedEventArgs e)
        {
            if (tip == "monitor")
            {
                UnosMonitorWindow unos = new UnosMonitorWindow(new Monitor(0, "", "", jib_racunara), true);

                if (unos.ShowDialog() == true)
                {
                    lvPrikazKomponenti.Items.Add(unos.monitor);
                    lvPrikazKomponenti.Items.Refresh();
                }
            }
            if (tip == "procesor")
            {
                UnosProcesorWindow unos = new UnosProcesorWindow(new Procesor(0, "", "", "", "", jib_racunara), true);

                if (unos.ShowDialog() == true)
                {
                    lvPrikazKomponenti.Items.Add(unos.procesor);
                    lvPrikazKomponenti.Items.Refresh();
                    if (lvPrikazKomponenti.Items.Count == 1)
                    {
                        btnUnosNoveKomponente.IsEnabled = false;
                    }
                }
            }
            if (tip == "maticna")
            {
                UnosMaticnaWindow unos = new UnosMaticnaWindow(new MaticnaPloca(0, "", "", jib_racunara), true);

                if (unos.ShowDialog() == true)
                {
                    lvPrikazKomponenti.Items.Add(unos.maticna);
                    lvPrikazKomponenti.Items.Refresh();
                    if (lvPrikazKomponenti.Items.Count == 1)
                    {
                        btnUnosNoveKomponente.IsEnabled = false;
                    }
                }
            }
            if (tip == "ram")
            {
                UnosRAMWindow unos = new UnosRAMWindow(new RAM(0, "", "", "", jib_racunara), true);

                if (unos.ShowDialog() == true)
                {
                    lvPrikazKomponenti.Items.Add(unos.ram);
                    lvPrikazKomponenti.Items.Refresh();
                }
            }
            if (tip == "hdd")
            {
                UnosHDDWindow unos = new UnosHDDWindow(new HDD(0, "", "", "", jib_racunara), true);

                if (unos.ShowDialog() == true)
                {
                    lvPrikazKomponenti.Items.Add(unos.hdd);
                    lvPrikazKomponenti.Items.Refresh();
                }
            }
            if (tip == "gpu")
            {
                UnosGPUWindow unos = new UnosGPUWindow(new GPU(0, "", "", "", jib_racunara), true);

                if (unos.ShowDialog() == true)
                {
                    lvPrikazKomponenti.Items.Add(unos.gpu);
                    lvPrikazKomponenti.Items.Refresh();
                }
            }
            if (tip == "dvd")
            {
                UnosDVDWindow unos = new UnosDVDWindow(new DVD(0, "", "", jib_racunara), true);

                if (unos.ShowDialog() == true)
                {
                    lvPrikazKomponenti.Items.Add(unos.dvd);
                    lvPrikazKomponenti.Items.Refresh();
                }
            }
        }

        private void btnBrisanjeKomponente_Click(object sender, RoutedEventArgs e)
        {
            if (lvPrikazKomponenti.SelectedItem != null)
            {
                if (tip == "monitor")
                {
                    Monitor monitor_za_brisanje = lvPrikazKomponenti.SelectedItem as Monitor;
                    string cmd_string = "DELETE FROM `komponenta` WHERE idkomponenta = " + monitor_za_brisanje.IDKomponente;
                    MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                    cmd.ExecuteNonQuery();

                    lvPrikazKomponenti.Items.Remove(lvPrikazKomponenti.SelectedItem);
                    lvPrikazKomponenti.Items.Refresh();
                }
                if (tip == "procesor")
                {
                    Procesor procesor_za_brisanje = lvPrikazKomponenti.SelectedItem as Procesor;
                    string cmd_string = "DELETE FROM `komponenta` WHERE idkomponenta = " + procesor_za_brisanje.IDKomponente;
                    MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                    cmd.ExecuteNonQuery();

                    lvPrikazKomponenti.Items.Remove(lvPrikazKomponenti.SelectedItem);
                    lvPrikazKomponenti.Items.Refresh();
                    if (lvPrikazKomponenti.Items.Count == 0)
                    {
                        btnUnosNoveKomponente.IsEnabled = true;
                    }
                }
                if (tip == "maticna")
                {
                    MaticnaPloca maticna_za_brisanje = lvPrikazKomponenti.SelectedItem as MaticnaPloca;
                    string cmd_string = "DELETE FROM `komponenta` WHERE idkomponenta = " + maticna_za_brisanje.IDKomponente;
                    MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                    cmd.ExecuteNonQuery();

                    lvPrikazKomponenti.Items.Remove(lvPrikazKomponenti.SelectedItem);
                    lvPrikazKomponenti.Items.Refresh();
                    if (lvPrikazKomponenti.Items.Count == 0)
                    {
                        btnUnosNoveKomponente.IsEnabled = true;
                    }
                }
                if (tip == "ram")
                {
                    RAM ram_za_brisanje = lvPrikazKomponenti.SelectedItem as RAM;
                    string cmd_string = "DELETE FROM `komponenta` WHERE idkomponenta = " + ram_za_brisanje.IDKomponente;
                    MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                    cmd.ExecuteNonQuery();

                    lvPrikazKomponenti.Items.Remove(lvPrikazKomponenti.SelectedItem);
                    lvPrikazKomponenti.Items.Refresh();
                }
                if (tip == "hdd")
                {
                    HDD hdd_za_brisanje = lvPrikazKomponenti.SelectedItem as HDD;
                    string cmd_string = "DELETE FROM `komponenta` WHERE idkomponenta = " + hdd_za_brisanje.IDKomponente;
                    MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                    cmd.ExecuteNonQuery();

                    lvPrikazKomponenti.Items.Remove(lvPrikazKomponenti.SelectedItem);
                    lvPrikazKomponenti.Items.Refresh();
                }
                if (tip == "gpu")
                {
                    GPU gpu_za_brisanje = lvPrikazKomponenti.SelectedItem as GPU;
                    string cmd_string = "DELETE FROM `komponenta` WHERE idkomponenta = " + gpu_za_brisanje.IDKomponente;
                    MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                    cmd.ExecuteNonQuery();

                    lvPrikazKomponenti.Items.Remove(lvPrikazKomponenti.SelectedItem);
                    lvPrikazKomponenti.Items.Refresh();
                }
                if (tip == "dvd")
                {
                    DVD dvd_za_brisanje = lvPrikazKomponenti.SelectedItem as DVD;
                    string cmd_string = "DELETE FROM `komponenta` WHERE idkomponenta = " + dvd_za_brisanje.IDKomponente;
                    MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                    cmd.ExecuteNonQuery();

                    lvPrikazKomponenti.Items.Remove(lvPrikazKomponenti.SelectedItem);
                    lvPrikazKomponenti.Items.Refresh();
                }
            }
        }
    }
}
