using Microsoft.AspNetCore.Mvc;

public class ProductosController : Controller
{
    // Asegúrate de que el nombre del método coincida con el nombre de la vista
    public ActionResult Producto()
    {
        return View();
    }
}
