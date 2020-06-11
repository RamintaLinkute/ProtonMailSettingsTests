using OpenQA.Selenium;

namespace ProtonMail.Infrastructure
{
    public class PageBase
    {
        public IWebDriver _driver;
        public PageBase(IWebDriver driver) => _driver = driver;
    }
}
