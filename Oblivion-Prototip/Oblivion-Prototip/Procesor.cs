using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblivion_Prototip
{
    public class Procesor
    {
        public int IDKomponente { get; set; }
        public string NazivProizvodjaca { get; set; }
        public string Ime { get; set; }
        public string Frekvencija { get; set; }
        public string BrojJezgara { get; set; }
        public int BrojRacunara { get; set; }

        public Procesor(int IDKomponente, string NazivProizvodjaca, string Ime, string Frekvencija, string BrojJezgara, int BrojRacunara)
        {
            this.IDKomponente = IDKomponente;
            this.NazivProizvodjaca = NazivProizvodjaca;
            this.Ime = Ime;
            this.Frekvencija = Frekvencija;
            this.BrojJezgara = BrojJezgara;
            this.BrojRacunara = BrojRacunara;
        }

        public override string ToString()
        {
            return Ime + "\t\t" + BrojJezgara + " jezgara" + "\t\t" + Frekvencija + " GHz";
        }
    }
}
