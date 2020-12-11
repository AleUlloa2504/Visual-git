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
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        string pathName = @"./usuarios.txt";
        public login()
        {
            InitializeComponent();
            VerificarArchivo();
        }

        private void VerificarArchivo()
        {
            try
            {
                if (!File.Exists(pathName))
                {
                    File.Create(pathName).Dispose();
                    Escribir("administrador,administrador,admin,adm2");
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void Escribir(string mensaje)
        {
            StreamWriter tuberiaEscritura = File.AppendText(pathName);
            tuberiaEscritura.WriteLine(mensaje);
            tuberiaEscritura.Close();
        }

        private void BtnIngresar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string usuario = txtUsuario.Text.Trim();
                string password = txbPassword.Password.Trim();
                if (usuario != "" && password != "")
                {
                    if (ValidarUsuario(usuario, password))
                    {
                        MainWindow ventana = new MainWindow();
                        ventana.Show();
                        this.Close();
                    }
                    else
                    {
                        lblMensaje.Content = "Datos incorrectos, inténtelo de nuevo";
                    }
                }
                else
                {
                    lblMensaje.Content = "Ingresa los datos";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private bool ValidarUsuario(string usuario, string password)
        {
            bool resultado = false;
            string[] datosUsuario;
            StreamReader tuberiaLectura = File.OpenText(pathName);
            string linea = tuberiaLectura.ReadLine();
            while (linea != null)
            {
                datosUsuario = linea.Split(',');
                if (datosUsuario[2] == usuario && datosUsuario[3] == password)
                {
                    resultado = true;
                    break;
                }
                linea = tuberiaLectura.ReadLine();
            }
            tuberiaLectura.Close();
            return resultado;
        }
    }
}
