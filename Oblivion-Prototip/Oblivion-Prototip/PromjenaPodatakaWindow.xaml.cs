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
using System.Windows.Shapes;

namespace Oblivion_Prototip
{
    /// <summary>
    /// Interaction logic for PromjenaPodatakaWindow.xaml
    /// </summary>
    public partial class PromjenaPodatakaWindow : Window
    {
        ZaposleniWindow parentWindow;

        public PromjenaPodatakaWindow(ZaposleniWindow parentWindow)
        {
            InitializeComponent();
            this.parentWindow = parentWindow;
            ucitavanje();
        }

        private void ucitavanje()
        {
            tbKorisnickoIme.Text = parentWindow.zaposlenik.KorisnickoIme;
            pbLozinka.IsEnabled = false;
            pbAutorizacija.IsEnabled = false;
        }

        private void cbPromjenaSifre_Checked(object sender, RoutedEventArgs e)
        {
            pbLozinka.IsEnabled = (bool)cbPromjenaSifre.IsChecked;
            pbAutorizacija.IsEnabled = (bool)cbPromjenaSifre.IsChecked;
        }
        private void cbPromjenaSifre_Unchecked(object sender, RoutedEventArgs e)
        {
            pbLozinka.IsEnabled = (bool)cbPromjenaSifre.IsChecked;
            pbAutorizacija.IsEnabled = (bool)cbPromjenaSifre.IsChecked;
        }
        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {
            if (tbKorisnickoIme.Text != "")
            {
                if ((bool)cbPromjenaSifre.IsChecked)
                {
                    if (pbLozinka.Password == pbAutorizacija.Password)
                    {
                        if (pbLozinka.Password.Length > 3)
                        {
                            bool izvrsena_promjena = parentWindow.zaposlenik.promjenaKorisnickogImenaiSifre(tbKorisnickoIme.Text, pbLozinka.Password, (bool)cbPromjenaSifre.IsChecked);
                            if (izvrsena_promjena)
                            {
                                parentWindow.btnOdjava.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                                this.DialogResult = true;
                            }
                            else
                            {
                                MessageBox.Show("Korisničko ime postoji!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }    
                        }
                        else
                        {
                            MessageBox.Show("Lozinka mora imati više od 3 karaktera.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lozinka nije potvrđena.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    bool izvrsena_promjena = parentWindow.zaposlenik.promjenaKorisnickogImenaiSifre(tbKorisnickoIme.Text, pbLozinka.Password, (bool)cbPromjenaSifre.IsChecked);
                    if (izvrsena_promjena)
                    {
                        parentWindow.btnOdjava.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Korisničko ime postoji!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Unesite korisničko ime.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}