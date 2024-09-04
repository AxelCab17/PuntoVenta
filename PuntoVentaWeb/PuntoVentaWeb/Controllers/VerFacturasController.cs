using Microsoft.AspNetCore.Mvc;
using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Models;
using PuntoVentaWeb.Services;

namespace PuntoVentaWeb.Controllers
{
    public class VerFacturasController(IVerFacturasModel iVerFacturasModel ) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ConsultarFacturas(DateTime? fechaInicio, DateTime? fechaFin)
        {
            if (!fechaInicio.HasValue || !fechaFin.HasValue)
            {
                // Si no se proporcionan fechas, muestra un mensaje de error o puedes establecer fechas predeterminadas.
                ViewBag.MsjPantalla = "Por favor, seleccione un rango de fechas válido.";
                return View(new List<VerFacturasEnt>());
            }

            var respuestaModelo = iVerFacturasModel.ConsultarFacturas(fechaInicio.Value, fechaFin.Value);

            if (respuestaModelo?.Codigo == "1")
                return View(respuestaModelo?.Datos);
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje ?? "No se encontraron facturas en el rango de fechas especificado.";
                return View(new List<VerFacturasEnt>());
            }
        }

    }
}
