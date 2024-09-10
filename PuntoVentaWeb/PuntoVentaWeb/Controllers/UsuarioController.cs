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
                // Encriptar la contraseña introducida por el usuario
                entidad.Contrasenna = _comunModel.Encrypt(entidad.Contrasenna!);

                // Llamar a la API para realizar el login
                var respuestaApi = await _usuarioModel.LoginUsuarioAsync(entidad);

                // Comenzar depuración de la respuesta de la API
                Console.WriteLine("Respuesta de la API recibida");

                // Verificar si la respuesta fue exitosa (código "1") y si el dato del usuario no es nulo
                if (respuestaApi?.Codigo == "1" && respuestaApi.Dato != null)
                {
                    Console.WriteLine("Código de respuesta de la API: " + respuestaApi.Codigo);

                    var datos = respuestaApi.Dato;

                    // Verificar si los datos del usuario no son nulos
                    Console.WriteLine("Datos recibidos del usuario: " + JsonSerializer.Serialize(datos));

                    // Validar si la contraseña del usuario es temporal
                    if (datos.EsTemporal)
                    {
                        Console.WriteLine("Contraseña temporal detectada");

                        // Si la contraseña temporal ha caducado, mostrar un mensaje
                        if (datos.VigenciaTemporal < DateTime.Now)
                        {
                            Console.WriteLine("La contraseña temporal ha caducado.");
                            ViewBag.MsjPantalla = "Su contraseña temporal ha caducado.";
                            return View(entidad);
                        }
                        else
                        {
                            Console.WriteLine("La contraseña temporal sigue siendo válida.");
                        }
                    }

                    // Establecer las variables de sesión con los datos del usuario
                    Console.WriteLine("Estableciendo variables de sesión...");

                    HttpContext.Session.SetString("TOKEN", datos.Token!);
                    HttpContext.Session.SetString("NOMBRE", datos.Nombre!);
                    HttpContext.Session.SetString("CORREO", datos.Correo!);
                    HttpContext.Session.SetString("ESTADO", datos.Estado!);
                    HttpContext.Session.SetInt32("IdUsuario", datos.IdUsuario!);
                    HttpContext.Session.SetString("ROL", datos.IdRol.ToString());

                    Console.WriteLine("Variables de sesión establecidas con éxito.");

                    // Redirigir al dashboard si el login fue exitoso
                    return RedirectToAction("Obtenerdashboard", "Dashboard");
                }
                else
                {
                    // Si las credenciales son incorrectas o la respuesta no fue exitosa
                    Console.WriteLine("Código de respuesta erróneo o datos nulos. Código: " + (respuestaApi?.Codigo ?? "null"));

                    ViewBag.MsjPantalla = "Credenciales incorrectas.";
                    return View(entidad);
                }
            }

            // Si el modelo no es válido, devolver la vista con el modelo de entidad
            Console.WriteLine("El modelo no es válido.");
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


		[HttpGet]
		public IActionResult RecuperarAcceso()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> RecuperarAcceso(UsuarioEnt ent)
		{
			var respuesta = await _usuarioModel.RecuperarAccesoAsync(ent.Identificacion!);

			if (respuesta?.Codigo == "1")
			{
				return RedirectToAction("LoginUsuario", "Usuario");
			}
			else
			{
				ViewBag.ms = respuesta?.Mensaje;
				return View();
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