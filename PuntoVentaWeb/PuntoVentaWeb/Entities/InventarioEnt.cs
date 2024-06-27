
namespace PuntoVentaWeb.Entities
{
    public class InventarioEnt
    {
        public long IdInventario { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public int IdProveedor { get; set; }
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }
        public string NombreProducto { get; set; }

        public class InventarioRespuesta
        {
            public InventarioRespuesta()
            {
                Codigo = "1";
                Mensaje = string.Empty;
                Dato = null;
                Datos = null;
            }

            public string Codigo { get; set; }
            public string Mensaje { get; set; }
            public InventarioEnt? Dato { get; set; }
            public List<InventarioEnt>? Datos { get; set; }
        }
    }
}
