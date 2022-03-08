using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MySql.Data.MySqlClient;

namespace Oblivion_Prototip
{
    public class Korisnik
    {
        public string JMBG { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public double Plata { get; set; }
        public int MjestoPTT { get; set; }
        public int RegBrojIgraonice { get; set; }
        public bool Administrator { get; set; }
        public string KorisnickoIme { get; set; }
        // private string Lozinka { get; set; }

        public Korisnik(string JMBG, string Ime, string Prezime, DateTime DatumZaposlenja, double Plata, int MjestoPTT, int RegBrojIgraonice, bool Administrator,
            string KorisnickoIme)
        {
            this.JMBG = JMBG;
            this.Ime = Ime;
            this.Prezime = Prezime;
            this.DatumZaposlenja = DatumZaposlenja;
            this.Plata = Plata;
            this.MjestoPTT = MjestoPTT;
            this.RegBrojIgraonice = RegBrojIgraonice;
            this.Administrator = Administrator;
            this.KorisnickoIme = KorisnickoIme;
          //  this.Lozinka = Lozinka;
        }

        public override string ToString()
        {
            string ispis = "JMBG\t\t\tIme\t\t\t\tPrezime\t\t\t\tDatum Zaposlenja\t\t\t\tPlata\t\t\t\tMjesto\n";
            string mjesto = "";

            string cmd_string = "SELECT `naziv` FROM `mjesto` WHERE `ptt` = " + MjestoPTT;
            MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                mjesto = reader["naziv"].ToString();
            }
            reader.Close();

            ispis += JMBG + "\t\t" + Ime + "\t\t\t\t" + Prezime + "\t\t\t\t" + DatumZaposlenja.ToString("dd-MM-yy") + "\t\t\t\t" + Plata + " KM \t\t\t\t" + mjesto;

            if (!Administrator)
            {
                cmd_string = "SELECT * FROM `racun` WHERE `radnik_idradnika` = '" + JMBG + "'";
                cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

                reader = cmd.ExecuteReader();

                ispis += "\n\n----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n\n";

                ispis += "ID_RACUNA\t\tUKUPNA CIJENA\n";

                while (reader.Read())
                {
                    string id_racuna = reader["idracun"].ToString();
                    double ukupna_cijena = (double)reader["ukupna_cijena"];

                    ispis += id_racuna + "\t\t\t" + ukupna_cijena + " KM\n";
                }

                reader.Close();

            }

            return ispis;
        }

        public void StampanjeIzvjestaja()
        {
            string path = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "Evidencija obrisanih zaposlenika");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllText(Path.Combine(path, Ime + " " + Prezime + " " + JMBG + ".txt"), this.ToString());
        }

        public bool promjenaKorisnickogImenaiSifre(string novoKorisnickoIme, string novaLozinka, bool promjena_sifre)
        {
            string cmd_string = "SELECT COUNT(*) as \"postoji\" FROM `radnik` WHERE `korisnicko_ime` = '" + novoKorisnickoIme + "'";
            MySqlCommand cmd = new MySqlCommand(cmd_string, Connection.GetConnection());

            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();
            int postoji = reader.GetInt32("postoji");

            reader.Close();

            if (promjena_sifre == true)
            {
                if ((postoji == 0) || (postoji == 1 && KorisnickoIme == novoKorisnickoIme))
                {
                        string cmd_string_u = "UPDATE `radnik` SET korisnicko_ime='" + novoKorisnickoIme + "', lozinka=PASSWORD('" + novaLozinka + "') WHERE `jmbg` = '" + JMBG + "'";
                        MySqlCommand cmd_u = new MySqlCommand(cmd_string_u, Connection.GetConnection());

                        cmd_u.ExecuteNonQuery();

                        KorisnickoIme = novoKorisnickoIme;
                        // Lozinka = novaLozinka;
                        return true;
                }
            }
            else
            {
                if (postoji == 0)
                {
                        string cmd_string_u = "UPDATE `radnik` SET korisnicko_ime='" + novoKorisnickoIme + "' WHERE `jmbg` = '" + JMBG + "'";
                        MySqlCommand cmd_u = new MySqlCommand(cmd_string_u, Connection.GetConnection());

                        cmd_u.ExecuteNonQuery();

                        KorisnickoIme = novoKorisnickoIme;
                        // Lozinka = novaLozinka;
                        return true;
                }
            }
            return false;
        }
    }
}
