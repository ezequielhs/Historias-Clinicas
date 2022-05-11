using System.ComponentModel.DataAnnotations;

namespace Historias_Clinicas_D.Models
{
    public enum TipoTelefono
    {
        [Display(Name = "Celular")]
        CELULAR,
        [Display(Name = "Fax")]
        FAX,
        [Display(Name = "Laboral")]
        LABORAL,
        [Display(Name = "Particular")]
        PARTICULAR
    }
}