using Microsoft.AspNetCore.Mvc;
using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Models;
using PuntoVentaWeb.Services;
using System.Text.Json;

namespace PuntoVentaWeb.Controllers
{
    public class PerfilController(IUsuarioModel iUsuarioModel)  : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ActualizarPerfilUsuario()
        {
            var IdUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (IdUsuario == null || IdUsuario <= 0)
            {
                ViewBag.MsjPantalla = "El IdUsuario en la sesión no es válido.";
                return View(new UsuarioEnt());
            }

            var respuestaModelo = await iUsuarioModel.ConsultarUnUsuarioAsync(IdUsuario.Value);

            if (respuestaModelo == null)
            {
                Console.WriteLine("La API no devolvió una respuesta.");
                ViewBag.MsjPantalla = "La API no devolvió una respuesta válida.";
                return View(new UsuarioEnt());
            }

            if (respuestaModelo.Codigo == "1" && respuestaModelo.Dato != null)
            {
                Console.WriteLine($"Datos del usuario: {JsonSerializer.Serialize(respuestaModelo.Dato)}");
                return View(respuestaModelo.Dato);
            }
            else
            {
                Console.WriteLine("La API devolvió un código diferente a 1 o los datos son nulos.");
                ViewBag.MsjPantalla = respuestaModelo.Mensaje ?? "Error al consultar los datos del usuario.";
                return View(new UsuarioEnt());
            }
        }


        [HttpPost]
        public async Task<IActionResult> ActualizarPerfilUsuario(UsuarioEnt ent)
        {
            var resp = await iUsuarioModel.ActualizarUsuarioAsync(ent);

            if (resp.Codigo == "1")
            {
                HttpContext.Session.SetString("NOMBRE", ent!.Nombre!);
                return RedirectToAction("Home", "Home");
            }

            ViewBag.msj = resp.Mensaje;
            return View(ent);
        }

        }
}
