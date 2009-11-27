using NUnit.Framework;
using WatiN.Core;

namespace Membrane.Tests.Acceptance
{
	[TestFixture, Category("Acceptance")]
	public class WhenLoginPageIsRequested : AcceptanceTestBase
	{
		[Test]
		public void ShouldRenderLoginForm()
		{
			using (var browser = new IE(BuildUrl("Login", "Index")))
			{
				Assert.AreEqual("Login :: Membrane", browser.Title);
				Assert.IsTrue(browser.ContainsText(string.Format("Please login with your Membrane Account")));
				Assert.AreEqual(2, browser.Form("loginDetails").Labels.Length);
				Assert.AreEqual("Login", browser.Form("loginDetails").Button("submit").Text);

			}
		}

		[Test]
		public void CanFailLoginAndDisplayErrorMessage()
		{
			using (var browser = new IE(BuildUrl("Login", "Index")))
			{
				// Fill in the form
				CompleteLoginForm(browser, "pollingj", "password2");

				// Now check that the error message is shown and we are still on the login index page
				Assert.AreEqual(BuildUrl("Login", "Index"), browser.Url);
				Assert.IsTrue(browser.ContainsText("Username or Password not recognised"));
			}
		}

		[Test]
		public void CanLoginAsAdministrator()
		{
			using (var browser = new IE(BuildUrl("Login", "Index")))
			{
				// Fill in the form
				CompleteLoginForm(browser, "johnpolling", "password");

				// Now check that the error message is shown and we are still on the login index page
				Assert.AreEqual(BuildUrl("Administrator", "Home", "Index"), browser.Url);
			}
		}

		[Test]
		public void CanLoginAsUser()
		{
			using (var browser = new IE(BuildUrl("Login", "Index")))
			{
				// Fill in the form
				CompleteLoginForm(browser, "andypike", "password");

				// Now check that the error message is shown and we are still on the login index page
				Assert.AreEqual(BuildUrl("Home", "Index"), browser.Url);
			}
		}
	}
}