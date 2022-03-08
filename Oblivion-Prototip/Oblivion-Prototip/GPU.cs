using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblivion_Prototip
{
    public class GPU
    {
        public int IDKomponente { get; set; }
        public string NazivProizvodjaca { get; set; }
        public string Ime { get; set; }
        public string KolicinaMemorije { get; set; }
        public int BrojRacunara { get; set; }

        public GPU(int IDKomponente, string NazivProizvodjaca, string Ime, string KolicinaMemorije, int BrojRacunara)
        {
            this.IDKomponente = IDKomponente;
            this.NazivProizvodjaca = NazivProizvodjaca;
            this.Ime = Ime;
            this.KolicinaMemorije = KolicinaMemorije;
            this.BrojRacunara = BrojRacunara;
        }

        public override string ToString()
        {
            return Ime + "\t\t" + KolicinaMemorije;
        }
    }
}
