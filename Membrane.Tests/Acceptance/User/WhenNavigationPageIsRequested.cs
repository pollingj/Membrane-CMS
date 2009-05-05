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
		public void UserCanViewNavigationListAndItemOrderForm()
		{
			browser.Link("Navigation").Click();
			browser.WaitForComplete();

			Assert.IsTrue(browser.ContainsText("Home"));

			Assert.IsTrue(browser.Form("itemOrdering").Exists);
			Assert.IsTrue(browser.Form("itemOrdering").Button("submit").Exists);

			Assert.AreEqual(listNavUrl, browser.Url);
		}

		[Test]
		public void UserCanOnlyMoveDownTopItem()
		{
			//browser.Link("Navigation").Click();
			//browser.WaitForComplete();

			//Assert.IsTrue(browser.Table("data").TableBodies[0]);
		}

		[Test]
		public void UserCanViewNewNavigationForm()
		{
			browser.GoTo(listNavUrl);
			browser.Link("New").Click();
			browser.WaitForComplete();

			Assert.AreEqual(newNavUrl, browser.Url);
			Assert.IsTrue(browser.Form("entryForm").Exists);
		}

		[Test]
		public void UserCanSuccessfullyCompleteNewNavigationForm()
		{
			browser.GoTo(newNavUrl);

			browser.TextField("item_Name").TypeText("How to Find Us");
			browser.SelectList("item_Type_Id").Select("Primary Navigation");
			browser.TextField("item_Title").TypeText("Find out where we are");
			browser.TextField("item_AccessKey").TypeText("4");
			browser.Button("submit").Click();

			browser.WaitForComplete();

			Assert.AreEqual(listNavUrl, browser.Url);
			Assert.IsTrue(browser.ContainsText("How to Find Us"));
		}

		[Test]
		public void NewNavigationFormCanFailValidation()
		{
			browser.GoTo(newNavUrl);

			FailFormValidation(newNavUrl);
		}

		[Test]
		public void UserCanViewEditForm()
		{
			GoToAction(browser, "Edit", "How to Find Us", listNavUrl);

			Assert.IsTrue(browser.Url.Contains(editNavUrl));
			Assert.AreEqual("How to Find Us", browser.TextField("item_Name").Text);
		}

		[Test]
		public void UserCanSuccessfullyCompleteEditForm()
		{
			GoToAction(browser, "Edit", "How to Find Us", listNavUrl);

			browser.TextField("item_Name").Clear();
			browser.TextField("item_Name").TypeText("How to Find Them");
			browser.TextField("item_Title").Clear();
			browser.TextField("item_Title").TypeText("Find out where they are");
			browser.TextField("item_AccessKey").Clear();
			browser.TextField("item_AccessKey").TypeText("5");

			browser.Button("submit").Click();

			browser.WaitForComplete();

			Assert.AreEqual(listNavUrl, browser.Url);
			Assert.IsFalse(browser.ContainsText("How to Find Us"));
			Assert.IsTrue(browser.ContainsText("How to Find Them"));
		}

		[Test]
		public void EditNavigationFormCanFailValidation()
		{
			GoToAction(browser, "Edit", "How to Find Them", listNavUrl);

			browser.TextField("item_Name").Clear();

			FailFormValidation(editNavUrl);
		}

		[Test]
		public void UserCanShowDeleteConfirmation()
		{
			GoToAction(browser, "Delete", "How to Find Them", listNavUrl);

			Assert.IsTrue(browser.Url.Contains(BuildUrl("Navigation", "ConfirmDelete")));
			Assert.IsTrue(browser.ContainsText("Are you certain you wish to delete"));
		}



		[Test]
		public void UserCanSuccessfullyDeleteNavigation()
		{
			GoToAction(browser, "Delete", "How to Find Them", listNavUrl);

			browser.Button("submit").Click();

			browser.WaitForComplete();

			Assert.AreEqual(listNavUrl, browser.Url);
			Assert.IsFalse(browser.ContainsText("About Them"));
		}

		private void GoToDeleteConfirmationPage()
		{
			browser.GoTo(listNavUrl);
			browser.Table("data").TableBodies[0].TableRow(Find.ByText("How to Find Us")).Link(Find.ByText("Delete")).Click();

			browser.WaitForComplete();
		}

		private void GoToEditForm()
		{
			browser.GoTo(listNavUrl);
			browser.Table("data").TableBodies[0].TableRow(Find.ByText("How to Find Us")).Link(Find.ByText("Edit")).Click();

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