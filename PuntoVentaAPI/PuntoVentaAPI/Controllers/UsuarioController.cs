using Dapper;
using PuntoVentaAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using static PuntoVentaAPI.Entities.UsuarioEnt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Hosting;
using PuntoVentaAPI.Interfaces;

namespace PuntoVentaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(IConfiguration iConfiguration, IComunesModel iComunesModel, IHostEnvironment iHost) : ControllerBase
    {

        [AllowAnonymous]
        [Route("RegistrarUsuario")]
        [HttpPost]
        public IActionResult RegistrarUsuario(UsuarioEnt ent)
        {
            UsuarioRespuesta usuarioRespuesta = new UsuarioRespuesta();
            try
            {
                using (var db = new SqlConnection(iConfiguration.GetConnectionString("DefaultConnection")))
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

        [AllowAnonymous]
        [HttpPost]
        [Route("LoginUsuario")]
        public IActionResult LoginUsuario(UsuarioEnt ent)
        {
            UsuarioRespuesta usuarioRespuesta = new UsuarioRespuesta();
            try
            {
                using (var db = new SqlConnection(iConfiguration.GetConnectionString("DefaultConnection")))
                {
                    var parametros = new
                    {
                        ent.Correo,
                        ent.Contrasenna
                    };

                    var usuario = db.QuerySingleOrDefault<UsuarioEnt>("LoginUsuario", parametros, commandType: CommandType.StoredProcedure);

                    if (usuario != null)
                    {

                     usuario.Token = GenerarToken(usuario.IdUsuario, usuario.IdRol);

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

        [Authorize]
        [Route("ConsultarUsuarios")]
        [HttpGet]
        public IActionResult ConsultarUsuarios()

        {
            if (!EsAdministrador())
                return StatusCode(403);




            UsuarioRespuesta UsuarioRespuesta = new UsuarioRespuesta();
            try
            {
                using (var db = new SqlConnection(iConfiguration.GetConnectionString("DefaultConnection")))
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


        [Authorize]
        [Route("ConsultarUnUsuario")]
        [HttpGet]
        public IActionResult ConsultarUnUsuario(int IdUsuario)
        {
            UsuarioRespuesta UsuarioRespuesta = new UsuarioRespuesta();
            try
            {
                using (var db = new SqlConnection(iConfiguration.GetConnectionString("DefaultConnection")))
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

        [Authorize]
        [Route("ActualizarUsuario")]
        [HttpPut]
        public IActionResult ActualizarUsuario(UsuarioEnt usuario)
        {
            UsuarioRespuesta usuarioRespuesta = new UsuarioRespuesta();
            try
            {
                using (var db = new SqlConnection(iConfiguration.GetConnectionString("DefaultConnection")))
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
        [Authorize]
        [Route("EliminarUsuario")]
        [HttpDelete]
        public IActionResult EliminarUsuario(int IdUsuario)
        {
            UsuarioRespuesta UsuarioRespuesta = new UsuarioRespuesta();
            try
            {
                using (var db = new SqlConnection(iConfiguration.GetConnectionString("DefaultConnection")))
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
        private string GenerarToken(int IdUsuario, int IdRol)
        {
            string SecretKey = iConfiguration.GetSection("settings:SecretKey").Value!;
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, IdUsuario.ToString()));
            claims.Add(new Claim("IdRol", IdRol.ToString()));


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

		[HttpGet]
		[Route("RecuperarAcceso")]
		public async Task<IActionResult> RecuperarAcceso(string Identificacion)
		{
			UsuarioRespuesta resp = new UsuarioRespuesta();

			using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
			{
				var result = await context.QueryFirstOrDefaultAsync<UsuarioEnt>("ConsultarUsuarioIdentificacion",
					new { Identificacion },
					commandType: System.Data.CommandType.StoredProcedure);

				if (result != null)
				{
					var codigoAleatorio = iComunesModel.GenerarCodigo();
					var Contrasenna = iComunesModel.Encrypt(codigoAleatorio);
					var EsTemporal = true;
					var VigenciaTemporal = DateTime.Now.AddMinutes(30);

					await context.ExecuteAsync("ActualizarContrasenna",
					new { result.IdUsuario, Contrasenna, EsTemporal, VigenciaTemporal },
					commandType: System.Data.CommandType.StoredProcedure);

					string ruta = Path.Combine(iHost.ContentRootPath, "FormatoCorreo.html");
					var html = System.IO.File.ReadAllText(ruta);

					html = html.Replace("@@Nombre", result.Nombre);
					html = html.Replace("@@Contrasenna", codigoAleatorio);
					html = html.Replace("@@Vencimiento", VigenciaTemporal.ToString("dd/MM/yyyy HH:mm"));

					iComunesModel.EnviarCorreo(result.Correo!, "Recuperar Acceso Sistema", html);

					resp.Codigo = "1";
					resp.Mensaje = "OK";
					resp.Dato = result;
					return Ok(resp);
				}
				else
				{
					resp.Codigo = "0";
					resp.Mensaje = "No hay usuarios registrados con esa identificación.";
					resp.Dato = null;
					return Ok(resp);
				}
			}
		}



		private bool EsAdministrador()
        {
            var userrol = User.Claims.Select(Claim => new {Claim.Type, Claim.Value}).
                FirstOrDefault( x => x.Type == "IdRol")!.Value;


            return (userrol == "1" ? true : false);

        }

    }
}
