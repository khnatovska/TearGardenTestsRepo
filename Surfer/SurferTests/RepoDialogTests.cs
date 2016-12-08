using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Surfer;
using Surfer.PageObjects;

namespace SurferTests
{
    [TestFixture]
    public class RepoDialogTests
    {
        private RepoPage repoPage;
        private RepoDialog repoDialog;
        private StorageLocationDialog storageLocationDialog;
        private MessageBox messageBox;

        [OneTimeSetUp]
        public void RepoDialogOneTimeSetUpMethod()
        {
            repoPage = new RepoPage();
        }

        [SetUp]
        public void RepoDialogSetUpMethod()
        {
            repoDialog = repoPage.ClickAddNewRepoBtn();
        }

        [TearDown]
        public void RepoDialogTearDownMethod()
        {
            repoPage.Refresh();
        }

        [OneTimeTearDown]
        public void RepoDialogOneTimeTearDownMethod()
        {
            repoPage.Close();
        }

        [Test]
        public void RepoDialogTitleIsAddNewRepository()
        {
            repoDialog.VerifyTitle();
        }

        [Test]
        public void AddStorageLocationBtbPresent()
        {
            repoDialog.VerifyStorageLocationBtn();
        }

        [Test]
        public void WarningWhenCreateRepositoryWithoutStorageLocation()
        {
            repoDialog.Create();
            messageBox = repoDialog.VerifyWarningAppearance();
            messageBox.HeaderEquals("Warning");
            messageBox.MessageEquals("You must add at least one storage location");
        }

        [Test]
        public void UnableCreateRepositoryWithEmptyName()
        {
            var repoName = repoDialog.GetRepoNameInputField();
            repoName.ClearInput();
            repoDialog.Create();
            repoName.InputValidationError();
            repoName.FormHasError();
        }

        [TestCaseSource(typeof(Root), "invalidRepoNames")]
        public void UnableCreateRepositoryWithInvalidName(string invalidName)
        {
            var repoName = repoDialog.GetRepoNameInputField();
            repoName.TypeIn(invalidName);
            repoDialog.Create();
            repoName.InputValidationError();
            repoName.FormHasError();
        }

        [TestCase("valid name")]
        public void INtentionallyFailedTestUnableCreateRepositoryWithInvalidName(string invalidName) //SHOULD FAIL TO CONFIRM THAT IT WON'T FAIL THE ENTIRE SUITE
        {
            var repoName = repoDialog.GetRepoNameInputField();
            repoName.TypeIn(invalidName);
            repoDialog.Create();
            messageBox = repoDialog.VerifyWarningAppearance();
            repoName.InputValidationError();
            repoName.FormHasError();
        }

        [TestCase(40)]
        public void UnableEnterTooLongRepositoryName(int limit)
        {
            var repoName = repoDialog.GetRepoNameInputField();
            var tooLongName = new string('q', limit + 1);
            repoName.TypeIn(tooLongName);
            Assert.AreEqual(limit, repoName.GetInputLength());
        }

        [Test]
        public void UnableCreateRepositoryWithEmptyConcurrentOperations()
        {
            var concurOperations = repoDialog.GetConcurOperationsInputField();
            concurOperations.ClearInput();
            repoDialog.Create();
            concurOperations.FormHasError();
        }

        [TestCaseSource(typeof(Root), "invalidRepoConcurOperations")]
        public void UnableCreateRepositoryWithInvalidConcurrentOperations(string invalidValue)
        {
            var concurOperations = repoDialog.GetConcurOperationsInputField();
            concurOperations.TypeIn(invalidValue);
            repoDialog.Create();
            concurOperations.FormHasError();
        }

        [TestCase("J:\\repo", "J:\\meta")]
        [TestCase("J:\\repo2", "J:\\meta2")]
        [TestCase("J:\\repo3", "J:\\meta3")]
        [TestCase("J:\\repo4", "J:\\meta4")]
        public void RemoveStorageLocationNewRepo(string datapath, string metadatapath)
        {
            storageLocationDialog = repoDialog.ClickAddNewStorageLocationBtn();
            var datapathField = storageLocationDialog.GetLocalDataPathInputField();
            var metadatapathField = storageLocationDialog.GetLocalMetadataPathInputField();
            datapathField.TypeIn(datapath);
            metadatapathField.TypeIn(metadatapath);
            storageLocationDialog.Save();
            storageLocationDialog = repoDialog.VerifyStorageDialogDisappearance();
            var storageLocationRow = repoDialog.GetStorageLocationTableRow(1);
            var actions = storageLocationRow.GetActionsDropdown();
            actions.Open();
            actions.ClickDelete(); 
            repoDialog.VerifyStorageLocationsConfigurationNoDataToDisplay();
        }

