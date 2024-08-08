using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PuntoVentaWeb.Entities;
using PuntoVentaWeb.Entities;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;

namespace PuntoVentaWeb.Controllers
{
    public class ImpresionController : Controller
    {
        private readonly IWebHostEnvironment _host;

        public ImpresionController(IWebHostEnvironment host)
        {
            _host = host;
        }

        public IActionResult Imprimir()
        {
           var datosFacturaJson = TempData["DatosFactura"] as string;
            if (!string.IsNullOrEmpty(datosFacturaJson))
            {
                var datosFactura = JsonConvert.DeserializeObject<List<FacturaEnt>>(datosFacturaJson);
                PrintContent(datosFactura);
            }

            return RedirectToAction("Carrito", "Carrito");
        }

        private void PrintContent(List<FacturaEnt> datosFactura)
        {
            var printDocument = new PrintDocument();

            printDocument.PrintPage += (sender, e) =>
            {
                var fontTitle = new Font("Arial", 16, FontStyle.Bold);
                var fontNormal = new Font("Arial", 12);
                var brush = Brushes.Black;
                var margin = 50;
                var yPosition = margin;

                // Encabezado centrado
                var header = "SUPER MAS";
                var titleSize = e.Graphics.MeasureString(header, fontTitle);
                var titlePosition = new PointF((e.PageBounds.Width - titleSize.Width) / 2, yPosition);
                e.Graphics.DrawString(header, fontTitle, brush, titlePosition);

                yPosition += 40; // Adjust vertical position after header

                // Datos generales de la factura
                var generalData = new StringBuilder();
                generalData.AppendLine($"Factura ID: {datosFactura[0].IdFactura}");
                generalData.AppendLine($"Fecha: {datosFactura[0].Fecha:dd/MM/yyyy}");
                generalData.AppendLine($"Subtotal: {datosFactura[0].SubTotal:C}");
                generalData.AppendLine($"IVA: {datosFactura[0].IVA:C}");
                generalData.AppendLine($"Total Factura: {datosFactura[0].TotalFactura:C}");

                e.Graphics.DrawString(generalData.ToString(), fontNormal, brush, margin, yPosition);

                yPosition += 100;
                // Encabezado de la tabla
                var columnWidth = new[] { 200, 120, 150, 150, 140 }; // Ancho de las columnas
                var columnTitles = new[] { "Producto", "Cantidad", "Precio Unitario", "Descuento", "Total Detalle" };

                // Imprimir encabezado de la tabla
                for (int i = 0; i < columnTitles.Length; i++)
                {
                    e.Graphics.DrawString(columnTitles[i], fontNormal, brush, margin + columnWidth[i] * i, yPosition);
                }

                yPosition += 40; // Adjust vertical position after column headers

                // Imprimir datos de la tabla
                foreach (var item in datosFactura)
                {
                    e.Graphics.DrawString(item.NombreProducto, fontNormal, brush, margin, yPosition);
                    e.Graphics.DrawString(item.Cantidad.ToString(), fontNormal, brush, margin + columnWidth[0], yPosition);
                    e.Graphics.DrawString(item.PrecioUnitario.ToString("C"), fontNormal, brush, margin + columnWidth[0] + columnWidth[1], yPosition);
                    e.Graphics.DrawString(item.Descuento.ToString("C"), fontNormal, brush, margin + columnWidth[0] + columnWidth[1] + columnWidth[2], yPosition);
                    e.Graphics.DrawString(item.TotalDetalle.ToString("C"), fontNormal, brush, margin + columnWidth[0] + columnWidth[1] + columnWidth[2] + columnWidth[3], yPosition);
                    yPosition += 30; // Adjust vertical position for each row
                }

                
            };

            // Configuración de la impresora
            var printerSettings = new PrinterSettings
            {
                PrinterName = "NPI032B12 (HP LaserJet MFP M130fw)"
            };

            printDocument.PrinterSettings = printerSettings;

            // Enviar a imprimir
            try
            {
                printDocument.Print();
            }
            catch (Exception ex)
            {
                // Manejo de errores de impresión
                Console.WriteLine($"Error al imprimir: {ex.Message}");
            }
        }
    }


}
