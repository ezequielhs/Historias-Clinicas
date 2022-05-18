using Historias_Clinicas_D.Helpers;
using Historias_Clinicas_D.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Historias_Clinicas_D.ViewModels
{
    public class Registro
    {
        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [StringLength(Restricciones.StrMaxNombre, MinimumLength = Restricciones.StrMinNombre, ErrorMessage = MensajesError.ErrMinMax)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [StringLength(Restricciones.StrMaxApellido, MinimumLength = Restricciones.StrMinApellido, ErrorMessage = MensajesError.ErrMinMax)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [StringLength(Restricciones.StrMaxDoc, MinimumLength = Restricciones.StrMinDoc, ErrorMessage = MensajesError.ErrMinMax)]
        [Display(Name = Alias.DNI)]
        public string DNI { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [Display(Name = Alias.ObraSocial)]
        public ObraSocial ObraSocial { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [EmailAddress(ErrorMessage = MensajesError.ErrNoValido)]
        [Remote("IsEmailEnUso", "Personas", HttpMethod = "POST", ErrorMessage = MensajesError.ErrCampoEnUso)]
        public string Email { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [DataType(DataType.Password)]
        [Display(Name = Alias.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [Display(Name = Alias.PasswordConfirm)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = MensajesError.ErrPasswordCompare)]
        public string ConfirmPassword { get; set; }
    }
}
