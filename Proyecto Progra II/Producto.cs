using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_II
{
    class Producto
    {
        private string id;
        private string nombre;
        private string precioVenta;
        private string precioCompra;
        private string cantidad;
        private string codigoBarras;
        public Producto() { }
        public Producto(string i, string n, string pc, string pv, string c, string cb)
        {
            id = i;
            nombre = n;
            precioCompra = pc;
            precioVenta = pv;
            cantidad = c;
            codigoBarras = cb;
        }
        public string venta
        {
            get { return precioVenta; }
            set { precioVenta = value; }
        }
        public string Compra
        {
            get { return precioCompra; }
            set { precioCompra = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string CodigoBarras
        {
            get { return codigoBarras;}
            set { codigoBarras = value; }
        }
        public string Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

    }
}
