using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Surfer.PageObjects
{
    public class RepoPage
    {
        public Root Root { get; }
        public IWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; set; }
        public string AddRepoBtnName { get; } = "Add New DVM Repository";
        public string URL { get; } = "Repository";

        public RepoPage()
        {
            Root = new Root();
            Driver = Root.StartBrowser("Chrome");
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            Root.OpenCoreAdmin();
            Root.NavigateFromBase(URL);
        }

        public void Close()
        {
            Root.CloseBrowser();
        }

        public void Refresh()
        {
            Root.RefreshPage();
            Root.RefreshPage();
        }

        public void VerifyDefaultRepoBtn()
        {
            var repoBtn = Driver.FindByCss(CssSelectors.NewRepoBtn);
            Assert.AreEqual(AddRepoBtnName, repoBtn.Text);
        }

        public RepoDialog ClickAddNewRepoBtn()
        {
            var repoBtn = Driver.FindByCssClickable(CssSelectors.NewRepoBtn);
            repoBtn.Click();
            var repoDialog = Driver.FindByCssVisible(CssSelectors.uiDialogLevelOne);
            return new RepoDialog(repoDialog, Driver);
        }

        public RepoDialog VerifyRepoDialogDisappearance()
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelectors.uiDialogLevelOne)));
            return null;
        }

        public RepoTableRow GetRepoTableRow(int rowNumber)
        {
            //currently works with rownumber only for the single repo in the list
            var rowSelector = CssSelectors.GetChild(CssSelectors.RepoTableRow, rowNumber + 1);
            Driver.WaitForAjax();
            Wait.Until(ExpectedConditions.ElementExists(By.CssSelector(rowSelector)));
            var table = Driver.FindByCss(CssSelectors.RepoTable);
            return new RepoTableRow(Driver, table, rowSelector);
        }
    }
}
