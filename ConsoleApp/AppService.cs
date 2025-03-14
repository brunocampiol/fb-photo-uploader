using ConsoleApp.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace ConsoleApp
{
    public class AppService
    {
        public void Run_V2()
        {
            var screenWidth = ScreenService.ScreenWidth;
            var screenHeight = ScreenService.ScreenHeight;
            Console.WriteLine($"Screen size: {screenWidth}x{screenHeight}");

            // Get and display the initial mouse position
            var initialPosition = MouseService.CursorPosition;
            Console.WriteLine($"Initial mouse position: X={initialPosition.X}, Y={initialPosition.Y}");

            Console.WriteLine("Mouse will start moving in 2 seconds...");
            Thread.Sleep(2000);

            //Console.WriteLine("RightClickHumanRandom");
            //MouseService.RightClickHumanRandom();

            MouseService.MoveMouseHumanlike(new Coordinate(900, 900));

            //Console.WriteLine("LeftClickHumanRandom");
            //MouseService.LeftClickHumanRandom();

            //Console.WriteLine("RightClickHumanRandom");
            //MouseService.RightClickHumanRandom();

            MouseService.MoveMouseHumanlike(new Coordinate(200, 200));

            //Console.WriteLine("LeftClickHumanRandom");
            //MouseService.LeftClickHumanRandom();

            //Console.WriteLine("ScrollDown");
            //MouseService.ScrollDown(10, 1);

            //Console.WriteLine("ScrollUp");
            //MouseService.ScrollUp(20, 0.5);

            MouseService.MoveMouseHumanlike(new Coordinate(900, 100));

            MouseService.MoveMouseHumanlike(new Coordinate(100, 900));

            Console.WriteLine("Mouse movement complete. Press any key to exit.");
            Console.ReadKey();
        }

        public async Task RunAsync_V1()
        {
            var options = GetRealBrowserChromeOptions();
            using IWebDriver driver = new ChromeDriver(options);

            var url = "https://brunocampiol.github.io/";
            Console.WriteLine($"Navigating to {url}");
            driver.Navigate().GoToUrl(url);
            await Task.Delay(GetRandomDelay());

            // Moving the mouse
            //var actions = new Actions(driver);
            //actions.

            // Wait for the search box to be present
            Console.WriteLine("Looking for element");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("posts")));

            // Find the h1 element and get its text
            IWebElement h1Element = driver.FindElement(By.TagName("h1"));
            var initialHeaderOne = h1Element.Text;
            Console.WriteLine($"Initial header text '{initialHeaderOne}'");

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

        private static ChromeOptions GetRealBrowserChromeOptions()
        {
            // Initialize Chrome WebDriver
            var chromeOptions = new ChromeOptions();
            // Mimic real user browser settings
            chromeOptions.AddArgument("start-maximized");
            chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
            chromeOptions.AddExcludedArgument("enable-automation");
            // Add user agent to appear as a regular browser
            chromeOptions.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/134.0.0.0 Safari/537.36");
            // Disable infobars that might identify automation
            chromeOptions.AddArgument("--disable-infobars");
            chromeOptions.AddArgument("--disable-notifications");
            // Add experimental options to hide automation
            chromeOptions.AddAdditionalChromeOption("useAutomationExtension", false);
            //chromeOptions.AddAdditionalChromeOption("excludeSwitches", new string[] { "enable-automation" });
            // Add language preference like a real user
            chromeOptions.AddArgument("--lang=en-US");
            
            return chromeOptions;
        }

        public static int GetRandomDelay()
        {
            return new Random().Next(1000, 3000);
        }

        //        using (IWebDriver driver = new ChromeDriver(service, chromeOptions))
//{
//    // Remove navigator.webdriver flag
//    ((IJavaScriptExecutor) driver).ExecuteScript("Object.defineProperty(navigator, 'webdriver', {get: () => undefined})");

//    Console.WriteLine("Navigating");
//    await driver.Navigate().GoToUrlAsync("https://automationintesting.com/selenium/testpage/");

//    // Randomize timing to mimic human behavior
//    //await Task.Delay(GetRandomDelay());

//    // Create Actions object to control mouse
//    Actions actions = new Actions(driver);

//    // 1. Move mouse to a specific element (slowly)
//    Console.WriteLine("Moving mouse 1");
//    IWebElement element = driver.FindElement(By.TagName("h1"));
//    await MoveMouseSlowlyToElement(actions, element);
//    await Task.Delay(1000);

//    // 3. Find input fields and click on them
//    Console.WriteLine("Moving to input elements");
//    var inputFields = driver.FindElements(By.TagName("input"));
//    foreach (var input in inputFields)
//    {
//        if (input.Displayed && input.Enabled)
//        {
//            await MoveMouseSlowlyToElement(actions, input);
//    await Task.Delay(GetRandomDelay());
//    actions.Click().Perform();
//    await Task.Delay(GetRandomDelay());

//    // Type some text if it's a text input
//    string inputType = input.GetAttribute("type") ?? "text";
//            if (inputType == "text" || inputType == "email")
//            {
//                actions.SendKeys("Sample text").Perform();
//    await Task.Delay(GetRandomDelay());
//}
//        }
//    }

//    Console.WriteLine("Moving random");
//// 4. Move to a random position
//await MoveMouseToRandomPosition(actions, driver);
//}




//        // Method to move mouse slowly to an element (more human-like)
//        private static async Task MoveMouseSlowlyToElement(Actions actions, IWebElement element)
//{
//    for (int i = 1; i <= 5; i++)
//    {
//        actions.MoveToElement(element, element.Size.Width * i / 10, element.Size.Height / 2).Perform();
//        await Task.Delay(50 + GetRandomDelay() / 5);
//    }
//    actions.MoveToElement(element).Perform();
//}

//// Method to move mouse to random position on screen
//private static async Task MoveMouseToRandomPosition(Actions actions, IWebDriver driver)
//{
//    Random random = new Random();
//    int viewportWidth = Convert.ToInt32(((IJavaScriptExecutor)driver)
//        .ExecuteScript("return window.innerWidth"));
//    int viewportHeight = Convert.ToInt32(((IJavaScriptExecutor)driver)
//        .ExecuteScript("return window.innerHeight"));

//    int randomX = random.Next(0, viewportWidth);
//    int randomY = random.Next(0, viewportHeight);

//    // Create an invisible element at the random position
//    ((IJavaScriptExecutor)driver).ExecuteScript(
//        "var mouseTarget = document.createElement('div');" +
//        "mouseTarget.setAttribute('id', 'mouse-target');" +
//        "mouseTarget.style.position = 'absolute';" +
//        "mouseTarget.style.width = '1px';" +
//        "mouseTarget.style.height = '1px';" +
//        $"mouseTarget.style.left = '{randomX}px';" +
//        $"mouseTarget.style.top = '{randomY}px';" +
//        "document.body.appendChild(mouseTarget);");

//    // Move to that invisible element
//    IWebElement mouseTarget = driver.FindElement(By.Id("mouse-target"));
//    await MoveMouseSlowlyToElement(actions, mouseTarget);

//    // Clean up
//    ((IJavaScriptExecutor)driver).ExecuteScript("document.body.removeChild(document.getElementById('mouse-target'));");
//}
    }
}
