using OpenQA.Selenium;

namespace LogginPractice.Pages
{
    internal class ContactUsPage : BaseApplicationPage
    {
        public ContactUsPage(IWebDriver driver) : base(driver)
        {
        }

        public bool IsLoaded
        {
            get
            {
                try
                {
                    return CenterColumn.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public IWebElement CenterColumn => Driver.FindElement(By.Id("center_column"));

        public void Login()
        {
            Driver.Navigate().GoToUrl("https://www.trioair.net");
            Driver.FindElement(By.Id("signInName")).SendKeys("demo@munters.co.il");
            Driver.FindElement(By.Id("password")).SendKeys("Demo123456");
        }
    }
}