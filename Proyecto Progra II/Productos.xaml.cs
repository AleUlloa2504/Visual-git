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
using System.Windows.Shapes;

namespace Proyecto_Progra_II
{
    /// <summary>
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : Window

    {
        string pathName = @"./productos.txt";
        string pathNameAuxiliar = @"./auxiliar.txt";
        private string nombre;
        private string precioCompra;
        private string precioVenta;
        private string cantidad;
        private string codigoBarras;

        public Productos(string id)
        {
            InitializeComponent();
            MostrarProductos();
            MostrarProductosDG();
        }

        public Productos(string id, string nombre, string precioCompra, string precioVenta, string cantidad, string codigoBarras) : this(id)
        {
            this.nombre = nombre;
            this.precioCompra = precioCompra;
            this.precioVenta = precioVenta;
            this.cantidad = cantidad;
            this.codigoBarras = codigoBarras;
        }
        private void MostrarProductosDG()
        {
            try
            {
                if (File.Exists(pathName))
                {
                    Producto producto;
                    List<Producto> listaProductos = new List<Producto>();
                    string[] datosProducto;
                    string nombre, codigoBarras, precioVenta, precioCompra, cantidad, id;
                    StreamReader tuberiaLectura = File.OpenText(pathName);
                    string linea = tuberiaLectura.ReadLine();
                    while (linea != null)
                    {
                        datosProducto = linea.Split(',');
                        id = datosProducto[0];
                        nombre = datosProducto[1];
                        precioVenta = datosProducto[2];
                        precioCompra = datosProducto[3];
                        cantidad = datosProducto[4];
                        codigoBarras = datosProducto[5];
                        producto = new Producto(id, nombre, precioCompra, precioVenta, cantidad, codigoBarras);
                        listaProductos.Add(producto);
                        producto = null;
                        linea = tuberiaLectura.ReadLine();
                    }
                    tuberiaLectura.Close();

                    dgProductos.ItemsSource = listaProductos;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void MostrarProductos()
        {
            try
            {
                txbListaProductos.Text = "";
                if (File.Exists(pathName))
                {
                    string fila;
                    StreamReader tuberiaLectura = File.OpenText(pathName);
                    fila = tuberiaLectura.ReadLine();
                    while (fila != null)
                    {
                        txbListaProductos.AppendText(fila + "\r\n");
                        fila = tuberiaLectura.ReadLine();
                    }
                    tuberiaLectura.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void BtnGuardarProducto_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (File.Exists(pathName))
                {
                    string idProducto = txbIdProducto.Text.Trim();
                    string nombreProducto = txbNombreProducto.Text.Trim();
                    string precioVenta = txbPrecioVenta.Text.Trim();
                    string precioCompra = txbPrecioCompra.Text.Trim();
                    string cantidad = txbCantidadProducto.Text.Trim();
                    string codigoBarras = txbCodigoBarras.Text.Trim();
                    if (nombreProducto != "" && idProducto != "")
                    {
                        if (ValidarId(idProducto))
                        {
                            StreamWriter tuberiaEscritura = File.AppendText(pathName);
                            tuberiaEscritura.WriteLine(idProducto + "," + nombreProducto + "," + precioVenta + " Bs." + "," + precioCompra + " Bs." + "," + cantidad + "," + codigoBarras);
                            tuberiaEscritura.Close();
                            MessageBox.Show("Producto creado con éxito");
                            txbNombreProducto.Text = "";
                            MostrarProductos();
                        }
                        else
                        {
                            MessageBox.Show("Ya existe un producto con ese id, pruebe con otro");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se permite vacio");
                    }
                }
                else
                {
                    CrearArchivoProducto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }
        private bool ValidarId(string idProducto)
        {
            bool respuesta = true;
            StreamReader tuberiaLectura = File.OpenText(pathName);
            string[] datos;
            string linea = tuberiaLectura.ReadLine();
            while (linea != null)
            {
                datos = linea.Split(',');
                if (datos[0] == idProducto)
                {
                    //ya hay un id con ese valor
                    respuesta = false;
                    break;
                }
                linea = tuberiaLectura.ReadLine();
            }
            tuberiaLectura.Close();
            return respuesta;
        }
        private void CrearArchivoProducto()
        {
            File.CreateText(pathName).Dispose();
        }

        private void BtnEliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string idProductoEliminar = txbNombreProductoEliminar.Text;
                string linea;
                string[] datosProducto;
                char separador = ',';
                bool eliminado = false;
                StreamReader tuberiaLectura = File.OpenText(pathName);
                StreamWriter tuberiaEscritura = File.AppendText(pathNameAuxiliar);
                linea = tuberiaLectura.ReadLine();
                while (linea != null)
                {
                    datosProducto = linea.Split(separador);
                    if (idProductoEliminar == datosProducto[0])
                    {
                        eliminado = true;
                    }
                    else
                    {
                        tuberiaEscritura.WriteLine(linea);
                    }
                    linea = tuberiaLectura.ReadLine();
                }
                if (eliminado)
                {
                    MessageBox.Show("El producto se eliminó con éxito");
                }
                else
                {
                    MessageBox.Show("Producto no encontrado, no se pudo eliminar");
                }
                tuberiaEscritura.Close();
                tuberiaLectura.Close();
                //Ahora debemos copiar todo el contenido del txt auxiliar al txt original
                // File.Delete(pathName)  borra
                // File.Move(pathName)  reemplaza el contenido
                File.Delete(pathName);
                File.Move(pathNameAuxiliar, pathName);
                File.Delete(pathNameAuxiliar);
                MostrarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar " + ex.Message);
            }
        }

        private void BtnModificarProductos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string idProductoAModificar = txbIdProducto.Text;
                string nombreProductoAModificar = txbNombreProducto.Text;
                string codigoProductoAModificar = txbCodigoBarras.Text;
                string precioVentaAModificar = txbPrecioVenta.Text;
                string precioCompraAModificar = txbPrecioCompra.Text;
                string cantidadProductoAModificar = txbCantidadProducto.Text;
                string linea;
                string[] datosProducto;
                char separador = ',';
                bool modificado = false;
                StreamReader tuberiaLectura = File.OpenText(pathName);
                StreamWriter tuberiaEscritura = File.AppendText(pathNameAuxiliar);
                linea = tuberiaLectura.ReadLine();
                while (linea != null)
                {
                    datosProducto = linea.Split(separador);
                    if (idProductoAModificar == datosProducto[0])
                    {
                        //esta es la linea que contiene la mascota que se quiere eliminar
                        modificado = true;
                        //Entonces debemos de enviar los nuevos datos y el id en el formato correcto
                        tuberiaEscritura.WriteLine(idProductoAModificar + "," + nombreProductoAModificar + "," + codigoProductoAModificar + "," + precioVentaAModificar + "," + precioCompraAModificar + "," + cantidadProductoAModificar);
                    }
                    else
                    {
                        //esta mascota no es la que se quiere eliminar asi que la vamos a copiar al txt aux
                        tuberiaEscritura.WriteLine(linea);
                    }
                    linea = tuberiaLectura.ReadLine();
                }
                if (modificado)
                {
                    MessageBox.Show("El producto se modificó con éxito");
                }
                else
                {
                    MessageBox.Show("Producto no encontrado");
                }
                tuberiaEscritura.Close();
                tuberiaLectura.Close();
                //Ahora debemos copiar todo el contenido del txt auxiliar al txt original
                // File.Delete(pathName)  borra
                // File.Move(pathName)  reemplaza el contenido
                File.Delete(pathName);
                File.Move(pathNameAuxiliar, pathName);
                File.Delete(pathNameAuxiliar);
                MostrarProductos();
                txbIdProducto.Text = "";
                txbNombreProducto.Text = "";
                txbCodigoBarras.Text = "";
                txbPrecioVenta.Text = "";
                txbPrecioCompra.Text = "";
                txbCantidadProducto.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el producto " + ex.Message);
            }
        }
        private void  DgProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Producto producto = new Producto();
            producto = (Producto)dgProductos.SelectedItem;
            txbIdProducto.Text = producto.Id;
        }
    }
    
}
