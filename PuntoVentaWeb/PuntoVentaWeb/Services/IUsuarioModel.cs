using PuntoVentaWeb.Entities;
using static PuntoVentaWeb.Entities.UsuarioEnt;

namespace PuntoVentaWeb.Services
{
    public interface IUsuarioModel
    {
        UsuarioRespuesta? RegistrarUsuario(UsuarioEnt entidad);
        UsuarioRespuesta? LoginUsuario(UsuarioEnt entidad);
        UsuarioRespuesta? ConsultarUsuarios();
        UsuarioRespuesta? ConsultarUnUsuario(long IdUsuario);
        UsuarioRespuesta? ActualizarUsuario(UsuarioEnt entidad);
        UsuarioRespuesta? EliminarUsuario(long IdUsuario);

    }
}


