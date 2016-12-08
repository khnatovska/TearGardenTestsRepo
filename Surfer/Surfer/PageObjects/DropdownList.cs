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
    public class DropdownList
    {
        public IWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; }
        public string ButtonSelector { get; }
        public string DropdownSelector { get; }
        public string ItemSelector { get; set; }
        public string ValueSelector;
        
        public DropdownList(IWebDriver driver, string buttonSelector, string dropdownSelector, string valueSelector)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            ButtonSelector = buttonSelector;
            DropdownSelector = dropdownSelector;
            ItemSelector = XPathSelectors.DropdownListItemSelector;
            ValueSelector = valueSelector;
        }

        public void Open()
        {
            Driver.FindByCss(ButtonSelector).Click();
            Driver.WaitForAjax();
            Assert.IsTrue(Driver.FindByCss(DropdownSelector).Displayed);
        }

        public void ValueEquals(string expectedValue)
        {
            var value = Driver.FindByCss(ValueSelector);
            Assert.AreEqual(expectedValue, value.Text);
        }

        public string GetValue()
        {
            return Driver.FindByCss(ValueSelector).Text;
        }

        public void Contains(string item)
        {
            var contains = false;
            var elements = Driver.FindByCss(DropdownSelector).FindElements(By.XPath(ItemSelector));
            foreach (var element in elements)
            {
                if (element.Text == item)
                {
                    contains = true;
                    break;
                }
            }
            Assert.IsTrue(contains);
        }

        public void Select(string item)
        {
            var elements = Driver.FindByCss(DropdownSelector).FindElements(By.XPath(ItemSelector));
            foreach (var element in elements)
            {
                if (element.Text == item)
                {
                    element.Click();
                    Driver.WaitForAjax();
                    return;
                }
            }
        }
    }
}
