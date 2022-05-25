using System;
using System.ComponentModel.DataAnnotations;
using Historias_Clinicas_D.Helpers;

namespace Historias_Clinicas_D.Models
{
	public class Nota
	{
		public int Id { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.EvolucionId)]
		public int EvolucionId { get; set; }

		public Evolucion Evolucion { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.EmpleadoId)]
		public int EmpleadoId { get; set; }

		public Empleado Empleado { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		public string Mensaje { get; set; }

		[Required]
		[Display(Name = Alias.FechaYHora)]
		public DateTime FechaYHora { get; set; } = Constantes.FechaActual;
	}

}
