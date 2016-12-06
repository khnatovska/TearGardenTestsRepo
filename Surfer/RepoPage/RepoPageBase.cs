using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Surfer;

namespace RepoPage
{
    public class RepoPageBase
    {
        public Root root { get; }
        public string AddRepoBtnName { get; } = "Add New DVM Repository";
        public string URL { get; } = "Repository";

        public RepoPageBase()
        {
            root = new Root();
            root.StartBrowser("Chrome");
            root.OpenCoreAdmin();
            root.NavigateFromBase(URL);
        }

        public void Close()
        {
            root.CloseBrowser();
        }

        public void VerifyDefaultRepoBtn()
        {
            var repoBtn = root.Find(By.CssSelector(CssSelectors.NewRepoBtn));
            Assert.AreEqual(AddRepoBtnName, repoBtn.Text);
        }

        public RepoDialog ClickAddNewRepoBtn()
        {
            var repoBtn = root.WaitAndFind(By.CssSelector(CssSelectors.NewRepoBtn));
            repoBtn.Click();
            var repoDialog = root.WaitAndFind(By.CssSelector(CssSelectors.uiDialogLevelOne));
            return new RepoDialog(repoDialog, root.driver);
        }
    }
}
