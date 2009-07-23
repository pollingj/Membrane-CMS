using Castle.Components.Validator;
using Membrane.Commons.Plugin.DTOs;

namespace Membrane.Core.DTOs
{
	public class UserDetailsRequestDTO : BaseDTO	
	{
		[ValidateNonEmpty]
		public string Username { get; set; }
		[ValidateEmail]
		public string Email { get; set; }
		public string Password { get; set; }
		[ValidateSameAs("Password")]
		public string ConfirmPassword { get; set; }

	}
}