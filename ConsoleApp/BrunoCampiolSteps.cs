using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ConsoleApp;

internal static class BrunoCampiolSteps
{
    public static void Run(ChromeOptions options)
    {
        using (IWebDriver driver = new ChromeDriver(options))
        {
            var url = "https://brunocampiol.github.io/";
            Console.WriteLine($"Navigating to {url}");
            driver.Navigate().GoToUrl(url);

            Console.WriteLine("Looking for element");

            // Wait for the search box to be present
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("posts")));

            // Find the h1 element and get its text
            IWebElement h1Element = driver.FindElement(By.TagName("h1"));
            var initialHeaderOne = h1Element.Text;
            Console.WriteLine($"Initial header text '{initialHeaderOne}'");

            // Move the mouse in a circle at the center of the web page
            //HumanBehavior.MoveMouseInCircle(driver, 100);
            //HumanBehavior.MoveMouseInParabola(driver, 10);

            // Find the first link inside the <ul class="posts">
            IWebElement firstLink = driver.FindElement(By.CssSelector("ul.posts li:first-child a"));
            var actions = new Actions(driver);
            actions.MoveToElement(firstLink, 0, firstLink.Size.Height + 10).Click().Perform();

            // Find the h1 element and get its text
            IWebElement newH1Element = driver.FindElement(By.TagName("h1"));
            var newHeaderOne = newH1Element.Text;
            Console.WriteLine($"New header text '{newHeaderOne}'");

            var isSame = initialHeaderOne.Equals(newHeaderOne, StringComparison.OrdinalIgnoreCase);
            if (isSame)
            {
                ColorConsole.WriteError("Headers are same");
            }
            else
            {
                ColorConsole.WriteSuccess("Worked!");
            }
        }
    }
}
