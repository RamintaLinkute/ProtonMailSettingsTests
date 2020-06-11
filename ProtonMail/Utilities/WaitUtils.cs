using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ProtonMail.Infrastructure;
using System;
using System.Configuration;
using System.Text;

namespace ProtonMail.Utilities
{
    public static class WaitUtils
    {
        public static void WaitUntilVisible(IWebElement element, IWebDriver driver)
        {
            new WebDriverUtils(driver).TurnOffImplicitlyWait();
            var wait = new WebDriverWait(driver,
                TimeSpan.FromSeconds(double.Parse(ConfigurationManager.AppSettings["ExplicitWaitTimeout"])));
            wait.Message = "Element should be visible.";
            element = wait.Until(ExpectedConditions.ElementIsVisible(element));
            new WebDriverUtils(driver).TurnOnImplicitlyWait();
        }

        public static void WaitUntilInvisible(IWebElement element, IWebDriver driver)
        {
            new WebDriverUtils(driver).TurnOffImplicitlyWait();
            var wait =
                new WebDriverWait(driver,
                    TimeSpan.FromSeconds(double.Parse(ConfigurationManager.AppSettings["ExplicitWaitTimeout"])))
                {
                    Message = "Element should be not visible."
                }.Until(
                    ExpectedConditions.ElementIsNotVisible(element));
            new WebDriverUtils(driver).TurnOnImplicitlyWait();
        }
    }
}
