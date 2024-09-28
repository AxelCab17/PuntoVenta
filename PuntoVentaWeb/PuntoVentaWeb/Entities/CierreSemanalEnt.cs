﻿using System.ComponentModel;

namespace PuntoVentaWeb.Entities
{
	public class CierreSemanalEnt
	{
		[DisplayName("Total de Billetes de la Semana")]
		public decimal TotalBilletesSemana { get; set; }

		[DisplayName("Total de Monedas de la Semana")]
		public decimal TotalMonedasSemana { get; set; }

		[DisplayName("Total de Efectivo Semanal")]
		public decimal TotalEfectivoSemana { get; set; }
	}



	public class CierreSemanalRespuesta
	{
		public CierreSemanalRespuesta()
		{
			Codigo = "1";
			Mensaje = string.Empty;
			Dato = null;
			Datos = null;
		}

		public string Codigo { get; set; }
		public string Mensaje { get; set; }
		public CierreSemanalEnt? Dato { get; set; }
		public List<CierreSemanalEnt>? Datos { get; set; }
	}

}



