using System;

namespace Membrane.Commons.CRUD
{
	public interface IDto
	{
		Guid Id { get; set; }
		string Name { get; set; }
	}
}