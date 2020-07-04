using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNUnitExtentFramework.pageMethods
{
    class LoginPage
    {
        private IWebDriver driver;

        String toggle_menu = "//a[@id='menu-toggle']/i";
        String loginMenu = "//a[contains(text(),'Login')]";
        String userID = "//input[@id='txt-username']";
        String password = "//input[@id='txt-password']";
        String loginBtn = "//button[@id='btn-login']";
        String menuDashboard = "//section[@id='appointment']/div/div/div/h2";

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void goToPage()
        {
            driver.Navigate().GoToUrl("http://demoaut.katalon.com/");
            Report.ExtentTestManager.logger("Launch Web Application");
        }

        public void goToToggleMenu()
        {
            driver.FindElement(By.XPath(toggle_menu)).Click();
            Report.ExtentTestManager.logger("Clicked on Toggle Menu");
        }
        public void goToLoginMenu()
        {
            driver.FindElement(By.XPath(loginMenu)).Click();
            Report.ExtentTestManager.logger("Clicked on Login Menu Option");
        }
        public void enterUserName(string text)
        {
            driver.FindElement(By.XPath(userID)).SendKeys(text);
            Report.ExtentTestManager.logger("Enter UserName");
        }
        public void enterPassword(string text)
        {
            driver.FindElement(By.XPath(password)).SendKeys(text);
            Report.ExtentTestManager.logger("Enter Password");
        }
        public void clickLoginBtn()
        {
            driver.FindElement(By.XPath(loginBtn)).Click();
            Report.ExtentTestManager.logger("Clicked on Login Button");
        }
        public Boolean verifyDashboard()
        {
            Boolean res = driver.FindElement(By.XPath(menuDashboard)).Displayed;
            return res;
        }
        public void closeBrowser()
        {
            driver.Quit();
        }
    }
}
