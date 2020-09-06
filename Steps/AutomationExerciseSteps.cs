using Alfresco.specs.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace Alfresco.specs
{
    [Binding]
    public class AutomationExerciseSteps
    {
        public static IWebDriver driver;

        public const string HomePageUrl = "http://qaexercise.envalfresco.com/settings";
        public const string LoginPageUrl = "http://qaexercise.envalfresco.com/login";
        public const string FilesPageUrl = "http://qaexercise.envalfresco.com/home";
        public const string HomeTitle = "Welcome - Alfresco ADF Application";
        public const string LoginTitle = "Welcome - Alfresco ADF Application";
        public const string FilesTitle = "Welcome - Alfresco ADF Application";
        public const string Username = "guest@example.com";
        public const string Password = "Password";
        public const string GithubUsername = "DaniGorgan";

        [Given(@"I access the web page")]
        public void GivenIAccessTheWebPage()
        {
            //Access to http://qaexercise.envalfresco.com/settings
            driver = new ChromeDriver();
            var currentPage = new Functions(driver);
            currentPage.NavigateTo(driver, HomePageUrl, HomeTitle);
        }
        
        [When(@"I do all the steps")]
        public void WhenIDoAllTheSteps()
        {
            var currentPage = new Functions(driver);

            //Change Provider to ECM
            currentPage.ClickProviderDropdown();
            currentPage.SelectProviderByText("ECM");

            //Click Apply
            currentPage.ClickApply();

            /*
             Navigate to http://qaexercise.envalfresco.com/login
                    Insert Username and Password
                    Username : guest@example.com
                    Password : Password
                    Click Login
             * */
            currentPage.EnsurePageLoaded(driver, LoginPageUrl, LoginTitle);
            currentPage.InsertLoginDetails(Username, Password);
            currentPage.ClickLogin();

            //Navigate to http://qaexercise.envalfresco.com/files
            currentPage.EnsurePageLoaded(driver, FilesPageUrl, FilesTitle);

            //Click on 'create new folder' icon.
            currentPage.ClickSidenavOption("Content Services");
            currentPage.ClickCreateNewFolder();

            //New folder dialog is displayed.
            currentPage.CheckPopUpAppeared();

            //Introduce your Github username (for example in my case) "magemello".
            currentPage.CompleteNameField(GithubUsername);

            //Name has been added.
            currentPage.CheckNameIsAdded(GithubUsername);

            //Click on 'Create' button.
            currentPage.ClickCreate();

            //Click on 'create new folder' icon.
            currentPage.ClickCreateNewFolder();

            //New folder dialog is displayed.
            currentPage.CheckPopUpAppeared();

            //Introduce your Github username(for example in my case) "magemello".
            currentPage.CompleteNameField(GithubUsername);

            //Name has been added.
            currentPage.CheckNameIsAdded(GithubUsername);

            //Click on 'Create' button.
            currentPage.ClickCreate();

            //The dialog is not closed.
            //The message "There's already a folder with this name. Try a different name" is displayed.
            //currentPage.CheckSameNameFolderUsedAlert();
            currentPage.ClickCancel();

            //Select the folder with your Github username
            currentPage.SelectFolderByText(GithubUsername);

            //Open Options window(3 dots)
            currentPage.ClickThreeDotsRelatedTo(GithubUsername);

            //Click delete
            currentPage.ClickDeleteAction();
        }

        [Then(@"it should work as expected")]
        public void ThenItShouldWorkAsExpected()
        {
            
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Close();
        }
    }
}
