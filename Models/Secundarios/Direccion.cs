using Historias_Clinicas_D.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Historias_Clinicas_D.Models
{
    public class Direccion
    {
        #region Propiedades Principales

        public int Id { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [StringLength(Restricciones.StrMaxCalle, MinimumLength = Restricciones.StrMinCalle, ErrorMessage = MensajesError.ErrMinMax)]
        public string Calle { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [Range(Restricciones.IntMinNumDir, Restricciones.IntMaxNumDir, ErrorMessage = MensajesError.ErrRangoMinMax)]
        public int Numero { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [StringLength(Restricciones.StrMaxCodPos, MinimumLength = Restricciones.StrMinCodPos, ErrorMessage = MensajesError.ErrMinMax)]
        [Display(Name = Alias.CodigoPostal)]
        public string CodigoPostal { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [StringLength(Restricciones.StrMaxLoc, MinimumLength = Restricciones.StrMinLoc, ErrorMessage = MensajesError.ErrMinMax)]
        public string Localidad { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        public Provincia Provincia { get; set; }

        #endregion

        #region Propiedades Relacionales

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [Display(Name = Alias.PersonaId)]
        public int PersonaId { get; set; }

        #endregion

        #region Propiedades Navegacionales

        public Persona Persona { get; set; }

        #endregion

        #region Metodos

        [Display(Name = Alias.Direccion)]
        public string DireccionCompleta { 
            get 
            {
                return Calle + ", " + Numero + ". " + Localidad;
            } 
        }

        #endregion
    }
}
