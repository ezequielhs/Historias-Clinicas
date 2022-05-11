using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Historias_Clinicas_D.Models
{
    public enum Especialidad
    {
        [Display(Name = "Cardiología")]
        CARDIOLOGIA,
        [Display(Name = "Endocrinología")]
        ENDOCRINOLOGIA,
        [Display(Name = "Geriatría")]
        GERIATRIA,
        [Display(Name = "Infectología")]
        INFECTOLOGIA,
        [Display(Name = "Neumología")]
        NEUMOLOGIA,
        [Display(Name = "Neurología")]
        NEUROLOGIA,
        [Display(Name = "Odontología")]
        ODONTOLOGIA,
        [Display(Name = "Pediatría")]
        PEDIATRIA,
        [Display(Name = "Reumatología")]
        REUMATOLOGIA 
    }
}
