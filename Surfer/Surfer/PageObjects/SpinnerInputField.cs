using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Surfer.PageObjects
{
    public class SpinnerInputField : InputField
    {
        public string SpinnerUp { get; }
        public string SpinnerDown { get; }

        public SpinnerInputField(IWebDriver driver, string inputSelector, string spinnerUpSelector,
            string spinnerDownSelector)
            : base(driver, inputSelector)
        {
            SpinnerUp = spinnerUpSelector;
            SpinnerDown = spinnerDownSelector;
        }

        public void SpinUp()
        {
            Driver.FindByCssClickable(SpinnerUp).Click();
        }

        public void SpinDown()
        {
            Driver.FindByCssClickable(SpinnerDown).Click();
        }
    }
}
