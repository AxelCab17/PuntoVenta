using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuntoVentaAPI.Entities;
using System.Data;
using System.Data.SqlClient;

namespace PuntoVentaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CategoriaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [Route("RegistrarCategoria")]
        [HttpPost]
        public IActionResult RegistrarCategoria(CategoriaEnt categoria)
        {
            var categoriaRespuesta = new CategoriaRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var parametros = new
                    {
                        categoria.Nombre
                    };

                    var result = db.Execute("RegistrarCategoria", parametros, commandType: CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        categoriaRespuesta.Codigo = "1";
                        categoriaRespuesta.Mensaje = "Categoría registrada con éxito.";
                        return Ok(categoriaRespuesta);
                    }
                    else
                    {
                        categoriaRespuesta.Codigo = "-1";
                        categoriaRespuesta.Mensaje = "No se pudo registrar la categoría.";
                        return BadRequest(categoriaRespuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al registrar la categoría.", error = ex.Message });
            }
        }

        [AllowAnonymous]
        [Route("ConsultarCategorias")]
        [HttpGet]
        public IActionResult ConsultarCategorias()
        {
            var categoriaRespuesta = new CategoriaRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var resultadoBD = db.Query<CategoriaEnt>("ConsultarCategorias", commandType: CommandType.StoredProcedure).ToList();

                    if (resultadoBD == null || resultadoBD.Count == 0)
                    {
                        categoriaRespuesta.Codigo = "-1";
                        categoriaRespuesta.Mensaje = "No hay categorías registradas.";
                    }
                    else
                    {
                        categoriaRespuesta.Datos = resultadoBD;
                        categoriaRespuesta.Codigo = "1";
                        categoriaRespuesta.Mensaje = "Categorías consultadas con éxito.";
                    }
                    return Ok(categoriaRespuesta);
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Error al consultar categorías en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado al consultar categorías.", error = ex.Message });
            }
        }

        [AllowAnonymous]
        [Route("ActualizarCategoria")]
        [HttpPut]
        public IActionResult ActualizarCategoria(CategoriaEnt categoria)
        {
            var categoriaRespuesta = new CategoriaRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Execute("ActualizarCategoria",
                        new
                        {
                            categoria.Id,
                            categoria.Nombre
                        },
                        commandType: CommandType.StoredProcedure);

                    if (result <= 0)
                    {
                        categoriaRespuesta.Codigo = "-1";
                        categoriaRespuesta.Mensaje = "No se ha podido actualizar la categoría en la base de datos, intenta de nuevo";
                        return BadRequest(categoriaRespuesta);
                    }
                    else
                    {
                        categoriaRespuesta.Codigo = "1";
                        categoriaRespuesta.Mensaje = "Categoría actualizada con éxito.";
                        return Ok(categoriaRespuesta);
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Error al actualizar la categoría en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado al actualizar la categoría.", error = ex.Message });
            }
        }

        [AllowAnonymous]
        [Route("EliminarCategoria")]
        [HttpDelete]
        public IActionResult EliminarCategoria(int id)
        {
            var categoriaRespuesta = new CategoriaRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Execute("EliminarCategoria",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);

                    if (result <= 0)
                    {
                        categoriaRespuesta.Codigo = "-1";
                        categoriaRespuesta.Mensaje = "No se ha podido eliminar la categoría en la base de datos, intenta de nuevo";
                        return BadRequest(categoriaRespuesta);
                    }
                    else
                    {
                        categoriaRespuesta.Codigo = "1";
                        categoriaRespuesta.Mensaje = "Categoría eliminada con éxito.";
                        return Ok(categoriaRespuesta);
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Error al eliminar la categoría en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado al eliminar la categoría.", error = ex.Message });
            }
        }
    }
}
