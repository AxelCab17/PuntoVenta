using PuntoVentaWeb.Entities;
using System.Collections.Generic;

namespace PuntoVentaWeb.Services
{
    public interface ICarritoModel
    {
        Carrito GetCarrito();
        void AgregarProducto(int productoId, int cantidad);
        void EliminarProducto(int productoId);
    }
}
