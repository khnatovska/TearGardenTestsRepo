using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using StartSel;

namespace TearGarden.TestScenarios
{
    [TestFixture]
    public class RepositoryPageNewRepo
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

        [TestCase("J:\\repo", "J:\\meta", "20")]
        [TestCase("J:\\repo2", "J:\\meta2", "2")]
        [TestCase("J:\\repo3", "J:\\meta3", "220")]
        [TestCase("J:\\repo4", "J:\\meta4", "134")]
        public void SaveValidLocalStorageLocation(string datapath, string metadatapath, string size)
        {
            //open addStorageLocationDialog
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtnText)).Text,
                "Add New DVM Repository");
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtn)).Click();
            IWebElement uiDialogLevelOneTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelOneTitle)));
            Assert.AreEqual(uiDialogLevelOneTitle.Text, "Add New Repository");
            IWebElement addStorageLocationBtn =
                driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogAddStorageLocationBtn));
            Assert.AreEqual(addStorageLocationBtn.Text, "Add Storage Location");
            addStorageLocationBtn.Click();
            IWebElement addStorageLocationDialogTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            Assert.AreEqual(addStorageLocationDialogTitle.Text, "Add Storage Location");

            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPath)).TypeIn(datapath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).TypeIn(metadatapath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).Clear();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).TypeIn(size);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).SendKeys(Keys.Tab);
            wait.Until(
                ExpectedConditions.TextToBePresentInElementValue(
                    By.CssSelector(CssSelestors.addStorageLocationDialogSize), size + ".00"));
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSaveBtn)).Click();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            driver.WaitForAjax();
            IWebElement storageLocationRow = wait.Until(
                ExpectedConditions.ElementExists(
                    By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableRowOne)));
            Assert.AreEqual(storageLocationRow.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableDataPathCell_relative)).Text, datapath);
            Assert.AreEqual(storageLocationRow.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableMetadataPathCell_relative)).Text, metadatapath);
            Assert.AreEqual(storageLocationRow.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableSizeCell_relative)).Text, size+" GB");
        }

        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "20")]
        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "2")]
        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "220")]
        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "134")]
        public void SaveValidNetworkStorageLocation(string networkpath, string username, string password, string size)
        {
            //open addStorageLocationDialog
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtnText)).Text,
                "Add New DVM Repository");
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtn)).Click();
            IWebElement uiDialogLevelOneTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelOneTitle)));
            Assert.AreEqual(uiDialogLevelOneTitle.Text, "Add New Repository");
            IWebElement addStorageLocationBtn =
                driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogAddStorageLocationBtn));
            Assert.AreEqual(addStorageLocationBtn.Text, "Add Storage Location");
            addStorageLocationBtn.Click();
            IWebElement addStorageLocationDialogTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            Assert.AreEqual(addStorageLocationDialogTitle.Text, "Add Storage Location");

            IWebElement sharedPathRadio = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSharedPathRadio));
            sharedPathRadio.Click();
            Assert.AreEqual(sharedPathRadio.GetAttribute("checked"), "true");
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath)).TypeIn(networkpath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUsername)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUsername)).TypeIn(username);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogPassword)).TypeIn(password);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).Clear();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).TypeIn(size);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).SendKeys(Keys.Tab);
            wait.Until(
                ExpectedConditions.TextToBePresentInElementValue(
                    By.CssSelector(CssSelestors.addStorageLocationDialogSize), size + ".00"));
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSaveBtn)).Click();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            driver.WaitForAjax();
            IWebElement storageLocationRow = wait.Until(
                ExpectedConditions.ElementExists(
                    By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableRowOne)));
            Assert.AreEqual(storageLocationRow.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableDataPathCell_relative)).Text, networkpath);
            Assert.AreEqual(storageLocationRow.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableMetadataPathCell_relative)).Text, networkpath);
            Assert.AreEqual(storageLocationRow.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableSizeCell_relative)).Text, size + " GB");
        }

        [TestCase("J:\\repo", "J:\\meta", "20", "K:\\repo", "K:\\meta", "21")]
        [TestCase("J:\\repo2", "J:\\meta2", "4", "K:\\repo2", "K:\\meta2", "3")]
        [TestCase("J:\\repo3", "J:\\meta3", "220", "K:\\repo3", "K:\\meta3", "221")]
        [TestCase("J:\\repo4", "J:\\meta4", "134", "K:\\repo4", "K:\\meta4", "133")]
        public void SaveSecondValidLocalStorageLocation(string datapath, string metadatapath, string size, string datapath2, string metadatapath2, string size2)
        {
            //open addStorageLocationDialog
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtnText)).Text,
                "Add New DVM Repository");
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtn)).Click();
            IWebElement uiDialogLevelOneTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelOneTitle)));
            Assert.AreEqual(uiDialogLevelOneTitle.Text, "Add New Repository");
            IWebElement addStorageLocationBtn =
                driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogAddStorageLocationBtn));
            Assert.AreEqual(addStorageLocationBtn.Text, "Add Storage Location");
            addStorageLocationBtn.Click();
            IWebElement addStorageLocationDialogTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            Assert.AreEqual(addStorageLocationDialogTitle.Text, "Add Storage Location");

            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPath)).TypeIn(datapath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).TypeIn(metadatapath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).Clear();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).TypeIn(size);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).SendKeys(Keys.Tab);
            wait.Until(
                ExpectedConditions.TextToBePresentInElementValue(
                    By.CssSelector(CssSelestors.addStorageLocationDialogSize), size + ".00"));
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSaveBtn)).Click();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            driver.WaitForAjax();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableRowOne)));

            //open add storage location dialog
            addStorageLocationBtn =
                driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogAddStorageLocationBtn));
            Assert.AreEqual(addStorageLocationBtn.Text, "Add Storage Location");
            addStorageLocationBtn.Click();
            addStorageLocationDialogTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            Assert.AreEqual(addStorageLocationDialogTitle.Text, "Add Storage Location");

            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPath)).TypeIn(datapath2);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).TypeIn(metadatapath2);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).Clear();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).TypeIn(size2);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).SendKeys(Keys.Tab);
            wait.Until(
                ExpectedConditions.TextToBePresentInElementValue(
                    By.CssSelector(CssSelestors.addStorageLocationDialogSize), size2 + ".00"));
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSaveBtn)).Click();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            driver.WaitForAjax();
            IWebElement storageLocationRow2 = wait.Until(
                ExpectedConditions.ElementExists(
                    By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableRowTwo)));
            IWebElement storageLocationRow = wait.Until(
                ExpectedConditions.ElementExists(
                    By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableRowOne)));
            Assert.AreEqual(storageLocationRow.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableDataPathCell_relative)).Text, datapath);
            Assert.AreEqual(storageLocationRow.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableMetadataPathCell_relative)).Text, metadatapath);
            Assert.AreEqual(storageLocationRow.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableSizeCell_relative)).Text, size + " GB");
            Assert.AreEqual(storageLocationRow2.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableDataPathCell_relative)).Text, datapath2);
            Assert.AreEqual(storageLocationRow2.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableMetadataPathCell_relative)).Text, metadatapath2);
            Assert.AreEqual(storageLocationRow2.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableSizeCell_relative)).Text, size2 + " GB");
        }

        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "20", "\\\\10.35.178.196\\sharedrepo2", "administrator", "123asdQ", "21")]
        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "4", "\\\\10.35.178.196\\sharedrepo2", "administrator", "123asdQ", "3")]
        [TestCase("\\\\10.35.178.196\\sharedrepo2", "administrator", "123asdQ", "220", "\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "221")]
        [TestCase("\\\\10.35.178.196\\sharedrepo2", "administrator", "123asdQ", "134", "\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "133")]
        public void SaveSecondValidNetworkStorageLocation(string networkpath, string username, string password, 
            string size, string networkpath2, string username2, string password2, string size2)
        {
            //open addStorageLocationDialog
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtnText)).Text,
                "Add New DVM Repository");
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtn)).Click();
            IWebElement uiDialogLevelOneTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelOneTitle)));
            Assert.AreEqual(uiDialogLevelOneTitle.Text, "Add New Repository");
            IWebElement addStorageLocationBtn =
                driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogAddStorageLocationBtn));
            Assert.AreEqual(addStorageLocationBtn.Text, "Add Storage Location");
            addStorageLocationBtn.Click();
            IWebElement addStorageLocationDialogTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            Assert.AreEqual(addStorageLocationDialogTitle.Text, "Add Storage Location");

            IWebElement sharedPathRadio = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSharedPathRadio));
            sharedPathRadio.Click();
            Assert.AreEqual(sharedPathRadio.GetAttribute("checked"), "true");
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath)).TypeIn(networkpath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUsername)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUsername)).TypeIn(username);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogPassword)).TypeIn(password);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).Clear();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).TypeIn(size);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).SendKeys(Keys.Tab);
            wait.Until(
                ExpectedConditions.TextToBePresentInElementValue(
                    By.CssSelector(CssSelestors.addStorageLocationDialogSize), size + ".00"));
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSaveBtn)).Click();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            driver.WaitForAjax();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableRowOne)));

            //open add storage location dialog
            addStorageLocationBtn =
                driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogAddStorageLocationBtn));
            Assert.AreEqual(addStorageLocationBtn.Text, "Add Storage Location");
            addStorageLocationBtn.Click();
            addStorageLocationDialogTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            Assert.AreEqual(addStorageLocationDialogTitle.Text, "Add Storage Location");

            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSharedPathRadio)).GetAttribute("checked"), "true");
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath)).TypeIn(networkpath2);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUsername)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUsername)).TypeIn(username2);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogPassword)).TypeIn(password2);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).Clear();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).TypeIn(size2);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).SendKeys(Keys.Tab);
            wait.Until(
                ExpectedConditions.TextToBePresentInElementValue(
                    By.CssSelector(CssSelestors.addStorageLocationDialogSize), size2 + ".00"));
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSaveBtn)).Click();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            driver.WaitForAjax();
            IWebElement storageLocationRow2 = wait.Until(
                ExpectedConditions.ElementExists(
                    By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableRowTwo)));
            IWebElement storageLocationRow = wait.Until(
                ExpectedConditions.ElementExists(
                    By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableRowOne)));

            Assert.AreEqual(storageLocationRow.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableDataPathCell_relative)).Text, networkpath);
            Assert.AreEqual(storageLocationRow.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableMetadataPathCell_relative)).Text, networkpath);
            Assert.AreEqual(storageLocationRow.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableSizeCell_relative)).Text, size + " GB");
            Assert.AreEqual(storageLocationRow2.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableDataPathCell_relative)).Text, networkpath2);
            Assert.AreEqual(storageLocationRow2.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableMetadataPathCell_relative)).Text, networkpath2);
            Assert.AreEqual(storageLocationRow2.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogStorageLocationsTableSizeCell_relative)).Text, size2 + " GB");
        }

        [TestCase("J:\\repo", "J:\\meta", "500")]
        [TestCase("J:\\repo2", "J:\\meta2", "700")]
        [TestCase("I:\\repo3", "I:\\meta3", "220")]
        [TestCase("I:\\repo4", "J:\\meta4", "50")]
        public void SaveInvalidLocalStorageLocationAdditionalSpaceRequired(string datapath, string metadatapath, string size)
        {
            //open addStorageLocationDialog
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtnText)).Text,
                "Add New DVM Repository");
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtn)).Click();
            IWebElement uiDialogLevelOneTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelOneTitle)));
            Assert.AreEqual(uiDialogLevelOneTitle.Text, "Add New Repository");
            IWebElement addStorageLocationBtn =
                driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogAddStorageLocationBtn));
            Assert.AreEqual(addStorageLocationBtn.Text, "Add Storage Location");
            addStorageLocationBtn.Click();
            IWebElement addStorageLocationDialogTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            Assert.AreEqual(addStorageLocationDialogTitle.Text, "Add Storage Location");

            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPath)).TypeIn(datapath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).TypeIn(metadatapath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).Clear();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).TypeIn(size);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).SendKeys(Keys.Tab);
            wait.Until(
                ExpectedConditions.TextToBePresentInElementValue(
                    By.CssSelector(CssSelestors.addStorageLocationDialogSize), size + ".00"));
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSaveBtn)).Click();
            driver.WaitForAjax();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(CssSelestors.uiDialogMessageBoxHeader)));
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.uiDialogMessageBoxHeader)).Text, "Error");
            Assert.True(driver.FindElement(By.CssSelector(CssSelestors.uiDialogMessageBoxMessageParagraph)).Text.Contains("free space is additionally required"));
        }

        [TestCase("S:\\repo", "J:\\meta")]
        [TestCase("J:\\repo2", "S:\\meta2")]
        [TestCase("P:\\repo3", "L:\\meta3")]
        [TestCase("J:\\repo4", "A:\\meta4")]
        public void SaveInvalidLocalStorageLocationIncorrectPath(string datapath, string metadatapath)
        {
            //open addStorageLocationDialog
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtnText)).Text,
                "Add New DVM Repository");
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtn)).Click();
            IWebElement uiDialogLevelOneTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelOneTitle)));
            Assert.AreEqual(uiDialogLevelOneTitle.Text, "Add New Repository");
            IWebElement addStorageLocationBtn =
                driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogAddStorageLocationBtn));
            Assert.AreEqual(addStorageLocationBtn.Text, "Add Storage Location");
            addStorageLocationBtn.Click();
            IWebElement addStorageLocationDialogTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            Assert.AreEqual(addStorageLocationDialogTitle.Text, "Add Storage Location");

            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogDataPath)).TypeIn(datapath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).TypeIn(metadatapath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogMetadataPath)).SendKeys(Keys.Tab);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSaveBtn)).Click();
            driver.WaitForAjax();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(CssSelestors.uiDialogMessageBoxHeader)));
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.uiDialogMessageBoxHeader)).Text, "Error");
            Assert.True(driver.FindElement(By.CssSelector(CssSelestors.uiDialogMessageBoxMessage)).Text.Contains("specified local paths are unreachable"));
        }

        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "400")]
        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "450")]
        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "500")]
        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "550")]
        public void SaveInvalidNetworkStorageLocationAdditionalSpaceRequired(string networkpath, string username, string password, string size)
        {
            //open addStorageLocationDialog
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtnText)).Text,
                "Add New DVM Repository");
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtn)).Click();
            IWebElement uiDialogLevelOneTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelOneTitle)));
            Assert.AreEqual(uiDialogLevelOneTitle.Text, "Add New Repository");
            IWebElement addStorageLocationBtn =
                driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogAddStorageLocationBtn));
            Assert.AreEqual(addStorageLocationBtn.Text, "Add Storage Location");
            addStorageLocationBtn.Click();
            IWebElement addStorageLocationDialogTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            Assert.AreEqual(addStorageLocationDialogTitle.Text, "Add Storage Location");

            IWebElement sharedPathRadio = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSharedPathRadio));
            sharedPathRadio.Click();
            Assert.AreEqual(sharedPathRadio.GetAttribute("checked"), "true");
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath)).TypeIn(networkpath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUsername)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUsername)).TypeIn(username);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogPassword)).TypeIn(password);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).Clear();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).TypeIn(size);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).SendKeys(Keys.Tab);
            wait.Until(
                ExpectedConditions.TextToBePresentInElementValue(
                    By.CssSelector(CssSelestors.addStorageLocationDialogSize), size + ".00"));
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSaveBtn)).Click();
            driver.WaitForAjax();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(CssSelestors.uiDialogMessageBoxHeader)));
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.uiDialogMessageBoxHeader)).Text, "Error");
            Assert.True(driver.FindElement(By.CssSelector(CssSelestors.uiDialogMessageBoxMessage)).Text.Contains("free space is additionally required"));
        }

        [TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "20")]
        //[TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "2")]
        //[TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "220")]
        //[TestCase("\\\\10.35.178.196\\sharedrepo", "administrator", "123asdQ", "134")]
        public void SaveInvalidNetworkStorageLocationIncorrectPath()
        {
            //open addStorageLocationDialog
            Assert.AreEqual(driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtnText)).Text,
                "Add New DVM Repository");
            driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryBtn)).Click();
            IWebElement uiDialogLevelOneTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelOneTitle)));
            Assert.AreEqual(uiDialogLevelOneTitle.Text, "Add New Repository");
            IWebElement addStorageLocationBtn =
                driver.FindElement(By.CssSelector(CssSelestors.addNewRepositoryDialogAddStorageLocationBtn));
            Assert.AreEqual(addStorageLocationBtn.Text, "Add Storage Location");
            addStorageLocationBtn.Click();
            IWebElement addStorageLocationDialogTitle =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelestors.uiDialogLevelTwoTitle)));
            Assert.AreEqual(addStorageLocationDialogTitle.Text, "Add Storage Location");

            IWebElement sharedPathRadio = driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSharedPathRadio));
            sharedPathRadio.Click();
            Assert.AreEqual(sharedPathRadio.GetAttribute("checked"), "true");
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath)).TypeIn(networkpath);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUsername)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUsername)).TypeIn(username);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogUNCPath)).Click();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogPassword)).TypeIn(password);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).Clear();
            driver.WaitForAjax();
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).TypeIn(size);
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSize)).SendKeys(Keys.Tab);
            wait.Until(
                ExpectedConditions.TextToBePresentInElementValue(
                    By.CssSelector(CssSelestors.addStorageLocationDialogSize), size + ".00"));
            driver.FindElement(By.CssSelector(CssSelestors.addStorageLocationDialogSaveBtn)).Click();
        }

        public void SaveSecondInvalidLocalStorageLocation() //path in use
        {

        }

        public void SaveSecondInvalidNetworkStorageLocation() //path is use
        {

        }

        public void SaveTwoStorageLocationsDifferentTypes()
        {
            
        }

        public void EditLocalStorageLocationNewRepo()
        {
            
        }

        public void EditNetworkStorageLocationNewRepo()
        {
            
        }

        
    }

    public class RepositoryPageExistingRepo
    {
        //EditLocalStorageLocationExistingRepo
        //EditNetworkStorageLocationExistingRepo
    }
}
