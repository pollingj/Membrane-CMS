using System;

namespace Membrane.Core.DTOs
{
	public class UserDetailsResponseDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Username { get; set; }
	}
}