using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SampleApp2
{
    internal class HomePage : BaseApplicationPage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {

        }

        public void Login()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl("https://www.trioair.net");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("signInName"))).SendKeys("demo@munters.co.il");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password"))).SendKeys("Demo123456");
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("next"))).Click();
        }

        /// <summary>
        /// The method updates sensors data within defined boundaries and verifies 
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<bool, string>> UpdateBoundaries()
        {
            string expectedToastMsg = "Changes Saved Successfully";
            List<KeyValuePair<bool, string>> result = new List<KeyValuePair<bool, string>>();
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            //Commented item were intermittently not displayed
            //wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".header-container .sidebar-toggle-button"))).Click();
            //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(" //*[text()=' Swine Demo ']//preceding::div[1]"))).Click();
            //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(" //*[text()=' Room ']//preceding::div[1]"))).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".e-list-item.e-level-3"))).Click();

            Driver.SwitchTo().Frame(0);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.button-icon-24.icon-small.icon-menu"))).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Climate"))).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(" //*[text()=' Temperature Curve ']"))).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(" .toolbar-button-container.ng-star-inserted"))).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(" .button-cancel")));
                       
            var listOfBoundaries = new List<KeyValuePair<bool, string>>();
            IList<IWebElement> fieldsList = Driver.FindElements(By.ClassName("k-numeric-wrap"));
            
            foreach (var field in fieldsList)
            {
                //click field
                field.Click();
                //Get and parse boundaries
                string boundaries = Driver.FindElement(By.ClassName("k-numeric-wrap")).Text;
                var split = boundaries.Split('-');
                
                List<int> boundaryValues = new List<int>();
                foreach (string value in split)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        int i = int.Parse(value);
                        Console.WriteLine("Number: {0}", i);
                        boundaryValues.Add(i);
                    }
                }

                //Type random values for the boundaries within range
                field.SendKeys(Convert.ToString(RandomNumber(boundaryValues[0], boundaryValues[1])));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Save"))).Click();
                
                //Verify the acknowledge toast displayed
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(" ")));
                string displayedToastText = Driver.FindElement(By.ClassName("")).Text;
                result.Add(GetTextEqualsResult(expectedToastMsg, displayedToastText , "Expected toast text is not equal to displayed toast text"));
                
            }
            
            return result;

        }

        // Instantiate random number generator.  
        private readonly Random _random = new Random();

        // Generates a random number within a range.      
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }        

        protected KeyValuePair<bool, string> GetTextEqualsResult(string actualText, string expectedText, string errorDescription)

        {

            actualText = String.IsNullOrEmpty(actualText) ? String.Empty : actualText;

            expectedText = String.IsNullOrEmpty(expectedText) ? String.Empty : expectedText;

            var mesage = GetEqualMesage(actualText, expectedText, errorDescription);

            return GetResultValue(actualText.Equals(expectedText), mesage);

        }
        private static string GetEqualMesage(string actualText, string expectedText, string errorDescription)

        {



            return $"{errorDescription}: Expected - '{expectedText}', Actual - '{actualText}'";

        }
        protected KeyValuePair<bool, string> GetResultValue(bool result, string message)

        {

            return new KeyValuePair<bool, string>(result, message);

        }
    }
}