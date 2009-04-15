using NUnit.Framework;
using WatiN.Core;

namespace Membrane.Tests.Acceptance.Administrator
{
	[TestFixture, Category("Acceptance")]
	public class WhenNavigationTypesPageIsRequested : AcceptanceTestBase
	{
		private IE browser;

		private string newNavTypeUrl;
		private string editNavTypeUrl;
		private string listNavTypeUrl;

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			browser = new IE(BuildUrl("Login", "Index"));
			CompleteLoginForm(browser, "johnpolling", "password");

			listNavTypeUrl = BuildUrl("Administrator", "NavigationTypes", "List");
			newNavTypeUrl = BuildUrl("Administrator", "NavigationTypes", "New");
			editNavTypeUrl = BuildUrl("Administrator", "NavigationTypes", "Edit");
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			browser.Dispose();
		}

		[Test]
		public void AdministratorCanViewNavigationTypesList()
		{
			browser.Link("NavigationTypes").Click();
			browser.WaitForComplete();

			Assert.IsTrue(browser.ContainsText("Primary Navigation"));
			Assert.AreEqual(1, browser.Table("data").TableBodies[0].TableRows.Length);
			Assert.AreEqual(listNavTypeUrl, browser.Url);
		}

		[Test]
		public void AdministratorCanViewNewNavigationTypeForm()
		{
			browser.GoTo(listNavTypeUrl);
			browser.Link("New").Click();
			browser.WaitForComplete();

			Assert.AreEqual(newNavTypeUrl, browser.Url);
			Assert.IsTrue(browser.Form("entryForm").Exists);
		}

		[Test]
		public void AdministratorCanSuccessfullyCompleteNewNavigationTypeForm()
		{
			browser.GoTo(newNavTypeUrl);

			browser.TextField("item_Name").TypeText("Secondary Navigation");
			browser.Button("submit").Click();

			browser.WaitForComplete();

			Assert.AreEqual(listNavTypeUrl, browser.Url);
			Assert.AreEqual(2, browser.Table("data").TableBodies[0].TableRows.Length);
			Assert.IsTrue(browser.ContainsText("Secondary Navigation"));
		}

		[Test]
		public void NewNavigationTypeFormCanFailValidation()
		{
			browser.GoTo(newNavTypeUrl);

			FailFormValidation(newNavTypeUrl);
		}

		[Test]
		public void AdministratorCanViewEditForm()
		{
			GoToEditForm();

			Assert.IsTrue(browser.Url.Contains(editNavTypeUrl));
			Assert.AreEqual("Primary Navigation", browser.TextField("item_Name").Text);
		}

		[Test]
		public void AdministratorCanSuccessfullyCompleteEditForm()
		{
			GoToEditForm();

			browser.TextField("item_Name").Clear();
			browser.TextField("item_Name").TypeText("New Pri Navigation");

			browser.Button("submit").Click();

			browser.WaitForComplete();

			Assert.AreEqual(listNavTypeUrl, browser.Url);
			Assert.IsFalse(browser.ContainsText("Primary Navigation"));
			Assert.IsTrue(browser.ContainsText("New Pri Navigation"));
		}

		[Test]
		public void EditNavigationTypeFormCanFailValidation()
		{
			GoToEditForm();

			browser.TextField("item_Name").Clear();

			FailFormValidation(editNavTypeUrl);
		}

		[Test]
		public void AdministratorCanShowDeleteConfirmation()
		{
			GoToDeleteConfirmationPage();

			Assert.IsTrue(browser.Url.Contains(BuildUrl("Administrator", "NavigationTypes", "ConfirmDelete")));
			Assert.IsTrue(browser.ContainsText("Are you certain you wish to delete"));
		}



		[Test]
		public void AdministratorCanSuccessfullyDeleteNavigationType()
		{
			GoToDeleteConfirmationPage();

			browser.Button("submit").Click();

			browser.WaitForComplete();

			Assert.AreEqual(listNavTypeUrl, browser.Url);
			Assert.IsFalse(browser.ContainsText("Secondary Navigation"));
		}

		private void GoToDeleteConfirmationPage()
		{
			browser.GoTo(listNavTypeUrl);
			browser.Table("data").TableBodies[0].TableRows[1].Links[1].Click();

			browser.WaitForComplete();
		}

		private void GoToEditForm()
		{
			browser.GoTo(listNavTypeUrl);
			browser.Table("data").TableBodies[0].TableRows[0].Links[0].Click();

			browser.WaitForComplete();
		}


		private void FailFormValidation(string formUrl)
		{
			browser.TextField("item_Name").Click();
			browser.Button("submit").Click();

			Assert.IsTrue(browser.Url.Contains(formUrl));
			Assert.AreEqual("error", browser.TextField("item_Name").ClassName);

			/*Assert.IsTrue(browser.Div("errors").Exists);
			Assert.IsTrue(browser.ContainsText("There was a problem with the form"));*/
		}
	}
}