        [TestCase("J:\\repo", "J:\\meta", "200", "J:\\repo2", "210")]
        [TestCase("J:\\repo2", "J:\\meta2", "100", "J:\\repo", "150")]
        [TestCase("J:\\repo3", "J:\\meta3", "200", "K:\\repo2", "90")]
        [TestCase("J:\\repo4", "J:\\meta4", "100", "K:\\repo2", "210")]
        public void EditLocalStorageLocationNewRepo(string datapath, string metadatapath, string size,
            string newdatapath, string newsize)
        {
            storageLocationDialog = repoDialog.ClickAddNewStorageLocationBtn();
            var datapathField = storageLocationDialog.GetLocalDataPathInputField();
            var metadatapathField = storageLocationDialog.GetLocalMetadataPathInputField();
            var sizeField = storageLocationDialog.GetSizeInputField();
            datapathField.TypeIn(datapath);
            metadatapathField.TypeIn(metadatapath);
            sizeField.TypeIn(size);
            storageLocationDialog.Save();
            storageLocationDialog = repoDialog.VerifyStorageDialogDisappearance();
            var storageLocationRow = repoDialog.GetStorageLocationTableRow(1);
            var actions = storageLocationRow.GetActionsDropdown();
            actions.Open();
            storageLocationDialog = actions.ClickEdit(); 
            storageLocationDialog.VerifyTitle();
            datapathField = storageLocationDialog.GetLocalDataPathInputField();
            metadatapathField = storageLocationDialog.GetLocalMetadataPathInputField();
            sizeField = storageLocationDialog.GetSizeInputField();
            datapathField.InputEquals(datapath);
            metadatapathField.InputEquals(metadatapath);
            sizeField.InputEqualsWithTwoDecimalPoints(size); 
            datapathField.TypeIn(newdatapath);
            sizeField.TypeIn(newsize);
            storageLocationDialog.Save();
            storageLocationDialog = repoDialog.VerifyStorageDialogDisappearance();
            storageLocationRow = repoDialog.GetStorageLocationTableRow(1);
            storageLocationRow.DataPathEquals(newdatapath);
            storageLocationRow.MetadataPathEquals(metadatapath);
            storageLocationRow.SizeInGbEquals(newsize);
        }

        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "20", "\\\\10.35.178.196\\sharedrepo2", "21")]
        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "4", "\\\\10.35.178.196\\sharedrepo2", "3")]
        [TestCase("\\\\10.35.178.196\\sharedrepo2", "administrator", "123asdQ", "220", "\\\\10.35.178.196\\sharedrepo", "21")]
        [TestCase("\\\\10.35.178.196\\sharedrepo2", "administrator", "123asdQ", "134", "\\\\10.35.178.196\\sharedrepo", "13")]
        public void EditNetworkStorageLocationNewRepo(string networkpath, string username, string password, string size,
            string newnetworkpath, string newsize)
        {
            storageLocationDialog = repoDialog.ClickAddNewStorageLocationBtn();
            storageLocationDialog.SelectNetworkPathRadio();
            storageLocationDialog.NetworkPathRadioIsSelected();
            var networkpathField = storageLocationDialog.GetUncPathInputField();
            var usernameField = storageLocationDialog.GetUsernameInputField();
            var passwordField = storageLocationDialog.GetPasswordInputField();
            var sizeField = storageLocationDialog.GetSizeInputField();
            networkpathField.TypeIn(networkpath);
            usernameField.TypeIn(username);
            passwordField.TypeIn(password);
            sizeField.TypeIn(size);
            storageLocationDialog.Save();
            storageLocationDialog = repoDialog.VerifyStorageDialogDisappearance();
            var storageLocationRow = repoDialog.GetStorageLocationTableRow(1);
            var actions = storageLocationRow.GetActionsDropdown();
            actions.Open();
            storageLocationDialog = actions.ClickEdit(); 
            storageLocationDialog.VerifyTitle();
            storageLocationDialog.NetworkPathRadioIsSelected();
            networkpathField = storageLocationDialog.GetUncPathInputField();
            usernameField = storageLocationDialog.GetUsernameInputField();
            passwordField = storageLocationDialog.GetPasswordInputField();
            sizeField = storageLocationDialog.GetSizeInputField();
            networkpathField.InputEquals(networkpath);
            usernameField.InputEquals(username);
            sizeField.InputEqualsWithTwoDecimalPoints(size);
            networkpathField.TypeIn(newnetworkpath);
            passwordField.TypeIn(password);
            sizeField.TypeIn(newsize);
            storageLocationDialog.Save();
            storageLocationDialog = repoDialog.VerifyStorageDialogDisappearance();
            storageLocationRow = repoDialog.GetStorageLocationTableRow(1);
            storageLocationRow.DataPathEquals(newnetworkpath);
            storageLocationRow.MetadataPathEquals(newnetworkpath);
            storageLocationRow.SizeInGbEquals(newsize);
        }
        
        
    }
}
