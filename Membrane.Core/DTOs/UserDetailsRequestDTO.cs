using System;
using Castle.Components.Validator;

namespace Membrane.Core.DTOs
{
	public class UserDetailsRequestDTO
	{
		[ValidateGuid(false)]
		public Guid Id { get; set; }
		[ValidateNonEmpty]
		public string Username { get; set; }
		[ValidateNonEmpty]
		public string Name { get; set; }
		[ValidateEmail]
		public string Email { get; set; }
		public string Password { get; set; }
		[ValidateSameAs("Password")]
		public string ConfirmPassword { get; set; }
	}
}