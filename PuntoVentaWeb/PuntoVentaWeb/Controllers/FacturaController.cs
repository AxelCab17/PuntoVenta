using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Models;
using PuntoVentaWeb.Services;
using System;

namespace PuntoVentaWeb.Controllers
{
    public class FacturaController(IFacturaModel _FacturaModel) : Controller
    {
        //Abre la vista:
        [HttpGet]
        public IActionResult RegistrarFactura()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarFactura(FacturaEnt entidad)
        {
            var RespuestaApi = _FacturaModel.RegistrarFactura(entidad);

            if (RespuestaApi?.Codigo == "1")
                return RedirectToAction("Carrito", "Carrito");
            else
            {
                return RedirectToAction("Carrito", "Carrito");
            }
        }

        [HttpPost]
        public IActionResult TerminarVenta(string itemsJson)
        {

            var items = JsonConvert.DeserializeObject<List<ItemModel>>(itemsJson);
            int cont = 0;
            var nuevaFact = 0;


            foreach (var item in items)
            {
                FacturaEnt entidad = new FacturaEnt();
                
                cont++;
                var productoId = item.ProductoId;
                var cantidad = item.Cantidad;
                var precio = item.Precio;
                var descuento = item.Descuento;
                
                if(cont == 1)
                {
                    nuevaFact = 1;
                }
                entidad.NuevaFactura = nuevaFact;
                entidad.Cantidad = cantidad;
                entidad.IdProducto = productoId;
                entidad.IdCajero = 1;
                entidad.Descuento = Decimal.ToInt32(descuento);
                nuevaFact = 0;
                RegistrarFactura(entidad);
                

            }
            

            return RedirectToAction("Carrito", "Carrito");
        }
    }

    public class ItemModel
    {
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
    }


}

