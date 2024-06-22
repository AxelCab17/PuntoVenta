using System.Collections.Generic;

namespace PuntoVentaAPI.Entities
{
    public class Carrito
    {
        public List<CarritoItemEnt> Items { get; set; } = new List<CarritoItemEnt>();
    }
}