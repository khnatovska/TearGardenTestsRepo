using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace StartSel
{
    public static class Helper
    {
        public static bool HasClass (this IWebElement element, string className)
        {
            return element.GetAttribute("class").Split(' ').Contains(className);
        }

        public static void WaitForAjax(this IWebDriver driver, int timeoutSecs = 10, bool throwException = false)
        {
            for (var i = 0; i < timeoutSecs; i++)
            {
                var ajaxIsComplete = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                if (ajaxIsComplete) return;
                Thread.Sleep(500);
            }
            if (throwException)
            {
                throw new Exception("WebDriver timed out waiting for AJAX call to complete");
            }
        }

        public static void WaitForPageToLoad(this IWebDriver driver, int timeoutSecs = 10, bool throwException = false)
        {
            for (var i = 0; i < timeoutSecs; i++)
            {
                var ajaxIsComplete = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return document.readyState == 'complete'"); //it works? really?
                if (ajaxIsComplete) return;
                Thread.Sleep(500);
            }
            if (throwException)
            {
                throw new Exception("WebDriver timed out waiting for page to load");
            }
        }

        public static string IntoDynamicSelector(this string id, string selector) //DOES NOT WORK PROPERLY - SHUT ITT DOWN
        {
            return "#" + id + " " + selector;
        }

        public static void AvoidInvalidSelector(this IWebDriver driver, string selector, int timeoutSecs = 10)
        {
            for (var i = 0; i < timeoutSecs; i++)
            {
                try
                {
                    IWebElement element = driver.FindElement(By.CssSelector(selector));
                    return;
                }
                catch (InvalidSelectorException)
                {
                    //var el =
                    //    driver.FindElement(By.CssSelector("#fileSpecificationsGrid tr.ui-widget-content > td:nth-child(2)"));
                    //Console.WriteLine("invalid exception caught: " + el.GetAttribute("aria-describedby"));
                }
                Thread.Sleep(500);
            }
        }

        public static void TypeIn(this IWebElement element, string text)
        {
            foreach (var character in text)
            {
                element.SendKeys(character.ToString());
            }
        }
    }
}
