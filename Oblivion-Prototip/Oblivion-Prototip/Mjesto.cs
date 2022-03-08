using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblivion_Prototip
{
    class Mjesto
    {
        public string Naziv { get; set; }
        public int PostanskiBroj { get; set; }

        public Mjesto(string naziv, int ptt)
        {
            Naziv = naziv;
            PostanskiBroj = ptt;
        }

        public override string ToString()
        {
            return Naziv;
        }
    }
}
