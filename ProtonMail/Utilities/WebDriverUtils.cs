using OpenQA.Selenium;
using System;
using System.Configuration;

namespace ProtonMail.Utilities
{
    public class WebDriverUtils
    {
        private readonly IWebDriver _driver;
        public WebDriverUtils(IWebDriver driver)
        {
            _driver = driver;
        }

        public void TurnOffImplicitlyWait()
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }

        public void TurnOnImplicitlyWait()
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(double.Parse(ConfigurationManager.AppSettings["ImplicitWaitTimeout"]));
        }
    }
}
