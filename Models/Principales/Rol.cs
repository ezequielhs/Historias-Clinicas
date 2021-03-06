using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Historias_Clinicas_D.Models
{
	public class Rol :	IdentityRole<int>
	{
		public Rol() : base() { }

		public Rol(string rolName) : base(rolName) { }

		[Display(Name = "Tipo de Rol")]
		public override string Name
		{
			get { return base.Name; }
			set { base.Name = value; }
		}

		public override string NormalizedName 
		{ 
			get => base.NormalizedName; 
			set => base.NormalizedName = value; 
		}
	}
}
