using System;
using Membrane.Commons.Persistence;
using Membrane.Core.DTOs;
using Membrane.Core.Queries.Users;
using Membrane.Core.Services;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities;
using Membrane.Entities.Enums;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Core.Services
{
	[TestFixture]
	public class AuthenticationServiceFixture : BaseFixture
	{
		private IAuthenticationService service;
		private IRepository<MembraneUser> userRepository;

		public override void SetUp()
		{
			base.SetUp();

			userRepository = mockery.DynamicMock<IRepository<MembraneUser>>();
			service = new AuthenticationService(userRepository);
		}

		[Test]
		public void CanReturnAuthenicatedUser()
		{
			var authenticationRequest = new AuthenticationRequestDTO { Username = "username", Password = "password" };
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(userRepository.FindOne(new UserByUsernameAndPassword(authenticationRequest.Username, authenticationRequest.Password))).IgnoreArguments().Return(new MembraneUser { Id = Guid.NewGuid(), Username = "username", Password = "password", Type = new MembraneUserType { Id = Guid.NewGuid(), Type = UserType.Administrator }}))
				.Verify(() => service.AuthenticateUser(authenticationRequest));

		}
	}
}