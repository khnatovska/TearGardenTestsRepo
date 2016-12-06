using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Surfer.PageObjects
{
    public class RepoPage
    {
        public Root Root { get; }
        public IWebDriver Driver { get; set; }
        public string AddRepoBtnName { get; } = "Add New DVM Repository";
        public string URL { get; } = "Repository";

        public RepoPage()
        {
            Root = new Root();
            Driver = Root.StartBrowser("Chrome");
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
    }
}
