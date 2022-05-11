using Historias_Clinicas_D.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Historias_Clinicas_D.Models
{
    public class Telefono
    {
        #region Propiedades Principales

        public int Id { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [Range(Restricciones.IntMinPrefTel, Restricciones.IntMaxPrefTel, ErrorMessage = MensajesError.ErrRangoMinMax)]
        public int Caracteristica { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        [Range(Restricciones.IntMinNumTel, Restricciones.IntMaxNumTel, ErrorMessage = MensajesError.ErrRangoMinMax)]
        public int Numero { get; set; }

        [Required(ErrorMessage = MensajesError.ErrRequired)]
        public TipoTelefono Tipo { get; set; }

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
        public string TelefonoCompleto
        {
            get
            {
                return Caracteristica.ToString() + Numero.ToString();
            }
        }

        #endregion
    }
}
