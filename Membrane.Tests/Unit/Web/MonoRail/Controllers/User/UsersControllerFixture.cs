using System;
using System.Collections.Generic;
using Membrane.Controllers.User;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Tests.Unit.Commons.Plugin.Controllers;
using NUnit.Framework;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers.User
{
	public class UsersControllerFixture : CRUDControllerFixture<UserDetailsRequestDTO, MembraneUser>
	{
		[SetUp]
		public override void SetUp()
		{
			base.SetUp();
			NewDTO = new UserDetailsRequestDTO { Username = "johnpolling", Name = "John Polling", Password = "pass", ConfirmPassword = "pass", Email = "john@test.com" };
			EditDTO = new UserDetailsRequestDTO { Id = Guid.NewGuid(), Username = "johnpolling", Name = "John Polling", Password = "pass", ConfirmPassword = "pass", Email = "john@test.com" };
			InvalidDTO = new UserDetailsRequestDTO { Id = Guid.NewGuid() };
			DeleteDTO = new UserDetailsRequestDTO { Id = Guid.NewGuid(), Username = "johnpolling", Name = "John Polling", Password = "pass", Email = "john@test.com" };

			ListDTO = new List<UserDetailsRequestDTO>
			          	{
			          		new UserDetailsRequestDTO {Id = Guid.NewGuid(), Username = "johnpolling", Name = "John Polling", Email = "john@test.com" },
			          		new UserDetailsRequestDTO {Id = Guid.NewGuid(), Username = "andypike", Name = "Andy Pike", Email = "andy@test.com"}
			          	};


			Controller = new UsersController(Service, PropertyReaderService);

			PrepareController(Controller);
		}
	}
}