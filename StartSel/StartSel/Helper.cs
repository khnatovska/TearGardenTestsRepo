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

        public static void WaitForOverlay(this IWebDriver driver, int timeoutSecs = 10, bool throwException = false)
        {
            for (var i = 0; i < timeoutSecs; i++)
            {
                var ajaxIsComplete = (bool)(new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(".ui-widget-overlay"))));
                if (ajaxIsComplete) return;
                Thread.Sleep(500);
            }
            if (throwException)
            {
                throw new Exception("WebDriver timed out waiting for overlay to disappear");
            }
        }
    }
}
