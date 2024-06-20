﻿namespace PuntoVentaAPI.Entities
{
    public class InventarioEnt
    {
        public long IdInventario { get; set; }
        public string IdProducto { get; set; }
        public string Cantidad { get; set; }
        public string IdProveedor { get; set; }


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
