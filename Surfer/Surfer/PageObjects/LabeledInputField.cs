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
    public class LabeledInputField : InputField
    {
        public string Label { get; }

        public LabeledInputField(IWebDriver driver, string inputSelector, string labelSelector)
            : base(driver, inputSelector)
        {
            Label = labelSelector;
        }

        public void LabelEquals(string expectedLabel)
        {
            var label = Driver.FindByCss(Label);
            Assert.AreEqual(expectedLabel, label.Text);
        }
        
        public void FormHasError()
        {
            Driver.WaitForAjax();
            var parent = Driver.FindByCss(Label).FindElement(By.XPath(".."));
            Assert.IsTrue(parent.HasClass("has-error"));
        }
    }
}
