using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblivion_Prototip
{
    public class RAM
    {
        public int IDKomponente { get; set; }
        public string NazivProizvodjaca { get; set; }
        public string Tip { get; set; }
        public string Kapacitet { get; set; }
        public int BrojRacunara { get; set; }

        public RAM(int IDKomponente, string NazivProizvodjaca, string Tip, string Kapacitet, int BrojRacunara)
        {
            this.IDKomponente = IDKomponente;
            this.NazivProizvodjaca = NazivProizvodjaca;
            this.Tip = Tip;
            this.Kapacitet = Kapacitet;
            this.BrojRacunara = BrojRacunara;
        }

        public override string ToString()
        {
            return NazivProizvodjaca + "\t\t" + Tip + "\t\t" + Kapacitet + " GB";
        }
    }
}
