using Historias_Clinicas_D.Helpers;
using Historias_Clinicas_D.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Historias_Clinicas_D.Models
{
	public class Medico : Empleado
	{
        #region Propiedades Principales

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [Display(Name = Alias.TipoMatricula)]
        public TipoMatricula TipoMatricula { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [StringLength(Restricciones.StrMax1, MinimumLength = Restricciones.StrMin1, ErrorMessage = MensajesError.ErrMinMax)]
		[Display(Name = Alias.Matricula)]
        public string Matricula { get; set; }

		public Especialidad Especialidad { get; set; }

        #endregion
    }
}
