using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblivion_Prototip
{
    public class Monitor
    {
        public int IDKomponente { get; set; }
        public string NazivProizvodjaca { get; set; }
        public string Dimenzija { get; set; }
        public int BrojRacunara { get; set; }

        public Monitor(int IDKomponente, string NazivProizvodjaca, string Dimenzija, int BrojRacunara)
        {
            this.IDKomponente = IDKomponente;
            this.NazivProizvodjaca = NazivProizvodjaca;
            this.Dimenzija = Dimenzija;
            this.BrojRacunara = BrojRacunara;
        }

        public override string ToString()
        {
            return NazivProizvodjaca + "\t\t" + Dimenzija + " inch";
        }
    }
}
