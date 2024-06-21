using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using PuntoVentaAPI.Entities;
using System.Data;
using System.Data.SqlClient;
using static PuntoVentaAPI.Entities.EmpleadoEnt;
using static PuntoVentaAPI.Entities.ProveedorEnt;


namespace PuntoVentaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmpleadoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [Route("RegistrarEmpleado")]
        [HttpPost]
        public IActionResult RegistrarEmpleado(EmpleadoEnt empleado)
        {
            EmpleadoRespuesta empleadoRespuesta = new EmpleadoRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var parametros = new
                    {
                        empleado.Nombre,
                        empleado.Apellido,
                        empleado.FechaIngreso ,
                        empleado.Salario,
                        empleado.DiasTrabajados,
                    };

                    var result = db.Execute("RegistrarEmpleado", parametros, commandType: CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        empleadoRespuesta.Codigo = "1";
                        empleadoRespuesta.Mensaje = "Empleado registrado con éxito.";
                        return Ok(empleadoRespuesta);
                    }
                    else
                    {
                        empleadoRespuesta.Codigo = "-1";
                        empleadoRespuesta.Mensaje = "No se pudo registrar el empleado.";
                        return BadRequest(empleadoRespuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al registrar el empleado.", error = ex.Message });
            }
        }

        [AllowAnonymous]
        [Route("ConsultarEmpleados")]
        [HttpGet]

        public IActionResult ConsultarEmpleados()
        {
            EmpleadoRespuesta empleadoRespuesta = new EmpleadoRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var resultadoBD = db.Query<EmpleadoEnt>("ConsultarEmpleados", new { }, commandType: CommandType.StoredProcedure).ToList();

                    if (resultadoBD == null || resultadoBD.Count == 0)
                    {
                        empleadoRespuesta.Codigo = "-1";
                        empleadoRespuesta.Mensaje = "No hay empleados registrados.";
                    }
                    else
                    {
                        empleadoRespuesta.Datos = resultadoBD;
                        empleadoRespuesta.Codigo = "1";
                        empleadoRespuesta.Mensaje = "Empleados consultados con éxito.";
                    }
                    return Ok(empleadoRespuesta);
                }
            }
            catch (SqlException ex)
            {

                return StatusCode(500, new { message = "Error al consultar empleados en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al consultar empleados.", error = ex.Message });
            }
        }

        [AllowAnonymous]
        [Route("ObtenerEmpleadoPorID")]
        [HttpGet]

        public IActionResult ObtenerEmpleadoPorID(long IdEmpleado)

        {
            EmpleadoRespuesta empleadoRespuesta = new EmpleadoRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Query<EmpleadoEnt>("ObtenerEmpleadoPorID",
                        new { IdEmpleado },
                        commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (result == null)
                    {
                        empleadoRespuesta.Codigo = "-1";
                        empleadoRespuesta.Mensaje = "No hay empleados registrados.";
                    }
                    else
                    {
                        empleadoRespuesta.Dato = result;
                        empleadoRespuesta.Codigo = "1";
                        empleadoRespuesta.Mensaje = "Empleado consultado con éxito.";
                    }

                    return Ok(empleadoRespuesta);
                }
            }
            catch (SqlException ex)
            {

                return StatusCode(500, new { message = "Error al consultar el empleado en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al consultar el empleado.", error = ex.Message });
            }
        }

        [AllowAnonymous]
        [Route("ActualizarEmpleado")]
        [HttpPut]

        public IActionResult ActualizarEmpleado(EmpleadoEnt empleado)
        {
            EmpleadoRespuesta empleadoRespuesta = new EmpleadoRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Execute("ActualizarEmpleado",
                        new
                        {
                            empleado.IdEmpleado,
                            empleado.Nombre,
                            empleado.Apellido,
                            empleado.FechaIngreso,
                            empleado.Salario,
                            empleado.DiasTrabajados,
                        },
                        commandType: CommandType.StoredProcedure);

                    if (result <= 0)
                    {
                        empleadoRespuesta.Codigo = "-1";
                        empleadoRespuesta.Mensaje = "No se ha podido actualizar en la base de datos, intenta de nuevo";
                        return BadRequest(empleadoRespuesta);
                    }
                    else
                    {
                        empleadoRespuesta.Codigo = "1";
                        empleadoRespuesta.Mensaje = "Empleado actualizado con éxito.";
                        return Ok(empleadoRespuesta);
                    }
                }
            }
            catch (SqlException ex)
            {

                return StatusCode(500, new { message = "Error al actualizar el empleado en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al actualizar el empleado.", error = ex.Message });
            }
        }

        [AllowAnonymous]
        [Route("EliminarEmpleado")]
        [HttpDelete]

        public IActionResult EliminarEmpleado (long IdEmpleado)
        {
            EmpleadoRespuesta empleadoRespuesta = new EmpleadoRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Execute("EliminarEmpleado",
                        new
                        {
                            IdEmpleado
                        },
                        commandType: CommandType.StoredProcedure);

                    if (result <= 0)
                    {
                        empleadoRespuesta.Codigo = "-1";
                        empleadoRespuesta.Mensaje = "No se ha podido eliminar el empleado en la base de datos, intenta de nuevo";
                        return BadRequest(empleadoRespuesta);
                    }
                    else
                    {
                        empleadoRespuesta.Codigo = "1";
                        empleadoRespuesta.Mensaje = "Empleado eliminado con éxito.";
                        return Ok(empleadoRespuesta);
                    }
                }
            }
            catch (SqlException ex)
            {

                return StatusCode(500, new { message = "Error al eliminar el empleado en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al eliminar el empleado.", error = ex.Message });
            }
        }
    }

    }


    

    
    

