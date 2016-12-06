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
    public class StorageLocationDialog
    {
        public IWebElement Dialog { get; }
        public IWebDriver Driver { get; }
        public WebDriverWait Wait { get; }
        public string Title { get; } = "Add Storage Location";
        public string DefaultSizeUnit { get; } = "GB";

        public StorageLocationDialog(IWebElement dialog, IWebDriver driver, bool editing = false)
        {
            Dialog = dialog;
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            if (editing)
                Title = "Edit Storage Location";
        }

        public void VerifyTitle()
        {
            var title = Dialog.FindByCss(CssSelectors.uiDialogLevelTwoTitle);
            Assert.AreEqual(Title, title.Text);
        }

        public void Save()
        {
            Driver.FindByCssClickable(CssSelectors.StorageLocationDialogSaveBtn).Click();
        }

        public void Close()
        {
            Driver.FindByCssClickable(CssSelectors.StorageLocationDialogCancelBtn).Click();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelectors.uiDialogLevelTwo)));
        }

        public void SelectLocalPathRadio()
        {
            Driver.FindByCss(CssSelectors.StorageLocationDialogLocalPathRadio).Click();
        }

        public void LocalPathRadioIsSelected()
        {
            Assert.AreEqual(Driver.FindByCss(CssSelectors.StorageLocationDialogLocalPathRadio).GetAttribute("checked"), "true");
        }

        public void SelectNetworkPathRadio()
        {
            Driver.FindByCss(CssSelectors.StorageLocationDialogNetworkPathRadio).Click();
        }

        public void NetworkPathRadioIsSelected()
        {
            Assert.AreEqual(Driver.FindByCss(CssSelectors.StorageLocationDialogNetworkPathRadio).GetAttribute("checked"), "true");
        }

        public LabeledInputField GetLocalDataPathInputField()
        {
            return new LabeledInputField(Driver, CssSelectors.StorageLocationDialogDataPath, CssSelectors.StorageLocationDialogDataPathLabel);
        }

        public LabeledInputField GetLocalMetadataPathInputField()
        {
            return new LabeledInputField(Driver, CssSelectors.StorageLocationDialogMetadataPath, CssSelectors.StorageLocationDialogMetadataPathLabel);
        }

        public LabeledInputField GetUncPathInputField()
        {
            return new LabeledInputField(Driver, CssSelectors.StorageLocationDialogUNCPath, CssSelectors.StorageLocationDialogUNCPathLabel);
        }

        public LabeledInputField GetUsernameInputField()
        {
            return new LabeledInputField(Driver, CssSelectors.StorageLocationDialogUsername, CssSelectors.StorageLocationDialogUsernameLabel);
        }

        public LabeledInputField GetPasswordInputField()
        {
            return new LabeledInputField(Driver, CssSelectors.StorageLocationDialogPassword, CssSelectors.StorageLocationDialogPasswordLabel);
        }

        public DropdownList GetSizeUnitDropdown()
        {
            return new DropdownList(Driver, CssSelectors.StorageLocationDialogSizeUnitSelectBtn, CssSelectors.StorageLocationDialogSizeUnitDropdown,
                CssSelectors.StorageLocationDialogSizeUnitValue);
        }

        public SpinnerInputField GetSizeInputField()
        {
            return new SpinnerInputField(Driver, CssSelectors.StorageLocationDialogSize, CssSelectors.StorageLocationDialogSizeControlUp,
                CssSelectors.StorageLocationDialogSizeControlDown);
        }

        public IWebElement GetDetailsToggle()
        {
            return Driver.FindByCss(CssSelectors.StorageLocationDialogDetailsToggle);
        }

        public void ToggleDetails()
        {
            Driver.FindByCss(CssSelectors.StorageLocationDialogDetailsToggle).Click();
            Driver.WaitForAjax();
        }

        public IWebElement GetCashingPolicyDropdown()
        {
            return Driver.FindByCss(CssSelectors.StorageLocationDialogDetailsCashingPolicyDropdown);
        }

        public IWebElement GetBytesPerSectorDropdown()
        {
            return Driver.FindByCss(CssSelectors.StorageLocationDialogDetailsBytesPerSectorDropdown);
        }

        public IWebElement GetAvrgBytesPerSector()
        {
            return Driver.FindByCss(CssSelectors.StorageLocationDialogDetailsAvrgBytesPerSector);
        }

        public ErrorMessageBox VerifyErrorAppearance()
        {
            var errorBox = Driver.FindByCssVisible(CssSelectors.uiDialogMessageBox);
            return new ErrorMessageBox(Driver, errorBox);
        }
    }
}
