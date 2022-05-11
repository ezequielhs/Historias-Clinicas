using System.ComponentModel.DataAnnotations;

namespace Historias_Clinicas_D.Models.Enums
{
    public enum TipoMatricula
    {
        [Display(Name="Nacional")]
        NACIONAL,
        [Display(Name = "Provincial")]
        PROVINCIAL,
        [Display(Name = "Extranjera")]
        EXTRANJERA,
        [Display(Name = "En Tramite")]
        EN_TRAMITE
        
    }
}
