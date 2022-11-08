using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support;
using System;
using System;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using SeleniumExtras.WaitHelpers;

namespace FrameworkXunit
{
	public class BaseClass : WebDriverInfra
	{
		public BaseClass()
        {
        }


        public void load_complete()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromMilliseconds(10000));

            // Wait for the page to load
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void click(IWebElement element)
        {
            load_complete();
            var Locator = element.ToString;
            //var wait = new WebDriverWait(webDriver, TimeSpan.FromMilliseconds(10000));
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000);
            //wait.Until(ExpectedConditions.ElementToBeClickable(element));
            element.Click();
            Console.WriteLine("click " + Locator);
        }

        public void enterText(IWebElement element, String text)
        {
            load_complete();
            var Locator = element.ToString;
            element.Clear();
            element.SendKeys(text);
            Console.WriteLine("entered: " + text + " в: " + Locator);
        }

        public static By ConvertToBy(IWebElement element)
        {
            if (element == null) throw new NullReferenceException();

            var attributes =
                ((IJavaScriptExecutor)webDriver).ExecuteScript(
                    "var items = {}; for (index = 0; index < arguments[0].attributes.length; ++index) { items[arguments[0].attributes[index].name] = arguments[0].attributes[index].value }; return items;",
                    element) as Dictionary<string, object>;
            if (attributes == null) throw new NullReferenceException();

            var selector = "//" + element.TagName;
            selector = attributes.Aggregate(selector, (current, attribute) =>
                 current + "[@" + attribute.Key + "='" + attribute.Value + "']");

            return By.XPath(selector);
        }
    }
}

