using PuntoVentaWeb.Entities;
using static PuntoVentaWeb.Entities.NominaEnt;

namespace PuntoVentaWeb.Services
{
    public interface INominaModel
    {

        NominaRespuesta? CrearNomina(NominaEnt nomina);
        List<NominaEnt> ObtenerNominas();
    }
}