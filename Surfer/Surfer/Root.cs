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

namespace Surfer
{
    public class Root
    {
        public IWebDriver driver { get; set; }
        private static string coreIP { get; } = "10.35.178.196";
        private static string coreUsername { get; } = "administrator";
        private static string corePassword { get; } = "123asdQ";
        private static string firefoxProfileName = "teargarden";
        public static readonly string validRepoPath = "J:\\repo";
        public static readonly List<string> invalidRepoNames = new List<string> { "?", "|", ":", "/", "\\", "*", "\"", ">", "<", "con", "prn", "aux", "nul",
            "com1", "com2", "com3", "com4", "com5", "com6", "com7", "com8", "com9", "lpt1", "lpt2", "lpt3", "lpt4", "lpt5", "lpt6", "lpt7", "lpt8", "lpt9" };
        public static readonly List<string> invalidRepoConcurOperations = new List<string> { "0", "1025", "q", "4.5", "-3", "=" };
        public static readonly List<string> invalidLocalPath = new List<string> { "C :\\", "C: \\", "C:", "CC:\\", " C:\\", "C:\\ ", "C:\\?", "C:\\|",
            "C:\\:", "C:\\/", "C:\\\\", "C:\\*", "C:\\\"", "C:\\>", "C:\\<", "C:\\con", "C:\\prn", "C:\\aux", "C:\\nul", "C:\\com1", "C:\\com2", "C:\\com3",
            "C:\\com4", "C:\\com5", "C:\\com6", "C:\\com7", "C:\\com8", "C:\\com9", "C:\\lpt1", "C:\\lpt2", "C:\\lpt3", "C:\\lpt4", "C:\\lpt5", "C:\\lpt6",
            "C:\\lpt7", "C:\\lpt8", "C:\\lpt9" };
        public static readonly List<string> invalidUNCPath = new List<string> { "\\\\hostname\\", "\\\\hostname\\?", "\\\\hostname\\|", "\\\\hostname\\:",
            "\\\\hostname\\/", "\\\\hostname\\\\", "\\\\hostname\\*", "\\\\hostname\\\"", "\\\\hostname\\>", "\\\\hostname\\<", "\\\\hostname\\con",
            "\\\\hostname\\prn", "\\\\hostname\\aux", "\\\\hostname\\nul", "\\\\hostname\\com1", "\\\\hostname\\com2", "\\\\hostname\\com3",
            "\\\\hostname\\com4", "\\\\hostname\\com5", "\\\\hostname\\com6", "\\\\hostname\\com7", "\\\\hostname\\com8", "\\\\hostname\\com9", "\\\\hostname\\lpt1",
            "\\\\hostname\\lpt2", "\\\\hostname\\lpt3", "\\\\hostname\\lpt4", "\\\\hostname\\lpt5", "\\\\hostname\\lpt6", "\\\\hostname\\lpt7",
            "\\\\hostname\\lpt8", "\\\\host name\\lpt9", "\\\\host?name\\fo", "\\\\host|name\\fo", "\\\\host:name\\fo", "\\\\host/name\\fo", "\\\\host>name\\fo",
            "\\\\host<name\\fo", "\\\\host\"name\\fo", "\\\\host*name\\fo", "\\\\host\\\\name\\fo", " \\\\hostname\\fo", "\\\\hostname\\ ", "\\\\ \\fo" };
        public static readonly List<string> invalidUserNames = new List<string> { "\"", "/", "[", "]", ":", ";", "|", "=", ",", "+", "*", "?", "<", ">" };
        public static readonly List<string> invalidStorageLocationSizeGb = new List<string> { "0.1", "99991000", "q", "-15" };
        public static readonly List<string> invalidStorageLocationSizeTb = new List<string> { "0.00001", "9999.01", "q", "-15" };

        public IWebDriver StartBrowser(string browserType)
        {
            switch (browserType)
            {
                case ("Chrome"):
                    driver = new ChromeDriver();
                    return driver;
                    break;
                case ("Firefox"):
                    //FIREFOX with lame untrusted certificate exception workaround
                    FirefoxProfileManager ffprofmanager = new FirefoxProfileManager();
                    FirefoxProfile firefoxProfile = ffprofmanager.GetProfile(firefoxProfileName);
                    firefoxProfile.AcceptUntrustedCertificates = true;
                    firefoxProfile.AssumeUntrustedCertificateIssuer = false;
                    return new FirefoxDriver(firefoxProfile);
                    break;
                default:
                    driver = new ChromeDriver();
                    return driver;
            }
            return driver;
        }

        public void CloseBrowser()
        {
            driver.Quit();
        }

        public void RefreshPage()
        {
            driver.Navigate().Refresh();
            //driver.WaitForLoad();
        }

        public void OpenCoreAdmin()
        {
            var CoreUrl = string.Format("https://{0}:{1}@{2}:8006/apprecovery/admin/", coreUsername, corePassword, coreIP);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(CoreUrl);
        }

        public void NavigateFromBase(string url)
        {
            driver.Navigate().GoToUrl(driver.Url + url);
        }
        
    }
}
