using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Surfer.PageObjects
{
    public abstract class InputField
    {
        public IWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; }
        public string Input { get; }

        protected InputField(IWebDriver driver, string inputSelector)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            Input = inputSelector;
        }
       
        public void InputEquals(string expectedInput)
        {
            var input = Driver.FindByCss(Input);
            Assert.AreEqual(expectedInput, input.GetAttribute("value"));
        }

        public void InputEqualsWithTwoDecimalPoints(string expectedInput)
        {
            var input = Driver.FindByCss(Input);
            Assert.AreEqual(expectedInput + ".00", input.GetAttribute("value"));
        }

        public void InputValidationError()
        {
            Driver.WaitForAjax();
            Assert.IsTrue(Driver.FindByCss(Input).HasClass("input-validation-error"));
        }
        
        public void ClearInput()
        {
            Driver.FindByCss(Input).Clear();
        }

        public void TypeIn(string text)
        {
            var input = Driver.FindByCss(Input);
            input.Clear();
            Driver.WaitForAjax(); //?? slows down the test but eliminates the overlay issue
            foreach (var character in text)
            {
                input.SendKeys(character.ToString());
            }
            input.SendKeys(Keys.Tab); // removes focus. maybe move to a separate method??
        }

        public int GetInputLength()
        {
            var input = Driver.FindByCss(Input);
            return input.GetAttribute("value").Length;
        }

        public string GetInput()
        {
            return Driver.FindByCss(Input).GetAttribute("value");
        }
    }
}
