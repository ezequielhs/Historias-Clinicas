using Historias_Clinicas_D.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Historias_Clinicas_D.Models
{
    public class Persona : IdentityUser<int>
    {
        #region Propiedades Principales

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [StringLength(Restricciones.StrMaxNombre, MinimumLength = Restricciones.StrMinNombre, ErrorMessage = MensajesError.ErrMinMax)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [StringLength(Restricciones.StrMaxApellido, MinimumLength = Restricciones.StrMinApellido, ErrorMessage = MensajesError.ErrMinMax)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [StringLength(Restricciones.StrMaxDoc, MinimumLength = Restricciones.StrMinDoc, ErrorMessage = MensajesError.ErrMinMax)]
        [Display (Name = Alias.DNI)]
        public string DNI { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [Display(Name = Alias.FechaAlta)]
        [DataType(DataType.Date)] // Seteamos el DataType como Date para que no pida la hora y minutos, sino solo la fecha, que es lo que se pedía.
        public DateTime FechaAlta { get; set; } = Defaults.FechaActual;

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [EmailAddress(ErrorMessage = MensajesError.ErrNoValido)]
        [Display(Name = Alias.Email)]
        [Remote("IsEmailEnUso", "Personas", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessage = MensajesError.ErrCampoEnUso)]
        public override string Email
        {
            get { return base.Email; }
            set { base.Email = value; }
        }

        #endregion

        #region Propiedades Navegacionales

[Display(Name = Alias.Telefonos)]
        public List<Telefono> Telefonos { get; set; }

        [Display(Name = Alias.Direccion)]
        public Direccion Direccion { get; set; }

        #endregion

        #region Metodos

        [Display(Name = Alias.NombreCompleto)]
        public string NombreCompleto
        {
            get
            {
                return Apellido + ", " + Nombre;
            }
        }

        #endregion
    }
}
