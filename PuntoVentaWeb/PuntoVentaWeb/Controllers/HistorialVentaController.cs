using Microsoft.AspNetCore.Mvc;

namespace PuntoVentaWeb.Controllers
{
    public class HistorialVentaController : Controller
    {
        public IActionResult HistorialVenta()
        {
            return View();
        }
    }
}
