using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
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
        public string ButtonGroupSelector { get; }
        public string ButtonSelector { get; }
        public string DropdownSelector { get; }
        public string ItemSelector { get; }
        public DropdownMenu(IWebDriver driver, string buttonGroupSelector, string buttonSelector, string dropdownSelector)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            ButtonGroupSelector = buttonGroupSelector;
            ButtonSelector = buttonSelector;
            DropdownSelector = dropdownSelector;
            ItemSelector = XPathSelectors.DropdownMenuItemSelector;
        }

        public void Open()
        {
            Driver.FindByCss(ButtonSelector).Click();
            Assert.IsTrue(Driver.FindByCss(DropdownSelector).Displayed);
        }
        
        private IWebElement LookFor(string option)
        {
            var elements = Driver.FindByCss(ButtonGroupSelector).FindElements(By.XPath(ItemSelector));
            foreach (var element in elements)
            {
                if (element.Text == option)
                {
                    return element;
                }
            }
            throw new NoSuchElementException("What you are looking for is not na option");
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
            return new StorageLocationDialog(storageLocationDialog, Driver, true);
        }
    }
}
