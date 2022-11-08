using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace OpenQA.Selenium
{
    public class WebDriverInfra
    {
        public static IWebDriver? webDriver;
        public static IWebDriver Create_Browser(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    return webDriver = new ChromeDriver();
                case BrowserType.Firefox:
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    return webDriver = new FirefoxDriver();
                case BrowserType.Safari:
                    return new SafariDriver();
                default:
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    return webDriver = new FirefoxDriver();
                    //throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null);
            }
        }
    }
}