using System;
using Membrane.Entities.Enums;

namespace Membrane.Core.DTOs
{
	public class AuthenticatedUserDTO
	{
		public Guid Id { get; set; }
		public UserType Type { get; set; }
	}
}