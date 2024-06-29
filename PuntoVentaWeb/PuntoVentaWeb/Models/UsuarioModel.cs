using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Services;
using System.Net.Http.Headers;
using static PuntoVentaWeb.Entities.UsuarioEnt;

namespace PuntoVentaWeb.Models
{
    public class UsuarioModel(HttpClient httpClient, IConfiguration iConfiguration, IHttpContextAccessor iContextAccesor) : IUsuarioModel
    {


        public UsuarioRespuesta? RegistrarUsuario(UsuarioEnt entidad)
        {
            string url = iConfiguration.GetSection("settings:UrlApi").Value + "api/Usuario/RegistrarUsuario";

            JsonContent body = JsonContent.Create(entidad);
            var respuestaApi = httpClient.PostAsync(url, body).Result;
            if (respuestaApi.IsSuccessStatusCode)
                return respuestaApi.Content.ReadFromJsonAsync<UsuarioRespuesta>().Result;
            return null;
        }

        public UsuarioRespuesta? LoginUsuario(UsuarioEnt entidad)
        {
            string url = iConfiguration.GetSection("settings:UrlApi").Value + "api/Usuario/LoginUsuario";

            JsonContent body = JsonContent.Create(entidad);
            var respuestaApi = httpClient.PostAsync(url, body).Result;
            if (respuestaApi.IsSuccessStatusCode)
                return respuestaApi.Content.ReadFromJsonAsync<UsuarioRespuesta>().Result;
            return null;
        }
        public UsuarioRespuesta? ConsultarUsuarios()
        {
            string url = iConfiguration.GetSection("settings:UrlApi").Value + "api/Usuario/ConsultarUsuarios";
            string token = iContextAccesor.HttpContext!.Session.GetString("TOKEN")!.ToString();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var respuestaApi = httpClient.GetAsync(url).Result;

            if (respuestaApi.IsSuccessStatusCode)
                return respuestaApi.Content.ReadFromJsonAsync<UsuarioRespuesta>().Result;

            return null;
        }

        public UsuarioRespuesta? ConsultarUnUsuario(long IdUsuario)
        {
            string url = iConfiguration.GetSection("settings:UrlApi").Value + "api/Usuario/ConsultarUnUsuario?IdUsuario=" + IdUsuario;
            var resp = httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<UsuarioRespuesta>().Result;

            return null;
        }
        public UsuarioRespuesta? ActualizarUsuario(UsuarioEnt entidad)
        {
            string url = iConfiguration.GetSection("settings:UrlApi").Value + "api/Usuario/ActualizarUsuario";
            JsonContent body = JsonContent.Create(entidad);
            var respuestaApi = httpClient.PutAsync(url, body).Result;

            if (respuestaApi.IsSuccessStatusCode)
                return respuestaApi.Content.ReadFromJsonAsync<UsuarioRespuesta>().Result;

            return null;
        }
        public UsuarioRespuesta? EliminarUsuario(long IdUsuario)
        {
            string url = iConfiguration.GetSection("settings:UrlApi").Value + "api/Usuario/EliminarUsuario?IdUsuario=" + IdUsuario;
            var resp = httpClient.DeleteAsync(url).Result;
            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<UsuarioRespuesta>().Result;

            return null;
        }

    }
}
