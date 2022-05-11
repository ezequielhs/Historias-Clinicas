using Historias_Clinicas_D.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Historias_Clinicas_D.Models
{

    public class Empleado : Persona
    {
        [Required]
        public string Legajo { get; set;} = Generadores.GetNewLegajoEmpleado(Restricciones.StrMaxLegajo);      
	}
}