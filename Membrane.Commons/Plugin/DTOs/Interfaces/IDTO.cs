using System;

namespace Membrane.Commons.Plugin.DTOs.Interfaces
{
	public interface IDto
	{
		Guid Id { get; set; }
		string Name { get; set; }
	}
}