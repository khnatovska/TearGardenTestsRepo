using System;
using System.Collections.Generic;
using System.Linq;
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
    public class StorageLocationDialogTests
    {
        private RepoPage repoPage;
        private RepoDialog repoDialog;
        private StorageLocationDialog storageLocationDialog;
        private MessageBox messageBox;

        [OneTimeSetUp]
        public void StorageLocationDialogOneTimeSetUpMethod()
        {
            repoPage = new RepoPage();
        }

        [SetUp]
        public void StorageLocationDialogSetUpMethod()
        {
            repoDialog = repoPage.ClickAddNewRepoBtn();
            storageLocationDialog = repoDialog.ClickAddNewStorageLocationBtn();
        }

        [TearDown]
        public void StorageLocationDialogTearDownMethod()
        {
            repoPage.Refresh();
        }

        [OneTimeTearDown]
        public void StorageLocationDialogOneTimeTearDownMethod()
        {
            repoPage.Close();
        }

        [Test]
        public void StorageLocationDialogTitleIsAddStorageLocation()
        {
            storageLocationDialog.VerifyTitle();
        }
        
        [Test]
        public void UnableSaveStorageLocationWithEmptyLocalPath()
        {
            storageLocationDialog.LocalPathRadioIsSelected();
            var datapath = storageLocationDialog.GetLocalDataPathInputField();
            var metadatapath = storageLocationDialog.GetLocalMetadataPathInputField();
            storageLocationDialog.Save();
            datapath.InputValidationError();
            datapath.FormHasError();
            metadatapath.InputValidationError();
            metadatapath.FormHasError();
        }

        [TestCaseSource(typeof(Root), "invalidLocalPath")]
        public void InputValidationStorageLocationWithInvalidLocalDataPath(string invalidPath)
        {
            storageLocationDialog.LocalPathRadioIsSelected();
            var datapath = storageLocationDialog.GetLocalDataPathInputField();
            datapath.TypeIn(invalidPath);
            datapath.InputValidationError();
            datapath.FormHasError();
        }

        [TestCaseSource(typeof(Root), "invalidLocalPath")]
        public void InputValidationStorageLocationWithInvalidLocalMetadataPath(string invalidPath)
        {
            storageLocationDialog.LocalPathRadioIsSelected();
            var metadatapath = storageLocationDialog.GetLocalMetadataPathInputField();
            metadatapath.TypeIn(invalidPath);
            metadatapath.InputValidationError();
            metadatapath.FormHasError();
        }

        [Test]
        public void UnableSaveStorageLocationWithEmptyNetworkPath()
        {
            storageLocationDialog.SelectNetworkPathRadio();
            storageLocationDialog.NetworkPathRadioIsSelected();
            var networkpath = storageLocationDialog.GetUncPathInputField();
            var username = storageLocationDialog.GetUsernameInputField();
            var password = storageLocationDialog.GetPasswordInputField();
            storageLocationDialog.Save();
            networkpath.InputValidationError();
            networkpath.FormHasError();
            username.FormHasError();
            password.FormHasError();
        }

        [TestCaseSource(typeof(Root), "invalidUNCPath")]
        public void InputValidationStorageLocationWithInvalidUncPath(string invalidPath)
        {
            storageLocationDialog.SelectNetworkPathRadio();
            storageLocationDialog.NetworkPathRadioIsSelected();
            var networkpath = storageLocationDialog.GetUncPathInputField();
            networkpath.TypeIn(invalidPath);
            networkpath.InputValidationError();
            networkpath.FormHasError();
        }

        [TestCaseSource(typeof(Root), "invalidUserNames")]
        public void InputValidationStorageLocationWithInvalidUsername(string invalidUsername)
        {
            storageLocationDialog.SelectNetworkPathRadio();
            storageLocationDialog.NetworkPathRadioIsSelected();
            var username = storageLocationDialog.GetUsernameInputField();
            username.TypeIn(invalidUsername);
            username.FormHasError();
        }

        [TestCase(254)]
        public void UnableEnterTooLongUsername(int limit)
        {
            storageLocationDialog.SelectNetworkPathRadio();
            storageLocationDialog.NetworkPathRadioIsSelected();
            var username = storageLocationDialog.GetUsernameInputField();
            var tooLongName = new string('q', limit + 1);
            username.TypeIn(tooLongName);
            Assert.AreEqual(limit, username.GetInputLength());
        }

        [TestCase(104)]
        public void UnableEnterTooLongPassword(int limit)
        {
            storageLocationDialog.SelectNetworkPathRadio();
            storageLocationDialog.NetworkPathRadioIsSelected();
            var password = storageLocationDialog.GetPasswordInputField();
            var tooLongName = new string('q', limit + 1);
            password.TypeIn(tooLongName);
            Assert.AreEqual(limit, password.GetInputLength());
        }

        [Test]
        public void ChangeStorageLocationSizeUnit()
        {
            var sizeUnitDropdown = storageLocationDialog.GetSizeUnitDropdown();
            sizeUnitDropdown.ValueEquals("GB");
            sizeUnitDropdown.Open();
            sizeUnitDropdown.Contains("GB");
            sizeUnitDropdown.Contains("TB");
            sizeUnitDropdown.Select("TB");
            sizeUnitDropdown.ValueEquals("TB");
        }

        [Test]
        public void ChangeStorageLocationSize()
        {
            var size = storageLocationDialog.GetSizeInputField();
            size.InputEquals("250.00");
            size.SpinUp();
            size.InputEquals("250.01");
            size.TypeIn("100");
            size.SpinDown();
            size.InputEquals("99.99");
            size.TypeIn("0");
            size.SpinDown();
            size.InputEquals("0.00");
        }

        [Test]
        public void UnableSaveStorageLocationWithEmptySize()
        {
            var datapath = storageLocationDialog.GetLocalDataPathInputField();
            var metadatapath = storageLocationDialog.GetLocalMetadataPathInputField();
            var size = storageLocationDialog.GetSizeInputField();
            var sizeUnit = storageLocationDialog.GetSizeUnitDropdown();
            datapath.TypeIn(Root.validRepoPath);
            metadatapath.TypeIn(Root.validRepoPath);
            size.ClearInput();
            storageLocationDialog.Save();
            size.InputValidationError();
            sizeUnit.Open();
            sizeUnit.Select("TB");
            storageLocationDialog.Save();
            size.InputValidationError();
        }

        [TestCaseSource(typeof(Root), "invalidStorageLocationSizeGb")]
        public void InputValidationStorageLocationWithInvalidUnitSizeGb(string invalidValue)
        {
            var sizeUnit = storageLocationDialog.GetSizeUnitDropdown();
            var size = storageLocationDialog.GetSizeInputField();
            sizeUnit.ValueEquals("GB");
            size.TypeIn(invalidValue);
            size.InputValidationError();
        }

        [TestCaseSource(typeof(Root), "invalidStorageLocationSizeTb")]
        public void InputValidationStorageLocationWithInvalidUnitSizeTb(string invalidValue)
        {
            var sizeUnit = storageLocationDialog.GetSizeUnitDropdown();
            var size = storageLocationDialog.GetSizeInputField();
            sizeUnit.Open();
            sizeUnit.Select("TB");
            sizeUnit.ValueEquals("TB");
            size.TypeIn(invalidValue);
            size.InputValidationError();
        }

        [Test]
        public void StorageConfigurationToggleMoreDetails()
        {
            var detailsToggle = storageLocationDialog.GetDetailsToggle();
            var cashingPolicy = storageLocationDialog.GetCashingPolicyDropdown();
            var bytesPerSector = storageLocationDialog.GetBytesPerSectorDropdown();
            var avrgBytesPerSector = storageLocationDialog.GetAvrgBytesPerSector();
            Assert.AreEqual("More Details", detailsToggle.Text);
            storageLocationDialog.ToggleDetails();
            Assert.AreEqual("Hide Details", detailsToggle.Text);
            Assert.IsTrue(cashingPolicy.Displayed);
            Assert.IsTrue(bytesPerSector.Displayed);
            Assert.IsTrue(avrgBytesPerSector.Displayed);
            storageLocationDialog.ToggleDetails();
            Assert.IsFalse(cashingPolicy.Displayed);
            Assert.IsFalse(bytesPerSector.Displayed);
            Assert.IsFalse(avrgBytesPerSector.Displayed);
        }

        [TestCase("J:\\repo", "J:\\meta", "900")]
        public void IntentionallyFailedTest(string datapath, string metadatapath, string size)
        {
            var datapathField = storageLocationDialog.GetLocalDataPathInputField();
            var metadatapathField = storageLocationDialog.GetLocalMetadataPathInputField();
            var sizeField = storageLocationDialog.GetSizeInputField();
            datapathField.TypeIn(datapath);
            metadatapathField.TypeIn(metadatapath);
            sizeField.TypeIn(size);
            storageLocationDialog.Save();
            storageLocationDialog = repoDialog.VerifyStorageDialogDisappearance();
            var storageLocationRow = repoDialog.GetStorageLocationTableRow(1);
            storageLocationRow.DataPathEquals(datapath);
            storageLocationRow.MetadataPathEquals(metadatapath);
            storageLocationRow.SizeInGbEquals(size);
        }

        [TestCase("J:\\repo", "J:\\meta", "20")]
        [TestCase("J:\\repo2", "J:\\meta2", "2")]
        [TestCase("J:\\repo3", "J:\\meta3", "220")]
        [TestCase("J:\\repo4", "J:\\meta4", "134")]
        public void SaveValidLocalStorageLocation(string datapath, string metadatapath, string size)
        {
            var datapathField = storageLocationDialog.GetLocalDataPathInputField();
            var metadatapathField = storageLocationDialog.GetLocalMetadataPathInputField();
            var sizeField = storageLocationDialog.GetSizeInputField();
            datapathField.TypeIn(datapath);
            metadatapathField.TypeIn(metadatapath);
            sizeField.TypeIn(size);
            storageLocationDialog.Save();
            storageLocationDialog = repoDialog.VerifyStorageDialogDisappearance();
            var storageLocationRow = repoDialog.GetStorageLocationTableRow(1);
            storageLocationRow.DataPathEquals(datapath);
            storageLocationRow.MetadataPathEquals(metadatapath);
            storageLocationRow.SizeInGbEquals(size);
        }

        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "20")]
        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "2")]
        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "220")]
        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "134")]
        public void SaveValidNetworkStorageLocation(string networkpath, string username, string password, string size)
        {
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
            storageLocationRow.DataPathEquals(networkpath);
            storageLocationRow.MetadataPathEquals(networkpath);
            storageLocationRow.SizeInGbEquals(size);
        }

        [TestCase("J:\\repo", "J:\\meta", "20", "K:\\repo", "K:\\meta", "21")]
        [TestCase("J:\\repo2", "J:\\meta2", "4", "K:\\repo2", "K:\\meta2", "3")]
        [TestCase("J:\\repo3", "J:\\meta3", "220", "K:\\repo3", "K:\\meta3", "221")]
        [TestCase("J:\\repo4", "J:\\meta4", "134", "K:\\repo4", "K:\\meta4", "133")]
        public void SaveSecondLocalStorageLocation(string datapath, string metadatapath, string size, string datapath2,
            string metadatapath2, string size2)
        {
            var datapathField = storageLocationDialog.GetLocalDataPathInputField();
            var metadatapathField = storageLocationDialog.GetLocalMetadataPathInputField();
            var sizeField = storageLocationDialog.GetSizeInputField();
            datapathField.TypeIn(datapath);
            metadatapathField.TypeIn(metadatapath);
            sizeField.TypeIn(size);
            storageLocationDialog.Save();
            storageLocationDialog = repoDialog.VerifyStorageDialogDisappearance();
            storageLocationDialog = repoDialog.ClickAddNewStorageLocationBtn();
            datapathField = storageLocationDialog.GetLocalDataPathInputField();
            metadatapathField = storageLocationDialog.GetLocalMetadataPathInputField();
            sizeField = storageLocationDialog.GetSizeInputField();
            datapathField.TypeIn(datapath2);
            metadatapathField.TypeIn(metadatapath2);
            sizeField.TypeIn(size2);
            storageLocationDialog.Save();
            storageLocationDialog = repoDialog.VerifyStorageDialogDisappearance();
            var storageLocationRowOne = repoDialog.GetStorageLocationTableRow(1);
            storageLocationRowOne.DataPathEquals(datapath);
            storageLocationRowOne.MetadataPathEquals(metadatapath);
            storageLocationRowOne.SizeInGbEquals(size);
            var storageLocationRowTwo = repoDialog.GetStorageLocationTableRow(2);
            storageLocationRowTwo.DataPathEquals(datapath2);
            storageLocationRowTwo.MetadataPathEquals(metadatapath2);
            storageLocationRowTwo.SizeInGbEquals(size2);
        }

        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "20", "\\\\10.35.178.196\\sharedrepo2", "administrator", "123asdQ", "21")]
        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "4", "\\\\10.35.178.196\\sharedrepo2", "administrator", "123asdQ", "3")]
        [TestCase("\\\\10.35.178.196\\sharedrepo2", "administrator", "123asdQ", "220", "\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "221")]
        [TestCase("\\\\10.35.178.196\\sharedrepo2", "administrator", "123asdQ", "134", "\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "133")]
        public void SaveSecondNetworkStorageLocation(string networkpath, string username, string password,
            string size, string networkpath2, string username2, string password2, string size2)
        {
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
            storageLocationDialog = repoDialog.ClickAddNewStorageLocationBtn();
            storageLocationDialog.NetworkPathRadioIsSelected();
            networkpathField = storageLocationDialog.GetUncPathInputField();
            usernameField = storageLocationDialog.GetUsernameInputField();
            passwordField = storageLocationDialog.GetPasswordInputField();
            sizeField = storageLocationDialog.GetSizeInputField();
            networkpathField.TypeIn(networkpath2);
            usernameField.TypeIn(username2);
            passwordField.TypeIn(password2);
            sizeField.TypeIn(size2);
            storageLocationDialog.Save();
            storageLocationDialog = repoDialog.VerifyStorageDialogDisappearance();
            var storageLocationRowOne = repoDialog.GetStorageLocationTableRow(1);
            storageLocationRowOne.DataPathEquals(networkpath);
            storageLocationRowOne.MetadataPathEquals(networkpath);
            storageLocationRowOne.SizeInGbEquals(size);
            var storageLocationRowTwo = repoDialog.GetStorageLocationTableRow(2);
            storageLocationRowTwo.DataPathEquals(networkpath2);
            storageLocationRowTwo.MetadataPathEquals(networkpath2);
            storageLocationRowTwo.SizeInGbEquals(size2);
        }

        [TestCase("J:\\repo", "J:\\meta", "500")]
        [TestCase("J:\\repo2", "J:\\meta2", "700")]
        [TestCase("I:\\repo3", "I:\\meta3", "220")]
        [TestCase("I:\\repo4", "J:\\meta4", "50")]
        public void UnableSaveInvalidLocalStorageLocationAdditionalSpaceRequired(string datapath, string metadatapath,
            string size)
        {
            var datapathField = storageLocationDialog.GetLocalDataPathInputField();
            var metadatapathField = storageLocationDialog.GetLocalMetadataPathInputField();
            var sizeField = storageLocationDialog.GetSizeInputField();
            datapathField.TypeIn(datapath);
            metadatapathField.TypeIn(metadatapath);
            sizeField.TypeIn(size);
            storageLocationDialog.Save();
            messageBox = storageLocationDialog.VerifyErrorAppearance();
            messageBox.HeaderEquals("Error");
            messageBox.MessageContains("free space is additionally required");
        }

        [TestCase("S:\\repo", "J:\\meta")]
        [TestCase("J:\\repo2", "S:\\meta2")]
        [TestCase("P:\\repo3", "L:\\meta3")]
        [TestCase("J:\\repo4", "A:\\meta4")]
        public void UnableSaveInvalidLocalStorageLocationNonexistentPath(string datapath, string metadatapath)
        {
            var datapathField = storageLocationDialog.GetLocalDataPathInputField();
            var metadatapathField = storageLocationDialog.GetLocalMetadataPathInputField();
            datapathField.TypeIn(datapath);
            metadatapathField.TypeIn(metadatapath);
            storageLocationDialog.Save();
            messageBox = storageLocationDialog.VerifyErrorAppearance();
            messageBox.HeaderEquals("Error");
            messageBox.MessageContains("specified local paths are unreachable");
        }

        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "400")]
        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "450")]
        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "500")]
        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "550")]
        public void UnableSaveInvalidNetworkStorageLocationAdditionalSpaceRequired(string networkpath, string username,
            string password, string size)
        {
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
            messageBox = storageLocationDialog.VerifyErrorAppearance();
            messageBox.HeaderEquals("Error");
            messageBox.MessageContains("free space is additionally required");
        }

        [TestCase("\\\\10.35.178.196\\sharedrepo3", "administrator", "123asdQ")]
        [TestCase("\\\\10.35.178.196\\sharedrep", "administrator", "123asdQ")]
        [TestCase("\\\\hostname\\sharedrepo", "administrator", "123asdQ")]
        [TestCase("\\\\10.35.178196\\sharedrepo", "administrator", "123asdQ")]
        public void UnableSaveInvalidNetworkStorageLocationNonexistentPath(string networkpath, string username, string password)
        {
            storageLocationDialog.SelectNetworkPathRadio();
            storageLocationDialog.NetworkPathRadioIsSelected();
            var networkpathField = storageLocationDialog.GetUncPathInputField();
            var usernameField = storageLocationDialog.GetUsernameInputField();
            var passwordField = storageLocationDialog.GetPasswordInputField();
            networkpathField.TypeIn(networkpath);
            usernameField.TypeIn(username);
            passwordField.TypeIn(password);
            storageLocationDialog.Save();
            messageBox = storageLocationDialog.VerifyErrorAppearance();
            messageBox.HeaderEquals("Error");
            messageBox.MessageContains("Error connecting to the network location");
        }

        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQqq")]
        [TestCase("\\\\10.35.178.196\\sharedrepo2", "admin", "123asdQ")]
        [TestCase("\\\\10.35.178.196\\sharedrepo", "admini", "123asdQ")]
        [TestCase("\\\\10.35.178.196\\sharedrepo2", "administrator", "123asdQqq")]
        public void UnableSaveInvalidNetworkStorageLocationIncorrectCredentials(string networkpath, string username,
            string password)
        {
            storageLocationDialog.SelectNetworkPathRadio();
            storageLocationDialog.NetworkPathRadioIsSelected();
            var networkpathField = storageLocationDialog.GetUncPathInputField();
            var usernameField = storageLocationDialog.GetUsernameInputField();
            var passwordField = storageLocationDialog.GetPasswordInputField();
            networkpathField.TypeIn(networkpath);
            usernameField.TypeIn(username);
            passwordField.TypeIn(password);
            storageLocationDialog.Save();
            messageBox = storageLocationDialog.VerifyErrorAppearance();
            messageBox.HeaderEquals("Error");
            messageBox.MessageContains("network credentials are incorrect");
        }

        [TestCase("J:\\repo", "J:\\meta", "20", "K:\\meta", "21")]
        [TestCase("J:\\repo2", "J:\\meta2", "4", "K:\\meta2", "3")]
        [TestCase("K:\\repo3", "J:\\meta3", "220", "K:\\meta3", "22")]
        [TestCase("K:\\repo4", "J:\\meta4", "134", "K:\\meta4", "13")]
        public void UnableSaveSecondInvalidLocalStorageLocationDataPathInUse(string datapath, string metadatapath, string size,
            string metadatapath2, string size2)
        {
            var datapathField = storageLocationDialog.GetLocalDataPathInputField();
            var metadatapathField = storageLocationDialog.GetLocalMetadataPathInputField();
            var sizeField = storageLocationDialog.GetSizeInputField();
            datapathField.TypeIn(datapath);
            metadatapathField.TypeIn(metadatapath);
            sizeField.TypeIn(size);
            storageLocationDialog.Save();
            storageLocationDialog = repoDialog.VerifyStorageDialogDisappearance();
            storageLocationDialog = repoDialog.ClickAddNewStorageLocationBtn();
            datapathField = storageLocationDialog.GetLocalDataPathInputField();
            metadatapathField = storageLocationDialog.GetLocalMetadataPathInputField();
            sizeField = storageLocationDialog.GetSizeInputField();
            datapathField.TypeIn(datapath);
            metadatapathField.TypeIn(metadatapath2);
            sizeField.TypeIn(size2);
            storageLocationDialog.Save();
            messageBox = storageLocationDialog.VerifyErrorAppearance();
            messageBox.HeaderEquals("Error");
            messageBox.MessageContains("The data path is in use");
        }

        [TestCase("J:\\repo", "J:\\meta", "20", "K:\\repo", "21")]
        [TestCase("J:\\repo2", "J:\\meta2", "4", "K:\\repo2", "3")]
        [TestCase("J:\\repo3", "K:\\meta3", "220", "K:\\repo3", "22")]
        [TestCase("J:\\repo4", "K:\\meta4", "134", "K:\\repo4", "13")]
        public void UnableSaveSecondInvalidLocalStorageLocationMetadataPathInUse(string datapath, string metadatapath,
            string size, string datapath2, string size2)
        {
            var datapathField = storageLocationDialog.GetLocalDataPathInputField();
            var metadatapathField = storageLocationDialog.GetLocalMetadataPathInputField();
            var sizeField = storageLocationDialog.GetSizeInputField();
            datapathField.TypeIn(datapath);
            metadatapathField.TypeIn(metadatapath);
            sizeField.TypeIn(size);
            storageLocationDialog.Save();
            storageLocationDialog = repoDialog.VerifyStorageDialogDisappearance();
            storageLocationDialog = repoDialog.ClickAddNewStorageLocationBtn();
            datapathField = storageLocationDialog.GetLocalDataPathInputField();
            metadatapathField = storageLocationDialog.GetLocalMetadataPathInputField();
            sizeField = storageLocationDialog.GetSizeInputField();
            datapathField.TypeIn(datapath2);
            metadatapathField.TypeIn(metadatapath);
            sizeField.TypeIn(size2);
            storageLocationDialog.Save();
            messageBox = storageLocationDialog.VerifyErrorAppearance();
            messageBox.HeaderEquals("Error");
            messageBox.MessageContains("The metadata path is in use");
        }

        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "20", "21")]
        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "4", "3")]
        [TestCase("\\\\10.35.178.196\\sharedrepo2", "administrator", "123asdQ", "220", "21")]
        [TestCase("\\\\10.35.178.196\\sharedrepo2", "administrator", "123asdQ", "134", "13")]
        public void UnableSaveSecondInvalidNetworkStorageLocationPathInUse(string networkpath, string username,
            string password, string size, string size2)
        {
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
            storageLocationDialog = repoDialog.ClickAddNewStorageLocationBtn();
            storageLocationDialog.NetworkPathRadioIsSelected();
            networkpathField = storageLocationDialog.GetUncPathInputField();
            usernameField = storageLocationDialog.GetUsernameInputField();
            passwordField = storageLocationDialog.GetPasswordInputField();
            sizeField = storageLocationDialog.GetSizeInputField();
            networkpathField.TypeIn(networkpath);
            usernameField.TypeIn(username);
            passwordField.TypeIn(password);
            sizeField.TypeIn(size2);
            storageLocationDialog.Save();
            messageBox = storageLocationDialog.VerifyErrorAppearance();
            messageBox.HeaderEquals("Error");
            messageBox.MessageContains("The metadata path is in use");
        }

        [TestCase("J:\\repo", "J:\\meta", "20", "\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "21")]
        [TestCase("K:\\repo", "K:\\meta", "4", "\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "3")]
        [TestCase("J:\\repo", "K:\\meta", "220", "\\\\10.35.178.196\\sharedrepo2", "administrator", "123asdQ", "21")]
        [TestCase("K:\\repo", "J:\\meta", "134", "\\\\10.35.178.196\\sharedrepo2", "administrator", "123asdQ", "13")]
        public void UnableSaveTwoStorageLocationsDifferentTypes(string datapath, string metadatapath, string size,
            string networkpath, string username, string password, string size2)
        {
            storageLocationDialog.LocalPathRadioIsSelected();
            var datapathField = storageLocationDialog.GetLocalDataPathInputField();
            var metadatapathField = storageLocationDialog.GetLocalMetadataPathInputField();
            var sizeField = storageLocationDialog.GetSizeInputField();
            datapathField.TypeIn(datapath);
            metadatapathField.TypeIn(metadatapath);
            sizeField.TypeIn(size);
            storageLocationDialog.Save();
            storageLocationDialog = repoDialog.VerifyStorageDialogDisappearance();
            storageLocationDialog = repoDialog.ClickAddNewStorageLocationBtn();
            storageLocationDialog.SelectNetworkPathRadio();
            storageLocationDialog.NetworkPathRadioIsSelected();
            var networkpathField = storageLocationDialog.GetUncPathInputField();
            var usernameField = storageLocationDialog.GetUsernameInputField();
            var passwordField = storageLocationDialog.GetPasswordInputField();
            sizeField = storageLocationDialog.GetSizeInputField();
            networkpathField.TypeIn(networkpath);
            usernameField.TypeIn(username);
            passwordField.TypeIn(password);
            sizeField.TypeIn(size);
            storageLocationDialog.Save();
            messageBox = storageLocationDialog.VerifyErrorAppearance();
            messageBox.HeaderEquals("Error");
            messageBox.MessageContains("network and local path mismatch is not allowed");
        }
        
        

        
    }
}
