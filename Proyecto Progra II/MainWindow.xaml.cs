using System;
using System.Collections.Generic;
using System.IO;
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

namespace Proyecto_Progra_II
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea salir?","Cerrar aplicación", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
            else
            {
            }



        }

        private void btnQuienesSomos_Click(object sender, RoutedEventArgs e)
        {
            quienessomos ventana = new quienessomos();
            ventana.Show();
        }

        private void btnProductos_Click(object sender, RoutedEventArgs e)
        {
            string id=null;
            Productos ventana = new Productos(id);
            ventana.Show();
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            registro ventana = new registro();
            ventana.Show();
        }

        private void btnVentas_Click(object sender, RoutedEventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
        }
    }
}