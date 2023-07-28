using System;
using System.Xml.XPath;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace HWSession17;

public class Test17
{
    private IWebDriver drv;
    private WebDriverWait wait;


    [SetUp]
    public void Setup()
    {
        drv = new ChromeDriver();

        wait = new WebDriverWait(drv, TimeSpan.FromSeconds(5));

        drv.Navigate().GoToUrl($"{Environment.GetEnvironmentVariable("ENT_QA_BASE_URL")}/CorpNet/Login.aspx");

        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("username")))
            .SendKeys(Environment.GetEnvironmentVariable("ENT_QA_USER"));

        drv.FindElement(By.Id("password")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_PASS"));
        drv.FindElement(By.Name("_companyText")).SendKeys(Environment.GetEnvironmentVariable("ENT_QA_COMPANY"));
        drv.FindElement(By.CssSelector("input.btn.login-submit-button")).Click();

        wait.Until(
            ExpectedConditions.ElementIsVisible(By.CssSelector("div.menu-secondary ul li.menu-user a.menu-drop")));
    }

    [TearDown]
    public void TearDown()
    {
        drv.Quit();
    }




    [Test]
    public void VerifyFirstReportGeneration()
    {
        var originalWindow = drv.CurrentWindowHandle;
        drv.Navigate().GoToUrl($"{Environment .GetEnvironmentVariable("ENT_QA_BASE_URL")}/corpnet/report/reportlist.aspx");
        var reportLink = By.XPath("//td[@data-column= 'DisplayAs'][1]/a");
        var reportLinkElement = wait.Until(drv => drv.FindElement(reportLink));
        var reportName = drv.FindElement(reportLink).Text;
        drv.FindElement(reportLink).Click();
        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".showHeaders .k-switch-container"))).Click();
        drv.FindElement(By.CssSelector(".id-generate-button")).Click();
        wait.Until(driver => driver.WindowHandles.Count == 2); 
        drv.SwitchTo().Window(drv.WindowHandles.Last());
        wait.Until(driver => driver.FindElement(By.XPath($"//span[contains(text(),'{reportName}')]")));

        Assert.IsTrue(drv.FindElements(By.XPath($"//span[contains(text(),'{reportName}')]")).Count>0, "Header not found");
        drv.SwitchTo().Window(originalWindow);
        drv.FindElement(By.CssSelector(".id-btn-close")).Click();
    }

    private bool IsVisible(By locator)
    {
        return drv.FindElements(locator).Count > 0;
    }
}
