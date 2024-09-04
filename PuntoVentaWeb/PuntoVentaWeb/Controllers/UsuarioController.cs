using Microsoft.AspNetCore.Mvc;
using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using PuntoVentaWeb.Models;
using System.Threading.Tasks;
using static PuntoVentaWeb.Entities.UsuarioEnt;
using System.Text.Json;

namespace PuntoVentaWeb.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioModel _usuarioModel;
        private readonly IComunModel _comunModel;

        public UsuarioController(IUsuarioModel usuarioModel, IComunModel comunModel)
        {
            _usuarioModel = usuarioModel;
            _comunModel = comunModel;
        }

        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            ViewBag.Roles = GetRoles();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario(UsuarioEnt entidad)
        {
            entidad.Contrasenna = _comunModel.Encrypt(entidad.Contrasenna!);
            if (ModelState.IsValid)
            {
                var respuestaApi = await _usuarioModel.RegistrarUsuarioAsync(entidad);
                if (respuestaApi?.Codigo == "1")
                {
                    return RedirectToAction("LoginUsuario");
                }
                else
                {
                    string mensajeError = respuestaApi?.Mensaje ?? "Error desconocido.";
                    ModelState.AddModelError("", mensajeError);
                    ViewBag.Roles = GetRoles();
                    return View(entidad);
                }
            }
            ViewBag.Roles = GetRoles();
            return View(entidad);
        }

        private List<SelectListItem> GetRoles()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Admin" },
                new SelectListItem { Value = "2", Text = "User" }
            };
        }

        [HttpGet]
        public IActionResult LoginUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUsuario(UsuarioEnt entidad)
        {
            if (ModelState.IsValid)
            {
                entidad.Contrasenna = _comunModel.Encrypt(entidad.Contrasenna!);
                var respuestaApi = await _usuarioModel.LoginUsuarioAsync(entidad);

                if (respuestaApi?.Codigo == "1" && respuestaApi.Dato != null)
                {
                    var datos = respuestaApi.Dato;
                    if (datos != null)
                    {
                        HttpContext.Session.SetString("TOKEN", datos.Token!);
                        HttpContext.Session.SetString("NOMBRE", datos.Nombre!);

                        HttpContext.Session.SetString("CORREO", datos.Correo!);
                        HttpContext.Session.SetString("ESTADO", datos.Estado!);
                        HttpContext.Session.SetInt32("IdUsuario", datos.IdUsuario!);

                        HttpContext.Session.SetString("ROL", datos.IdRol.ToString());


                        return RedirectToAction("Obtenerdashboard", "Dashboard");
                    }
                }

                ViewBag.MsjPantalla = "Credenciales incorrectas.";
                return View(entidad);
            }
            return View(entidad);
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarUsuarios()
        {
            var respuestaApi = await _usuarioModel.ConsultarUsuariosAsync();
            if (respuestaApi?.Codigo == "1" && respuestaApi.Datos != null)
            {
                var datos = respuestaApi.Datos;
                return View(datos);
            }

            return View(new List<UsuarioEnt>());
        }
        [HttpGet]
        public async Task<IActionResult> ActualizarUsuario(int IdUsuario)
        {
            try
            {
                // Verificación del IdUsuario
                if (IdUsuario <= 0)
                {
                    ViewBag.MsjPantalla = "El IdUsuario proporcionado no es válido.";
                    return RedirectToAction("ConsultarUsuarios");
                }

                // Llamada a la API
                var respuestaModelo = await _usuarioModel.ConsultarUnUsuarioAsync(IdUsuario);

                // Verificación de que la respuesta no sea nula
                if (respuestaModelo == null)
                {
                    ViewBag.MsjPantalla = "La API no devolvió una respuesta válida.";
                    return RedirectToAction("ConsultarUsuarios");
                }

                if (respuestaModelo.Codigo == "1" && respuestaModelo.Dato != null)
                {
                    return View(respuestaModelo.Dato);
                }
                else
                {
                    ViewBag.MsjPantalla = respuestaModelo.Mensaje ?? "Error al consultar los datos del usuario.";
                    return RedirectToAction("ConsultarUsuarios");
                }
            }
            catch (Exception ex)
            {
                ViewBag.MsjPantalla = $"Ocurrió un error: {ex.Message}";
                return RedirectToAction("ConsultarUsuarios");
            }
        }



        [HttpPost]
        public async Task<IActionResult> ActualizarUsuario(UsuarioEnt entidad)
        {
            try
            {
                var respuestaModelo = await _usuarioModel.ActualizarUsuarioAsync(entidad);
                if (respuestaModelo?.Codigo == "1")
                {
                    return RedirectToAction("ConsultarUsuarios");
                }
                else
                {
                    ViewBag.MsjPantalla = respuestaModelo?.Mensaje ?? "Error al actualizar los datos del usuario.";
                    return View(entidad);
                }
            }
            catch (Exception ex)
            {
                ViewBag.MsjPantalla = $"Ocurrió un error: {ex.Message}";
                return View(entidad);
            }
        }






        [HttpPost]
        public async Task<IActionResult> EliminarUsuario(UsuarioEnt entidad)
        {
            var respuestaModelo = await _usuarioModel.EliminarUsuarioAsync(entidad.IdUsuario);
            if (respuestaModelo?.Codigo == "1")
                return RedirectToAction("ConsultarUsuarios");
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View();
            }
        }


        [HttpGet]
        public IActionResult Salir()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginUsuario", "Usuario");
        }

    }
}