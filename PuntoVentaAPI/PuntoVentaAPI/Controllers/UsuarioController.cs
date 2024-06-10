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

        [AllowAnonymous]
        [Route("ConsultarUsuarios")]
        [HttpGet]
        public IActionResult ConsultarUsuarios()
        {
            UsuarioRespuesta UsuarioRespuesta = new UsuarioRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var resultadoBD = db.Query<UsuarioEnt>("ConsultarUsuarios", new { }, commandType: CommandType.StoredProcedure).ToList();

                    if (resultadoBD == null || resultadoBD.Count == 0)
                    {
                        UsuarioRespuesta.Codigo = "-1";
                        UsuarioRespuesta.Mensaje = "No hay Usuarios registrados.";
                    }
                    else
                    {
                        UsuarioRespuesta.Datos = resultadoBD;
                        UsuarioRespuesta.Codigo = "1";
                        UsuarioRespuesta.Mensaje = "Usuarios consultados con éxito.";
                    }
                    return Ok(UsuarioRespuesta);
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Error al consultar Usuarios en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado al consultar Usuarios.", error = ex.Message });
            }
        }


        [AllowAnonymous]
        [Route("ConsultarUnUsuario")]
        [HttpGet]
        public IActionResult ConsultarUnUsuario(long IdUsuario)
        {
            UsuarioRespuesta UsuarioRespuesta = new UsuarioRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Query<UsuarioEnt>("ObtenerUsuarioPorID",
                        new { IdUsuario },
                        commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (result == null)
                    {
                        UsuarioRespuesta.Codigo = "-1";
                        UsuarioRespuesta.Mensaje = "No hay Usuarios registrados.";
                    }
                    else
                    {
                        UsuarioRespuesta.Dato = result;
                        UsuarioRespuesta.Codigo = "1";
                        UsuarioRespuesta.Mensaje = "Usuario consultado con éxito.";
                    }

                    return Ok(UsuarioRespuesta);
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Error al consultar el Usuario en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado al consultar el Usuario.", error = ex.Message });
            }
        }

        [AllowAnonymous]
        [Route("ActualizarUsuario")]
        [HttpPut]
        public IActionResult ActualizarUsuario(UsuarioEnt usuario)
        {
            UsuarioRespuesta usuarioRespuesta = new UsuarioRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Execute("ActualizarUsuario",
                        new
                        {
                            usuario.IdUsuario,
                            usuario.Identificacion,
                            usuario.Nombre,
                            usuario.Correo,
                            usuario.Contrasenna,
                            usuario.Estado,
                            usuario.IdRol
                        },
                        commandType: CommandType.StoredProcedure);

                    if (result <= 0)
                    {
                        usuarioRespuesta.Codigo = "-1";
                        usuarioRespuesta.Mensaje = "No se ha podido actualizar en la base de datos, intenta de nuevo";
                        return BadRequest(usuarioRespuesta);
                    }
                    else
                    {
                        usuarioRespuesta.Codigo = "1";
                        usuarioRespuesta.Mensaje = "Usuario actualizado con éxito.";
                        return Ok(usuarioRespuesta);
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el Usuario en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado al actualizar el Usuario.", error = ex.Message });
            }
        }
        [AllowAnonymous]
        [Route("EliminarUsuario")]
        [HttpDelete]
        public IActionResult EliminarUsuario(long IdUsuario)
        {
            UsuarioRespuesta UsuarioRespuesta = new UsuarioRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Execute("EliminarUsuario",
                        new
                        {
                            IdUsuario
                        },
                        commandType: CommandType.StoredProcedure);

                    if (result <= 0)
                    {
                        UsuarioRespuesta.Codigo = "-1";
                        UsuarioRespuesta.Mensaje = "No se ha podido eliminar el Usuario en la base de datos, intenta de nuevo";
                        return BadRequest(UsuarioRespuesta);
                    }
                    else
                    {
                        UsuarioRespuesta.Codigo = "1";
                        UsuarioRespuesta.Mensaje = "Usuario eliminado con éxito.";
                        return Ok(UsuarioRespuesta);
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el Usuario en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado al eliminar el Usuario.", error = ex.Message });
            }
        }

    }
}
