using System;
using System.Collections.Generic;
using Membrane.Commons.Plugin.DTOs;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Tests.Unit.Commons.Plugin.Services;

namespace Membrane.Tests.Unit.Core.Services
{
	public class NavigationNodeServiceFixture : OrderCRUDServiceFixture<NavigationNodeDTO, NavigationNode>
	{
		public override void SetUp()
		{
			SingleEntity = new NavigationNode { Id = Guid.NewGuid(), Name = "Home" };
			ListEntity = new List<NavigationNode>
			             	{
									new NavigationNode {Id = Guid.NewGuid(), Name = "Home"},
									new NavigationNode {Id = Guid.NewGuid(), Name = "About Us"},
									new NavigationNode {Id = Guid.NewGuid(), Name = "Contact Us"},
			                 		new NavigationNode {Id = Guid.NewGuid(), Name = "News"},
			                 		new NavigationNode {Id = Guid.NewGuid(), Name = "Blog"},
									new NavigationNode {Id = Guid.NewGuid(), Name = "History"}
			             	};
			SingleDTO = new NavigationNodeDTO { Id = Guid.NewGuid(), Name = "New Group" };

			ListDTO = new List<NavigationNodeDTO>
			             	{
									new NavigationNodeDTO {Id = Guid.NewGuid(), Name = "Home"},
									new NavigationNodeDTO {Id = Guid.NewGuid(), Name = "About Us"},
									new NavigationNodeDTO {Id = Guid.NewGuid(), Name = "Contact Us"},
			                 		new NavigationNodeDTO {Id = Guid.NewGuid(), Name = "News"},
			                 		new NavigationNodeDTO {Id = Guid.NewGuid(), Name = "Blog"},
									new NavigationNodeDTO {Id = Guid.NewGuid(), Name = "History"}
			             	};

			CurrentItemOrder = new ItemOrderRequestDTO
							{
								Ids = new[]
      		      					{
      		      						Guid.NewGuid(),
      		      						Guid.NewGuid(),
      		      						Guid.NewGuid(),
      		      						Guid.NewGuid(),
      		      						Guid.NewGuid(),
      		      						Guid.NewGuid()
      		      					}
							};

			base.SetUp();
		}
	}
}