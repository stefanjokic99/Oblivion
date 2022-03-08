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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Oblivion_Prototip
{
    /// <summary>
    /// Interaction logic for UcIgrica.xaml
    /// </summary>
    public partial class UcIgrica : UserControl
    {
        public string Ime { get; set; }
        public List<UcRacunar> Racunari { get; set; }

        public UcIgrica(string ime, string slika, string vrsta, List<UcRacunar> racunari)
        {
            InitializeComponent();
            tbImeIgrice.Text = ime;
            this.Ime = ime;
            imgSlikaIgrice.Source = new BitmapImage(new Uri(@"/Resources/" + slika, UriKind.Relative));
            tbVrsta.Text = vrsta;
            this.Racunari = racunari;

            if (racunari != null)
            {
                foreach (UcRacunar racunar in Racunari)
                {
                    TextBlock tb = new TextBlock();

                    tb.Foreground = Brushes.WhiteSmoke;
                    tb.Margin = new Thickness(3, 3, 0, 0);
                    tb.FontSize = 13;
                    tb.TextWrapping = TextWrapping.WrapWithOverflow;
                    tb.Text = racunar.MreznoIme;

                    spRacunari.Children.Add(tb);
                }
            }
        }
    }
}
