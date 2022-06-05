using System;
using System.ComponentModel.DataAnnotations;
using Historias_Clinicas_D.Helpers;

namespace Historias_Clinicas_D.Models
{
	public class Evolucion
	{
		
		public int Id { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.MedicoId)]
		public int MedicoId { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.EpisodioId)]
		public int EpisodioId { get; set; }

		public Medico Medico { get; set; }

		public Episodio Episodio { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.FechaYHoraInicio)]
		public DateTime FechaYHoraInicio { get; set; } = Constantes.FechaActual;

		[Display(Name = Alias.FechaYHoraAlta)]
		public DateTime? FechaYHoraAlta { get; set; } = null;

		[Display(Name = Alias.FechaYHoraCierre)]
		public DateTime? FechaYHoraCierre { get; set; } = null;

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.DescripcionAtencion)]
		public string DescripcionAtencion { get; set; }

		[Display(Name = Alias.EstadoAbierto)]
		public bool EstadoAbierto { get; set; } = Constantes.EstadoAbierto;

		public List<Nota> Notas { get; set; }
	}
}
