using Microsoft.AspNetCore.Mvc;

namespace PuntoVentaWeb.Controllers
{
    public class NuevaVentaController : Controller
    {
        public IActionResult NuevaVenta()
        {
            return View();
        }
    }
}
