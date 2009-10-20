using System;
using System.Collections.Generic;
using Membrane.Commons.FormGeneration;
using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Controllers.User;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities.Enums;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers.User
{
	[TestFixture]
	public class UserDetailsControllerFixture : BaseControllerFixture
	{
		private IUserService userService;
		private IPropertyReaderService<UserDetailsRequestDTO> readerService; 
		private UserDetailsController controller;

		[SetUp]
		public override void SetUp()
		{
 			base.SetUp();

			userService = mockery.DynamicMock<IUserService>();
			readerService = mockery.DynamicMock<IPropertyReaderService<UserDetailsRequestDTO>>();

			controller = new UserDetailsController(userService, readerService);
			PrepareController(controller, "UserDetails");
		}


		[Test]
		public void UserCanViewTheirCurrentDetails()
		{
			var userDetails = new UserDetailsResponseDTO { Id = Guid.NewGuid(), Username = "johnpolling", Name = "John Polling", Email = "john@theusualsuspect.com"  };
			var authenticatedUser = new AuthenticatedUserDTO
			                            	{Id = Guid.NewGuid(), Type = UserType.Administrator};

			var fields = new List<FormField>
			             	{
			             		new FormField {Id = "Id"},
			             		new FormField {Id = "Username"},
								new FormField {Id = "Name"},
								new FormField {Id = "Email"}
			             	};


			Context.Session["user"] = authenticatedUser;

			With.Mocks(mockery)
				.Expecting(() =>
				           	{
				           		Expect.Call(userService.LoadDetails(authenticatedUser.Id)).Return(userDetails);
				           		Expect.Call(() => readerService.ReadViewModelProperties());
				           		Expect.Call(readerService.FormFields).Return(fields);
				           	})
				.Verify(() => controller.Show());

			Assert.AreEqual(userDetails, controller.PropertyBag["details"]);
			Assert.AreEqual(fields, controller.PropertyBag["fields"]);
			Assert.AreEqual("/Shared/Form", controller.SelectedViewName);
		}

		[Test]
		public void UserCanSuccessfullyUpdateTheirCurrentDetails()
		{
			MockUserDetailsUpdate(true);

			Assert.IsNull(controller.Flash["errors"]);
			Assert.AreEqual("/UserDetails/Show.castle", Response.RedirectedTo);
		}

		[Test]
		public void UpdateDetailsCanFailValidation()
		{
			var updatedUserDetails = new UserDetailsRequestDTO
			                         	{
			                         		Id = Guid.NewGuid(),
											Username = "johnpolling",
			                         		Name = "John Polling",
			                         		Email = string.Empty,
			                         		Password = string.Empty,
			                         		ConfirmPassword = string.Empty
			                         	};

			controller.Update(updatedUserDetails);

			Assert.IsNotNull(controller.Flash["errors"]);
			Assert.AreEqual("/UserDetails/Show.castle", Response.RedirectedTo);
		}

		[Test]
		public void ErrorMessageCanBeShownOnFailureOfService()
		{
			MockUserDetailsUpdate(false);

			Assert.IsNotNull(controller.Flash["errors"]);
			Assert.AreEqual("/UserDetails/Show.castle", Response.RedirectedTo);
		}

		private void MockUserDetailsUpdate(bool success)
		{
			var updatedUserDetails = new UserDetailsRequestDTO
			{
				Id = Guid.NewGuid(),
				Username = "johnpolling",
				Name = "John Polling",
				Email = "john@theusualsusepct.com",
				Password = string.Empty,
				ConfirmPassword = string.Empty
			};
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(userService.UpdateDetails(updatedUserDetails)).Return(success))
				.Verify(() => controller.Update(updatedUserDetails));
		}

	}
}