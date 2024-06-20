using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuntoVentaAPI.Entities;
using System.Data;
using System.Data.SqlClient;
using static PuntoVentaAPI.Entities.InventarioEnt;

namespace PuntoVentaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public InventarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [Route("RegistrarInventario")]
        [HttpPost]
        public IActionResult RegistrarInventario(InventarioEnt Inventario)
        {

            InventarioRespuesta InventarioRespuesta = new InventarioRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var parametros = new
                    {
                        Inventario.IdInventario,
                        Inventario.IdProducto,
                        Inventario.Cantidad,
                        Inventario.IdProveedor
                    };

                    var result = db.Execute("RegistrarInventario", parametros, commandType: CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        InventarioRespuesta.Codigo = "1";
                        InventarioRespuesta.Mensaje = "Producto registrado con éxito.";
                        return Ok(InventarioRespuesta);
                    }
                    else
                    {
                        InventarioRespuesta.Codigo = "-1";
                        InventarioRespuesta.Mensaje = "No se pudo registrar el producto en Inventario.";
                        return BadRequest(InventarioRespuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al registrar el producto en Inventario.", error = ex.Message });
            }
        }
        [AllowAnonymous]
        [Route("ConsultarInventario")]
        [HttpGet]
        public IActionResult ConsultarInventario()
        {
            InventarioRespuesta InventarioRespuesta = new InventarioRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var resultadoBD = db.Query<InventarioEnt>("ConsultarInventario", new { }, commandType: CommandType.StoredProcedure).ToList();

                    if (resultadoBD == null || resultadoBD.Count == 0)
                    {
                        InventarioRespuesta.Codigo = "-1";
                        InventarioRespuesta.Mensaje = "No hay Inventario registrado.";
                    }
                    else
                    {
                        InventarioRespuesta.Datos = resultadoBD;
                        InventarioRespuesta.Codigo = "1";
                        InventarioRespuesta.Mensaje = "Inventario consultado con éxito.";
                    }
                    return Ok(InventarioRespuesta);
                }
            }
            catch (SqlException ex)
            {
               
                return StatusCode(500, new { message = "Error al consultar Inventario en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, new { message = "Ocurrió un error inesperado al consultar Inventario.", error = ex.Message });
            }
        }



        [AllowAnonymous]
        [Route("ActualizarInventario")]
        [HttpPut]
        public IActionResult ActualizarInventario(InventarioEnt Inventario)
        {
            InventarioRespuesta InventarioRespuesta = new InventarioRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Execute("ActualizarInventario",
                        new
                        {
                            Inventario.IdInventario,
                            Inventario.IdProducto,
                            Inventario.Cantidad,
                            Inventario.IdProveedor
                        },
                        commandType: CommandType.StoredProcedure);

                    if (result <= 0)
                    {
                        InventarioRespuesta.Codigo = "-1";
                        InventarioRespuesta.Mensaje = "No se ha podido actualizar en la base de datos, intenta de nuevo";
                        return BadRequest(InventarioRespuesta);
                    }
                    else
                    {
                        InventarioRespuesta.Codigo = "1";
                        InventarioRespuesta.Mensaje = "Inventario actualizado con éxito.";
                        return Ok(InventarioRespuesta);
                    }
                }
            }
            catch (SqlException ex)
            {
                
                return StatusCode(500, new { message = "Error al actualizar el Inventario en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { message = "Ocurrió un error inesperado al actualizar el Inventario.", error = ex.Message });
            }
        }

        [AllowAnonymous]
        [Route("EliminarInventario")]
        [HttpDelete]
        public IActionResult EliminarInventario(long IdInventario)
        {
            InventarioRespuesta InventarioRespuesta = new InventarioRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Execute("EliminarInventario",
                        new
                        {
                            IdInventario
                        },
                        commandType: CommandType.StoredProcedure);

                    if (result <= 0)
                    {
                        InventarioRespuesta.Codigo = "-1";
                        InventarioRespuesta.Mensaje = "No se ha podido eliminar el producto del Inventario en la base de datos, intenta de nuevo";
                        return BadRequest(InventarioRespuesta);
                    }
                    else
                    {
                        InventarioRespuesta.Codigo = "1";
                        InventarioRespuesta.Mensaje = "Producto eliminado con éxito.";
                        return Ok(InventarioRespuesta);
                    }
                }
            }
            catch (SqlException ex)
            {
         
                return StatusCode(500, new { message = "Error al eliminar el producto del Inventario en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, new { message = "Ocurrió un error inesperado al eliminar el Inventario.", error = ex.Message });
            }
        }

    }
}


