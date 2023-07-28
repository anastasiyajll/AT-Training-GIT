using OpenQA.Selenium;

namespace HWSession15
{
    internal class WebDriverWait
    {
        private IWebDriver driver;
        private TimeSpan timeSpan;

        public WebDriverWait(IWebDriver driver, TimeSpan timeSpan)
        {
            this.driver = driver;
            this.timeSpan = timeSpan;
        }

        internal object Until(object value)
        {
            throw new NotImplementedException();
        }
    }
}