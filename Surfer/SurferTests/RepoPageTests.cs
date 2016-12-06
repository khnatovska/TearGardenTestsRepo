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
using Surfer.PageObjects;

namespace SurferTests
{
    [TestFixture]
    public class RepoPageTests
    {
        private RepoPage repoPage;
        private RepoDialog repoDialog;

        [SetUp]
        public void RepoPageSetupMethod()
        {
            repoPage = new RepoPage();
        }

        [Test]
        public void MainRepositoryIsDVM()
        {
            repoPage.VerifyDefaultRepoBtn();
        }

        [TearDown]
        public void RepoPageTearDown()
        {
            repoPage.Close();
        }
    }
}
