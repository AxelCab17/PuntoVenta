using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuntoVentaAPI.Entities;
using System.Data;
using System.Data.SqlClient;
using static PuntoVentaAPI.Entities.ProveedorEnt;

namespace PuntoVentaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProveedorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [Route("RegistrarProveedor")]
        [HttpPost]
        public IActionResult RegistrarProveedor(ProveedorEnt proveedor)
        {

            ProveedorRespuesta proveedorRespuesta = new ProveedorRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var parametros = new
                    {
                        proveedor.Imagen,
                        proveedor.NumeroDocumento,
                        proveedor.RazonSocial,
                        proveedor.Correo,
                        proveedor.Direccion,
                        proveedor.Telefono,
                        proveedor.Impuesto
                    };

                    var result = db.Execute("RegistrarProveedor", parametros, commandType: CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        proveedorRespuesta.Codigo = "1";
                        proveedorRespuesta.Mensaje = "Proveedor registrado con éxito.";
                        return Ok(proveedorRespuesta);
                    }
                    else
                    {
                        proveedorRespuesta.Codigo = "-1";
                        proveedorRespuesta.Mensaje = "No se pudo registrar el proveedor.";
                        return BadRequest(proveedorRespuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al registrar el proveedor.", error = ex.Message });
            }
        }
        [AllowAnonymous]
        [Route("ConsultarProveedores")]
        [HttpGet]
        public IActionResult ConsultarProveedores()
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                ProveedorRespuesta proveedorRespuesta = new ProveedorRespuesta();

                var resultadoBD = db.Query<ProveedorEnt>("ConsultarProveedores", new { }, commandType: CommandType.StoredProcedure).ToList();

                if (resultadoBD == null || resultadoBD.Count == 0)
                {
                    proveedorRespuesta.Codigo = "-1";
                    proveedorRespuesta.Mensaje = "No hay servicios registrados.";
                }
                else
                {
                    proveedorRespuesta.Datos = resultadoBD;
                }
                return Ok(proveedorRespuesta);
            }
        }
    }
}

