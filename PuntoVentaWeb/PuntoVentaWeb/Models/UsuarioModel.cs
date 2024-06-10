using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Services;
using static PuntoVentaWeb.Entities.UsuarioEnt;

namespace PuntoVentaWeb.Models
{
    public class UsuarioModel : IUsuarioModel
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _configuration;

        public UsuarioModel(HttpClient http, IConfiguration configuration)
        {
            _http = http;
            _configuration = configuration;
        }

        public UsuarioRespuesta? RegistrarUsuario(UsuarioEnt entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Usuario/RegistrarUsuario";

            JsonContent body = JsonContent.Create(entidad);
            var respuestaApi = _http.PostAsync(url, body).Result;
            if (respuestaApi.IsSuccessStatusCode)
                return respuestaApi.Content.ReadFromJsonAsync<UsuarioRespuesta>().Result;
            return null;
        }

        public UsuarioRespuesta? LoginUsuario(UsuarioEnt entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Usuario/LoginUsuario";

            JsonContent body = JsonContent.Create(entidad);
            var respuestaApi = _http.PostAsync(url, body).Result;
            if (respuestaApi.IsSuccessStatusCode)
                return respuestaApi.Content.ReadFromJsonAsync<UsuarioRespuesta>().Result;
            return null;
        }
        public UsuarioRespuesta? ConsultarUsuarios()
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Usuario/ConsultarUsuarios";
            var respuestaApi = _http.GetAsync(url).Result;

            if (respuestaApi.IsSuccessStatusCode)
                return respuestaApi.Content.ReadFromJsonAsync<UsuarioRespuesta>().Result;

            return null;
        }

        public UsuarioRespuesta? ConsultarUnUsuario(long IdUsuario)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Usuario/ConsultarUnUsuario?IdUsuario=" + IdUsuario;
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<UsuarioRespuesta>().Result;

            return null;
        }
        public UsuarioRespuesta? ActualizarUsuario(UsuarioEnt entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Usuario/ActualizarUsuario";
            JsonContent body = JsonContent.Create(entidad);
            var respuestaApi = _http.PutAsync(url, body).Result;

            if (respuestaApi.IsSuccessStatusCode)
                return respuestaApi.Content.ReadFromJsonAsync<UsuarioRespuesta>().Result;

            return null;
        }
        public UsuarioRespuesta? EliminarUsuario(long IdUsuario)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Usuario/EliminarUsuario?IdUsuario=" + IdUsuario;
            var resp = _http.DeleteAsync(url).Result;
            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<UsuarioRespuesta>().Result;

            return null;
        }

    }
}
