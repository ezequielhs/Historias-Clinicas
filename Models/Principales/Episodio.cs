using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Historias_Clinicas_D.Helpers;

namespace Historias_Clinicas_D.Models
{
	public class Episodio
	{
		public int Id { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.PacienteId)]
		public int PacienteId { get; set; }

		public Paciente Paciente { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[StringLength(Restricciones.StrMax3, MinimumLength = Restricciones.StrMin2, ErrorMessage = MensajesError.ErrMinMax)]
		public string Motivo { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[StringLength(Restricciones.StrMax4, MinimumLength = Restricciones.StrMin3, ErrorMessage = MensajesError.ErrMinMax)]
		public string Descripcion { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.FechaYHoraInicio)]
		public DateTime FechaYHoraInicio { get; set; } = Defaults.FechaActual;

		[Display(Name = Alias.FechaYHoraAlta)]
		public DateTime? FechaYHoraAlta { get; set; }

		[Display(Name = Alias.FechaYHoraCierre)]
		public DateTime? FechaYHoraCierre { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.EstadoAbierto)]
		public bool EstadoAbierto { get; set; } = Defaults.EstadoAbierto;

		public List<Evolucion> Evoluciones { get; set; }

		public Epicrisis Epicrisis { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.EmpleadoResgistra)]
		public int EmpleadoRegistraId { get; set; }

		[Display(Name = Alias.EmpleadoResgistra)]
		public Empleado EmpleadoRegistra { get; set; }


	}
}
