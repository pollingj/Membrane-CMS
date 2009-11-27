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
			CheckDataListLILength(browser, 3);
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
			CheckDataListLILength(browser, 6);
			Assert.IsTrue(browser.ContainsText("Secondary Navigation"));
		}

		[Test]
		public void NewNavigationTypeFormCanFailValidation()
		{
			browser.GoTo(newNavTypeUrl);

			failFormValidation(newNavTypeUrl);
		}

		[Test]
		public void AdministratorCanViewEditForm()
		{
			GoToAction(browser, "Edit", "Secondary Navigation", listNavTypeUrl);

			Assert.IsTrue(browser.Url.Contains(editNavTypeUrl));
			Assert.AreEqual("Secondary Navigation", browser.TextField("item_Name").Text);
		}

		[Test]
		public void AdministratorCanSuccessfullyCompleteEditForm()
		{
			GoToAction(browser, "Edit", "Secondary Navigation", listNavTypeUrl);

			browser.TextField("item_Name").Clear();
			browser.TextField("item_Name").TypeText("Tertiary Navigation");

			browser.Button("submit").Click();

			browser.WaitForComplete();

			Assert.AreEqual(listNavTypeUrl, browser.Url);
			Assert.IsFalse(browser.ContainsText("Secondary Navigation"));
			Assert.IsTrue(browser.ContainsText("Tertiary Navigation"));
		}

		[Test]
		public void EditNavigationTypeFormCanFailValidation()
		{
			GoToAction(browser, "Edit", "Tertiary Navigation", listNavTypeUrl);

			browser.TextField("item_Name").Clear();

			failFormValidation(editNavTypeUrl);
		}

		[Test]
		public void AdministratorCanShowDeleteConfirmation()
		{
			GoToAction(browser, "Delete", "Tertiary Navigation", listNavTypeUrl);

			Assert.IsTrue(browser.Url.Contains(BuildUrl("Administrator", "NavigationTypes", "ConfirmDelete")));
			Assert.IsTrue(browser.ContainsText("Are you certain you wish to delete"));
		}



		[Test]
		public void AdministratorCanSuccessfullyDeleteNavigationType()
		{
			GoToAction(browser, "Delete", "Tertiary Navigation", listNavTypeUrl);

			browser.Button("submit").Click();

			browser.WaitForComplete();

			Assert.AreEqual(listNavTypeUrl, browser.Url);
			Assert.IsFalse(browser.ContainsText("Tertiary Navigation"));
		}

		private void failFormValidation(string formUrl)
		{
			browser.TextField("item_Name").Click();
			browser.Button("submit").Click();

			Assert.IsTrue(browser.Url.Contains(formUrl));
			Assert.AreEqual("error", browser.TextField("item_Name").ClassName);
		}


	}
}