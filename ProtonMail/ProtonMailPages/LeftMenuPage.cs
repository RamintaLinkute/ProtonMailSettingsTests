using OpenQA.Selenium;
using ProtonMail.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

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
