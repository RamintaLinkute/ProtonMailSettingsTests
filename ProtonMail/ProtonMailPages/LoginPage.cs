using OpenQA.Selenium;
using ProtonMail.Infrastructure;
using ProtonMail.Utilities;
using System.Configuration;
using System.Threading;

namespace ProtonMail.ProtonMailPages
{
    public class LoginPage : PageBase
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement UserNameInput => _driver.FindElement(By.XPath("//form[@id='pm_login']//*[@id='username']"));
        public IWebElement PasswordInput => _driver.FindElement(By.Id("password"));
        public IWebElement LoginButton => _driver.FindElement(By.Id("login_btn"));
        public IWebElement ProtonLoader => _driver.FindElement(By.Id("pm_loading"));
        public IWebElement AtomLoader => _driver.FindElement(By.Id("pm_slow"));

        public LoginPage NavigateToProtonMailLoginPage()
        {
            var url = ConfigurationManager.AppSettings["ApplicationBaseUrl"];
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(url);
            WaitUtils.WaitUntilInvisible(AtomLoader, _driver);
            return this;
        }

        public LoginPage EnterCrediantialsAndClickLogin(string userName, string password)
        {
            UserNameInput.SendKeys(userName);
            PasswordInput.SendKeys(password);
            LoginButton.Click();
            WaitUtils.WaitUntilInvisible(ProtonLoader, _driver);
            return this;
        }

        public LoginPage NavigateToProtonMailAndLogin()
        {
            string userName = ConfigurationManager.AppSettings["ProtonMailUserName"];
            string password = ConfigurationManager.AppSettings["ProtonMailPassword"];
            NavigateToProtonMailLoginPage();
            EnterCrediantialsAndClickLogin(userName, password);
            return this;
        }
    }
}
