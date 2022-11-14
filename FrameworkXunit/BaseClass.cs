using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Xunit;

namespace FrameworkXunit
{
	public class BaseClass : WebDriverInfra
	{
		public BaseClass()
        {
        }

        public void waitLoadPageIsComplete()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromMilliseconds(10000));
            // Wait for the page to load
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void click(IWebElement element)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromMilliseconds(10000));
            waitLoadPageIsComplete();
            var Locator = element.ToString;
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000);
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
            element.Click();
            Console.WriteLine("click " + Locator);
        }

        public void enterText(IWebElement element, String text)
        {
            waitLoadPageIsComplete();
            var Locator = element.ToString;
            element.Clear();
            element.SendKeys(text);
            Console.WriteLine("entered: " + text + " в: " + Locator);
        }

        public void clickElementInList(IList<IWebElement> listOfElements, String result)
        {
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000);
            Console.WriteLine("List: "+listOfElements);
            foreach (IWebElement element  in listOfElements)
            {
                Console.WriteLine("Ele: " + element.Text);

                if (element.Text == result)
                {
                    Console.WriteLine("Click: " + element.Text);
                    click(element);
                    return;
                }
            }
        }

        public void checkTitle(String expectedTitle)
        {
            var actualTitle = webDriver.Title;
            Console.WriteLine("Check: " + actualTitle + " = " + expectedTitle);
            Assert.Equal(expectedTitle, actualTitle);
        }
    }    
}

