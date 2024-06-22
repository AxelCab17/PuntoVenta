namespace PuntoVentaAPI.Entities
{
    public class CarritoItemEnt
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public ProductoEnt Producto { get; set; }
    }
}
