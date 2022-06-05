using System;
using System.ComponentModel.DataAnnotations;
using Historias_Clinicas_D.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Historias_Clinicas_D.Models
{
	[Authorize]
	public class Paciente : Persona
	{
		[Required (ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.ObraSocial)]
		public ObraSocial ObraSocial { get; set; }

		public List<Episodio> Episodios { get; set; }
	}
}
