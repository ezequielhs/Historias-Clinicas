using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Historias_Clinicas_D.Helpers;

namespace Historias_Clinicas_D.Models
{
	public class Epicrisis
	{

        #region Propiedades Relacionales

		public int Id { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.EpisodioId)]
		public int EpisodioId { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.MedicoId)]
		public int MedicoId { get; set; }

		#endregion

		#region Propiedades Navegacionales

		public Episodio Episodio { get; set; }
			
        public Medico Medico { get; set; }

		[Display(Name = Alias.FechaYHora)]
		public DateTime FechaYHora { get; set; } = Defaults.FechaActual;

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.Diagnostico)]
		public string Diagnostico { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.Recomendacion)]
		public string Recomendacion { get; set; }

        #endregion
    }
}
