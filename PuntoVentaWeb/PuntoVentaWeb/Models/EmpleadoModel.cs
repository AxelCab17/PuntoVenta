using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Services;
using static PuntoVentaWeb.Entities.EmpleadoEnt;

namespace PuntoVentaWeb.Models
{
    public class EmpleadoModel(HttpClient _http, IConfiguration _configuration) : IEmpleadoModel
    {

        public EmpleadoRespuesta? RegistrarEmpleado(EmpleadoEnt entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Empleado/RegistrarEmpleado";

            JsonContent body = JsonContent.Create(entidad);
            var RespuestaApi = _http.PostAsync(url, body).Result;
            if (RespuestaApi.IsSuccessStatusCode)
                return RespuestaApi.Content.ReadFromJsonAsync<EmpleadoRespuesta>().Result;
            return null;
        }

        public EmpleadoRespuesta? ConsultarEmpleados()
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Empleado/ConsultarEmpleados";
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<EmpleadoRespuesta>().Result;

            return null;
        }

        public EmpleadoRespuesta? ObtenerEmpleadoPorId(long IdEmpleado)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Empleado/ObtenerEmpleadoPorId?IdEmpleado=" + IdEmpleado;
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<EmpleadoRespuesta>().Result;

            return null;
        }

        public EmpleadoRespuesta? ActualizarEmpleado(EmpleadoEnt entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Empleado/ActualizarEmpleado";
            JsonContent body = JsonContent.Create(entidad);
            var resp = _http.PutAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<EmpleadoRespuesta>().Result;

            return null;
        }


        public EmpleadoRespuesta? EliminarEmpleado(long IdEmpleado)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Empleado/EliminarEmpleado?IdEmpleado=" + IdEmpleado;
            var resp = _http.DeleteAsync(url).Result;
            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<EmpleadoRespuesta>().Result;

            return null;
        }
    }
}