using Microsoft.AspNetCore.Mvc;

namespace PuntoVentaWeb.Controllers
{
    public class CarritoController : Controller
    {
        public IActionResult Carrito()
        {
            return View();
        }
    }
}
