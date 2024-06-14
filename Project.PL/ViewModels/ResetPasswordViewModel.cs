using System.ComponentModel.DataAnnotations;

namespace Project.PL.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "invalid email address")]
		public string Email { get; set; }
	}
}
