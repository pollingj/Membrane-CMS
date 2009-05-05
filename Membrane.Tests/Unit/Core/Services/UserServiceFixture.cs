using System;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.Exceptions;
using Membrane.Core.DTOs;
using Membrane.Core.Services;
using Membrane.Entities;
using Membrane.Entities.Enums;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Core.Services
{
	[TestFixture]
	public class UserServiceFixture : BaseFixture
	{
		private UserService service;
		private IRepository<MembraneUser> repository;

		private UserDetailsRequestDTO userDetails;

		public override void SetUp()
		{
			base.SetUp();

			repository = mockery.DynamicMock<IRepository<MembraneUser>>();

			service = new UserService(repository);

			userDetails = new UserDetailsRequestDTO { Id = Guid.NewGuid(), Username = "johnpolling", Name = "John Polling", Email = "john@theusualsuspect.com" };

		}

		[Test]
		public void CanSuccessfullyUserDetails()
		{
			UserDetailsResponseDTO result = null;

			var user = new MembraneUser
			           	{
			           		Id = userDetails.Id,
			           		Group = new UserGroup {Id = Guid.NewGuid(), Name = "Admin"},
			           		Password = "Password",
			           		Type = new MembraneUserType {Id = Guid.NewGuid(), Type = UserType.Administrator},
			           		Username = "johnpolling",
							Name = "John Polling",
							Email = "john@theusualsuspect.com"
			           	};

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(repository.FindById(userDetails.Id)).Return(user))
				.Verify(() => result = service.LoadDetails(userDetails.Id));

			Assert.AreEqual(userDetails.Id, result.Id);
			Assert.AreEqual(userDetails.Name, result.Name);
			Assert.AreEqual(userDetails.Email, result.Email);
		}

		[Test]
		public void CanSuccessfullyUpdateUserDetailsWithNoPassword()
		{
			var user = new MembraneUser
			           	{
			           		Id = userDetails.Id,
			           		Username = "johnpolling",
			           		Name = "John Polling",
			           		Email = "john@theusualsuspect.com"
						};

			MockUpdatingUserDetails(user);
		}



		[Test]
		public void CanFailUpdatingUserDetails()
		{
			var result = true;

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(() => repository.Update(null)).IgnoreArguments().Throw(new RepositoryUpdateException()))
				.Verify(() => result = service.UpdateDetails(userDetails));

			Assert.IsFalse(result);
		}

		[Test]
		public void CanSuccessfullyUpdateUserDetailsWithPassword()
		{
			var user = new MembraneUser
			           	{
			           		Id = userDetails.Id,
			           		Username = "johnpolling",
			           		Name = "John Polling",
			           		Email = "john@theusualsuspect.com",
			           		Password = "asd"
						};

			userDetails.ConfirmPassword = userDetails.Password = "newpassword";

			MockUpdatingUserDetails(user);
		}

		private void MockUpdatingUserDetails(MembraneUser user)
		{
			var result = false;
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(() => repository.Update(user)))
				.Verify(() => result = service.UpdateDetails(userDetails));

			Assert.IsTrue(result);
		}
	}
}