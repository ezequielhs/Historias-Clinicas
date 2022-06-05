using Historias_Clinicas_D.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Historias_Clinicas_D.ViewModels
{
    public class CambiarPassword
    {
		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[DataType(DataType.Password)]
		[Display(Name = Alias.CurrentPassword)]
		public string CurrentPassword { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[DataType(DataType.Password)]
		[Display(Name = Alias.NewPassword)]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = MensajesError.ErrRequired)]
		[DataType(DataType.Password)]
		[Display(Name = Alias.PasswordConfirm)]
		[Compare("NewPassword", ErrorMessage = MensajesError.ErrPasswordMissmatching)]
		public string ConfirmPassword { get; set; }
	}
}
