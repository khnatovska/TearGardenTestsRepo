//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NUnit.Framework;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Firefox;
//using OpenQA.Selenium.Support.UI;
//using Surfer;
//using RepoPage;


//namespace RepoPageUnitTests
//{
//    [TestFixture]
//    public class RepoPageTests
//    {
//        private RepoPageBase repoPage;
//        private RepoDialog repoDialog;

//        [OneTimeSetUp]
//        public void OneTimeSetUpMethod()
//        {
//            repoPage = new RepoPageBase();
//        }

//        [Test]
//        public void TestMethod1()
//        {
//            repoPage.ClickAddNewRepoBtn();
//        }

//        [Test]
//        public void TestMethod2()
//        {
//            repoPage.VerifyDefaultRepoBtn();
//        }

//        [Test]
//        public void TestMethod3()
//        {
//            var dialog = repoPage.ClickAddNewRepoBtn();
//            dialog.VerifyTitle();
//        }

//        [TearDown]
//        public void TearDownMethod()
//        {
//            repoDialog?.Close();
//        }

//        [OneTimeTearDown]
//        public void OneTimeTearDown()
//        {
//            repoPage.Close();
//        }
//    }
//}
