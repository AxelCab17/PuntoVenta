using PuntoVentaWeb.Entities;
using static PuntoVentaWeb.Entities.EmpleadoEnt;


namespace PuntoVentaWeb.Services
{
    public interface IEmpleadoModel
    {
        EmpleadoRespuesta? ConsultarEmpleados();

        EmpleadoRespuesta? ObtenerEmpleadoPorId(long IdEmpleado);

        EmpleadoRespuesta? RegistrarEmpleado(EmpleadoEnt entidad);
        EmpleadoRespuesta? ActualizarEmpleado(EmpleadoEnt entidad);

        EmpleadoRespuesta? EliminarEmpleado(long IdEmpleado);
    }
}
