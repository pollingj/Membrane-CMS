using System;
using Castle.Components.Validator;
using Membrane.Commons.Persistence;

namespace Membrane.Core.DTOs
{
	/// <summary>
	/// The ViewModel for the Navigation Types 
	/// </summary>
	public class NavigationTypeDTO : IEntity
	{
		public Guid Id { get; set; }

		[ValidateNonEmpty("Navigation Type cannot be empty")]
		public string Name { get; set; }
	}
}