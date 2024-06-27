using Microsoft.AspNetCore.Mvc;
using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Services;

namespace PuntoVentaWeb.Controllers
{
    public class InventarioController(IInventarioModel _InventarioModel) : Controller
    {
        //Abre la vista:
        [HttpGet]
        public IActionResult RegistrarInventario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarInventario(InventarioEnt entidad)
        {
            var RespuestaApi = _InventarioModel.RegistrarInventario(entidad);

            if (RespuestaApi?.Codigo == "1")
                return RedirectToAction("ConsultarInventario", "Inventario");
            else
            {
                return RedirectToAction("ConsultarInventario", "Inventario");
            }
        }

        [HttpGet]
        public IActionResult ConsultarInventario()
        {
            var respuestaModelo = _InventarioModel.ConsultarInventario();

            if (respuestaModelo?.Codigo == "1")
                return View(respuestaModelo?.Datos);
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View(new List<InventarioEnt>());
            }
        }


        [HttpPost]
        public IActionResult ActualizarInventario(InventarioEnt entidad)
        {
            var respuestaModelo = _InventarioModel.ActualizarInventario(entidad);

            if (respuestaModelo?.Codigo == "1")
                return RedirectToAction("ConsultarInventario", "Inventario");
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View();
            }
        }

        [HttpPost]
        public IActionResult EliminarInventario(InventarioEnt entidad)
        {
            var respuestaModelo = _InventarioModel.EliminarInventario(entidad.IdInventario);

            if (respuestaModelo?.Codigo == "1")
                return RedirectToAction("ConsultarInventario", "Inventario");
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View();
            }
        }

    }
}
