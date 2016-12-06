using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Surfer.PageObjects
{
    public class MessageBox
    {
        public IWebElement Box { get; set; }
        public IWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; }
        public string Header { get; set; }
        public string Message { get; set; }

        public MessageBox(IWebDriver driver, IWebElement box)
        {
            Box = box;
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            Header = Box.FindByCss(CssSelectors.uiDialogMessageBoxHeader).Text;
            Message = Box.FindByCss(CssSelectors.uiDialogMessageBoxMessage).Text;
        }

        public void HeaderEquals(string expectedHeader)
        {
            Assert.AreEqual(expectedHeader, Header);
        }

        public void MessageEquals(string expectedMessage)
        {
            Assert.AreEqual(expectedMessage, Message);
        }

        public void MessageContains(string expectedContent)
        {
            Assert.IsTrue(Message.Contains(expectedContent));
        }

        public void Close()
        {
            Driver.FindByCssClickable(CssSelectors.uiDialogMessageBoxDefaultBtn).Click();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(CssSelectors.uiDialogMessageBox)));
        }
    }
}
