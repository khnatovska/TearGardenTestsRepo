using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Surfer.PageObjects
{
    public class DropdownMenu
    {
        //design is valid for the storage location dropdown menu in repo dialog. to be extended later
        public IWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; }
        public string Row { get; }
        public string ItemSelector { get; }

        public DropdownMenu(IWebDriver driver, string rowSelector)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            Row = rowSelector;
            ItemSelector = XPathSelectors.DropdownMenuItemSelector;
        }
        
        public void Open()
        {
            GetButton().Click();
            Driver.WaitForAjax();
            var dropdown = GetDropdown();
            Assert.IsTrue(dropdown.Displayed);
        }

        private IWebElement GetActionsCell()
        {
            var row = Driver.FindByCss(Row);
            var actionsCell = row.FindByCss(CssSelectors.RepoDialogStorageLocationsTableActionsCell);
            return actionsCell;
        }

        private IWebElement GetButton()
        {
            var cell = GetActionsCell();
            var btn = cell.FindByCss(CssSelectors.DropdownMenuBtn);
            return btn;
        }

        private IWebElement GetDropdown()
        {
            var cell = GetActionsCell();
            var dropdown = cell.FindByCss(CssSelectors.DropdownMenuDropdown);
            return dropdown;
        }

        private IReadOnlyCollection<IWebElement> GetOptions()
        {
            var cell = GetActionsCell();
            var options = cell.FindElements(By.CssSelector(CssSelectors.DropdownMenuOptions));
            return options;
        }
        
        private IWebElement LookFor(string option)
        {
            var elements = GetOptions();
            foreach (var element in elements)
            {
                if (element.Text == option)
                {
                    Wait.Until(ExpectedConditions.ElementToBeClickable(element));
                    return element;
                }
            }
            throw new NoSuchElementException("What you are looking for is not an option");
        }

        public void ClickDelete()
        {
            var delecteOption = LookFor("Delete");
            delecteOption.Click();
        }

        public StorageLocationDialog ClickEdit()
        {
            var editOption = LookFor("Edit");
            editOption.Click();
            var storageLocationDialog = Driver.FindByCssVisible(CssSelectors.uiDialogLevelTwo);
            return new StorageLocationDialog(storageLocationDialog, Driver, editing: true);
        }
    }
}
