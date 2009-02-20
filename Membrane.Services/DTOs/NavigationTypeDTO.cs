using Castle.Components.Validator;

namespace Membrane.Core.DTOs
{
	/// <summary>
	/// The ViewModel for the Navigation Types 
	/// </summary>
	public class NavigationTypeDTO
	{
		public int Id { get; set; }

		[ValidateNonEmpty("Navigation Type cannot be empty")]
		public string Name { get; set; }
	}
}