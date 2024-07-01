using Microsoft.AspNetCore.Mvc;
using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Models;
using PuntoVentaWeb.Services;

namespace PuntoVentaWeb.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ICarritoModel _carritoModel;

        public CarritoController(ICarritoModel carritoModel)
        {
            _carritoModel = carritoModel;
        }

        public IActionResult Carrito()
        {
            var carrito = _carritoModel.GetCarrito();
            return View(carrito);
        }

        [HttpGet]
        public IActionResult AgregarProducto(int productoId, int cantidad)
        {
            try
            {
                _carritoModel.AgregarProducto(productoId, cantidad);
            }
            catch (System.Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return RedirectToAction("Carrito");
        }

        [HttpPost]
        public IActionResult EliminarProducto(int productoId)
        {
            try
            {
                _carritoModel.EliminarProducto(productoId);
            }
            catch (System.Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return RedirectToAction("Carrito");
        }
    }
}
