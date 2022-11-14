using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support;
using System;
namespace FrameworkXunit
{
    public class Page 
          {
        public Page(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//input[@name='q']")]
        public IWebElement searchInput;

        [FindsBy(How = How.XPath, Using = ".//div[@role='option']/../../..//li[not (@id)]")]
        public IList<IWebElement> popUpList;
    }
}

