using PrestamosdeJuegos.UI.Consultas;
using PrestamosdeJuegos.UI.Registros;
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

namespace PrestamosdeJuegos
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //——————————————————————————————————————————[Registros]——————————————————————————————————————————
        private void rAmigosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rAmigos rAmigos = new rAmigos();
            rAmigos.Show();
        }

        private void rJuegosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rJuegos rJuegos = new rJuegos();
            rJuegos.Show();
        }

        private void rPrestamosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rPrestamos rPrestamos = new rPrestamos();
            rPrestamos.Show();
        }

        private void rEntradasJuegosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rEntradasJuegos rEntradasJuegos = new rEntradasJuegos();
            rEntradasJuegos.Show();
        }


        //——————————————————————————————————————————[Consultas]——————————————————————————————————————————
        private void cAmigosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            cAmigos cAmigos = new cAmigos();
            cAmigos.Show();
        }  

        private void cJuegosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            cJuegos cJuegos = new cJuegos();
            cJuegos.Show();
        }

        private void cPrestamosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            cPrestamos cPrestamos = new cPrestamos();
            cPrestamos.Show();
        }        

        private void cEntradasJuegosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            cEntradasJuegos cEntradasJuegos = new cEntradasJuegos();
            cEntradasJuegos.Show();
        }
    }
}
