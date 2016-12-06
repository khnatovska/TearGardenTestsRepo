using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Surfer;

namespace RepoPage
{
    public class RepoDialog
    {
        public IWebElement Dialog { get; }
        public IWebDriver Driver { get; }
        public WebDriverWait Wait { get; }
        public string Title { get; } = "Add New Repository";
        public string Name { get; }
        public string ConcurOperations { get; }
        public string Comments { get; }
        
    }
}
