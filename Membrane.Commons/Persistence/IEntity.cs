using System;

namespace Membrane.Commons.Persistence
{
	public interface IEntity
	{
		Guid Id { get; set; }
	}

}