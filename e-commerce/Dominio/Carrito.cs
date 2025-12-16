using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Carrito
    {
        public List<CarritoItem> Items { get; set; }

        public Carrito()
        {
            Items = new List<CarritoItem>();
        }

        // Agregar producto
        public void AgregarProducto(int productoId, string nombre, decimal precio, int cantidad = 1)
        {
            var item = Items.FirstOrDefault(x => x.ProductoId == productoId);
            if (item != null)
            {
                item.Cantidad += cantidad;
            }
            else
            {
                Items.Add(new CarritoItem
                {
                    ProductoId = productoId,
                    Nombre = nombre,
                    Precio = precio,
                    Cantidad = cantidad
                });
            }
        }

        // Quitar producto
        public void QuitarProducto(int productoId)
        {
            var item = Items.FirstOrDefault(x => x.ProductoId == productoId);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        // Limpiar carrito
        public void Limpiar()
        {
            Items.Clear();
        }

        // Total del carrito
        public decimal Total()
        {
            return Items.Sum(x => x.Subtotal);
        }
    }
}