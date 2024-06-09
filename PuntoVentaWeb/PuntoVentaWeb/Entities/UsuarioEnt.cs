using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PuntoVentaWeb.Entities
{
    public class UsuarioEnt
    {

        public string? Identificacion { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Contrasenna { get; set; }
        public int IdRol { get; set; }

        public class UsuarioRespuesta
        {
            public UsuarioRespuesta()
            {
                Codigo = "1";
                Mensaje = string.Empty;
                Dato = null;
                Datos = null;
            }


            public string Codigo { get; set; }
            public string Mensaje { get; set; }
            public UsuarioEnt? Dato { get; set; }
            public List<UsuarioEnt>? Datos { get; set; }

        }
    }

}