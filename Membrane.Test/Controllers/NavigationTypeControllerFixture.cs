using System;
using System.Collections.Generic;
using Castle.MonoRail.Framework.Helpers;
using Membrane.Commons.Services;
using Membrane.Controllers.MembraneAdmin;
using Membrane.Core.DTOs;
using NUnit.Framework;

namespace Membrane.Test.Controllers
{
   /* [TestFixture]
	public class NavigationTypeControllerFixture : BaseCrudControllerFixture<NavigationTypeDTO>
    {
		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			service = mockery.DynamicMock<IBaseCrudService<NavigationTypeDTO>>();

			controller = new NavigationTypeController(service);

			PrepareController(controller, "NavigationType");

			// Set up the base controller testing values (item, items and paginatedData)
			item = new NavigationTypeDTO { Id = new Guid("10000000-0000-0000-0000-000000000000"), Name = "Primary Navigation" };

			items = new List<NavigationTypeDTO>
			        	{
			        		item, 
							new NavigationTypeDTO {Id = new Guid(), Name = "Secondary Navigation"}, 
							new NavigationTypeDTO {Id = new Guid(), Name = "Tertiary Navigation"}
			        	};	

			paginatedData = PaginationHelper.CreatePagination(items, defaultPage, defaultDisplayCount);
		}

    }*/

}