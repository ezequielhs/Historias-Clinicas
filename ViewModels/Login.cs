using Historias_Clinicas_D.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Historias_Clinicas_D.ViewModels
{
    public class Login
    {
        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [EmailAddress(ErrorMessage = MensajesError.ErrNoValido)]
        public string Email { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [DataType(DataType.Password)]
        [Display(Name = Alias.Password)]
        public string Password { get; set; }

        [Display(Name = Alias.RememberMe)]
        public bool RememberMe { get; set; }
    }
}
