
using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Services;

using static PuntoVentaWeb.Entities.FacturaEnt;

namespace PuntoVentaWeb.Models
{
    public class FacturaModel(HttpClient _http, IConfiguration _configuration) : IFacturaModel
    {

        public FacturaRespuesta? RegistrarFactura(FacturaEnt entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Factura/RegistrarFactura";

            JsonContent body = JsonContent.Create(entidad);
            var RespuestaApi = _http.PostAsync(url, body).Result;
            if (RespuestaApi.IsSuccessStatusCode)
                return RespuestaApi.Content.ReadFromJsonAsync<FacturaRespuesta>().Result;
            return null;
        }
    }

       
}