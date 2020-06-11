using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ProtonMail.Infrastructure
{
    public class TestBase
    {
        public IWebDriver Driver;

        public TestBase() => Driver = new ChromeDriver();
    }
}
