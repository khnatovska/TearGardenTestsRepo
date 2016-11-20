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
using StartSel;

namespace TearGarden.Tests
{
    static class TestConfig
    {
        public static IWebDriver driver;
        public static WebDriverWait wait;
        
        public static IWebDriver RepositoryTestSetup()
        {
            //ChromeDriver WITH OVERLAY WIDGET BUG
            driver = Root.SetUpChromeDriver();

            ////FIREFOX with lame untrusted certificate exception workaround
            //driver = Root.SetUpFirefoxDriver();

            //wait = Root.SetUptWebDriverWait(driver);
            Root.OpenCoreAdmin(driver);
            Root.NavigateToRepositoriesPage(driver);
            return driver;
        }
        
        public static void RepositoryTestTearDown()
        {
            Root.QuitCore(driver);
        }
    }
}
