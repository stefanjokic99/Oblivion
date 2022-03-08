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

namespace Oblivion_Prototip
{
    /// <summary>
    /// Interaction logic for PrikazInformacijaIgraonica.xaml
    /// </summary>
    public partial class PrikazInformacijaIgraonica : Window
    {
        Korisnik korisnik;

        public PrikazInformacijaIgraonica(Korisnik korisnik)
        {
            InitializeComponent();
            this.korisnik = korisnik;
            ucitavanjeInfo();
        }

        private void lblNaslov_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void lblIzlaz_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
        }

        private void ucitavanjeInfo()
        {
            string cmd_string = "SELECT igraonica.naziv as \"naziv_igraonice\", broj_ispostave, adresa, vlasnik, telefon, mjesto_ptt, mjesto.naziv as \"naziv_mjesta\" FROM `igraonica` JOIN mjesto ON(mjesto.ptt = igraonica.mjesto_ptt)" +
                " WHERE reg_broj = " + korisnik.RegBrojIgraonice;
            MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            tbNaziv.Text = reader["naziv_igraonice"].ToString() + " br." + reader.GetInt32("broj_ispostave");
            tbVlasnik.Text = reader["vlasnik"].ToString();
            tbAdresa.Text = reader["adresa"].ToString() + ", " + reader["naziv_mjesta"].ToString() + " " + reader.GetInt32("mjesto_ptt");
            tbBrojTel.Text = reader["telefon"].ToString();

            reader.Close();

            cmd_string = "SELECT count(*) as \"broj_radnika\" FROM `radnik` WHERE igraonica_reg_broj = " + korisnik.RegBrojIgraonice + " AND zaposlen = 1";
            cmd.CommandText = cmd_string;

            reader = cmd.ExecuteReader();

            reader.Read();

            int broj_radnika = reader.GetInt32("broj_radnika");

            tbBrojRadnika.Text = broj_radnika.ToString();

            reader.Close();

            cmd_string = "SELECT count(*) as \"broj_racunara\" FROM `racunar` WHERE igraonica_reg_broj = " + korisnik.RegBrojIgraonice;
            cmd.CommandText = cmd_string;

            reader = cmd.ExecuteReader();

            reader.Read();

            int broj_racunara = reader.GetInt32("broj_racunara");

            tbBrojRačunara.Text = broj_racunara.ToString();

            reader.Close();

            cmd_string = "SELECT COUNT(DISTINCT idigrica) as \"broj_igrica\" FROM `instalirano` NATURAL JOIN igrica where racunar_broj_racunara in (Select racunar_broj_racunara from racunar where igraonica_reg_broj = " + korisnik.RegBrojIgraonice + ")";

            cmd.CommandText = cmd_string;

            reader = cmd.ExecuteReader();

            reader.Read();

            int broj_igrica = reader.GetInt32("broj_igrica");

            tbBrojIgrica.Text = broj_igrica.ToString();

            reader.Close();
        }
    }
}
