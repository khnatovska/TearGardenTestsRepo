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
    //[TestFixture]
    public class Repository
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        //[OneTimeSetUp]
        public void RepositoryTestSetup()
        {
            //ChromeDriver WITH OVERLAY WIDGET BUG
            //driver = Root.SetUpChromeDriver();

            ////FIREFOX with lame untrusted certificate exception workaround
            driver = Root.SetUpFirefoxDriver();

            wait = Root.SetUptWebDriverWait(driver);
            Root.OpenCoreAdmin(driver);
            Root.NavigateToRepositoriesPage(driver);
        }

        //[Test]
        public void LaunchDVMAddNewRepositoryDialog() //start: repositories page. find Add New Repo button, verify title, open add repo dialog, verify title
        {
            IWebElement addNewRepoBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#newMainRepository > span")));
            string addNewRepoBtnText = addNewRepoBtn.Text;
            Assert.AreEqual(addNewRepoBtnText, "Add New DVM Repository");
            addNewRepoBtn.Click();
            IWebElement addNewRepoDialogTitle = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("ui-id-3")));
            Assert.AreEqual(addNewRepoDialogTitle.Text, "Add New Repository");
        }

        //[Test]
        public void CloseDVMAddNewRepositoryDialog() //start: repositories page. open add repo dialog, close with button, reopen dialog, close with cross icon
        {
            LaunchDVMAddNewRepositoryDialog();
            IWebElement cancelButton = driver.FindElement(By.Id("createRepoCancel"));
            //FIND AN ELEGANT WAY TO CHECK THAT DIALOG IS REALLY CLOSED
            cancelButton.Click();
            var dialogClosed = wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("ui-id-3")));
            Assert.IsTrue(dialogClosed);
            //LaunchDVMAddNewRepositoryDialog();
            //IWebElement cancelIcon = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.ui-dialog-titlebar span.ui-icon-closeright")));
            //cancelIcon.Click();
            //dialogClosed = wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("ui-id-3")));
            //Assert.IsTrue(dialogClosed);
        }

        //[Test]
        public void CreateDVMRepoWithoutStorageLocation() //start: add new DVM repo dialog. find create repo button, click, verify warning message, close warning dialog
        {
            LaunchDVMAddNewRepositoryDialog();
            IWebElement createRepoBtn = driver.FindElement(By.Id("createRepoButton"));
            createRepoBtn.Click();
            IWebElement warningHeader = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("msgbox-message-header")));
            IWebElement warningMessage = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("msgbox-message")));
            Assert.AreEqual(warningHeader.Text, "Warning");
            Assert.AreEqual(warningMessage.Text, "You must add at least one storage location");
            IWebElement closeBtn = driver.FindElement(By.CssSelector("div.messageboxButtonsContainer button.default"));
            closeBtn.Click();
        }

        //[Test]
        public void CreareDVMRepoWithEmptyName() //start: add new DVM repo dialog. find name, verify label. clear, click create button, vefiry validation
        {
            LaunchDVMAddNewRepositoryDialog();
            IWebElement repoNameLabel = driver.FindElement(By.CssSelector("label.control-label[for=\"Name\""));
            IWebElement repoNameLabelParent = repoNameLabel.FindElement(By.XPath(".."));
            Assert.AreEqual(repoNameLabel.Text, "Repository name");
            IWebElement repoName = driver.FindElement(By.Id("RepoName"));
            repoName.Clear();
            IWebElement createRepoBtn = driver.FindElement(By.Id("createRepoButton"));
            createRepoBtn.Click();
            driver.WaitForAjax();
            Assert.IsTrue(repoName.HasClass("input-validation-error")); //IS IT RELIABLE - CHECK CLASS AND NOT ACTUAL STYLE? HOW TO CHECK ACTUAL STYLE? NO WAY TO CHECK TOOLTIP
            Assert.IsTrue(repoNameLabelParent.HasClass("has-error"));
        }

        //[Test]
        public void CreareDVMRepoWithInvalidName() //start: add new DVM repo dialog. find name. loop - incorrect value, click create button, vefiry validation
        {
            LaunchDVMAddNewRepositoryDialog();
            IWebElement repoNameLabel = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("label.control-label[for=\"Name\"")));
            IWebElement repoNameLabelParent = repoNameLabel.FindElement(By.XPath(".."));
            Assert.AreEqual(repoNameLabel.Text, "Repository name");
            IWebElement repoName = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("RepoName")));
            IWebElement createRepoBtn = wait.Until(ExpectedConditions.ElementToBeClickable((By.Id("createRepoButton"))));
            //invalid values
            foreach (var invalidName in Root.invalidRepoNames)
            {
                repoName.SendKeys(invalidName);
                createRepoBtn.Click();
                driver.WaitForAjax();
                Assert.IsTrue(repoName.HasClass("input-validation-error")); //IS IT RELIABLE - CHECK CLASS AND NOT ACTUAL STYLE? HOW TO CHECK ACTUAL STYLE? NO WAY TO CHECK TOOLTIP
                Assert.IsTrue(repoNameLabelParent.HasClass("has-error"));
                repoName.Clear();
            }
            //too long
            string longName = new String('q', 41);
            repoName.SendKeys(longName);
            Assert.AreEqual(repoName.GetAttribute("value").Length, 40);
        }

        //[Test]
        public void CreareDVMRepoWithEmptyConcurOperations() //start: add new DVM repo dialog. find concurrent operations. clear, click create button, vefiry validation
        {
            LaunchDVMAddNewRepositoryDialog();
            IWebElement concurOperationsLabel = driver.FindElement(By.CssSelector("label.control-label[for=\"MaxConcurrentOperations\""));
            IWebElement concurOperationsLabelParent = concurOperationsLabel.FindElement(By.XPath(".."));
            Assert.AreEqual(concurOperationsLabel.Text, "Concurrent operations");
            IWebElement concurOperations = driver.FindElement(By.Id("MaximumConcurrentOperations"));
            concurOperations.Clear();
            IWebElement createRepoBtn = driver.FindElement(By.Id("createRepoButton"));
            createRepoBtn.Click();
            driver.WaitForAjax();
            //CLASS BELOW IS NOT APPLIED FOR SOME REASON
            //Assert.IsTrue(concurOperations.HasClass("input-validation-error")); //IS IT RELIABLE - CHECK CLASS AND NOT ACTUAL STYLE? HOW TO CHECK ACTUAL STYLE? NO WAY TO CHECK TOOLTIP
            Assert.IsTrue(concurOperationsLabelParent.HasClass("has-error"));
        }

        //[Test]
        public void CreareDVMRepoWithInvalidConcurOperations() //start: add new DVM repo dialog. find concurrent operations. loop - incorect value, click create button, vefiry validation
        {
            LaunchDVMAddNewRepositoryDialog();
            IWebElement concurOperationsLabel = driver.FindElement(By.CssSelector("label.control-label[for=\"MaxConcurrentOperations\""));
            IWebElement concurOperationsLabelParent = concurOperationsLabel.FindElement(By.XPath(".."));
            Assert.AreEqual(concurOperationsLabel.Text, "Concurrent operations");
            IWebElement concurOperations = driver.FindElement(By.Id("MaximumConcurrentOperations"));
            concurOperations.Clear();
            IWebElement createRepoBtn = driver.FindElement(By.Id("createRepoButton"));
            var invalidConcurOperations = new List<string> { "0", "1025", "q", "4.5" };
            foreach (var invalidValue in invalidConcurOperations)
            {
                concurOperations.SendKeys(invalidValue);
                createRepoBtn.Click();
                driver.WaitForAjax();
                Assert.IsTrue(concurOperationsLabelParent.HasClass("has-error"));
                concurOperations.Clear();
            }
        }

        //[Test]
        public void LaunchAddStorageLocationDialog() //start: add new DVM repo dialog. find add storage location button, verify title, click, verify dialog title
        {
            LaunchDVMAddNewRepositoryDialog();
            IWebElement addStorageLocationBtn = driver.FindElement(By.CssSelector("#popup1 div.popup-inner-content nav.navbar-actions span"));
            Assert.AreEqual(addStorageLocationBtn.Text, "Add Storage Location");
            addStorageLocationBtn.Click();
            IWebElement addStorageLocationDialogTitle = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("ui-id-4")));
            Assert.AreEqual(addStorageLocationDialogTitle.Text, "Add Storage Location");
            
        }

        //[Test]
        public void SaveStorageLocationWithEmptyLocalPath() //start: add storage location dialog. verify local path radio and labels, find save button, click, verify validation
        {
            LaunchDVMAddNewRepositoryDialog();
            LaunchAddStorageLocationDialog();
            IWebElement localPathRadio = driver.FindElement(By.Id("addOnlocalDisk"));
            IWebElement dataPathLabel = driver.FindElement(By.CssSelector("label.control-label[for=\"DataPath\""));
            IWebElement dataPathLabelParent = dataPathLabel.FindElement(By.XPath(".."));
            IWebElement dataPath = driver.FindElement(By.Id("FileSpecDataPath"));
            IWebElement metadataPathLabel = driver.FindElement(By.CssSelector("label.control-label[for=\"MetadataPath\""));
            IWebElement metadataPathLabelParent = metadataPathLabel.FindElement(By.XPath(".."));
            IWebElement metadataPath = driver.FindElement(By.Id("FileSpecMetadataPath"));
            IWebElement saveButton = driver.FindElement(By.Id("editFileSpecOK"));
            Assert.AreEqual(localPathRadio.GetAttribute("checked"), "true");
            saveButton.Click();
            driver.WaitForAjax();
            Assert.IsTrue(dataPath.HasClass("input-validation-error")); //IS IT RELIABLE - CHECK CLASS AND NOT ACTUAL STYLE? HOW TO CHECK ACTUAL STYLE? NO WAY TO CHECK TOOLTIP
            Assert.IsTrue(dataPathLabelParent.HasClass("has-error"));
            Assert.IsTrue(metadataPath.HasClass("input-validation-error"));
            Assert.IsTrue(metadataPathLabelParent.HasClass("has-error"));
        }

        //[Test]
        public void SaveStorageLocationWithInvalidLocalPath() //start: add storage location dialog. verify local radio, loop - enter invalid values and check validation for data and metadata
        {
            //THIS IS INEFFICIENT MAKE IT BETTER
            LaunchDVMAddNewRepositoryDialog();
            LaunchAddStorageLocationDialog();
            IWebElement localPathRadio = driver.FindElement(By.Id("addOnlocalDisk"));
            IWebElement dataPathLabel = driver.FindElement(By.CssSelector("label.control-label[for=\"DataPath\""));
            IWebElement dataPathLabelParent = dataPathLabel.FindElement(By.XPath(".."));
            IWebElement dataPath = driver.FindElement(By.Id("FileSpecDataPath"));
            IWebElement metadataPathLabel = driver.FindElement(By.CssSelector("label.control-label[for=\"MetadataPath\""));
            IWebElement metadataPathLabelParent = metadataPathLabel.FindElement(By.XPath(".."));
            IWebElement metadataPath = driver.FindElement(By.Id("FileSpecMetadataPath"));
            IWebElement saveButton = driver.FindElement(By.Id("editFileSpecOK"));
            Assert.AreEqual(localPathRadio.GetAttribute("checked"), "true");
            string validPath = "C:\\";
            foreach (var invalidValue in Root.invalidRepoNames)
            {
                string invalidPath = validPath + invalidValue;
                Verifier.VerifyInputFieldValidation(driver, dataPath, dataPathLabelParent, invalidValue, "input-validation-error", "has-error");
                Verifier.VerifyInputFieldValidation(driver, dataPath, dataPathLabelParent, invalidPath, "input-validation-error", "has-error");
                Verifier.VerifyInputFieldValidation(driver, metadataPath, metadataPathLabelParent, invalidValue, "input-validation-error", "has-error");
                Verifier.VerifyInputFieldValidation(driver, metadataPath, metadataPathLabelParent, invalidPath, "input-validation-error", "has-error");
            }
            var invalidPathWithSpace = new List<string> { " ", validPath + " ", " " + validPath };
            foreach (var invalidPath in invalidPathWithSpace)
            {
                Verifier.VerifyInputFieldValidation(driver, dataPath, dataPathLabelParent, invalidPath, "input-validation-error", "has-error");
                Verifier.VerifyInputFieldValidation(driver, metadataPath, metadataPathLabelParent, invalidPath, "input-validation-error", "has-error");
            }
            var invalidDrive = new List<string> { "C :\\", "C: \\", "C:", "CC:\\" };
            foreach (var invalidPath in invalidDrive)
            {
                Verifier.VerifyInputFieldValidation(driver, dataPath, dataPathLabelParent, invalidPath, "input-validation-error", "has-error");
                Verifier.VerifyInputFieldValidation(driver, metadataPath, metadataPathLabelParent, invalidPath, "input-validation-error", "has-error");
            }
        }

        //[Test]
        public void SaveStorageLocationWithEmptySharedPath() //start: add storage location dialog. select shared path radio, verify labels, find save button, click, verify validation
        {
            LaunchDVMAddNewRepositoryDialog();
            LaunchAddStorageLocationDialog();
            IWebElement sharedPathRadio = driver.FindElement(By.Id("addOnCifsShare"));
            IWebElement saveButton = driver.FindElement(By.Id("editFileSpecOK"));
            sharedPathRadio.Click();
            Assert.AreEqual(sharedPathRadio.GetAttribute("checked"), "true");
            IWebElement sharedPathLabel = driver.FindElement(By.CssSelector("label.control-label[for=\"UncPath\""));
            IWebElement sharedPathLabelParent = sharedPathLabel.FindElement(By.XPath(".."));
            IWebElement sharedPath = driver.FindElement(By.Id("UncPath"));
            IWebElement userNameLabel = driver.FindElement(By.CssSelector("label.control-label[for=\"NetworkUserName\""));
            IWebElement userNameLabelParent = userNameLabel.FindElement(By.XPath(".."));
            IWebElement userName = driver.FindElement(By.Id("FileSpecNetworkUserName"));
            IWebElement passwordLabel = driver.FindElement(By.CssSelector("label.control-label[for=\"NetworkPassword\""));
            IWebElement passwordLabelParent = passwordLabel.FindElement(By.XPath(".."));
            IWebElement password = driver.FindElement(By.Id("FileSpecNetworkPassword"));
            saveButton.Click();
            driver.WaitForAjax();
            Assert.IsTrue(sharedPath.HasClass("input-validation-error")); //IS IT RELIABLE - CHECK CLASS AND NOT ACTUAL STYLE? HOW TO CHECK ACTUAL STYLE? NO WAY TO CHECK TOOLTIP
            Assert.IsTrue(sharedPathLabelParent.HasClass("has-error"));
            //Assert.IsTrue(userName.HasClass("input-validation-error")); //CLASS NOT APPLIED FOR SOME REASON
            Assert.IsTrue(userNameLabelParent.HasClass("has-error"));
            //Assert.IsTrue(password.HasClass("input-validation-error")); //CLASS NOT APPLIED FOR SOME REASON
            Assert.IsTrue(passwordLabelParent.HasClass("has-error"));
        }

        //[Test]
        public void SaveStorageLocationWithInvalidSharedPath() //start: add storage location dialog. select shared path radio, loop - enter invalid values for shared path and check validation for path
        {
            //THIS IS INEFFICIENT MAKE IT BETTER
            LaunchDVMAddNewRepositoryDialog();
            LaunchAddStorageLocationDialog();
            IWebElement sharedPathRadio = driver.FindElement(By.Id("addOnCifsShare"));
            IWebElement saveButton = driver.FindElement(By.Id("editFileSpecOK"));
            sharedPathRadio.Click();
            Assert.AreEqual(sharedPathRadio.GetAttribute("checked"), "true");
            IWebElement sharedPathLabel = driver.FindElement(By.CssSelector("label.control-label[for=\"UncPath\""));
            IWebElement sharedPathLabelParent = sharedPathLabel.FindElement(By.XPath(".."));
            IWebElement sharedPath = driver.FindElement(By.Id("UncPath"));
            string validSharedPath = "\\\\validsharedpath\\";
            foreach (var invalidValue in Root.invalidRepoNames)
            {
                string invalidSharedPath = validSharedPath + invalidValue;
                string invalidHostName = "\\" + invalidValue;
                Verifier.VerifyInputFieldValidation(driver, sharedPath, sharedPathLabelParent, invalidValue, "input-validation-error", "has-error");
                Verifier.VerifyInputFieldValidation(driver, sharedPath, sharedPathLabelParent, invalidSharedPath, "input-validation-error", "has-error");
                Verifier.VerifyInputFieldValidation(driver, sharedPath, sharedPathLabelParent, invalidHostName, "input-validation-error", "has-error");
            }
            var invalidSharedPathWithSpace = new List<string> { " ", "\\\\validsharedpath\\ ", " " + validSharedPath, "\\ \\folder" };
            foreach (var invalidPath in invalidSharedPathWithSpace)
            {
                Verifier.VerifyInputFieldValidation(driver, sharedPath, sharedPathLabelParent, invalidPath, "input-validation-error", "has-error");
            }
        }

        //[Test]
        public void SaveStorageLocationWithInvalidSharedPathUserName() //start: add storage location dialog. select shared path radio, loop - enter invalid values for user of shared path and check validation for name
        {
            LaunchDVMAddNewRepositoryDialog();
            LaunchAddStorageLocationDialog();
            IWebElement sharedPathRadio = driver.FindElement(By.Id("addOnCifsShare"));
            IWebElement saveButton = driver.FindElement(By.Id("editFileSpecOK"));
            sharedPathRadio.Click();
            Assert.AreEqual(sharedPathRadio.GetAttribute("checked"), "true");
            IWebElement userNameLabel = driver.FindElement(By.CssSelector("label.control-label[for=\"NetworkUserName\""));
            IWebElement userNameLabelParent = userNameLabel.FindElement(By.XPath(".."));
            IWebElement userName = driver.FindElement(By.Id("FileSpecNetworkUserName"));
            //invalid values
            foreach (var invalidValue in Root.invalidUserNames)
            {
                Verifier.VerifyInputFieldValidation(driver, userName, userNameLabelParent, invalidValue, "has-error");
            }
            //too long
            string longName = new String('q', 255);
            userName.SendKeys(longName);
            Assert.AreEqual(userName.GetAttribute("value").Length, 254);
        }

       // [Test]
        public void SaveStorageLocationWithInvalidSharedPathPassword() //start: add storage location dialog. select shared path radio, enter invalid (too log) password for shared path and check validation for password
        {
            LaunchDVMAddNewRepositoryDialog();
            LaunchAddStorageLocationDialog();
            IWebElement sharedPathRadio = driver.FindElement(By.Id("addOnCifsShare"));
            IWebElement saveButton = driver.FindElement(By.Id("editFileSpecOK"));
            sharedPathRadio.Click();
            Assert.AreEqual(sharedPathRadio.GetAttribute("checked"), "true");
            IWebElement passwordLabel = driver.FindElement(By.CssSelector("label.control-label[for=\"NetworkPassword\""));
            IWebElement passwordLabelParent = passwordLabel.FindElement(By.XPath(".."));
            IWebElement password = driver.FindElement(By.Id("FileSpecNetworkPassword"));
            //too long
            string longName = new String('q', 105);
            password.SendKeys(longName);
            Assert.AreEqual(password.GetAttribute("value").Length, 104);
        }

        //[Test]
        public void AddStorageLocationChangeSize() //start: add storage location dialog. verify size units in dropdown, select them, change size with arrows, enter size
        {
            LaunchDVMAddNewRepositoryDialog();
            LaunchAddStorageLocationDialog();
            IWebElement size = driver.FindElement(By.Id("FileSpecSize"));
            IWebElement sizeUnit = driver.FindElement(By.Id("dropdown-visual-input-FileSpecSizeUnit"));
            IWebElement sizeUnitDefaultValue = driver.FindElement(By.CssSelector("#dropdown-wrapper-FileSpecSizeUnit > span.dropdown-inner-text.cutText"));
            //check default values
            Assert.AreEqual(sizeUnitDefaultValue.Text, "GB");
            Assert.AreEqual(size.GetAttribute("value"), "250.00");
            //change size unit
            sizeUnit.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("dropdown-menu-FileSpecSizeUnit")));
            Assert.AreEqual(driver.FindElement(By.CssSelector("#dropdown-menu-FileSpecSizeUnit > ul > li:nth-child(1) > label")).Text, "GB");
            Assert.AreEqual(driver.FindElement(By.CssSelector("#dropdown-menu-FileSpecSizeUnit > ul > li:nth-child(2) > label")).Text, "TB");
            Assert.IsTrue(driver.FindElement(By.CssSelector("#dropdown-menu-FileSpecSizeUnit > ul > li:nth-child(1)")).HasClass("selected"));
            driver.FindElement(By.CssSelector("#dropdown-menu-FileSpecSizeUnit > ul > li:nth-child(2)")).Click();
            Assert.AreEqual(sizeUnitDefaultValue.Text, "TB");
            //change size
            driver.FindElement(By.CssSelector("#popup2 button.bootstrap-touchspin-up")).Click();
            Assert.AreEqual(size.GetAttribute("value"), "250.01");
            size.Clear();
            size.SendKeys("100");
            driver.FindElement(By.CssSelector("#popup2 button.bootstrap-touchspin-down")).Click();
            Assert.AreEqual(size.GetAttribute("value"), "99.99");
            size.Clear();
            size.SendKeys("0");
            driver.FindElement(By.CssSelector("#popup2 button.bootstrap-touchspin-down")).Click();
            Assert.AreEqual(size.GetAttribute("value"), "0.00");
        }

        //[Test]
        public void SaveStorageLocationWithEmptySize() //start: add storage location dialog. add local path. remove size, click save, verify validation
        {
            LaunchDVMAddNewRepositoryDialog();
            LaunchAddStorageLocationDialog();
            IWebElement size = driver.FindElement(By.Id("FileSpecSize"));
            IWebElement saveButton = driver.FindElement(By.Id("editFileSpecOk"));
            size.Clear();

        }

        //[Test]
        //public void SaveStorageLocationWithInvalidSize() //start: add storage location dialog. 
        //{

        //}

        //[Test]
        //public void SaveStorageLocationWithInvalidSharedPathPassword3() //start: add storage location dialog. 
        //{

        //}

        //[OneTimeTearDown]
        public void RepositoryTestTearDown()
        {
            Root.QuitCore(driver);
        }
    }
}