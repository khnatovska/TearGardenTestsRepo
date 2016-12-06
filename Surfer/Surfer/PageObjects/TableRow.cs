using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Surfer.PageObjects
{
    public abstract class TableRow
    {
        public IWebElement Table { get; set; }
        public IWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; }
        public string Actions { get; set; }

        protected TableRow(IWebDriver driver, IWebElement table)
        {
            Table = table;
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
    }
}
