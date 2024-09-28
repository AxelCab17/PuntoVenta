 using Microsoft.AspNetCore.Mvc;
using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Services;

namespace PuntoVentaWeb.Controllers
{
    public class ProductoController(IProductoModel _ProductoModel) : Controller
    {
        //Abre la vista:
        [HttpGet]
        public IActionResult RegistrarProducto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarProducto(ProductoEnt entidad)
        {
            var RespuestaApi = _ProductoModel.RegistrarProducto(entidad);

            if (RespuestaApi?.Codigo == "1")
                return RedirectToAction("ConsultarProductos", "Producto");
            else
            {
                return RedirectToAction("ConsultarProductos", "Producto");
            }
        }

        [HttpGet]
        public IActionResult ConsultarProductos()
        {
            var respuestaModelo = _ProductoModel.ConsultarProductos();

            if (respuestaModelo?.Codigo == "1")
                return View(respuestaModelo?.Datos);
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View(new List<ProductoEnt>());
            }
        }

        [HttpGet]
        public IActionResult ActualizarProducto(string IdProducto)
        {
            var respuestaModelo = _ProductoModel.ConsultarUnProducto(IdProducto);
            return View(respuestaModelo?.Dato);
        }

        [HttpPost]
        public IActionResult ActualizarProducto(ProductoEnt entidad)
        {
            var respuestaModelo = _ProductoModel.ActualizarProducto(entidad);

            if (respuestaModelo?.Codigo == "1")
                return RedirectToAction("ConsultarProductos", "Producto");
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View();
            }
        }

        [HttpPost]
        public IActionResult EliminarProducto(ProductoEnt entidad)
        {
            var respuestaModelo = _ProductoModel.EliminarProducto(entidad.IdProducto);

            if (respuestaModelo?.Codigo == "1")
                return RedirectToAction("ConsultarProductos", "Producto");
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View();
            }
        }

    }
}
