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
using Surfer.Models;
using Surfer.PageObjects;

namespace SurferTests
{
    [TestFixture]
    public class RepoPageTests
    {
        private RepoPage repoPage;
        private RepoDialog repoDialog;
        private StorageLocationDialog storageLocationDialog;
        
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

        [TestCase("K:\\repo")]
        public void CreateLocalRepositorySingleStorageLocationDefault(string datapath)
        {
            repoDialog = repoPage.ClickAddNewRepoBtn();
            storageLocationDialog = repoDialog.ClickAddNewStorageLocationBtn();
            var datapathField = storageLocationDialog.GetLocalDataPathInputField();
            var metadatapathField = storageLocationDialog.GetLocalMetadataPathInputField();
            datapathField.TypeIn(datapath);
            metadatapathField.TypeIn(datapath);
            storageLocationDialog.Save();
            storageLocationDialog = repoDialog.VerifyStorageDialogDisappearance();
            var repoModel = repoDialog.Create();
            repoDialog = repoPage.VerifyRepoDialogDisappearance();

            //get reporow, check elements agains repomodel
        }

        [TearDown]
        public void RepoPageTearDown()
        {
            repoPage.Close();
        }
    }
}
