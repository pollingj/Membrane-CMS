using System;
using Membrane.Commons.CRUD;

namespace Membrane.Core.DTOs
{
	public class NavigationNodeDTO : IDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}