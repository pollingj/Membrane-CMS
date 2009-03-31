using NUnit.Framework;
using WatiN.Core;

namespace Membrane.Tests.Acceptance
{
	[TestFixture, Category("Acceptance")]
	public class WhenUserLogsOff : AcceptanceTestBase
	{
		[Test]
		public void AdministratorCanLogOff()
		{
			LogOffAction("johnpolling", "password");
		}

		[Test]
		public void UserCanLogOff()
		{
			LogOffAction("andypike", "password");
		}

		private void LogOffAction(string username, string password)
		{
			using (var browser = new IE(BuildUrl("Login", "Index")))
			{
				CompleteLoginForm(browser, username, password);

				browser.Link("LogOff").Click();

				Assert.AreEqual(BuildBaseUrl(), browser.Url);
			}
		}
	}
}