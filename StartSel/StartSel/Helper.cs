using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
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
    }
}
