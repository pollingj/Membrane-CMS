using NUnit.Framework;
using WatiN.Core;

namespace Membrane.Tests.Acceptance.Administrator
{
	[TestFixture, Category("Acceptance")]
	public class WhenUserGroupsPageIsRequested : AcceptanceTestBase
	{
		private IE browser;

		private string newGroupUrl;
		private string editGroupUrl;
		private string listGroupUrl;

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			browser = new IE(BuildUrl("Login", "Index"));
			CompleteLoginForm(browser, "johnpolling", "password");

			listGroupUrl = BuildUrl("Administrator", "UserGroups", "List");
			newGroupUrl = BuildUrl("Administrator", "UserGroups", "New");
			editGroupUrl = BuildUrl("Administrator", "UserGroups", "Edit");
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			browser.Dispose();
		}

		[Test]
		public void AdministratorCanViewUserGroupsList()
		{
			browser.Link("UserGroups").Click();
			browser.WaitForComplete();

			Assert.IsTrue(browser.ContainsText("Blogging Group"));
			CheckDataListLILength(browser, 3);
			Assert.AreEqual(listGroupUrl, browser.Url);
		}

		[Test]
		public void AdministratorCanViewNewUserGroupForm()
		{
			browser.GoTo(listGroupUrl);
			browser.Link("New").Click();
			browser.WaitForComplete();

			Assert.AreEqual(newGroupUrl, browser.Url);
			Assert.IsTrue(browser.Form("entryForm").Exists);
		}

		[Test]
		public void AdministratorCanSuccessfullyCompleteNewUserGroupForm()
		{
			browser.GoTo(newGroupUrl);

			browser.TextField("item_Name").TypeText("News Group");
			browser.Button("submit").Click();

			browser.WaitForComplete();

			Assert.AreEqual(listGroupUrl, browser.Url);
			CheckDataListLILength(browser, 6);
			Assert.IsTrue(browser.ContainsText("News Group"));
		}

		[Test]
		public void NewUserGroupFormCanFailValidation()
		{
			browser.GoTo(newGroupUrl);

			FailFormValidation(newGroupUrl);
		}

		[Test]
		public void AdministratorCanViewEditForm()
		{
			GoToAction(browser, "Edit", "News Group", listGroupUrl);

			Assert.IsTrue(browser.Url.Contains(editGroupUrl));
			Assert.AreEqual("News Group", browser.TextField("item_Name").Text);
		}

		[Test]
		public void AdministratorCanSuccessfullyCompleteEditForm()
		{
			GoToAction(browser, "Edit", "News Group", listGroupUrl);

			browser.TextField("item_Name").Clear();
			browser.TextField("item_Name").TypeText("News Editing Group");

			browser.Button("submit").Click();

			browser.WaitForComplete();

			Assert.AreEqual(listGroupUrl, browser.Url);
			Assert.IsFalse(browser.ContainsText("News Group"));
			Assert.IsTrue(browser.ContainsText("News Editing Group"));
		}

		[Test]
		public void EditUserGroupFormCanFailValidation()
		{
			GoToAction(browser, "Edit", "News Editing Group", listGroupUrl);

			browser.TextField("item_Name").Clear();

			FailFormValidation(editGroupUrl);
		}

		[Test]
		public void AdministratorCanShowDeleteConfirmation()
		{
			GoToAction(browser, "Delete", "News Editing Group", listGroupUrl);

			Assert.IsTrue(browser.Url.Contains(BuildUrl("Administrator", "UserGroups", "ConfirmDelete")));
			Assert.IsTrue(browser.ContainsText("Are you certain you wish to delete"));
		}



		[Test]
		public void AdministratorCanSuccessfullyDeleteGroup()
		{
			GoToAction(browser, "Delete", "News Editing Group", listGroupUrl);

			browser.Button("submit").Click();

			browser.WaitForComplete();

			Assert.AreEqual(listGroupUrl, browser.Url);
			Assert.IsFalse(browser.ContainsText("News Editing Group"));
		}


		private void FailFormValidation(string formUrl)
		{
			browser.TextField("item_Name").Click();
			browser.Button("submit").Click();

			Assert.IsTrue(browser.Url.Contains(formUrl));
			Assert.AreEqual("error", browser.TextField("item_Name").ClassName);

		}
	}
}