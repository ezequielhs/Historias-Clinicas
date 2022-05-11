using System;
using System.ComponentModel.DataAnnotations;
using Historias_Clinicas_D.Helpers;

namespace Historias_Clinicas_D.Models
{
	public class Paciente : Persona
	{
		[Required (ErrorMessage = MensajesError.ErrRequired)]
		[Display(Name = Alias.ObraSocial)]
		public ObraSocial ObraSocial { get; set; }

		public List<Episodio> Episodios { get; set; }
	}
}
