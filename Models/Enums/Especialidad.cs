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
        [Display(Name = "Kinesiología")]
        KINESIOLOGIA,
        [Display(Name = "Medicina General")]
        MEDICINA_GENERAL,
        [Display(Name = "Nefrología")]
        NEFROLOGIA,
        [Display(Name = "Neumología")]
        NEUMOLOGIA,
        [Display(Name = "Neurología")]
        NEUROLOGIA,
        [Display(Name = "Odontología")]
        ODONTOLOGIA,
        [Display(Name = "Pediatría")]
        PEDIATRIA,
        [Display(Name = "Reumatología")]
        REUMATOLOGIA,
        [Display(Name = "Traumatología")]
        TRAUMATOLOGIA
    }
}
