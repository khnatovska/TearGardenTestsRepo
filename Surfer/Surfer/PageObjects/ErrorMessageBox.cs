using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Surfer.PageObjects
{
    public class ErrorMessageBox : MessageBox
    {
        public ErrorMessageBox(IWebDriver driver, IWebElement box) : base(driver, box)
        {

        }

        public void VerifyErrorHEader()
        {
            Assert.AreEqual("Error", Header);
        }
    }
}
