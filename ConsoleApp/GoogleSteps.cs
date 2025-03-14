using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ConsoleApp;

internal static class GoogleSteps
{
    public static void Run(ChromeOptions options)
    {
        using (IWebDriver driver = new ChromeDriver(options))
        {
            try
            {
                //var url = "https://brunocampiol.github.io/";

                var url = "https://www.google.com";
                Console.WriteLine($"Navigating to {url}");
                driver.Navigate().GoToUrl(url);

                Console.WriteLine("Looking for element");

                // Wait for the search box to be present
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementIsVisible(By.Name("q")));

                // Find the search box and enter "ADP"
                IWebElement searchBox = driver.FindElement(By.Name("q"));
                searchBox.SendKeys("ADP");
                searchBox.Submit();

                // Wait for the results to load and display the results
                System.Threading.Thread.Sleep(2000);

                // Click on the first search result
                IWebElement firstResult = driver.FindElement(By.CssSelector("h3"));
                firstResult.Click();

                // Locate the first link in the list
                //IWebElement firstLink = driver.FindElement(By.CssSelector("ul.posts li:first-child a"));

                //if (firstLink is null)
                //{
                //    Console.WriteLine("Not able to get an element with posts");
                //    return;
                //}

                // Click the first link
                //firstLink.Click();

                //IWebElement headerOne = driver.FindElement(By.TagName("h1"));

                //var result = headerOne.Text.Contains("feedback");




                //IWebElement contactUsPageHeader = driver.FindElement(By.TagName("h1"));




                //// Navigate to Facebook login page
                //driver.Navigate().GoToUrl("https://www.facebook.com/login");

                //// Find and fill email field
                //var emailField = driver.FindElement(By.Id("email"));
                //emailField.SendKeys(email);

                //// Find and fill password field
                //var passwordField = driver.FindElement(By.Id("pass"));
                //passwordField.SendKeys(password);

                //// Click login button
                //var loginButton = driver.FindElement(By.Name("login"));
                //loginButton.Click();

                //// Wait to ensure login completes (adjust timeout as needed)
                System.Threading.Thread.Sleep(5000);

                //// Print current URL (to verify success)
                //Console.WriteLine("Current URL: " + driver.Url);
            }
            finally
            {
                driver.Quit(); // Close browser
            }
        }
    }
}