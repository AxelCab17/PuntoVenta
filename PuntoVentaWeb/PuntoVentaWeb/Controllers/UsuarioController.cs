using Microsoft.AspNetCore.Mvc;
using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Models;
using PuntoVentaWeb.Services;

namespace PuntoVentaWeb.Controllers
{
    public class UsuarioController(IUsuarioModel _usuarioModel) : Controller
    {
        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuario(UsuarioEnt Ent)
        {
            if (ModelState.IsValid)
            {
                var respuestaApi = _usuarioModel.RegistrarUsuario(Ent);
                if (respuestaApi?.Codigo == "1")
                    return RedirectToAction("Login");
                else
                    return RedirectToAction("Error", "Home");
            }
            return View(Ent);
        }

        [HttpGet]
        public IActionResult LoginUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginUsuario(UsuarioEnt Ent)
        {
            if (ModelState.IsValid)
            {
                var respuestaApi = _usuarioModel.LoginUsuario(Ent);
                if (respuestaApi?.Codigo == "1")
                    return RedirectToAction("Index", "Home");
                else
                {
                    ViewBag.MsjPantalla = "Credenciales incorrectas.";
                    return View();
                }
            }
            return View(Ent);
        }
    }
}
