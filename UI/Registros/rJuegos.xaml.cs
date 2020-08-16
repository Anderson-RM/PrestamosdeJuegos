using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PrestamosdeJuegos.BLL;
using PrestamosdeJuegos.Entidades;

namespace PrestamosdeJuegos.UI.Registros
{
    public partial class rJuegos : Window
    {
        private Juegos juegos = new Juegos();
        public rJuegos()
        {
            InitializeComponent();
            this.DataContext = juegos;
        }
        //——————————————————————————————————————————————————————————————[ Cargar ]———————————————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = juegos;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.juegos = new Juegos();
            this.DataContext = juegos;
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (JuegoIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Juegos encontrado = JuegosBLL.Buscar(Utilidades.ToInt(JuegoIdTextBox.Text));

            if (encontrado != null)
            {
                this.juegos = encontrado;
                Cargar();
            }
            else
            {
                this.juegos = new Juegos();
                this.DataContext = this.juegos;
                MessageBox.Show($"Este Juego no fue encontrado.\n\nAsegúrese que existe o cree una nuevo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                Limpiar();
                JuegoIdTextBox.SelectAll();
                JuegoIdTextBox.Focus();
            }
        }
        //——————————————————————————————————————————————————————————————[ Nuevo ]———————————————————————————————————————————————————————————————
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        //——————————————————————————————————————————————————————————————[ Guardar ]———————————————————————————————————————————————————————————————
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (!Validar())
                    return;

                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO ]———————————————————————————————————————————————————————
                //—————————————————————————————————[ Juego Id ]—————————————————————————————————
                if (JuegoIdTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Libro Id) está vacío.\n\nAsigne un Id al Juego.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    JuegoIdTextBox.Text = "0";
                    JuegoIdTextBox.Focus();
                    JuegoIdTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Fecha Compra ]—————————————————————————————————
                if (FechaCompraDatePicker.Text.Trim() == string.Empty)
                {
                    MessageBox.Show($"El Campo (Fecha Compra) está vacío.\n\nPorfavor, Seleccione una fecha.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    FechaCompraDatePicker.Focus();
                    return;
                }
                //—————————————————————————————————[ Descripcion ]—————————————————————————————————
                if (DescripcionTextBox.Text == string.Empty)
                {
                    MessageBox.Show("El Campo (Descripcion) está vacío.\n\nEscriba un de Descripcion.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    DescripcionTextBox.Clear();
                    DescripcionTextBox.Focus();
                    return;
                }
                //—————————————————————————————————[ Precio ]—————————————————————————————————
                if (PrecioTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Precio) está vacío.\n\nAsigne el Precio al juego.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    PrecioTextBox.Text = "0";
                    PrecioTextBox.Focus();
                    PrecioTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Existencia ]—————————————————————————————————
                if (ExistenciaTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Existencia) está vacío.\n\nEscriba la existencia actual del Libro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ExistenciaTextBox.Text = "0";
                    ExistenciaTextBox.Focus();
                    ExistenciaTextBox.SelectAll();
                    return;
                }
                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO - FIN ]———————————————————————————————————————————————————————
                var paso = JuegosBLL.Guardar(juegos);
                if (paso)
                {
                    Limpiar();
                    MessageBox.Show("Transacción Exitosa", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Transaccion Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //——————————————————————————————————————————————————————————————[ Eliminar ]———————————————————————————————————————————————————————————————
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (JuegosBLL.Eliminar(Utilidades.ToInt(JuegoIdTextBox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //—————————————————————————————————————————————————————————————[ TEXT CHANGED ]—————————————————————————————————————————————————————————————

        //——————————————————————————————————————————[ Juego Id ]——————————————————————————————————————————
        private void JuegoIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (JuegoIdTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(JuegoIdTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Juego Id) no es un número.\n\nPor favor, digite un número.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                JuegoIdTextBox.Text = "0";
                JuegoIdTextBox.Focus();
                JuegoIdTextBox.SelectAll();
            }
        }
        //——————————————————————————————————————————[ Precio ]——————————————————————————————————————————
        private void PrecioTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (PrecioTextBox.Text.Trim() != string.Empty)
                {
                    double.Parse(PrecioTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show("El valor digitado en el campo (Precio) no es un número.\n\nPor favor, digite un número.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                PrecioTextBox.Text = "0";
                PrecioTextBox.Focus();
                PrecioTextBox.SelectAll();
            }
        }
        //——————————————————————————————————————————[ Existencia ]——————————————————————————————————————————
        private void ExistenciaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            double Existencia;

            if (double.TryParse(ExistenciaTextBox.Text, out Existencia))
            {
                if (Existencia < 0)
                {
                    ExistenciaTextBox.Foreground = Brushes.Red;
                }
                if (Existencia == 0)
                {
                    ExistenciaTextBox.Foreground = Brushes.Black;
                }
                else if (Existencia > 0)
                {
                    ExistenciaTextBox.Foreground = Brushes.Green;
                }
            }
        }
    }
}