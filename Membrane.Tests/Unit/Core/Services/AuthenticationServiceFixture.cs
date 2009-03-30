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

		private AuthenticationRequestDTO authenticationRequest;

		public override void SetUp()
		{
			base.SetUp();

			userRepository = mockery.DynamicMock<IRepository<MembraneUser>>();
			service = new AuthenticationService(userRepository);

			authenticationRequest = new AuthenticationRequestDTO {Username = "username", Password = "password"};
		}

		[Test]
		public void CanReturnAuthenicatedUser()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(userRepository.FindOne(new UserByUsernameAndPassword(authenticationRequest.Username, authenticationRequest.Password))).Return(new MembraneUser { Id = Guid.NewGuid(), Username = "username", Password = "password", Type = new MembraneUserType { Id = Guid.NewGuid(), Type = UserType.Administrator }}));
			service.AuthenticateUser(authenticationRequest);

		}
	}
}