using PuntoVentaWeb.Entities;
using static PuntoVentaWeb.Entities.UsuarioEnt;

namespace PuntoVentaWeb.Services
{
    public interface IUsuarioModel
    {
        UsuarioRespuesta? RegistrarUsuario(UsuarioEnt entidad);
        UsuarioRespuesta? LoginUsuario(UsuarioEnt entidad);
    }
}