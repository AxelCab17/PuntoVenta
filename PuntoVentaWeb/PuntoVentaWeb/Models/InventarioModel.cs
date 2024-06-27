using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Services;
using static PuntoVentaWeb.Entities.InventarioEnt;

namespace PuntoVentaWeb.Models
{
    public class InventarioModel(HttpClient _http, IConfiguration _configuration) : IInventarioModel
    {

        public InventarioRespuesta? RegistrarInventario(InventarioEnt entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Inventario/RegistrarInventario";

            JsonContent body = JsonContent.Create(entidad);
            var RespuestaApi = _http.PostAsync(url, body).Result;
            if (RespuestaApi.IsSuccessStatusCode)
                return RespuestaApi.Content.ReadFromJsonAsync<InventarioRespuesta>().Result;
            return null;
        }

        public InventarioRespuesta? ConsultarInventario()
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Inventario/ConsultarInventario";
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<InventarioRespuesta>().Result;

            return null;
        }


        public InventarioRespuesta? ActualizarInventario(InventarioEnt entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Inventario/ActualizarInventario";
            JsonContent body = JsonContent.Create(entidad);
            var resp = _http.PutAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<InventarioRespuesta>().Result;

            return null;
        }


        public InventarioRespuesta? EliminarInventario(long IdInventario)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Inventario/EliminarInventario?IdInventario=" + IdInventario;
            var resp = _http.DeleteAsync(url).Result;
            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<InventarioRespuesta>().Result;

            return null;
        }
    }
}