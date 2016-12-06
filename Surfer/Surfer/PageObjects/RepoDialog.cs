using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Surfer.PageObjects
{
    public class RepoDialog
    {
        public IWebElement Dialog { get; }
        public IWebDriver Driver { get; }
        public WebDriverWait Wait { get; }
        public string Title { get; } = "Add New Repository";
        public string Name { get; }
        public string ConcurOperations { get; }
        public string Comments { get; }
        public string StorageLocationBtn { get; } = "Add Storage Location";

        public RepoDialog(IWebElement dialog, IWebDriver driver)
        {
            Dialog = dialog;
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void VerifyTitle()
        {
            var title = Dialog.FindByCss(CssSelectors.uiDialogLevelOneTitle);
            Assert.AreEqual(Title, title.Text);
        }

        public void VerifyStorageLocationBtn()
        {
            var storageLocationBtn = Dialog.FindByCss(CssSelectors.RepoDialogAddStorageLocationBtn);
            Assert.AreEqual(StorageLocationBtn, storageLocationBtn.Text);
        }

        public StorageLocationDialog ClickAddNewStorageLocationBtn()
        {
            var addStorageLocationBtn = Driver.FindByCssClickable(CssSelectors.RepoDialogAddStorageLocationBtn);
            addStorageLocationBtn.Click();
            var storageLocationDialog = Driver.FindByCssVisible(CssSelectors.uiDialogLevelTwo);
            return new StorageLocationDialog(storageLocationDialog, Driver);
        }

        public void Create()
        {
            Driver.FindByCssClickable(CssSelectors.RepoDialogCreateBtn).Click();
        }

        public void Close()
        {
            Driver.FindByCssClickable(CssSelectors.RepoDialogCancelBtn).Click();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelectors.uiDialogLevelOne)));
        }

        public WarningMessageBox VerifyWarningAppearance()
        {
            var warningBox = Driver.FindByCssVisible(CssSelectors.uiDialogMessageBox);
            return new WarningMessageBox(Driver, warningBox);
        }

        public StorageLocationDialog VerifyStorageDialogDisappearance()
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelectors.uiDialogLevelTwo)));
            return null;
        }

        public LabeledInputField GetRepoNameInputField()
        {
            return new LabeledInputField(Driver, CssSelectors.RepoDialogRepoName, CssSelectors.RepoDialogRepoNameLabel);
        }

        public LabeledInputField GetConcurOperationsInputField()
        {
            return new LabeledInputField(Driver, CssSelectors.RepoDialogConcurOperations, CssSelectors.RepoDialogConcurOperationsLabel);
        }

        public StorageLocationTableRow GetStorageLocationTableRow(int rowNumber)
        {
            var rowSelector = CssSelectors.GetChild(CssSelectors.RepoDialogStorageLocationsTableRow, rowNumber + 1);
            Driver.WaitForAjax();
            Wait.Until(ExpectedConditions.ElementExists(By.CssSelector(rowSelector)));
            var table = Driver.FindByCss(CssSelectors.RepoDialogStorageLocationsTable);
            return new StorageLocationTableRow(Driver, table, rowSelector);
        }

        public void VerifyStorageLocationsConfigurationNoDataToDisplay()
        {
            Driver.WaitForAjax();
            Wait.Until(ExpectedConditions.ElementExists(By.CssSelector(CssSelectors.RepoDialogStorageLocationsTableNoData)));
            Assert.AreEqual("No data to display", Driver.FindByCss(CssSelectors.RepoDialogStorageLocationsTableNoData).Text);
        }
    }
}
