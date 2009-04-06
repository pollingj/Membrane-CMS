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
			CompleteLoginForm(browser, "pollingj", "password");

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
			Assert.AreEqual(1, browser.Table("data").TableRows.Length);
			Assert.AreEqual(listGroupUrl, browser.Url);
		}

		[Test]
		public void AdministratorCanViewNewUserGroupForm()
		{
			browser.GoTo(listGroupUrl);
			browser.Link("New").Click();
			browser.WaitForComplete();

			Assert.IsTrue(browser.ContainsText("New User Group"));
			Assert.AreEqual(newGroupUrl, browser.Url);
		}

		[Test]
		public void AdministratorCanSuccessfullyCompleteNewUserGroupForm()
		{
			browser.GoTo(newGroupUrl);

			browser.TextField("group.Name").TypeText("News Group");
			browser.Button("submit").Click();

			browser.WaitForComplete();

			Assert.AreEqual(listGroupUrl, browser.Url);
			Assert.AreEqual(2, browser.Table("data").TableRows.Length);
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
			GoToEditForm();

			Assert.AreEqual(editGroupUrl, browser.Url);
			Assert.AreEqual("Blogging Group", browser.TextField("group.Name").Text);
		}

		[Test]
		public void AdministratorCanSuccessfullyCompleteEditForm()
		{
			GoToEditForm();

			browser.TextField("group.Name").Clear();
			browser.TextField("group.Name").TypeText("Blog Editing Group");

			browser.Button("submit").Click();

			browser.WaitForComplete();

			Assert.AreEqual(listGroupUrl, browser.Url);
			Assert.IsFalse(browser.ContainsText("Blogging Group"));
			Assert.IsTrue(browser.ContainsText("Blog Editing Group"));
		}

		[Test]
		public void EditUserGroupFormCanFailValidation()
		{
			GoToEditForm();

			browser.TextField("group.Name").Clear();

			FailFormValidation(editGroupUrl);
		}

		[Test]
		public void AdministratorCanShowDeleteConfirmation()
		{
			browser.GoTo(listGroupUrl);
			browser.Table("data").TableRows[0].Links[1].Click();

			browser.WaitForComplete();

			Assert.AreEqual(BuildUrl("Administrator", "UserGroups", "DeleteConfirmation"), browser.Url);
			Assert.IsTrue(browser.ContainsText("Are you certain you wish to delete"));
		}

		private void GoToEditForm()
		{
			browser.GoTo(listGroupUrl);
			browser.Table("data").TableRows[0].Links[0].Click();

			browser.WaitForComplete();
		}


		private void FailFormValidation(string formUrl)
		{
			browser.TextField("group.Name").Click();
			browser.Button("submit").Click();

			browser.WaitForComplete();

			Assert.Contains(formUrl, browser.Url);
			Assert.IsTrue(browser.Div("errors").Exists);
			Assert.IsTrue(browser.ContainsText("There was a problem with the form"));
		}
	}
}