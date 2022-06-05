using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Historias_Clinicas_D.Helpers;

namespace Historias_Clinicas_D.Models
{
	public class Episodio
	{
		#region Propiedades Principales

		public int Id { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[StringLength(Restricciones.StrMax4, MinimumLength = Restricciones.StrMin2, ErrorMessage = MensajesError.ErrMinMax)]
		public string Motivo { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[StringLength(Restricciones.StrMax4, MinimumLength = Restricciones.StrMin3, ErrorMessage = MensajesError.ErrMinMax)]
		public string Descripcion { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.FechaYHoraInicio)]
		public DateTime FechaYHoraInicio { get; set; } = Constantes.FechaActual;

		[Display(Name = Alias.FechaYHoraAlta)]
		public DateTime? FechaYHoraAlta { get; set; }

		[Display(Name = Alias.FechaYHoraCierre)]
		public DateTime? FechaYHoraCierre { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.EstadoAbierto)]
		public bool EstadoAbierto { get; set; } = Constantes.EstadoAbierto;

		public List<Evolucion> Evoluciones { get; set; }

		public Epicrisis Epicrisis { get; set; }

		#endregion

		#region Propiedades Relacionales

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.PacienteId)]
		public int PacienteId { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.EmpleadoResgistra)]
		public int EmpleadoRegistraId { get; set; }

		#endregion

		#region Propiedades Navegacionales

		public Empleado EmpleadoRegistra { get; set; }

		public Paciente Paciente { get; set; }

        #endregion
    }
}
