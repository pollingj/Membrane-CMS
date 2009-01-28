using Membrane.Models.Interfaces.UserManagement;
using Membrane.Models.UserManagement;
using Membrane.Services;
using NUnit.Framework;
using Query;
using Rhino.Commons;
using Rhino.Mocks;

namespace Membrane.Test.Services
{
	[TestFixture]
    public class UserServiceTestCases
    {
        private MockRepository mockery;
        private IRepository<User> userRepository;
    	private UserService service;
    	private User user;

        [SetUp]
        public void SetUp()
        {
            mockery = new MockRepository();
            userRepository = mockery.DynamicMock<IRepository<User>>();
			service = new UserService(userRepository); 

        	user = new User();
        	user.Email = "john@test.com";
        	user.Name = "John Polling";
        	user.Username = "pollingj";
        }

        [TearDown]
        public void TearDown()
        {
        }

		[Test]
		public void CanAuthoriseUserAccess()
		{
			using(mockery.Record())
			{
				Expect.Call(userRepository.FindOne(Where.User.Username.Eq("username") && Where.User.Password.Eq(service.Hash("password")))).Return(user);
			}

			using(mockery.Playback())
			{
				User authUser = service.AuthouriseUser("username", "password");
				Assert.AreEqual("John Polling", authUser.Name);
			}
		}

		public void CanFailUserAccess()
		{
			
		}
    }
}