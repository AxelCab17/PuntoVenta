namespace PuntoVentaWeb.Entities
{
    public class ProductoEnt
    {
        
            public string IdProducto { get; set; }
            public string Nombre { get; set; }
            public decimal Precio { get; set; }
            public int Stock { get; set; }

        public int IdCategoria { get; set;}
        }
}

