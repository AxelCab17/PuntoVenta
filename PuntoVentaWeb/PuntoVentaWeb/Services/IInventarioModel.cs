using PuntoVentaWeb.Entities;
using static PuntoVentaWeb.Entities.InventarioEnt;

namespace PuntoVentaWeb.Services
{
    public interface IInventarioModel
    {
        InventarioRespuesta? ConsultarInventario();

        InventarioRespuesta? RegistrarInventario(InventarioEnt entidad);
        InventarioRespuesta? ActualizarInventario(InventarioEnt entidad);

        InventarioRespuesta? EliminarInventario(long IdInventario);
    }
}
