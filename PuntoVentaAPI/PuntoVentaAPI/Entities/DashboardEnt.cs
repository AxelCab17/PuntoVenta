﻿using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PuntoVentaAPI.Entities
{
    public class DashboardEnt
    {
        public int TotalProveedores { get; set; }
        public int TotalProductos { get; set; }
        public int TotalVentas { get; set; }
        public decimal IngresosTotales { get; set; }
        public int TotalCategorias { get; set; }

        public class DashboardRespuesta
        {
            public DashboardRespuesta()
            {
                Codigo = "1";
                Mensaje = string.Empty;
                Dato = null;
                Datos = null;
            }

            public string Codigo { get; set; }
            public string Mensaje { get; set; }
            public DashboardEnt? Dato { get; set; }
            public List<DashboardEnt>? Datos { get; set; }
        }
    }
}
