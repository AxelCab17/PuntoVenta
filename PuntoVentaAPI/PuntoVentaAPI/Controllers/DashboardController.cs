using Dapper;
using Microsoft.AspNetCore.Mvc;
using PuntoVentaAPI.Entities;
using System.Data;
using System.Data.SqlClient;

namespace PuntoVentaAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DashboardController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public DashboardController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[HttpGet]
		[Route("ObtenerDashboard")]
		public IActionResult ObtenerDashboard()
		{
			var dashboardRespuesta = new DashboardEnt.DashboardRespuesta();
			try
			{
				using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
				{
					var result = db.QuerySingleOrDefault<DashboardEnt>("ObtenerDatosDashboard", commandType: CommandType.StoredProcedure);

					if (result == null)
					{
						dashboardRespuesta.Codigo = "-1";
						dashboardRespuesta.Mensaje = "No se encontraron datos para el dashboard.";
						return NotFound(dashboardRespuesta);
					}

					dashboardRespuesta.Dato = result;
					dashboardRespuesta.Mensaje = "Datos del dashboard obtenidos con éxito.";
					return Ok(dashboardRespuesta);
				}
			}
			catch (SqlException ex)
			{
				return StatusCode(500, new { message = "Error al obtener los datos del dashboard en la base de datos.", error = ex.Message });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Ocurrió un error inesperado al obtener los datos del dashboard.", error = ex.Message });
			}
		}
	}
}
