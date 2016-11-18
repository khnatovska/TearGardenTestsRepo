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
    public class Root
    {
        public static readonly List<string> invalidRepoNames = new List<string> { "?", "|", ":", "/", "\\", "*", "\"", ">", "<", "con", "prn", "aux", "nul", "com1", "com2", "com3", "com4", "com5", "com6", "com7", "com8", "com9", "lpt1", "lpt2", "lpt3", "lpt4", "lpt5", "lpt6", "lpt7", "lpt8", "lpt9" };
        public static readonly List<string> invalidUserNames = new List<string> { "\"", "/", "[", "]", ":", ";", "|", "=", ",", "+", "*", "?", "<", ">" };
        public static string coreIP { get; } = "10.35.178.196";
        public static string coreUsername { get; } = "administrator";
        public static string corePassword { get; } = "123asdQ";
        public static string localRepoPath { get; } = "J:\repo";
        public static string firefoxProfileName { get; } = "teargarden";

        public static void OpenCoreAdmin(IWebDriver webdriver)
        {
            string CoreUrl = String.Format("https://{0}:{1}@{2}:8006/apprecovery/admin/", coreUsername, corePassword, coreIP);
            webdriver.Navigate().GoToUrl(CoreUrl);
            webdriver.Manage().Window.Maximize();
        }

        public static void QuitCore(IWebDriver webdriver)
        {
            webdriver.Quit();
        }

        public static IWebDriver SetUpFirefoxDriver()
        {
            //FIREFOX with lame untrusted certificate exception workaround
            FirefoxProfileManager ffprofmanager = new FirefoxProfileManager();
            FirefoxProfile firefoxProfile = ffprofmanager.GetProfile(firefoxProfileName);
            firefoxProfile.AcceptUntrustedCertificates = true;
            firefoxProfile.AssumeUntrustedCertificateIssuer = false;
            return new FirefoxDriver(firefoxProfile);
        }

        public static IWebDriver SetUpChromeDriver()
        {
            //ChromeDriver WITH OVERLAY WIDGET BUG
            return new ChromeDriver();
        }

        public static WebDriverWait SetUptWebDriverWait(IWebDriver webdriver)
        {
            return new WebDriverWait(webdriver, TimeSpan.FromSeconds(10));
        }

        public static void NavigateToRepositoriesPage(IWebDriver webdriver)
        {
            webdriver.Navigate().GoToUrl(webdriver.Url + "Repository");
        }
    }
}
