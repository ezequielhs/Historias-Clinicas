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
		#region Propiedades Principales

		public int Id { get; set; }

		[Display(Name = Alias.FechaYHora)]
		public DateTime FechaYHora { get; set; } = Constantes.FechaActual;

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.Diagnostico)]
		public string Diagnostico { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.Recomendacion)]
		public string Recomendacion { get; set; }

		#endregion

		#region Propiedades Relacionales

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.EpisodioId)]
		public int EpisodioId { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.EmpleadoId)]
		public int EmpleadoId { get; set; }

		#endregion

		#region Propiedades Navegacionales

		public Episodio Episodio { get; set; }
			
        public Empleado Empleado { get; set; }

        #endregion
    }
}
