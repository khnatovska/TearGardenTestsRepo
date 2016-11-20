using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace StartSel
{
    public class Verifier
    {
        public static void VerifyInputFieldValidation(IWebDriver webdriver, IWebElement inputField, IWebElement labelParent, string invalidValue,
             string inputFieldClass, string labelParentClass)
        {
            //enters incorrect value to input field, removes focus, verifies error classed added to input field and label parent elements, clears input field
            inputField.SendKeys(invalidValue);
            inputField.SendKeys(Keys.Tab);
            webdriver.WaitForAjax();
            Assert.IsTrue(inputField.HasClass(inputFieldClass)); //IS IT RELIABLE - CHECK CLASS AND NOT ACTUAL STYLE? HOW TO CHECK ACTUAL STYLE? NO WAY TO CHECK TOOLTIP
            Assert.IsTrue(labelParent.HasClass(labelParentClass));
            //inputField.Clear();
        }

        public static void VerifyInputFieldValidation(IWebDriver webdriver, IWebElement inputField, IWebElement labelParent, string invalidValue,
              string labelParentClass)
        {
            //THIS OVERLOAD WORKS FOR THOSE INPUT ELEMENTS WHICH DO NOT GET ERROR CLASS FOR SOME REASON - FIND OUT WHY NOT 
            //enters incorrect value to input field, removes focus, verifies error classed added to label parent element, clears input field
            inputField.SendKeys(invalidValue);
            inputField.SendKeys(Keys.Tab);
            webdriver.WaitForAjax();
            Assert.IsTrue(labelParent.HasClass(labelParentClass));
            inputField.Clear();
        }
    }
}
