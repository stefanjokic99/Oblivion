using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblivion_Prototip
{
    public class DVD
    {
        public int IDKomponente { get; set; }
        public string NazivProizvodjaca { get; set; }
        public string Brzina { get; set; }
        public int BrojRacunara { get; set; }

        public DVD(int IDKomponente, string NazivProizvodjaca, string Brzina, int BrojRacunara)
        {
            this.IDKomponente = IDKomponente;
            this.NazivProizvodjaca = NazivProizvodjaca;
            this.Brzina = Brzina;
            this.BrojRacunara = BrojRacunara;
        }

        public override string ToString()
        {
            return NazivProizvodjaca + "\t\t" + Brzina + " MB/s";
        }
    }
}
