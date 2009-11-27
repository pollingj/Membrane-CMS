using System;

namespace Membrane.Commons.Persistence
{
	public interface IEntity
	{
		// The unqie identifier of an object
		Guid Id { get; set; }
	}

}