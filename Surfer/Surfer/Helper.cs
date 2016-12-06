using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Surfer
{
    public static class Helper
    {
        public static IWebElement FindByCss(this IWebElement element, string selector)
        {
            return element.FindElement(By.CssSelector(selector));
        }

        public static IWebElement FindByCss(this IWebDriver driver, string selector)
        {
            return driver.FindElement(By.CssSelector(selector));
        }

        public static IWebElement FindByCssClickable(this IWebDriver driver, string selector)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(selector)));
            return element;
        }

        public static IWebElement FindByCssVisible(this IWebDriver driver, string selector)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(selector)));
            return element;
        }

        public static bool HasClass(this IWebElement element, string className)
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

        public static void WaitForLoad(this IWebDriver driver, int timeoutSec = 15)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeoutSec));
            wait.Until(wd => (wd as IJavaScriptExecutor).ExecuteScript("return document.readyState") == "complete");
        }
    }
}
