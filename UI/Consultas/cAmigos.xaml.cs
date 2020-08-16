using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.Generic;
using PrestamosdeJuegos.BLL;
using PrestamosdeJuegos.Entidades;

namespace PrestamosdeJuegos.UI.Consultas
{
    public partial class cAmigos : Window
    {
        public cAmigos()
        {
            InitializeComponent();
        }
        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Amigos>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        try
                        {
                            listado = AmigosBLL.GetList(a => a.AmigoId == Utilidades.ToInt(CriterioTextBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Debes ingresar un Critero valido para aplicar este filtro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;

                    case 1:
                        try
                        {
                            listado = AmigosBLL.GetList(a => a.NombreCompleto == (CriterioTextBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Debes ingresar un Critero valido para aplicar este filtro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;

                    case 2:
                        try
                        {
                            long id = long.Parse(CriterioTextBox.Text);
                            listado = AmigosBLL.GetList(a => a.Telefono == id);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Debes ingresar un Critero valido para aplicar este filtro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;

                    case 3:
                        try
                        {
                            long id = long.Parse(CriterioTextBox.Text);
                            listado = AmigosBLL.GetList(a => a.Celular == id);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Debes ingresar un Critero valido para aplicar este filtro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;

                    case 4:
                        try
                        {
                            listado = AmigosBLL.GetList(a => a.Email == (CriterioTextBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Debes ingresar un Critero valido para aplicar este filtro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;
                }
            }
            else
            {
                listado = AmigosBLL.GetList(c => true);
            }

            if (DesdeDatePicker.SelectedDate != null)
                listado = AmigosBLL.GetList(c => c.FechaNacimiento.Date >= DesdeDatePicker.SelectedDate);

            if (HastaDatePicker.SelectedDate != null)
                listado = AmigosBLL.GetList(c => c.FechaNacimiento.Date <= HastaDatePicker.SelectedDate);

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}
