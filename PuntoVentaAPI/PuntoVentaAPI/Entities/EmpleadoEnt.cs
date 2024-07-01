using PuntoVentaAPI.Entities;


namespace PuntoVentaAPI.Entities
{
    public class EmpleadoEnt
    {
        public long IdEmpleado { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime  FechaIngreso { get; set; }

        public int HorasTrabajadas { get; set; }

        public int HorasRebajadas { get; set; }

        public int ValorPorHora { get; set; }

        public int SalarioAjustado { get; set; }



        public class EmpleadoRespuesta
        {
            public EmpleadoRespuesta()
            {
                Codigo = "1";
                Mensaje = string.Empty;
                Dato = null;
                Datos = null;
            }

            public string Codigo { get; set; }
            public string Mensaje { get; set; }
            public EmpleadoEnt? Dato { get; set; }
            public List<EmpleadoEnt>? Datos { get; set; }
        }
    }
}

