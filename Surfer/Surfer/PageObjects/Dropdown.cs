//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NUnit.Framework;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Support.UI;

//namespace Surfer.PageObjects
//{
//    public abstract class Dropdown
//    {
//        public IWebDriver Driver { get; set; }
//        public WebDriverWait Wait { get; }
//        public string ButtonSelector { get; }
//        public string DropdownSelector { get; }
//        public string ItemSelector { get; set; }

//        protected Dropdown(IWebDriver driver, string buttonSelector, string dropdownSelector)
//        {
//            Driver = driver;
//            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
//            ButtonSelector = buttonSelector;
//            DropdownSelector = dropdownSelector;
//        }

//        public abstract void Open();
        
//        public void Contains(string item)
//        {
//            var contains = false;
//            var elements = Driver.FindByCss(DropdownSelector).FindElements(By.XPath(ItemSelector));
//            foreach (var element in elements)
//            {
//                if (element.Text == item)
//                {
//                    contains = true;
//                    break;
//                }
//            }
//            Assert.IsTrue(contains);
//        }

//        public void Select(string item)
//        {
//            var elements = Driver.FindByCss(DropdownSelector).FindElements(By.XPath(ItemSelector));
//            foreach (var element in elements)
//            {
//                if (element.Text == item)
//                {
//                    element.Click();
//                    Driver.WaitForAjax();
//                    return;
//                }
//            }
//        }
//    }
//}
