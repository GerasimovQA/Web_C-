using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
/* For using Remote Selenium WebDriver */
using OpenQA.Selenium.Remote;
using FrameworkXunit;
using System.Formats.Asn1;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Xml.Linq;
using AngleSharp.Dom.Events;
using System.Net.Mime;
using RestSharp;

[assembly: CollectionBehavior(MaxParallelThreads = 3)]

namespace xUnit_Test_Cross_Browser
{
    public class UnitTest : WebDriverInfra, IDisposable
    {
        public UnitTest()
        {
            var driver = WebDriverInfra.Create_Browser(BrowserType.NotSet);
        }

        public void Dispose()
        {
            webDriver.Quit();
        }

        BaseClass baseClass = new BaseClass();

        [Theory]
        [InlineData("https://www.google.com", "I am working")]
        [InlineData("https://www.google.com", "I am running")]
        [InlineData("https://www.google.com", "I am swimming")]
        public void testGoogle1(String url, String text)
        {
            Page page = new Page(webDriver);
            webDriver.Navigate().GoToUrl(url);
            Console.WriteLine(webDriver.Title);
            baseClass.enterText(page.searchInput, text);
            baseClass.click(page.popUpList);
            Thread.Sleep(3000);
        }

        [Theory(Skip = "Test Skip")]
        [InlineData("https://www.google.com", "I am working")]
        [InlineData("https://www.google.com", "I am running")]
        [InlineData("https://www.google.com", "I am swimming")]
        public void testGoogle2(String url, String text)
        {
            Page page = new Page(webDriver);
            webDriver.Navigate().GoToUrl(url);
            Console.WriteLine(webDriver.Title);
            baseClass.enterText(page.searchInput, text);
            baseClass.click(page.popUpList);
            Thread.Sleep(3000);
        }

        [Fact]
        public void getRequest()
        {
            var restclient = new RestClient("https://reqres.in");
            var restRequest = new RestRequest("/api/users/2");

            RestResponse response = restclient.Get(restRequest);
            JObject json = JObject.Parse(response.Content);

            Assert.True(200 == (int)response.StatusCode);
            Assert.True("janet.weaver@reqres.in" == (String)json["data"]["email"]);
        }

        [Fact]
        public void postRequest()
        {
            String name = "Neo";
            String job = "THE ONE";
            String payload = "{\"name\":\"" + name + "\", \"job\":\"" + job + "\"}";
            var restclient = new RestClient("https://reqres.in");
            var restRequest = new RestRequest("/api/users").AddJsonBody(payload);

            RestResponse response = restclient.Post(restRequest);
            JObject json = JObject.Parse(response.Content);

            Assert.True(201 == (int)response.StatusCode);
            Assert.True(name == (String)json["name"]);
            Assert.True(job == (String)json["job"]);
        }

        [Fact]
        public void putRequest()
        {
            String name = "Neo";
            String job = "THE ONE";
            String payload = "{\"name\":\"" + name + "\", \"job\":\"" + job + "\"}";
            var restclient = new RestClient("https://reqres.in");
            var restRequest = new RestRequest("/api/users/2").AddJsonBody(payload);

            RestResponse response = restclient.Put(restRequest);
            JObject json = JObject.Parse(response.Content);

            Assert.True(200 == (int)response.StatusCode);
            Assert.True(name== (String)json["name"]);
            Assert.True(job == (String)json["job"]);
            Assert.True("0" != (String)json["updatedAt"]);
        }


        [Fact]
        public void deleteRequest()
        {
            var restclient = new RestClient("https://reqres.in");
            var restRequest = new RestRequest("/api/users/2");

            RestResponse response = restclient.Delete(restRequest);

            Assert.True(204 == (int)response.StatusCode);
            Assert.True("" == ((String)response.Content));
        }
    }
}