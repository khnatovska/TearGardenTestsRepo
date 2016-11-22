using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
