using Microsoft.AspNetCore.Mvc;
using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Services;

namespace PuntoVentaWeb.Controllers
{
    public class ProveedorController(IProveedorModel _proveedorModel) : Controller
    {
        //Abre la vista:
        [HttpGet]
        public IActionResult RegistrarProveedor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarProveedor(ProveedorEnt entidad)
        {
            var RespuestaApi = _proveedorModel.RegistrarProveedor(entidad);

            if (RespuestaApi?.Codigo == "1")
                return RedirectToAction("RegistrarProveedor", "Proveedor");
            else
            {
                return RedirectToAction("ERROR", "Producto");
            }
        }

        [HttpGet]
        public IActionResult ConsultarProveedores()
        {
            var respuestaModelo = _proveedorModel.ConsultarProveedores();

            if (respuestaModelo?.Codigo == "1")
                return View(respuestaModelo?.Datos);
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View(new List<ProveedorEnt>());
            }
        }

        [HttpGet]
        public IActionResult ActualizarProveedor(long IdProveedor)
        {
            var respuestaModelo = _proveedorModel.ConsultarUnProveedor(IdProveedor);
            return View(respuestaModelo?.Dato);
        }

        [HttpPost]
        public IActionResult ActualizarProveedor(ProveedorEnt entidad)
        {
            var respuestaModelo = _proveedorModel.ActualizarProveedor(entidad);

            if (respuestaModelo?.Codigo == "1")
                return RedirectToAction("ConsultarProveedores", "Proveedor");
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View();
            }
        }

        [HttpPost]
        public IActionResult EliminarProveedor(ProveedorEnt entidad)
        {
            var respuestaModelo = _proveedorModel.EliminarProveedor(entidad.IdProveedor);

            if (respuestaModelo?.Codigo == "1")
                return RedirectToAction("ConsultarProveedores", "Proveedor");
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View();
            }
        }

    }
}
