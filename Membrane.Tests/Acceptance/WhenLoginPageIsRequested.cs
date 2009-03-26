using NUnit.Framework;
using WatiN.Core;

namespace Membrane.Tests.Acceptance
{
	public class WhenLoginPageIsRequested : AcceptanceTestBase
	{
		[Test, Category("Acceptance")]
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
	}
}