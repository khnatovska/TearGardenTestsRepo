using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace StartSel
{
    public class Program
    {
        //static IWebDriver driver = new ChromeDriver();
        //static WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        //static readonly List<string> invalidRepoNames = new List<string> { "?", "|", ":", "/", "\\", "*", "\"", ">", "<", "con", "prn", "aux", "nul", "com1", "com2", "com3", "com4", "com5", "com6", "com7", "com8", "com9", "lpt1", "lpt2", "lpt3", "lpt4", "lpt5", "lpt6", "lpt7", "lpt8", "lpt9" };

        static void Main(string[] args)
        {
           
            //string CoreIP = "10.35.178.196";
            //string CoreUsername = "administrator";
            //string CorePassword = "123asdQ";
            //OpenBrowser(CoreIP, CoreUsername, CorePassword);
            //RunRepositoriesScenario();
            //CloseBrowser();

            //FIREFOX
            //FirefoxProfileManager ffprofmanager = new FirefoxProfileManager();
            //FirefoxProfile firefoxProfile = ffprofmanager.GetProfile("teargarden");
            //firefoxProfile.AcceptUntrustedCertificates = true;
            //firefoxProfile.AssumeUntrustedCertificateIssuer = false;
            //IWebDriver driver = new FirefoxDriver(firefoxProfile);
            //driver.Navigate().GoToUrl("https://administrator:123asdQ@10.35.178.196:8006/apprecovery/admin");
            //Console.WriteLine(firefoxProfile.AcceptUntrustedCertificates);
            //Console.WriteLine(firefoxProfile.AssumeUntrustedCertificateIssuer);
        }
    

        //public static void OpenBrowser(string coreIP, string coreUsername, string corePassword)
        //{
        //    string CoreUrl = String.Format("https://{0}:{1}@{2}:8006/apprecovery/admin/", coreUsername, corePassword, coreIP);
        //    driver.Navigate().GoToUrl(CoreUrl);
        //    driver.Manage().Window.Maximize();
        //    Console.WriteLine(driver.Title);
        //}

        public static void OpenBrowser(IWebDriver webdriver, string coreIP, string coreUsername, string corePassword)
        {
            string CoreUrl = String.Format("https://{0}:{1}@{2}:8006/apprecovery/admin/", coreUsername, corePassword, coreIP);
            webdriver.Navigate().GoToUrl(CoreUrl);
            webdriver.Manage().Window.Maximize();
            Console.WriteLine(webdriver.Title);
        }
        
        //public static void CloseBrowser()
        //{
        //    driver.Close();
        //}

        public static void CloseBrowser(IWebDriver webdriver)
        {
            webdriver.Close();
        }

        public static void QuitBrowser(IWebDriver webdriver)
        {
            webdriver.Quit();
        }

        //static void RunRepositoriesScenario()
        //{
        //    OpenRepositoriesPage();
        //    LaunchDVMAddNewRepositoryDialog();
        //    CreateDVMRepoWithoutStorageLocation();
        //    CreareDVMRepoWithEmptyName();
        //    CreareDVMRepoWithInvalidName();
        //    CreareDVMRepoWithEmptyConcurOperations();
        //    //LaunchAddStorageLocationDialog();
        //}
        
        //static void OpenRepositoriesPage() //start: any page. find Repositories in More Navigation menu, open Repositories page, verify URL
        //{
        //    IWebElement more = driver.FindElement(By.CssSelector("#coreNavigationMore span.ui-navigation-core-menu-icon"));
        //    more.Click();
        //    IWebElement repoInMore = driver.FindElement(By.CssSelector("#coreNavigationMoreMenu > li:nth-child(5) > a"));
        //    repoInMore.Click();
        //    wait.Until(ExpectedConditions.UrlContains("Repository"));
        //    Console.WriteLine(driver.Url);
        //}

        //static void LaunchDVMAddNewRepositoryDialog() //start: repositories page. find Add New Repo button, verify title, open add repo dialog, verify title
        //{
        //    IWebElement addNewRepoBtn = wait.Until(d=>driver.FindElement(By.CssSelector("#newMainRepository > span")));
        //    string addNewRepoBtnText = addNewRepoBtn.Text;
        //    Assert.AreEqual(addNewRepoBtnText, "Add New DVM Repository");
        //    addNewRepoBtn.Click();
        //    IWebElement addNewRepoDialogTitle = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("ui-id-3")));
        //    Assert.AreEqual(addNewRepoDialogTitle.Text, "Add New Repository");
        //    //

        //}

        //static void CloseDVMAddNewRepositoryDialog() //start: repositories page. open add repo dialog, close with button, reopen dialog, close with cross icon
        //{
        //    LaunchDVMAddNewRepositoryDialog();
        //    IWebElement cancelButton = driver.FindElement(By.Id("createRepoCancel"));
        //    //FIND A WAY TO CHECK THAT DIALOG IS REALLY CLOSED !!!!!

        //}

        //static void CreateDVMRepoWithoutStorageLocation() //start: add new DVM repo dialog. find create repo button, click, verify warning message, close warning dialog
        //{
        //    IWebElement createRepoBtn = driver.FindElement(By.Id("createRepoButton"));
        //    createRepoBtn.Click();
        //    IWebElement warningHeader = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("msgbox-message-header")));
        //    IWebElement warningMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("msgbox-message")));
        //    Assert.AreEqual(warningHeader.Text, "Warning");
        //    Assert.AreEqual(warningMessage.Text, "You must add at least one storage location");
        //    IWebElement closeBtn = driver.FindElement(By.CssSelector("div.messageboxButtonsContainer button.default"));
        //    closeBtn.Click();
        //}

        //static void CreareDVMRepoWithEmptyName() //start: add new DVM repo dialog. find name, verify label. clear, click create button, vefiry validation
        //{
        //    IWebElement repoNameLabel = driver.FindElement(By.CssSelector("label.control-label[for=\"Name\""));
        //    IWebElement repoNameLabelParent = repoNameLabel.FindElement(By.XPath(".."));
        //    Assert.AreEqual(repoNameLabel.Text, "Repository name");
        //    IWebElement repoName = driver.FindElement(By.Id("RepoName"));
        //    repoName.Clear();
        //    IWebElement createRepoBtn = driver.FindElement(By.Id("createRepoButton"));
        //    createRepoBtn.Click();
        //    driver.WaitForAjax();
        //    Assert.IsTrue(repoName.HasClass("input-validation-error")); //IS IT RELIABLE - CHECK CLASS AND NOT ACTUAL STYLE? HOW TO CHECK ACTUAL STYLE? NO WAY TO CHECK TOOLTIP
        //    Assert.IsTrue(repoNameLabelParent.HasClass("has-error"));
        //}

        //static void CreareDVMRepoWithInvalidName() //start: add new DVM repo dialog. find name. loop - incorrect value, click create button, vefiry validation
        //{
        //    IWebElement repoNameLabel = driver.FindElement(By.CssSelector("label.control-label[for=\"Name\""));
        //    IWebElement repoNameLabelParent = repoNameLabel.FindElement(By.XPath(".."));
        //    Assert.AreEqual(repoNameLabel.Text, "Repository name");
        //    IWebElement repoName = driver.FindElement(By.Id("RepoName"));
        //    IWebElement createRepoBtn = driver.FindElement(By.Id("createRepoButton"));
        //    //invalid values
        //    foreach (var invalidName in invalidRepoNames)
        //    {
        //        repoName.SendKeys(invalidName);
        //        createRepoBtn.Click();
        //        driver.WaitForAjax();
        //        Assert.IsTrue(repoName.HasClass("input-validation-error")); //IS IT RELIABLE - CHECK CLASS AND NOT ACTUAL STYLE? HOW TO CHECK ACTUAL STYLE? NO WAY TO CHECK TOOLTIP
        //        Assert.IsTrue(repoNameLabelParent.HasClass("has-error"));
        //        repoName.Clear();
        //    }
        //    //too long
        //    string longName = new String('q', 41);
        //    repoName.SendKeys(longName);
        //    Assert.AreEqual(repoName.GetAttribute("value").Length, 40);
        //}

        //static void CreareDVMRepoWithEmptyConcurOperations() //start: add new DVM repo dialog. find concurrent operations. clear, click create button, vefiry validation
        //{
        //    IWebElement concurOperationsLabel = driver.FindElement(By.CssSelector("label.control-label[for=\"MaxConcurrentOperations\""));
        //    IWebElement concurOperationsLabelParent = concurOperationsLabel.FindElement(By.XPath(".."));
        //    Assert.AreEqual(concurOperationsLabel.Text, "Concurrent operations");
        //    IWebElement concurOperations = driver.FindElement(By.Id("MaximumConcurrentOperations"));
        //    concurOperations.Clear();
        //    IWebElement createRepoBtn = driver.FindElement(By.Id("createRepoButton"));
        //    createRepoBtn.Click();
        //    //BELOW DOES NOT WORK YET
        //    //river.WaitForAjax();
        //    //Assert.IsTrue(concurOperations.HasClass("input-validation-error")); //IS IT RELIABLE - CHECK CLASS AND NOT ACTUAL STYLE? HOW TO CHECK ACTUAL STYLE? NO WAY TO CHECK TOOLTIP
        //    //Assert.IsTrue(concurOperationsLabelParent.HasClass("has-error"));
        //}

        //static void CreareDVMRepoWithInvalidConcurOperations() //start: add new DVM repo dialog. find concurrent operations. loop - incorect value, click create button, vefiry validation
        //{

        //}

        //static void LaunchAddStorageLocationDialog() //start: add new DVM repo dialog. find add storage location button, verify title, click, verify dialog title
        //{
        //    IWebElement addStorageLocationBtn = driver.FindElement(By.CssSelector("#popup1 div.popup-inner-content nav.navbar-actions span"));
        //    Assert.AreEqual(addStorageLocationBtn.Text, "Add Storage Location");
        //    addStorageLocationBtn.Click();
        //    IWebElement addStorageLocationDialogTitle = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("ui-id-4")));
        //    Assert.AreEqual(addStorageLocationDialogTitle.Text, "Add Storage Location");
        //}

        //static void CreateDVMRepositoryWithOneLocalStorageLocation()
        //{

        //}
    }
}
