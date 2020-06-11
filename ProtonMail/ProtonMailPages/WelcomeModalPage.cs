using OpenQA.Selenium;
using ProtonMail.Infrastructure;

namespace ProtonMail.ProtonMailPages
{
    public class WelcomeModalPage : PageBase
    {
        public WelcomeModalPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement CancelModalButton => _driver.FindElement(By.CssSelector("[ng-click='ctrl.cancel()']"));

        public WelcomeModalPage CloseWelcomeModal()
        {
            CancelModalButton.Click();
            return this;
        }

    }
}
