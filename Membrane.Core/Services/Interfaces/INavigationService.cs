using System.Collections.Generic;
using Membrane.Core.DTOs;

namespace Membrane.Core.Services.Interfaces
{
	public interface INavigationService
	{
		ICollection<NavigationTypeDTO> GetAllNavigationTypes();
		bool AddNavigationType(NavigationTypeDTO navType);
	}
}