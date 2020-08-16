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
    public partial class rPrestamos : Window
    {
        private Prestamos prestamos = new Prestamos();
        public rPrestamos()
        {
            InitializeComponent();
            this.DataContext = prestamos;
            //—————————————————————————————————————[ ComboBox AmigoId ]—————————————————————————————————————
            AmigoIdComboBox.ItemsSource = AmigosBLL.GetAmigos();
            AmigoIdComboBox.SelectedValuePath = "AmigoId";
            AmigoIdComboBox.DisplayMemberPath = "NombreCompleto";
            //—————————————————————————————————————[ ComboBox JuegoId ]—————————————————————————————————————
            JuegoIdComboBox.ItemsSource = JuegosBLL.GetJuegos();
            JuegoIdComboBox.SelectedValuePath = "JuegoId";
            JuegoIdComboBox.DisplayMemberPath = "Descripcion";
        }

        //——————————————————————————————————————————————————————————————[ Cargar ]———————————————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = prestamos;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.prestamos = new Prestamos();
            this.DataContext = prestamos;
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (PrestamoIdTextbox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Prestamos encontrado = PrestamosBLL.Buscar(prestamos.PrestamoId);

            if (encontrado != null)
            {
                prestamos = encontrado;
                Cargar();
            }
            else
            {
                MessageBox.Show($"Este Préstamo no fue encontrado.\n\nAsegúrese que existe o cree uno nuevo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                Limpiar();
                PrestamoIdTextbox.SelectAll();
                PrestamoIdTextbox.Focus();
            }
        }
        //——————————————————————————————————————————————————————————————[ Agregar Fila ]———————————————————————————————————————————————————————————————
        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            //—————————————————————————————————[ Juego Id ]—————————————————————————————————
            if (JuegoIdComboBox.Text == string.Empty)
            {
                MessageBox.Show("El Campo (Juego Id) está vacío.\n\nSelecione un Juego.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                JuegoIdComboBox.IsDropDownOpen = true;
                return;
            }
            //—————————————————————————————————[ Cantidad Juegos ]—————————————————————————————————
            if (CantidadJuegosTextBox.Text == string.Empty)
            {
                MessageBox.Show("El Campo (Cantidad Juegos) está vacío.\n\nPorfavor, Escriba la cantidad de juegos a prestar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                CantidadJuegosTextBox.Text = "0";
                CantidadJuegosTextBox.Focus();
                CantidadJuegosTextBox.SelectAll();
                return;
            }

            var filaDetalle = new PrestamosDetalle
            {
                PrestamosId = this.prestamos.PrestamoId,
                JuegoId = Convert.ToInt32(JuegoIdComboBox.SelectedValue.ToString()),
                //——————————————————————————————[ Nombre en el ComboBox ]——————————————————————————————
                FK_Juegos = (Juegos)JuegoIdComboBox.SelectedItem,
                //—————————————————————————————————————————————————————————————————————————————————————
                CantidadJuegos = Convert.ToSingle(CantidadJuegosTextBox.Text)
            };
            //——————————————————————————————[Prestamos Total]——————————————————————————————
            prestamos.CantidadJuegosTotal += Convert.ToDouble(CantidadJuegosTextBox.Text.ToString());
            //——————————————————————————————————————————————————————————————————————————
            this.prestamos.Detalle.Add(filaDetalle);
            Cargar();

            JuegoIdComboBox.SelectedIndex = -1;
            CantidadJuegosTextBox.Text = "0";
        }
        //——————————————————————————————————————————————————————————————[ Remover Fila ]———————————————————————————————————————————————————————————————
        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double total = Convert.ToDouble(CantidadJuegosTextBox.Text);
                if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
                {
                    prestamos.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                    prestamos.CantidadJuegosTotal -= total;
                    Cargar();
                }
            }
            catch
            {
                MessageBox.Show("No has seleccionado ninguna Fila\n\nSeleccione la Fila a Remover.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
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

                //—————————————————————————————————[ Prestamo Id ]—————————————————————————————————
                if (PrestamoIdTextbox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Préstamo Id) está vacío.\n\nAsigne un Id al Préstamo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    PrestamoIdTextbox.Text = "0";
                    PrestamoIdTextbox.Focus();
                    PrestamoIdTextbox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Amigo Id ]—————————————————————————————————
                if (AmigoIdComboBox.Text == string.Empty)
                {
                    MessageBox.Show("El Campo (Amigo Id) está vacío.\n\nPorfavor, Seleccione un Amigo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    AmigoIdComboBox.IsDropDownOpen = true;
                    return;
                }

                var paso = PrestamosBLL.Guardar(this.prestamos);
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
                if (PrestamosBLL.Eliminar(Utilidades.ToInt(PrestamoIdTextbox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //——————————————————————————————————————————————————————————————[ TextChanged ]———————————————————————————————————————————————————————————————
        //——————————————————————————————————————————[ Prestamo Id ]——————————————————————————————————————————
        private void PrestamoIdTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (PrestamoIdTextbox.Text.Trim() != string.Empty)
                {
                    int.Parse(PrestamoIdTextbox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Préstamo Id) no es un número.\n\nPor favor, digite un número.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                PrestamoIdTextbox.Text = "0";
                PrestamoIdTextbox.Focus();
                PrestamoIdTextbox.SelectAll();
            }
        }

        private void CantidadJuegosTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (CantidadJuegosTextBox.Text.Trim() != string.Empty)
                {
                    double.Parse(CantidadJuegosTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Cantidad Juegos) no es un número.\n\nPor favor, digite un número.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                CantidadJuegosTextBox.Text = "0";
                CantidadJuegosTextBox.Focus();
                CantidadJuegosTextBox.SelectAll();
            }
        }
    }
}
