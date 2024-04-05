using Microsoft.AspNetCore.Mvc;

namespace PuntoVentaWeb.Controllers
{
    public class ReportesController : Controller
    {
        public IActionResult Reportes()
        {
            return View();
        }
    }
}
