using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Proyecto_Progra_II
{
    /// <summary>
    /// Lógica de interacción para Ventas.xaml
    /// </summary>
    public partial class Ventas : Window
    {
        string pathName = @".\ventas.txt";
        string pathNameP = @".\productos.txt";
        string pathNameT = @".\total.txt";
        private object dgVentas;
        private object dgMostrarVentas;

        public Ventas()
        {
            InitializeComponent();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string productoid = txbIdProducto .Text;
                string linea;
                int total;
                string[] datosProductoo;
                char separador = ',';
                bool encontrado = false;
                StreamReader tuberiaLectura = File.OpenText(pathNameP);
                StreamWriter tuberiaEscritura = File.AppendText(pathNameT);
                linea = tuberiaLectura.ReadLine();
                while (linea != null)
                {
                    datosProductoo = linea.Split(separador);
                    if (datosProductoo[0] == productoid)
                    {
                        string datosV = datosProductoo[2];
                        total = Convert.ToInt32(datosV);
                        tuberiaEscritura.WriteLine(datosProductoo[0] + "," + datosProductoo[1] + "," + datosProductoo[2] + "," + datosProductoo[3] + "," + datosProductoo[4]);
                        tuberiaEscritura.Close();
                        MessageBox.Show("Producto encontrado\n id: " +
                        datosProductoo[0] + "\nnombre: " + datosProductoo[1] +
                        "\nPrecio Venta: " + datosProductoo[2] + "\nPrecio Compra: " + datosProductoo[3] + "\ncantidad: " + datosProductoo[4] + "\nTotal a pagar: " + total);
                        MostrarFinalDG();
                        encontrado = true;
                    }
                    linea = tuberiaLectura.ReadLine();
                }
                if (!encontrado)
                {
                    MessageBox.Show(" No se encontró el ID " + productoid);
                }
                tuberiaLectura.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la búsqueda" + ex.Message);
            }
        }
        private bool ValidarNit(string nit)
        {
            bool respuesta = true;
            string[] datosSeparados;
            StreamReader tuberiaLectura = File.OpenText(pathName);
            string linea = tuberiaLectura.ReadLine();
            while (linea != null)
            {
                datosSeparados = linea.Split(',');
                if (nit == datosSeparados[0])
                {
                    respuesta = false;
                    break;
                }
                linea = tuberiaLectura.ReadLine();
            }
            tuberiaLectura.Close();
            return respuesta;
        }

        private void BtnRealizarVenta_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(pathName))
                {
                    string nit = txbNitCi.Text.Trim();
                    string razonSocial = txbNomRazonSocial.Text.Trim();
                    string fecha = DateTime.Now.ToString();
                    StreamReader tuberiaLectura = File.OpenText(pathNameP);
                    string linea;
                    int total = 0;
                    string[] datosProductoo;
                    char separador = ',';
                    linea = tuberiaLectura.ReadLine();
                    if (nit != "" && razonSocial != "" && fecha != "")
                    {
                        datosProductoo = linea.Split(separador);
                        if (ValidarNit(nit))
                        {
                            string datosV = datosProductoo[2];
                            total = Convert.ToInt32(datosV);
                            tuberiaLectura.Close();

                            StreamWriter tuberiaEscritura = File.AppendText(pathName);
                            tuberiaEscritura.WriteLine(nit + "," + razonSocial + "," + fecha + "," + total);
                            tuberiaEscritura.Close();
                            MessageBox.Show("La factura se grabó con exito");
                            txbNitCi.Text = "";
                            txbNomRazonSocial.Text = "";
                            txbIdProducto.Text = " ";
                            MostrarVentasDG();
                        }
                        else
                        {
                            MessageBox.Show("El id debe de ser unico");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se permite vacio");
                    }
                }
                else
                {
                    File.CreateText(pathName).Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error " + ex.Message);
            }


        }
        private void MostrarVentasDG()
        {
            try
            {
                Venta venta;
                List<Venta> ventas = new List<Venta>();
                string nit, razonSocial, fecha, total;
                string[] datosVenta;
                if (File.Exists(pathName))
                {
                    StreamReader tuberiaLectura = File.OpenText(pathName);
                    string linea = tuberiaLectura.ReadLine();
                    while (linea != null)
                    {
                        datosVenta = linea.Split(',');
                        nit = datosVenta[0];
                        razonSocial = datosVenta[1];
                        fecha = datosVenta[2];
                        total = datosVenta[3];

                        venta = new Venta(nit, razonSocial, fecha, total);
                        ventas.Add(venta);
                        linea = tuberiaLectura.ReadLine();
                    }
                    tuberiaLectura.Close();
                    dgMostrarVentas = ventas;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar las ventas " + ex.Message);
                Console.WriteLine(ex);
            }
        }

        private void MostrarFinalDG()
        {
            try
            {
                Producto producto;
                List<Producto> productos = new List<Producto>();
                string id, nombre, precioVenta, precioCompra, cantidad;
                string[] datosProducto;
                if (File.Exists(pathNameT))
                {
                    StreamReader tuberiaLectura = File.OpenText(pathNameT);
                    string linea = tuberiaLectura.ReadLine();
                    while (linea != null)
                    {
                        datosProducto = linea.Split(',');
                        id = datosProducto[0];
                        nombre = datosProducto[1];
                        precioVenta = datosProducto[2];
                        precioCompra = datosProducto[3];
                        cantidad = datosProducto[4];
                        producto = new Producto(id, nombre, precioVenta, precioCompra, cantidad);
                        productos.Add(producto);
                        linea = tuberiaLectura.ReadLine();
                    }
                    tuberiaLectura.Close();
                    dgVentas = productos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar los clientes " + ex.Message);
                Console.WriteLine(ex);
            }

        }
    }

    internal class Venta
    {
        private string nit;
        private string razonSocial;
        private string fecha;
        private string total;

        public Venta(string nit, string razonSocial, string fecha, string total)
        {
            this.nit = nit;
            this.razonSocial = razonSocial;
            this.fecha = fecha;
            this.total = total;
        }
    }
}
