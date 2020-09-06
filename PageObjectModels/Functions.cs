using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace Alfresco.specs.PageObjectModels
{
    public class Functions : Common
    {
        private readonly IWebDriver Driver;
        private readonly ITestOutputHelper output;

        public const string HomeUrl = "https://www.demoblaze.com/";
        public const string HomeTitle = "STORE";

        protected readonly WebDriverWait Wait;

        public Functions(IWebDriver driver, ITestOutputHelper output)
        {
            Driver = driver;
            this.output = output;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public Functions(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private IWebElement ProviderDropdown =>
            Wait.Until(ExpectedConditions.ElementIsVisible(
                By.Id("adf-provider-selector")));

        private IReadOnlyCollection<IWebElement> ProviderDropdownElements => Wait.Until(
            ExpectedConditions.PresenceOfAllElementsLocatedBy(
                By.ClassName("mat-option-text")));

        private IReadOnlyCollection<IWebElement> SideNavElements => Wait.Until(
            ExpectedConditions.PresenceOfAllElementsLocatedBy(
                By.TagName("mat-list-item")));

        private IReadOnlyCollection<IWebElement> CreatedFolderElements => Wait.Until(
            ExpectedConditions.PresenceOfAllElementsLocatedBy(
                By.XPath("//div[@class='adf-datatable-body']//adf-datatable-row")));


        public void TypeUsername(string username) => Wait.Until(ExpectedConditions.ElementIsVisible(
                By.Id("username"))).SendKeys(username);

        public void TypePassword(string password) => Wait.Until(ExpectedConditions.ElementIsVisible(
                By.Id("password"))).SendKeys(password);

        public void ClickApply() => Wait.Until(ExpectedConditions.ElementIsVisible(
                By.Id("host-button"))).Click();

        public void ClickProviderDropdown() => ProviderDropdown.Click();

        public void ClickLogin() => Wait.Until(ExpectedConditions.ElementIsVisible(
                By.Id("login-button"))).Click();

        public void ClickCreate() => Wait.Until(ExpectedConditions.ElementIsVisible(
                By.Id("adf-folder-create-button"))).Click();

        public void ClickCancel() => Wait.Until(ExpectedConditions.ElementIsVisible(
                By.Id("adf-folder-cancel-button"))).Click();

        public void ClickCreateNewFolder() => Wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//mat-icon[contains(text(),'create_new_folder')]"))).Click();

        public void ClickDeleteFolderButton() => Wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//button[6]"))).Click();

        public void ClickDeleteAction() => Wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//span[contains(text(),'Delete')]"))).Click();

        public void CompleteNameField(string inputText) => Wait.Until(ExpectedConditions.ElementIsVisible(
                By.Id("adf-folder-name-input"))).SendKeys(inputText);

        public void CheckNameIsAdded(string inputText)
        {
            string valueTyped;
            //click outside of Name box
            Driver.FindElement(By.Id("adf-folder-title-input")).Click();
            
            valueTyped = Driver.FindElement(By.Id("adf-folder-name-input")).GetAttribute("value");
            Assert.Equal(inputText, valueTyped);
        }

        public void ClickSidenavOption(string option)
        {
            foreach (var optionElement in SideNavElements)
            {
                if (SideNavElements.ElementAt(SideNavElements.ToList().IndexOf(optionElement)).Text.Contains(option))
                {
                    SideNavElements.ElementAt(SideNavElements.ToList().IndexOf(optionElement)).Click();
                    Thread.Sleep(1000);
                    break;
                }
            }
        }

        public void SelectProviderByText(string projectType)
        {
            foreach (var projectTypeElement in ProviderDropdownElements)
            {
                if (ProviderDropdownElements.ElementAt(ProviderDropdownElements.ToList().IndexOf(projectTypeElement)).Text.Contains(projectType))
                {
                    ProviderDropdownElements.ElementAt(ProviderDropdownElements.ToList().IndexOf(projectTypeElement)).Click();
                    Thread.Sleep(1000);
                    break;
                }
            }
        }

        public void SelectFolderByText(string folderName)
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//span[contains(text(),'"+ folderName + "')]"))).Click();
        }

        public void ClickThreeDotsRelatedTo(string folderName)
        {
            int count = 0;
            foreach (var folderEntryElement in CreatedFolderElements)
            {
                if (CreatedFolderElements.ElementAt(CreatedFolderElements.ToList().IndexOf(folderEntryElement)).Text.Contains(folderName))
                {
                    CreatedFolderElements.ElementAt(CreatedFolderElements.ToList().IndexOf(folderEntryElement)).FindElement(By.Id("action_menu_right_" + count)).Click();
                    Thread.Sleep(1000);
                    break;
                }
                count++;
            }
        }

        public void InsertLoginDetails(string username, string password)
        {
            TypeUsername(username);
            TypePassword(password);
        }

        public void CheckPopUpAppeared()
        {
            try
            {
                Driver.SwitchTo().ActiveElement();
                Thread.Sleep(1000);
            }
            catch (NoAlertPresentException exception)
            {
                output.WriteLine("Popup is not present: " + exception);
            }
        }

        public void CheckSameNameFolderUsedAlert()
        {
            var errorMessage = Driver.SwitchTo().Alert().Text;
            Assert.Equal("There's already a folder with this name. Try a different name.", errorMessage);
        }
    }
}
