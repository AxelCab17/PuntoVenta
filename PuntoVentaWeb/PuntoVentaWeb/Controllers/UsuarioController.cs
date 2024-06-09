﻿using Microsoft.AspNetCore.Mvc;
using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PuntoVentaWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioModel _usuarioModel;

        public UsuarioController(IUsuarioModel usuarioModel)
        {
            _usuarioModel = usuarioModel;
        }

        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            ViewBag.Roles = GetRoles(); // Obtener los roles disponibles
            return View();
        }
        [HttpPost]
        public IActionResult RegistrarUsuario(UsuarioEnt entidad)
        {
            if (ModelState.IsValid)
            {
                var respuestaApi = _usuarioModel.RegistrarUsuario(entidad);
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
                var respuestaApi = _usuarioModel.LoginUsuario(entidad);
                if (respuestaApi?.Codigo == "1")
                    return RedirectToAction("Index", "Home");
                else
                {
                    ViewBag.MsjPantalla = "Credenciales incorrectas.";
                    return View();
                }
            }
            return View(entidad);
        }
    }
}
