using System.ComponentModel.DataAnnotations;
using Historias_Clinicas_D.Helpers;

namespace Historias_Clinicas_D.ViewModels
{
	public class LogIn
	{
        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [EmailAddress(ErrorMessage = MensajesError.ErrNoValido)]
        [Display(Name = Alias.Email)]
        public string Email { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [DataType(DataType.Password)]
        [Display(Name = Alias.Password)]
        public string Password { get; set; }

        [Display(Name = Alias.RememberMe)]
        public bool RememberMe { get; set; }
    }
}