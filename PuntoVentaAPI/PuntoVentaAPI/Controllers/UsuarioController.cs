using Dapper;
using PuntoVentaAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using static PuntoVentaAPI.Entities.UsuarioEnt;

namespace PuntoVentaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [Route("RegistrarUsuario")]
        [HttpPost]
        public IActionResult RegistrarUsuario(UsuarioEnt ent)
        {
            UsuarioRespuesta usuarioRespuesta = new UsuarioRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var parametros = new
                    {
                        ent.Identificacion,
                        ent.Nombre,
                        ent.Correo,
                        ent.Contrasenna,
                        ent.IdRol // Asegúrate de pasar IdRol
                    };

                    var result = db.Execute("RegistrarUsuario", parametros, commandType: CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        usuarioRespuesta.Codigo = "1";
                        usuarioRespuesta.Mensaje = "Usuario registrado con éxito.";
                        return Ok(usuarioRespuesta);
                    }
                    else
                    {
                        usuarioRespuesta.Codigo = "-1";
                        usuarioRespuesta.Mensaje = "No se pudo registrar el usuario.";
                        return BadRequest(usuarioRespuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al registrar el usuario.", error = ex.Message });
            }
        }

        [HttpPost]
        [Route("LoginUsuario")]
        public IActionResult LoginUsuario(UsuarioEnt ent)
        {
            UsuarioRespuesta usuarioRespuesta = new UsuarioRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var parametros = new
                    {
                        ent.Correo,
                        ent.Contrasenna
                    };

                    var usuario = db.QuerySingleOrDefault<UsuarioEnt>("LoginUsuario", parametros, commandType: CommandType.StoredProcedure);

                    if (usuario != null)
                    {
                        usuarioRespuesta.Codigo = "1";
                        usuarioRespuesta.Mensaje = "OK";
                        usuarioRespuesta.Dato = usuario;
                        return Ok(usuarioRespuesta);
                    }
                    else
                    {
                        usuarioRespuesta.Codigo = "-1";
                        usuarioRespuesta.Mensaje = "La información del usuario no se encuentra registrada.";
                        return Ok(usuarioRespuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al intentar iniciar sesión.", error = ex.Message });
            }
        }
    }
}
