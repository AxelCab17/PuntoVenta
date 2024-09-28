using Microsoft.AspNetCore.Mvc;
using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Services;

namespace PuntoVentaWeb.Controllers
{
    public class EmpleadoController(IEmpleadoModel _EmpleadoModel) : Controller
    {
        //Abre la vista:
        [HttpGet]
        public IActionResult RegistrarEmpleado()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarEmpleado(EmpleadoEnt entidad)
        {
            var RespuestaApi = _EmpleadoModel.RegistrarEmpleado(entidad);

            if (RespuestaApi?.Codigo == "1")
                return RedirectToAction("ConsultarEmpleados", "Empleado");
            else
            {
                ViewData["Mensaje"] = RespuestaApi?.Mensaje ?? "Error al registrar el empleado";
                return View(entidad);
            }
        }

        [HttpGet]
        public IActionResult ConsultarEmpleados()
        {
            var respuestaModelo = _EmpleadoModel.ConsultarEmpleados();

            if (respuestaModelo?.Codigo == "1")
                return View(respuestaModelo?.Datos);
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View(new List<EmpleadoEnt>());
            }
        }

        [HttpGet]
        public IActionResult ActualizarEmpleado(int Cedula)
        {
            var respuestaModelo = _EmpleadoModel.ObtenerEmpleadoPorId(Cedula);
            return View(respuestaModelo?.Dato);
        }

        [HttpPost]
        public IActionResult ActualizarEmpleado(EmpleadoEnt entidad)
        {
            var respuestaModelo = _EmpleadoModel.ActualizarEmpleado(entidad);

            if (respuestaModelo?.Codigo == "1")
                return RedirectToAction("ConsultarEmpleados", "Empleado");
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View();
            }
        }

        [HttpPost]
        public IActionResult EliminarEmpleado(EmpleadoEnt entidad)
        {
            var respuestaModelo = _EmpleadoModel.EliminarEmpleado(entidad.Cedula);

            if (respuestaModelo?.Codigo == "1")
                return RedirectToAction("ConsultarEmpleados", "Empleado");
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View();
            }
        }

    }
}
