using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblivion_Prototip
{
    public class HDD
    {
        public int IDKomponente { get; set; }
        public string NazivProizvodjaca { get; set; }
        public string Kapacitet { get; set; }
        public string BrzinaObrtaja { get; set; }
        public int BrojRacunara { get; set; }

        public HDD(int IDKomponente, string NazivProizvodjaca, string Kapacitet, string BrzinaObrtaja, int BrojRacunara)
        {
            this.IDKomponente = IDKomponente;
            this.NazivProizvodjaca = NazivProizvodjaca;
            this.Kapacitet = Kapacitet;
            this.BrzinaObrtaja = BrzinaObrtaja;
            this.BrojRacunara = BrojRacunara;
        }

        public override string ToString()
        {
            return NazivProizvodjaca + "\t\tBrzina obrtaja:" + BrzinaObrtaja + "\t\t" + Kapacitet + " GB";
        }
    }
}
