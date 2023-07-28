using System;
using System.Xml.XPath;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace HWSession16;

public class NastyaTest
{
    private const string ActionTitleXpath = "//*[@data-role='woactivityloggrid']//tbody//tr[1]//td[@data-column='ActionTitle']";
    private IWebDriver drv;
    private WebDriverWait wait;
    //private object notes;

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
   // Create Automated Test for Login Flow (positive)
    public void TestLogin()
    {
        Assert.True(IsVisible(By.CssSelector("div.menu-secondary ul li.menu-user a.menu-drop")));
    }

    

    [Test]
    public void TestWoFlow()
    {
        var pickUpComment = "Autotest";
        var url = $"{Environment.GetEnvironmentVariable("ENT_QA_BASE_URL")}/CorpNet/workorder/workorderlist.aspx";
        Console.WriteLine(url);
        drv.Navigate()
            .GoToUrl(url);


        var woLink = By.XPath("//td[@data-column='WOStatus'][contains(text(), 'New')][1]/following-sibling::td/a");

        var woLinkElement = wait.Until(drv => drv.FindElement(woLink));

        var woNumber = drv.FindElement(woLink).Text;

        drv.FindElement(woLink).Click();
        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("span[title=\"Pick-Up\"]"))).Click();
        IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//textarea[@placeholder=\"Type any comments or notes about this status change here\"]")));
        string comment = "Autotest";
        element.SendKeys(comment);
        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[normalize-space()=\"Pick up\"]"))).Click();
        wait.Until(ExpectedConditions.ElementIsVisible
            (By.XPath("//div[@id=\"WoQvActivityLogGrid\"]//div[@class=\"kg-header\"]")));
        drv.FindElement(
                By.XPath("//*[@data-role='woactivityloggrid']//tbody//tr[1]//td[@data-column='ActionTitle']"));
        var action =
             drv.FindElement(
                 By.XPath(ActionTitleXpath));
        Assert.That(comment, Is.EqualTo(pickUpComment), "Comment does not match the expected pickUpComment.");
        IWebElement modalTitleWrapper = drv.FindElement(By.XPath("//div[@class='modal-title-wrapper']"));
        IWebElement woNumberElement = modalTitleWrapper.FindElement(By.XPath("//span[contains(@data-bind, 'htmlIfNonEmpty: header.title')]"));
        // Find and click on the "Close" button
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='close btn-dismiss']"))).Click();
        //drv.FindElement(By.XPath("//button[@class='close btn-dismiss']")).Click();
        wait.Until(ExpectedConditions.StalenessOf(woLinkElement));

        var woStatus = drv.FindElement(By.XPath($"//td[@data-column='Number']/a[contains(text(), '{woNumber}')]/../../td[@data-column='WOStatus']"));
        Assert.That(woStatus.Text, Is.EqualTo("Open"));
    }

    private bool IsVisible(By locator)
    {
        return drv.FindElements(locator).Count > 0;
    }
}