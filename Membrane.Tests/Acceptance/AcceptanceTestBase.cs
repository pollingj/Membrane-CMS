using System.Configuration;
using NUnit.Framework;
using WatiN.Core;

namespace Membrane.Tests.Acceptance
{
	public class AcceptanceTestBase
	{	
		protected string server;
		protected int port;
		protected string extension;
		protected string productName;

		protected AcceptanceTestBase()
		{
			server = ConfigurationManager.AppSettings["server"] ?? "localhost";
			extension = ConfigurationManager.AppSettings["extension"] ?? "rails";
			extension = extension.Length == 0 ? "" : "." + extension;
			port = 80;

			if (ConfigurationManager.AppSettings["port"] != null)
			{
				port = int.Parse(ConfigurationManager.AppSettings["port"]);
			}
		}



		protected string BuildUrl(string controller, string action)
		{
			return string.Format("http://{0}:{1}/{2}/{3}{4}", server, port, controller, action, extension);
		}

		protected string BuildUrl(string area, string controller, string action)
		{
			return string.Format("http://{0}:{1}/{2}/{3}/{4}{5}", server, port, area, controller, action, extension);
		}

		protected string BuildBaseUrl()
		{
			return string.Format("http://{0}:{1}/", server, port);
		}


		protected void CompleteLoginForm(IE browser, string username, string password)
		{
			browser.TextField("login_username").Clear();
			browser.TextField("login_username").TypeText(username);
			browser.TextField("login_password").Clear();
			browser.TextField("login_password").TypeText(password);

			// Submit the form
			browser.Form("loginDetails").Submit();

			// Wait for the post back to complete
			browser.WaitForComplete();
		}

		protected void GoToAction(IE browser, string action, string itemName, string listActionUrl)
		{
			browser.GoTo(listActionUrl);
			//browser.Table("data").TableBodies[0].TableRows[0].Links[0].Click();

			var actionElements = ((ElementsContainer)browser.Element(Find.ById("data"))).Elements.Filter(Find.ByClass("actions"));

			foreach (var element in actionElements)
			{
				if (((ElementsContainer)element).PreviousSibling.InnerHtml == itemName)
				{
					var links = ((ElementsContainer)element).Links.Filter(Find.ByUrl(href => href.Contains(action)));
					links[0].Click();
					break;
				}
			}
			browser.WaitForComplete();
		}

		protected void CheckDataListLILength(IE browser, int compareLength)
		{
			var ulElement = (ElementsContainer)browser.Element("ul", Find.ById("data"));
			Assert.AreEqual(compareLength, ulElement.Elements.Filter(Find.By("tagName", "LI")).Length);
		}
	}
}