using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblivion_Prototip
{
    public class MaticnaPloca
    {
        public int IDKomponente { get; set; }
        public string NazivProizvodjaca { get; set; }
        public string Cipset { get; set; }
        public int BrojRacunara { get; set; }

        public MaticnaPloca(int IDKomponente, string NazivProizvodjaca, string Cipset, int BrojRacunara)
        {
            this.IDKomponente = IDKomponente;
            this.NazivProizvodjaca = NazivProizvodjaca;
            this.Cipset = Cipset;
            this.BrojRacunara = BrojRacunara;
        }

        public override string ToString()
        {
            return NazivProizvodjaca + "\t\tcipset: " + Cipset;
        }
    }
}
