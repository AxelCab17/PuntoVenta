using PuntoVentaWeb.Entities;
using static PuntoVentaWeb.Entities.UsuarioEnt;

namespace PuntoVentaWeb.Services
{
    public interface IUsuarioModel
    {
        UsuarioRespuesta? RegistrarUsuario(UsuarioEnt entidad);
        UsuarioRespuesta? LoginUsuario(UsuarioEnt entidad);
        UsuarioRespuesta? ConsultarUsuarios();
        UsuarioRespuesta? ConsultarUnUsuario(string Identificacion);
        UsuarioRespuesta? ActualizarUsuario(UsuarioEnt entidad);

    }
}