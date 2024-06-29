using Microsoft.AspNetCore.Mvc;
using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using PuntoVentaWeb.Models;
using System.Text.Json;
using static PuntoVentaWeb.Entities.UsuarioEnt;

namespace PuntoVentaWeb.Controllers
{

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class UsuarioController(IUsuarioModel usuarioModel, IComunModel comunModel) : Controller
    {
            [HttpGet]
            public IActionResult RegistrarUsuario()
            {
                ViewBag.Roles = GetRoles(); // Obtener los roles disponibles
                return View();
            }
            [HttpPost]
            public IActionResult RegistrarUsuario(UsuarioEnt entidad)
            {

            entidad.Contrasenna = comunModel.Encrypt(entidad.Contrasenna!);
                if (ModelState.IsValid)
                {
                    var respuestaApi = usuarioModel.RegistrarUsuario(entidad);
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
                // Aquí deberías obtener los roles desde tu base de datos
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
        public IActionResult LoginUsuario(UsuarioEnt entidad)
        {
            if (ModelState.IsValid)
            {
                entidad.Contrasenna = comunModel.Encrypt(entidad.Contrasenna!);
                var respuestaApi = usuarioModel.LoginUsuario(entidad);

                if (respuestaApi?.Codigo == "1" && respuestaApi.Dato != null)
                {
                    var datos = respuestaApi.Dato;
                    if (datos != null)
                    {
                        HttpContext.Session.SetString("TOKEN", datos.Token!);
                        HttpContext.Session.SetString("NOMBRE", datos.Nombre!);
                        return RedirectToAction("Index", "Home");
                    }
                }

                ViewBag.MsjPantalla = "Credenciales incorrectas.";
                return View(entidad);
            }
            return View(entidad);
        }



        [HttpGet]
            public IActionResult ConsultarUsuarios()
            {
                var respuestaModelo = usuarioModel.ConsultarUsuarios();
                if (respuestaModelo?.Codigo == "1")
                {
                    return View(respuestaModelo.Datos);
                }
                else
                {
                    ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                    return View(new List<UsuarioEnt>());
                }
            }

            [HttpGet]
            public IActionResult ActualizarUsuario(long IdUsuario)
            {
                var respuestaModelo = usuarioModel.ConsultarUnUsuario(IdUsuario);
                if (respuestaModelo?.Codigo == "1")
                {
                    return View(respuestaModelo.Dato);
                }
                else
                {
                    ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                    return RedirectToAction("ConsultarUsuarios");
                }
            }

            [HttpPost]
            public IActionResult ActualizarUsuario(UsuarioEnt entidad)
            {
                var respuestaModelo = usuarioModel.ActualizarUsuario(entidad);
                if (respuestaModelo?.Codigo == "1")
                {
                    return RedirectToAction("ConsultarUsuarios");
                }
                else
                {
                    ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                    return View(entidad);
                }
            }

            [HttpPost]
            public IActionResult EliminarUsuario(UsuarioEnt entidad)
            {
                var respuestaModelo = usuarioModel.EliminarUsuario(entidad.IdUsuario);

                if (respuestaModelo?.Codigo == "1")
                    return RedirectToAction("ConsultarUsuarios", "Usuario");
                else
                {
                    ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                    return View();
                }
            }


        }


    }

