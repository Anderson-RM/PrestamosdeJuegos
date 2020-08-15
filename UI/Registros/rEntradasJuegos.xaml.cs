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
    public partial class rEntradasJuegos : Window
    {
        private EntradasJuegos entradasJuegos = new EntradasJuegos();
        public rEntradasJuegos()
        {
            InitializeComponent();
            this.DataContext = entradasJuegos;
            //—————————————————————————————————————[ ComboBox JuegoId ]—————————————————————————————————————
            JuegoIdComboBox.ItemsSource = JuegosBLL.GetJuegos();
            JuegoIdComboBox.SelectedValuePath = "JuegoId";
            JuegoIdComboBox.DisplayMemberPath = "Nombre";
        }
        //——————————————————————————————————————————————————————————————[ Cargar ]———————————————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = entradasJuegos;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.entradasJuegos = new EntradasJuegos();
            this.DataContext = entradasJuegos;
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (EntradaJuegoIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            EntradasJuegos encontrado = EntradasJuegosBLL.Buscar(Utilidades.ToInt(EntradaJuegoIdTextBox.Text));

            if (encontrado != null)
            {
                this.entradasJuegos = encontrado;
                Cargar();
            }
            else
            {
                this.entradasJuegos = new EntradasJuegos();
                this.DataContext = this.entradasJuegos;
                MessageBox.Show($"Esta Entrada de Juegos no fue encontrada.\n\nAsegúrese que existe o cree una nueva.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
                EntradaJuegoIdTextBox.SelectAll();
                EntradaJuegoIdTextBox.Focus();
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
                //—————————————————————————————————[ EntradaJuego Id ]—————————————————————————————————
                if (EntradaJuegoIdTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (EntradaJuego Id) está vacío.\n\nDebe asignar un Id a la Entrada del Juego.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    EntradaJuegoIdTextBox.Text = "0";
                    EntradaJuegoIdTextBox.Focus();
                    EntradaJuegoIdTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Juego Id ]—————————————————————————————————
                if (JuegoIdComboBox.Text == string.Empty)
                {
                    MessageBox.Show("El Campo (Juego Id) está vacío.\n\nAsigne un Id al Juego.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    JuegoIdComboBox.IsDropDownOpen = true;
                    return;
                }
                //—————————————————————————————————[ Fecha ]—————————————————————————————————
                if (FechaDatePicker.Text.Trim() == string.Empty)
                {
                    MessageBox.Show($"El Campo (Fecha) está vacío.\n\nSeleccione una fecha para la Salida del Juego.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    FechaDatePicker.Focus();
                    return;
                }
                //—————————————————————————————————[ Cantidad ]—————————————————————————————————
                if (CantidadTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Cantidad) está vacío.\n\nEscriba la cantidad de Juegos.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    CantidadTextBox.Text = "0";
                    CantidadTextBox.Focus();
                    CantidadTextBox.SelectAll();
                    return;
                }
                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO - FIN ]———————————————————————————————————————————————————————

                JuegosBLL.SumarJuegos(Convert.ToInt32(JuegoIdComboBox.SelectedValue), Convert.ToDouble(CantidadTextBox.Text)); //suma la existencia

                var paso = EntradasJuegosBLL.Guardar(entradasJuegos);
                if (paso)
                {
                    Limpiar();
                    MessageBox.Show("Transacción Exitosa", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //——————————————————————————————————————————————————————————————[ Eliminar ]———————————————————————————————————————————————————————————————
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (EntradasJuegosBLL.Eliminar(Utilidades.ToInt(EntradaJuegoIdTextBox.Text)))
                {
                    JuegosBLL.RestarJuegos(Convert.ToInt32(JuegoIdComboBox.SelectedValue), Convert.ToDouble(CantidadTextBox.Text)); //-----------------
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //—————————————————————————————————————————————————————————————[ TEXT CHANGED ]—————————————————————————————————————————————————————————————

        //——————————————————————————————————————————[ EntradaJuego Id]——————————————————————————————————————————
        private void EntradaLibroIdTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (EntradaJuegoIdTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(EntradaJuegoIdTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (EntradaJuego Id) no es un número.\n\nPor favor, digite un número.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                EntradaJuegoIdTextBox.Text = "0";
                EntradaJuegoIdTextBox.Focus();
                EntradaJuegoIdTextBox.SelectAll();
            }
        }

        //——————————————————————————————————————————[ Cantidad ]——————————————————————————————————————————
        private void CantidadTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (CantidadTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(CantidadTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Cantidad) no es un número.\n\nPor favor, digite un número.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                CantidadTextBox.Text = "0";
                CantidadTextBox.Focus();
                CantidadTextBox.SelectAll();
            }
        }
    }
}