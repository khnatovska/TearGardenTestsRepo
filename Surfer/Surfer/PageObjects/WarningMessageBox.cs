using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Surfer.PageObjects
{
    public class WarningMessageBox : MessageBox
    {
        public WarningMessageBox(IWebDriver driver, IWebElement box) : base(driver, box)
        {
            
        }

        public void VerifyWarningHeader()
        {
            Assert.AreEqual("Warning", Header);
        }
    }
}
