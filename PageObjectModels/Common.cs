using OpenQA.Selenium;
using System;
using System.Threading;

namespace Alfresco.specs.PageObjectModels
{
    public class Common
    {
        public void NavigateTo(IWebDriver Driver, string Url, string Title)
        {
            Driver.Navigate().GoToUrl(Url);
            Driver.Manage().Window.Maximize();
            EnsurePageLoaded(Driver, Url, Title);
        }

        public bool EnsurePageLoaded(IWebDriver Driver, string Url, string Title)
        {
            Thread.Sleep(2000);
            bool pageHasLoaded = (Driver.Url == Url) && (Driver.Title == Title);

            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load page. Page URL = '{Driver.Url}' Page Source: \r\n {Driver.PageSource}");
            }

            return pageHasLoaded;
        }
    }
}
