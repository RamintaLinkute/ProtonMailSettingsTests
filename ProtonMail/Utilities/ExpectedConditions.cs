using OpenQA.Selenium;
using System;

namespace ProtonMail.Utilities
{
    public static class ExpectedConditions
    {
        public static Func<IWebDriver, IWebElement> ElementIsVisible(IWebElement element)
        {
            return driver =>
            {
                try
                {
                    return ElementIfVisible(element);
                }
                catch (NoSuchElementException)
                {
                    return (IWebElement)null;
                }
            };
        }

        private static IWebElement ElementIfVisible(IWebElement element)
        {
            if (element.Displayed && element.Enabled)
                return element;
            else
                return (IWebElement)null;
        }

        public static Func<IWebDriver, bool> ElementIsNotVisible(IWebElement element)
        {
            return driver =>
            {
                try
                {
                    return !element.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
            };
        }

        public static bool IsElementVisible(IWebElement element)
        {
            return element.Displayed && element.Enabled;
        }
    }
}
