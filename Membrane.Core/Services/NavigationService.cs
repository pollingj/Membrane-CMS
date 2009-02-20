using System.Collections.Generic;
using AutoMapper;
using Membrane.Core.Domain;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using Rhino.Commons;

namespace Membrane.Core.Services
{
	/// <summary>
	/// Service to handle the navigation management work.
	/// </summary>
	public class NavigationService : INavigationService
	{
		private IRepository<NavigationType> navigationTypeRepository;

		public NavigationService(IRepository<NavigationType> navigationTypeRepository)
		{
			this.navigationTypeRepository = navigationTypeRepository;	
		}

		/// <summary>
		/// Returns all of the navigiaton types
		/// This method uses the AutoMapper project, (http://www.codeplex.com/AutoMapper),
		/// this should cut down on the need for writing mapper objects 
		/// and tests for the mapper object.....boring :-)
		/// </summary>
		/// <returns></returns>
		public ICollection<NavigationTypeDTO> GetAllNavigationTypes()
		{
			var types = navigationTypeRepository.FindAll();
			Mapper.CreateMap<NavigationType, NavigationTypeDTO>();
			Mapper.AssertConfigurationIsValid();
			return Mapper.Map<ICollection<NavigationType>, ICollection<NavigationTypeDTO>>(types);
		}

		public bool AddNavigationType(NavigationTypeDTO navType)
		{
			throw new System.NotImplementedException();
		}
	}
}