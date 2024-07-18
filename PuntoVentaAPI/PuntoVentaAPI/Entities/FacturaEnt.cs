﻿using System.Numerics;

namespace PuntoVentaAPI.Entities
{
    public class FacturaEnt
    {
        
        public int IdDetalle { get; }
        public int IdFactura { get; }
        public int IdProducto { get; set; }
        public string NombreProducto { get;}
        public int Cantidad { get; set; }
        public decimal PrecioUnitatio { get;}
        public decimal Total { get; }
        public int Ultimo { get; set; }
        public int NuevaFactura { get; set; }
        public int IdCajero { get; set; }
        public int Descuento { get; set; }

    }

    public class FacturaRespuesta
    {
        public FacturaRespuesta()
        {
            Codigo = "1";
            Mensaje = string.Empty;
            Dato = null;
            Datos = null;
        }

        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public FacturaEnt? Dato { get; set; }
        public List<FacturaEnt>? Datos { get; set; }
    }
}

