using OpenQA.Selenium;
using ProtonMail.Infrastructure;

namespace ProtonMail.ProtonMailPages
{
    public class LeftMenuPage : PageBase
    {
        public LeftMenuPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement FoldersAndLabelsMenuItem => _driver.FindElement(By.Id("tour-label-settings"));

        public LeftMenuPage NavigateToFoldersAndLabels()
        {
            FoldersAndLabelsMenuItem.Click();
            return this;
        }
    }
}
