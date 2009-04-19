using System;

namespace Membrane.Commons.Plugin
{
	public interface IDto
	{
		Guid Id { get; set; }
		string Name { get; set; }
	}
}