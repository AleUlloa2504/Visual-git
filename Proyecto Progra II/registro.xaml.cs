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

namespace Proyecto_Progra_II
{
    /// <summary>
    /// Interaction logic for registro.xaml
    /// </summary>
    public partial class registro : Window
    {
        public registro()
        {
            InitializeComponent();
        }
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nombre = txbNombre.Text;
                string tipo = txbTipo.Text;
                string usuario = txbUsuario.Text;
                string password = txbPassword.Text;
                string confirmarPassword = txbConfirmarPassword.Text;
                if (password == confirmarPassword)
                {
                    login Login = new login();
                    Login.Escribir(nombre + "," + tipo + "," + usuario + "," + password);
                    MessageBox.Show("Usuario registrado exitosamente");
                    this.Close();
                } else
                {
                    MessageBox.Show("La contraseña debe ser la misma en la confirmación. Intente de nuevo");
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
