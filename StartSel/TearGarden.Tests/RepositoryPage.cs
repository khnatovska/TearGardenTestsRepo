using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using StartSel;

namespace TearGarden.Tests
{
    [TestFixture]
    public class RepositoryPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [OneTimeSetUp]
        public void AddNewRepositoryDialogSetupOnetime()
        {
            driver = TestConfig.RepositoryTestSetup();
            wait = Root.SetUptWebDriverWait(driver);
        }

        [OneTimeTearDown]
        public void AddNewRepositoryDialogTeardownOneTime()
        {
            TestConfig.RepositoryTestTearDown();
        }

        [TearDown]
        public void RepositoryPageTeardown()
        {
            driver.Navigate().Refresh();
            driver.WaitForPageToLoad();
        }

        [Test]
        [Category("atRepositoryPage")]
        public void SaveValidLocalStorageLocation()
        {
            //open addStorageLocationDialog
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtnText)).Text, "Add New DVM Repository");
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtn)).Click();
            IWebElement uiDialogLevelOneTitle = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelOneTitle)));
            Assert.AreEqual(uiDialogLevelOneTitle.Text, "Add New Repository");
            IWebElement addStorageLocationBtn = driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogAddStorageLocationBtn));
            Assert.AreEqual(addStorageLocationBtn.Text, "Add Storage Location");
            addStorageLocationBtn.Click();
            IWebElement addStorageLocationDialogTitle = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            Assert.AreEqual(addStorageLocationDialogTitle.Text, "Add Storage Location");

            string metadataPath = Root.localRepoPath + "meta";
            string storageLocationSize = "300";
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPath)).SendKeys(Root.localRepoPath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).Click();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).SendKeys(metadataPath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).Clear();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).SendKeys(storageLocationSize);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).SendKeys(Keys.Tab);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSaveBtn)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle))));
            driver.WaitForAjax();
            string storageLocationId =
                driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableRowOne))
                    .GetAttribute("id");
            string dataPathCellContent =
                driver.FindElement(
                    By.CssSelector(
                        storageLocationId.IntoDynamicSelector(
                            CssSelestors.addNewRepositoryDialogStorageLocationsTableDataPathCell_part))).Text;
            string metadataPathCellContent =
                driver.FindElement(
                    By.CssSelector(
                        storageLocationId.IntoDynamicSelector(
                            CssSelestors.addNewRepositoryDialogStorageLocationsTableMetadataPathCell_part))).Text;
            string sizeCellContent =
                driver.FindElement(
                    By.CssSelector(
                        storageLocationId.IntoDynamicSelector(
                            CssSelestors.addNewRepositoryDialogStorageLocationsTableSizeCell_part))).Text;
            Assert.AreEqual(dataPathCellContent, Root.localRepoPath);
            Assert.AreEqual(metadataPathCellContent, metadataPath);
            Assert.AreEqual(sizeCellContent, storageLocationSize + " GB");
        }

        [Test]
        [Category("atRepositoryPage")]
        public void SaveValidNetworkStorageLocation()
        {
            //open addStorageLocationDialog
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtnText)).Text, "Add New DVM Repository");
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtn)).Click();
            IWebElement uiDialogLevelOneTitle = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelOneTitle)));
            Assert.AreEqual(uiDialogLevelOneTitle.Text, "Add New Repository");
            IWebElement addStorageLocationBtn = driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogAddStorageLocationBtn));
            Assert.AreEqual(addStorageLocationBtn.Text, "Add Storage Location");
            addStorageLocationBtn.Click();
            IWebElement addStorageLocationDialogTitle = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            Assert.AreEqual(addStorageLocationDialogTitle.Text, "Add Storage Location");

            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSharedPathRadio)).Click();
            string storageLocationSize = "30";
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath)).SendKeys(Root.networkRepoPath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUsername)).Click();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUsername)).SendKeys(Root.networkRepoUsername);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogPassword)).Click();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogPassword)).SendKeys(Root.networkRepoPassword);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).Clear();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).SendKeys(storageLocationSize);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).SendKeys(Keys.Tab);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSaveBtn)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle))));
            driver.WaitForAjax();
            string storageLocationId =
                driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableRowOne))
                    .GetAttribute("id");
            string dataPathCellContent =
                driver.FindElement(
                    By.CssSelector(
                        storageLocationId.IntoDynamicSelector(
                            CssSelestors.addNewRepositoryDialogStorageLocationsTableDataPathCell_part))).Text;
            string metadataPathCellContent =
                driver.FindElement(
                    By.CssSelector(
                        storageLocationId.IntoDynamicSelector(
                            CssSelestors.addNewRepositoryDialogStorageLocationsTableMetadataPathCell_part))).Text;
            string sizeCellContent =
                driver.FindElement(
                    By.CssSelector(
                        storageLocationId.IntoDynamicSelector(
                            CssSelestors.addNewRepositoryDialogStorageLocationsTableSizeCell_part))).Text;
            Assert.AreEqual(dataPathCellContent, Root.networkRepoPath);
            Assert.AreEqual(metadataPathCellContent, Root.networkRepoPath);
            Assert.AreEqual(sizeCellContent, storageLocationSize + " GB");
        }

        [Test]
        [Category("atRepositoryPage")]
        public void SaveTwoValidStorageLocations()
        {
            //open addStorageLocationDialog
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtnText)).Text, "Add New DVM Repository");
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtn)).Click();
            IWebElement uiDialogLevelOneTitle = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelOneTitle)));
            Assert.AreEqual(uiDialogLevelOneTitle.Text, "Add New Repository");
            IWebElement addStorageLocationBtn = driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogAddStorageLocationBtn));
            Assert.AreEqual(addStorageLocationBtn.Text, "Add Storage Location");
            addStorageLocationBtn.Click();
            IWebElement addStorageLocationDialogTitle = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            Assert.AreEqual(addStorageLocationDialogTitle.Text, "Add Storage Location");

            string metadataPath = Root.localRepoPath + "meta";
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPath)).SendKeys(Root.localRepoPath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).Click();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).SendKeys(metadataPath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).SendKeys(Keys.Tab);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSaveBtn)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle))));
            driver.WaitForAjax();
            string storageLocationOneId =
                driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableRowOne))
                    .GetAttribute("id");
            string dataPathCellContentOne =
                driver.FindElement(
                    By.CssSelector("#"+
                        storageLocationOneId+" "+
                            CssSelestors.addNewRepositoryDialogStorageLocationsTableDataPathCell_part)).Text;
            string metadataPathCellContentOne =
                driver.FindElement(
                    By.CssSelector("#"+
                        storageLocationOneId+" "+
                            CssSelestors.addNewRepositoryDialogStorageLocationsTableMetadataPathCell_part)).Text;
            string sizeCellContentOne =
                driver.FindElement(
                    By.CssSelector("#"+
                        storageLocationOneId+" "+
                            CssSelestors.addNewRepositoryDialogStorageLocationsTableSizeCell_part)).Text;

            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogAddStorageLocationBtn)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));

            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPath)).SendKeys(Root.localRepoPath + "2");
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).Click();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).SendKeys(metadataPath + "2");
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).Clear();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).SendKeys("10");
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).SendKeys(Keys.Tab);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSaveBtn)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle))));
            driver.WaitForAjax();
            string storageLocationTwoId =
                driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableRowTwo))
                    .GetAttribute("id");
            string dataPathCellContentTwo =
                driver.FindElement(
                    By.CssSelector("#"+
                        storageLocationTwoId+" "+
                            CssSelestors.addNewRepositoryDialogStorageLocationsTableDataPathCell_part)).Text;
            string metadataPathCellContentTwo =
                driver.FindElement(
                    By.CssSelector("#"+
                        storageLocationTwoId+" "+
                            CssSelestors.addNewRepositoryDialogStorageLocationsTableMetadataPathCell_part)).Text;
            string sizeCellContentTwo =
                driver.FindElement(
                    By.CssSelector("#"+
                        storageLocationTwoId+" "+
                            CssSelestors.addNewRepositoryDialogStorageLocationsTableSizeCell_part)).Text;

            Assert.AreEqual(dataPathCellContentOne, Root.localRepoPath);
            Assert.AreEqual(metadataPathCellContentOne, metadataPath);
            Assert.AreEqual(sizeCellContentOne, "250 GB");
            Assert.AreEqual(dataPathCellContentTwo, Root.localRepoPath + "2");
            Assert.AreEqual(metadataPathCellContentTwo, metadataPath + "2");
            Assert.AreEqual(sizeCellContentTwo, "10 GB");
        }
    }

    [TestFixture]
    public class AddNewRepositoryDialog
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [OneTimeSetUp]
        public void AddNewRepositoryDialogSetupOnetime()
        {
            driver = TestConfig.RepositoryTestSetup();
            wait = Root.SetUptWebDriverWait(driver);
        }

        [OneTimeTearDown]
        public void AddNewRepositoryDialogTeardownOneTime()
        {
            TestConfig.RepositoryTestTearDown();
        }

        [SetUp]
        public void AddNewRepositoryDialogSetup()
        {
            //open dialog
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtnText)).Text, "Add New DVM Repository");
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtn)).Click();
            IWebElement uiDialogLevelOneTitle = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelOneTitle)));
            Assert.AreEqual(uiDialogLevelOneTitle.Text, "Add New Repository");
        }

        [TearDown]
        public void AddNewRepositoryDialogTeardown()
        {
            //close dialog
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogCancelBtn)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelestors.uiDialogLevelOneTitle))));
        }

        [Test]
        [Category("atAddNewRepositoryDialog")]
        public void CreateDVMRepoWithoutStorageLocation() //start: add new DVM repo dialog. find create repo button, click, verify warning message, close warning dialog
        {
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogCreatelBtn)).Click();
            IWebElement warningHeader = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogMessageBoxHeader)));
            IWebElement warningMessage = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogMessageBoxMessage)));
            Assert.AreEqual(warningHeader.Text, "Warning");
            Assert.AreEqual(warningMessage.Text, "You must add at least one storage location");
            driver.FindElement(By.CssSelector(CssSelestors.uiDialogMessageBoxDefaultBtn)).Click();
        }

        [Test]
        [Category("atAddNewRepositoryDialog")]
        public void CreateDVMRepoWithEmptyName() //start: add new DVM repo dialog. find name, verify label. clear, click create button, vefiry validation
        {
            IWebElement repoNameLabel = driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogRepoNameLabel));
            IWebElement repoNameLabelParent = repoNameLabel.FindElement(By.XPath(".."));
            Assert.AreEqual(repoNameLabel.Text, "Repository name");
            IWebElement repoName = driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogRepoName));
            repoName.Clear();
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogCreatelBtn)).Click();
            driver.WaitForAjax();
            Assert.IsTrue(repoName.HasClass("input-validation-error")); //IS IT RELIABLE - CHECK CLASS AND NOT ACTUAL STYLE? HOW TO CHECK ACTUAL STYLE? NO WAY TO CHECK TOOLTIP
            Assert.IsTrue(repoNameLabelParent.HasClass("has-error"));
        }

        [TestCaseSource(typeof(Root), "invalidRepoNames")]
        [Category("atAddNewRepositoryDialog")]
        public void CreateDVMRepoWithInvalidName(string invalidName) //start: add new DVM repo dialog. find name. loop - incorrect value, click create button, vefiry validation
        {
            IWebElement repoNameLabel = driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogRepoNameLabel));
            IWebElement repoNameLabelParent = repoNameLabel.FindElement(By.XPath(".."));
            Assert.AreEqual(repoNameLabel.Text, "Repository name");
            IWebElement repoName = driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogRepoName));
            repoName.Clear();
            repoName.SendKeys(invalidName);
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogCreatelBtn)).Click();
            driver.WaitForAjax();
            Assert.IsTrue(repoName.HasClass("input-validation-error")); //IS IT RELIABLE - CHECK CLASS AND NOT ACTUAL STYLE? HOW TO CHECK ACTUAL STYLE? NO WAY TO CHECK TOOLTIP
            Assert.IsTrue(repoNameLabelParent.HasClass("has-error"));
        }

        [Test]
        [Category("atAddNewRepositoryDialog")]
        public void CreateDVMRepoWithTooLongName()
        {
            IWebElement repoName = driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogRepoName));
            repoName.Clear();
            repoName.SendKeys(new String('q', 41));
            Assert.AreEqual(repoName.GetAttribute("value").Length, 40);
        }

        [Test]
        [Category("atAddNewRepositoryDialog")]
        public void CreateDVMRepoWithEmptyConcurOperations() //start: add new DVM repo dialog. find concurrent operations. clear, click create button, vefiry validation
        {
            IWebElement concurOperationsLabel = driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogConcurOperationsLabel));
            IWebElement concurOperationsLabelParent = concurOperationsLabel.FindElement(By.XPath(".."));
            Assert.AreEqual(concurOperationsLabel.Text, "Concurrent operations");
            IWebElement concurOperations = driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogConcurOperations));
            concurOperations.Clear();
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogCreatelBtn)).Click();
            driver.WaitForAjax();
            Assert.IsTrue(concurOperationsLabelParent.HasClass("has-error"));
        }

        [TestCaseSource(typeof(Root), "invalidRepoConcurOperations")]
        [Category("atAddNewRepositoryDialog")]
        public void CreateDVMRepoWithInvalidConcurOperations(string invalidValue) //start: add new DVM repo dialog. find concurrent operations. loop - incorect value, click create button, vefiry validation
        {
            IWebElement concurOperationsLabel = driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogConcurOperationsLabel));
            IWebElement concurOperationsLabelParent = concurOperationsLabel.FindElement(By.XPath(".."));
            Assert.AreEqual(concurOperationsLabel.Text, "Concurrent operations");
            IWebElement concurOperations = driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogConcurOperations));
            concurOperations.Clear();
            concurOperations.SendKeys(invalidValue);
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogCreatelBtn)).Click();
            driver.WaitForAjax();
            Assert.IsTrue(concurOperationsLabelParent.HasClass("has-error"));
        }
    }

    [TestFixture]
    public class AddStorageLocationDialog
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [OneTimeSetUp]
        public void AddStorageLocationDialogSetupOneTime()
        {
            driver = TestConfig.RepositoryTestSetup();
            wait = Root.SetUptWebDriverWait(driver);
        }

        [OneTimeTearDown]
        public void AddStorageLocationDialogTearDownOneTime()
        {
            TestConfig.RepositoryTestTearDown();
        }

        [SetUp]
        public void AddStorageLocationDialogSetup()
        {
            //open dialog
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtnText)).Text, "Add New DVM Repository");
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtn)).Click();
            IWebElement uiDialogLevelOneTitle = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelOneTitle)));
            Assert.AreEqual(uiDialogLevelOneTitle.Text, "Add New Repository");
            IWebElement addStorageLocationBtn = driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogAddStorageLocationBtn));
            Assert.AreEqual(addStorageLocationBtn.Text, "Add Storage Location");
            addStorageLocationBtn.Click();
            IWebElement addStorageLocationDialogTitle = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            Assert.AreEqual(addStorageLocationDialogTitle.Text, "Add Storage Location");
        }

        [TearDown]
        public void AddStorageLocationDialogTeardown()
        {
            //close dialog
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogCancelBtn)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle))));
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogCancelBtn)).Click();
            Assert.IsTrue(wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelestors.uiDialogLevelOneTitle))));
        }

        [Test]
        [Category("atAddStorageLocationDialog")]
        public void SaveStorageLocationWithEmptyLocalPath() //start: add storage location dialog. verify local path radio and labels, find save button, click, verify validation
        {
            IWebElement dataPathLabelParent = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPathLabel)).FindElement(By.XPath(".."));
            IWebElement metadataPathLabelParent = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPathLabel)).FindElement(By.XPath(".."));
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogLocalPathRadio)).GetAttribute("checked"), "true");
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSaveBtn)).Click();
            driver.WaitForAjax();
            Assert.IsTrue(driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPath)).HasClass("input-validation-error"));
            Assert.IsTrue(driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).HasClass("input-validation-error"));
            Assert.IsTrue(dataPathLabelParent.HasClass("has-error"));
            Assert.IsTrue(metadataPathLabelParent.HasClass("has-error"));
        }

        [TestCaseSource(typeof(Root), "invalidLocalPath")]
        [Category("atAddStorageLocationDialog")]
        public void SaveStorageLocationWithInvalidLocalDataPath(string invalidPath) //start: add storage location dialog. verify local radio, loop - enter invalid values and check validation for data and metadata
        {
            IWebElement dataPathLabelParent = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPathLabel)).FindElement(By.XPath(".."));
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogLocalPathRadio)).GetAttribute("checked"), "true");
            IWebElement dataPath = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPath));
            Verifier.VerifyInputFieldValidation(driver, dataPath, dataPathLabelParent, invalidPath, "input-validation-error", "has-error");
        }

        [TestCaseSource(typeof(Root), "invalidLocalPath")]
        [Category("atAddStorageLocationDialog")]
        public void SaveStorageLocationWithInvalidLocalMetadataPath(string invalidPath) //start: add storage location dialog. verify local radio, loop - enter invalid values and check validation for data and metadata
        {
            IWebElement metadataPathLabelParent = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPathLabel)).FindElement(By.XPath(".."));
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogLocalPathRadio)).GetAttribute("checked"), "true");
            IWebElement metadataPath = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath));
            Verifier.VerifyInputFieldValidation(driver, metadataPath, metadataPathLabelParent, invalidPath, "input-validation-error", "has-error");
        }

        [Test]
        [Category("atAddStorageLocationDialog")]
        public void SaveStorageLocationWithEmptySharedPath() //start: add storage location dialog. select shared path radio, verify labels, find save button, click, verify validation
        {
            IWebElement sharedPathRadio = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSharedPathRadio));
            sharedPathRadio.Click();
            Assert.AreEqual(sharedPathRadio.GetAttribute("checked"), "true");
            IWebElement sharedPathLabelParent = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPathLabel)).FindElement(By.XPath(".."));
            IWebElement userNameLabelParent = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUsernameLabel)).FindElement(By.XPath(".."));
            IWebElement passwordLabelParent = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogPasswordLabel)).FindElement(By.XPath(".."));
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSaveBtn)).Click();
            driver.WaitForAjax();
            Assert.IsTrue(driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath)).HasClass("input-validation-error")); //IS IT RELIABLE - CHECK CLASS AND NOT ACTUAL STYLE? HOW TO CHECK ACTUAL STYLE? NO WAY TO CHECK TOOLTIP
            Assert.IsTrue(sharedPathLabelParent.HasClass("has-error"));
            Assert.IsTrue(userNameLabelParent.HasClass("has-error"));
            Assert.IsTrue(passwordLabelParent.HasClass("has-error"));
        }

        [TestCaseSource(typeof(Root), "invalidUNCPath")]
        [Category("atAddStorageLocationDialog")]
        public void SaveStorageLocationWithInvalidSharedPath(string invalidPath) //start: add storage location dialog. select shared path radio, loop - enter invalid values for shared path and check validation for path
        {
            IWebElement sharedPathRadio = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSharedPathRadio));
            sharedPathRadio.Click();
            Assert.AreEqual(sharedPathRadio.GetAttribute("checked"), "true");
            IWebElement sharedPathLabelParent = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPathLabel)).FindElement(By.XPath(".."));
            IWebElement sharedPath = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath));
            Verifier.VerifyInputFieldValidation(driver, sharedPath, sharedPathLabelParent, invalidPath, "input-validation-error", "has-error");
        }

        [TestCaseSource(typeof(Root), "invalidUserNames")]
        [Category("atAddStorageLocationDialog")]
        public void SaveStorageLocationWithInvalidSharedPathUserName(string invalidUsername) //start: add storage location dialog. select shared path radio, loop - enter invalid values for user of shared path and check validation for name
        {
            IWebElement sharedPathRadio = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSharedPathRadio));
            sharedPathRadio.Click();
            Assert.AreEqual(sharedPathRadio.GetAttribute("checked"), "true");
            IWebElement usernameLabelParent = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUsernameLabel)).FindElement(By.XPath(".."));
            IWebElement username = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUsername));
            Verifier.VerifyInputFieldValidation(driver, username, usernameLabelParent, invalidUsername, "has-error");
        }

        [Test]
        [Category("atAddStorageLocationDialog")]
        public void SaveStorageLocationWithTooLongSharedPathUserName() //start: add storage location dialog. select shared path radio, loop - enter invalid values for user of shared path and check validation for name
        {
            IWebElement sharedPathRadio = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSharedPathRadio));
            sharedPathRadio.Click();
            Assert.AreEqual(sharedPathRadio.GetAttribute("checked"), "true");
            IWebElement username = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUsername));
            username.SendKeys(new String('q', 255));
            Assert.AreEqual(username.GetAttribute("value").Length, 254);
        }

        [Test]
        [Category("atAddStorageLocationDialog")]
        public void SaveStorageLocationWithTooLongSharedPathPassword() //start: add storage location dialog. select shared path radio, enter invalid (too log) password for shared path and check validation for password
        {
            IWebElement sharedPathRadio = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSharedPathRadio));
            sharedPathRadio.Click();
            Assert.AreEqual(sharedPathRadio.GetAttribute("checked"), "true");
            IWebElement password = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogPassword));
            password.SendKeys(new String('q', 105));
            Assert.AreEqual(password.GetAttribute("value").Length, 104);
        }

        [Test]
        [Category("atAddStorageLocationDialog")]
        public void ChangeStorageLocationSizeUnit()
        {
            IWebElement sizeUnit = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSizeUnitSelectBtn));
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSizeUnitDefaultValue)).Text, "GB");
            sizeUnit.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(CssSelestors.addStorageLocationDialogSizeUnitDropdown)));
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSizeUnitDropdownOption1)).Text, "GB");
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSizeUnitDropdownOption2)).Text, "TB");
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSizeUnitDropdownOption2)).Click();
            driver.WaitForAjax();
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSizeUnitDefaultValue)).Text, "TB");
        }

        [Test]
        [Category("atAddStorageLocationDialog")]
        public void ChangeStorageLocationSize()
        {
            IWebElement size = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize));
            Assert.AreEqual(size.GetAttribute("value"), "250.00");
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSizeControlArrowUp)).Click();
            Assert.AreEqual(size.GetAttribute("value"), "250.01");
            size.Clear();
            size.SendKeys("100");
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSizeControlArrowDown)).Click();
            Assert.AreEqual(size.GetAttribute("value"), "99.99");
            size.Clear();
            size.SendKeys("0");
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSizeControlArrowDown)).Click();
            Assert.AreEqual(size.GetAttribute("value"), "0.00");
        }

        [Test]
        [Category("atAddStorageLocationDialog")]
        public void SaveStorageLocationWithEmptySize()
        {
            IWebElement size = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize));
            IWebElement saveBtn = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSaveBtn));
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPath)).SendKeys(Root.localRepoPath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).Click();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).SendKeys(Root.localRepoPath);
            size.Clear();
            saveBtn.Click();
            driver.WaitForAjax();
            Assert.IsTrue(size.HasClass("input-validation-error"));
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSizeUnitSelectBtn)).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(CssSelestors.addStorageLocationDialogSizeUnitDropdown)));
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSizeUnitDropdownOption2)).Click();
            driver.WaitForAjax();
            saveBtn.Click();
            driver.WaitForAjax();
            Assert.IsTrue(size.HasClass("input-validation-error"));
        }

        [TestCaseSource(typeof(Root), "invalidStorageLocationSizeGB")]
        [Category("atAddStorageLocationDialog")]
        public void SaveStorageLocationSizeInvalidValueGB(string invalidValue)
        {
            IWebElement size = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize));
            IWebElement sizeParent = size.FindElement(By.XPath("../../.."));
            size.Clear();
            Verifier.VerifyInputFieldValidation(driver, size, sizeParent, invalidValue, "input-validation-error", "has-error");
        }

        [TestCaseSource(typeof(Root), "invalidStorageLocationSizeTB")]
        [Category("atAddStorageLocationDialog")]
        public void SaveStorageLocationSizeInvalidValueTB(string invalidValue)
        {
            IWebElement size = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize));
            IWebElement sizeParent = size.FindElement(By.XPath("../../.."));
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSizeUnitSelectBtn)).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(CssSelestors.addStorageLocationDialogSizeUnitDropdown)));
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSizeUnitDropdownOption2)).Click();
            driver.WaitForAjax();
            size.Clear();
            Verifier.VerifyInputFieldValidation(driver, size, sizeParent, invalidValue, "input-validation-error", "has-error");
        }

        [Test]
        [Category("atAddStorageLocationDialog")]
        public void StorageConfigurationToggleMoreDetails()
        {
            IWebElement detailsToggle =
                driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDetailsToggle));
            Assert.AreEqual(detailsToggle.Text, "More Details");
            detailsToggle.Click();
            driver.WaitForAjax();
            Assert.AreEqual(detailsToggle.Text, "Hide Details");
            Assert.IsTrue(driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDetailsCashingPolicyDropdown)).Displayed);
            Assert.IsTrue(driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDetailsBytesPerSectorDropdown)).Displayed);
            Assert.IsTrue(driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDetailsAvrgBytesPerSector)).Displayed);
            detailsToggle.Click();
            driver.WaitForAjax();
            Assert.IsFalse(driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDetailsCashingPolicyDropdown)).Displayed);
            Assert.IsFalse(driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDetailsBytesPerSectorDropdown)).Displayed);
            Assert.IsFalse(driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDetailsAvrgBytesPerSector)).Displayed);
        }
    }
}
