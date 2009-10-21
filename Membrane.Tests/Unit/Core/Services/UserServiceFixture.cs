using System;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.Exceptions;
using Membrane.Core.DTOs;
using Membrane.Core.Services;
using Membrane.Core.Services.Interfaces;
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
		private IEncryptionService encryptionService;
		private IRepository<MembraneUser> userRepository;

		private UserDetailsRequestDTO userDetails;

		private MembraneUser currentUser;

		public override void SetUp()
		{
			base.SetUp();

			userRepository = mockery.DynamicMock<IRepository<MembraneUser>>();
			encryptionService = mockery.DynamicMock<IEncryptionService>();

			service = new UserService(userRepository, encryptionService);

			userDetails = new UserDetailsRequestDTO { Id = Guid.NewGuid(), Username = "johnpolling", Name = "John Polling", Email = "john@theusualsuspect.com" };

			currentUser = new MembraneUser
			{
				Id = userDetails.Id,
				Group = new UserGroup { Id = Guid.NewGuid(), Name = "Admin" },
				Password = "Password",
				Type = new MembraneUserType { Id = Guid.NewGuid(), Type = UserType.Administrator },
				Username = "johnpolling",
				Name = "John Polling",
				Email = "john@theusualsuspect.com"
			};

		}

		[Test]
		public void CanSuccessfullyUserDetails()
		{
			UserDetailsResponseDTO result = null;



			With.Mocks(mockery)
				.Expecting(() => Expect.Call(userRepository.FindById(userDetails.Id)).Return(currentUser))
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

			var result = false;
			With.Mocks(mockery)
				.Expecting(() =>
				           	{
								Expect.Call(userRepository.FindById(userDetails.Id)).Return(currentUser);
				           		Expect.Call(() => userRepository.Update(user)).IgnoreArguments();
				           	})
				.Verify(() => result = service.UpdateDetails(userDetails));

			Assert.IsTrue(result);
		}



		[Test]
		public void CanFailUpdatingUserDetails()
		{
			var result = true;

			With.Mocks(mockery)
				.Expecting(() =>
				           	{
				           		Expect.Call(userRepository.FindById(userDetails.Id)).Return(currentUser);
				           		Expect.Call(() => userRepository.Update(null)).IgnoreArguments().Throw(new RepositoryUpdateException());
				           	})
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
							Password = "7D-D2-9A-9C-96-43-FD-52-4E-1B-43-60-96-4B-89-CE-59-91-4E-68-D1-FD-1A-B0-4D-D6-1F-BA-AA-BC-58-E5-79-DC-FF-B5-B7-45-4A-B0-1E-58-6C-8A-E9-8E-53-8B-5D-6E-0F-F3-AE-7D-D4-42-DE-73-33-48-6D-C9-DF-1A"
						};

			userDetails.ConfirmPassword = userDetails.Password = "newpassword";

			var result = false;
			With.Mocks(mockery)
				.Expecting(() => 
					{
						Expect.Call(userRepository.FindById(userDetails.Id)).Return(currentUser);
						Expect.Call(encryptionService.Encrypt("newpassword")).Return("7D-D2-9A-9C-96-43-FD-52-4E-1B-43-60-96-4B-89-CE-59-91-4E-68-D1-FD-1A-B0-4D-D6-1F-BA-AA-BC-58-E5-79-DC-FF-B5-B7-45-4A-B0-1E-58-6C-8A-E9-8E-53-8B-5D-6E-0F-F3-AE-7D-D4-42-DE-73-33-48-6D-C9-DF-1A");
						Expect.Call(() => userRepository.Update(user)).IgnoreArguments();
					})
				.Verify(() => result = service.UpdateDetails(userDetails));

			Assert.IsTrue(result);
		}
	}
}