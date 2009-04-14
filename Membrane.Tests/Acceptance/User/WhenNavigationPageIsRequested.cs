using NUnit.Framework;
using WatiN.Core;

namespace Membrane.Tests.Acceptance.User
{
	[TestFixture, Category("Acceptance")]
	public class WhenNavigationPageIsRequested : AcceptanceTestBase
	{
		private IE browser;

		private string newNavUrl;
		private string editNavUrl;
		private string listNavUrl;

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			browser = new IE(BuildUrl("Login", "Index"));
			CompleteLoginForm(browser, "andypike", "password");

			listNavUrl = BuildUrl("Navigation", "List");
			newNavUrl = BuildUrl("Navigation", "New");
			editNavUrl = BuildUrl("Navigation", "Edit");
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			browser.Dispose();
		}

		[Test]
		public void UserCanViewNavigationTypesList()
		{
			browser.Link("Navigation").Click();
			browser.WaitForComplete();

			Assert.IsTrue(browser.ContainsText("Home"));
			Assert.AreEqual(1, browser.Table("data").TableBodies[0].TableRows.Length);
			Assert.AreEqual(listNavUrl, browser.Url);
		}

		[Test]
		public void UserCanViewNewNavigationTypeForm()
		{
			browser.GoTo(listNavUrl);
			browser.Link("New").Click();
			browser.WaitForComplete();

			Assert.AreEqual(newNavUrl, browser.Url);
			Assert.IsTrue(browser.Form("entryForm").Exists);
		}

		[Test]
		public void UserCanSuccessfullyCompleteNewNavigationTypeForm()
		{
			browser.GoTo(newNavUrl);

			browser.TextField("item_Name").TypeText("About Us");
			browser.SelectList("item_Type_Id").Select("Primary Navigation");
			browser.TextField("item_Title").TypeText("Find out more about us");
			browser.TextField("item_AccessKey").TypeText("2");
			browser.Button("submit").Click();

			browser.WaitForComplete();

			Assert.AreEqual(listNavUrl, browser.Url);
			Assert.AreEqual(2, browser.Table("data").TableBodies[0].TableRows.Length);
			Assert.IsTrue(browser.ContainsText("About Us"));
		}

		[Test]
		public void NewNavigationTypeFormCanFailValidation()
		{
			browser.GoTo(newNavUrl);

			FailFormValidation(newNavUrl);
		}

		[Test]
		public void UserCanViewEditForm()
		{
			GoToEditForm();

			Assert.IsTrue(browser.Url.Contains(editNavUrl));
			Assert.AreEqual("About Us", browser.TextField("item_Name").Text);
		}

		[Test]
		public void UserCanSuccessfullyCompleteEditForm()
		{
			GoToEditForm();

			browser.TextField("item_Name").Clear();
			browser.TextField("item_Name").TypeText("About Them");

			browser.Button("submit").Click();

			browser.WaitForComplete();

			Assert.AreEqual(listNavUrl, browser.Url);
			Assert.IsFalse(browser.ContainsText("About Us"));
			Assert.IsTrue(browser.ContainsText("About Them"));
		}

		[Test]
		public void EditNavigationTypeFormCanFailValidation()
		{
			GoToEditForm();

			browser.TextField("item_Name").Clear();

			FailFormValidation(editNavUrl);
		}

		[Test]
		public void UserCanShowDeleteConfirmation()
		{
			GoToDeleteConfirmationPage();

			Assert.IsTrue(browser.Url.Contains(BuildUrl("Navigation", "ConfirmDelete")));
			Assert.IsTrue(browser.ContainsText("Are you certain you wish to delete"));
		}



		[Test]
		public void UserCanSuccessfullyDeleteGroup()
		{
			GoToDeleteConfirmationPage();

			browser.Button("submit").Click();

			browser.WaitForComplete();

			Assert.AreEqual(listNavUrl, browser.Url);
			Assert.IsFalse(browser.ContainsText("About Them"));
		}

		private void GoToDeleteConfirmationPage()
		{
			browser.GoTo(listNavUrl);
			browser.Table("data").TableBodies[0].TableRows[0].Links[1].Click();

			browser.WaitForComplete();
		}

		private void GoToEditForm()
		{
			browser.GoTo(listNavUrl);
			browser.Table("data").TableBodies[0].TableRows[1].Links[0].Click();

			browser.WaitForComplete();
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