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
    public partial class rAmigos : Window
    {
        private Amigos amigos = new Amigos();
        public rAmigos()
        {
            InitializeComponent();
            this.DataContext = amigos;
        }
        //——————————————————————————————————————————————————————————————[ Cargar ]———————————————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = amigos;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.amigos = new Amigos();
            this.DataContext = amigos;
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (AmigoIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Amigos encontrado = AmigosBLL.Buscar(Utilidades.ToInt(AmigoIdTextBox.Text));

            if (encontrado != null)
            {
                this.amigos = encontrado;
                Cargar();
            }
            else
            {
                this.amigos = new Amigos();
                this.DataContext = this.amigos;
                MessageBox.Show($"Este Amigo no fue encontrado.\n\nAsegúrese que existe o cree una nuevo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                Limpiar();
                AmigoIdTextBox.SelectAll();
                AmigoIdTextBox.Focus();
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
                //—————————————————————————————————[ Amigo Id ]—————————————————————————————————
                if (AmigoIdTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Amigo Id) está vacío.\n\nAsigne un Id al Amigo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    AmigoIdTextBox.Text = "0";
                    AmigoIdTextBox.Focus();
                    AmigoIdTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Nombre Completo ]—————————————————————————————————
                if (NombreCompletoTextBox.Text == string.Empty)
                {
                    MessageBox.Show("El Campo (Nombre Completo) está vacío.\n\nAsigne un Nombre Completo al Amigo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    NombreCompletoTextBox.Clear();
                    NombreCompletoTextBox.Focus();
                    return;
                }
                //—————————————————————————————————[ Direccion ]—————————————————————————————————
                if (DireccionTextBox.Text == string.Empty)
                {
                    MessageBox.Show("El Campo (Direccion) está vacío.\n\nAsigne una Direccion al Amigo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    DireccionTextBox.Clear();
                    DireccionTextBox.Focus();
                    return;
                }
                //—————————————————————————————————[ Telefono ]—————————————————————————————————
                if (TelefonoTextBox.Text == string.Empty)
                {
                    MessageBox.Show("El Campo (Telefono) está vacío.\n\nAsigne un Teléfono al Amigo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TelefonoTextBox.Clear();
                    TelefonoTextBox.Focus();
                    return;
                }
                if (TelefonoTextBox.Text.Length != 10)
                {
                    MessageBox.Show($"El Celular({TelefonoTextBox.Text}) no es válido.\n\nEl Teléfono debe tener 10 dígitos (0-9).", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TelefonoTextBox.Clear();
                    TelefonoTextBox.Focus();
                    return;
                }
                //—————————————————————————————————[ Celular ]—————————————————————————————————
                if (CelularTextBox.Text == string.Empty)
                {
                    MessageBox.Show("El Campo (Celular) está vacío.\n\nAsigne un Celular al Amigo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    CelularTextBox.Clear();
                    CelularTextBox.Focus();
                    return;
                }
                if (CelularTextBox.Text.Length != 10)
                {
                    MessageBox.Show($"El Celular({CelularTextBox.Text}) no es válido.\n\nEl Celular debe tener 10 dígitos (0-9).", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    CelularTextBox.Clear();
                    CelularTextBox.Focus();
                    return;
                }
                //—————————————————————————————————[ Email ]—————————————————————————————————
                if (EmailTextBox.Text == string.Empty)
                {
                    MessageBox.Show("El Campo (Email) está vacío.\n\nAsigne un Email al Amigo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    EmailTextBox.Clear();
                    EmailTextBox.Focus();
                    return;
                }
                //—————————————————————————————————[ Fecha Nacimiento ]—————————————————————————————————
                if (FechaNacimientoDatePicker.Text.Trim() == string.Empty)
                {
                    MessageBox.Show($"El Campo (Fecha Nacimiento) está vacío.\n\nPorfavor, Seleccione una fecha.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    FechaNacimientoDatePicker.Focus();
                    return;
                }
                //-----------------------------------------------------------------------------------------------------------------------
                var paso = AmigosBLL.Guardar(amigos);
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
                if (AmigosBLL.Eliminar(Utilidades.ToInt(AmigoIdTextBox.Text)))
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
        private void AmigoIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (AmigoIdTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(AmigoIdTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Amigo Id) no es un número.\n\nPor favor, digite un número.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                AmigoIdTextBox.Text = "0";
                AmigoIdTextBox.Focus();
                AmigoIdTextBox.SelectAll();
            }
        }
        //——————————————————————————————————————————[ TelefonoTextBox_TextChanged ]——————————————————————————————————————————
        private void TelefonoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (TelefonoTextBox.Text.Trim() != string.Empty)
                {
                    long.Parse(TelefonoTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show("El valor digitado en el campo (Telefono) no es un número.\n\nPor favor, digite un número.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                TelefonoTextBox.Text = "0";
                TelefonoTextBox.Focus();
                TelefonoTextBox.SelectAll();
            }
        }
        private void CelularTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (CelularTextBox.Text.Trim() != string.Empty)
                {
                    long.Parse(CelularTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show("El valor digitado en el campo (Celular) no es un número.\n\nPor favor, digite un número.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                CelularTextBox.Text = "0";
                CelularTextBox.Focus();
                CelularTextBox.SelectAll();
            }
        }
    }
}