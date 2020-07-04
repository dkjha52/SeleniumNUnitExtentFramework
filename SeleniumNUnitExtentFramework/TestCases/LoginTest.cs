using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SeleniumNUnitExtentFramework.pageMethods;
using SeleniumNUnitExtentFramework.Manager;

namespace SeleniumNUnitExtentFramework.TestCases
{
    [TestFixture]
    class LoginTest: ReportListener
    {
        LoginPage loginPage;
        [Test]
        [Category("Login")]
        public void LoginTest_validLogin()
        {
            loginPage = new LoginPage(GetDriver());
            loginPage.goToPage();
            loginPage.goToToggleMenu();
            loginPage.goToLoginMenu();
            loginPage.enterUserName("John Doe");
            loginPage.enterPassword("ThisIsNotAPassword");
            loginPage.clickLoginBtn();
            Assert.IsTrue(loginPage.verifyDashboard());
            loginPage.closeBrowser();
        }

        [Test]
        [Category("Smoke")]
        public void LoginTest_invalidLogin()
        {
            loginPage = new LoginPage(GetDriver());
            loginPage.goToPage();
            loginPage.goToToggleMenu();
            loginPage.goToLoginMenu();
            loginPage.enterUserName("John Doe");
            loginPage.enterPassword("ThisIsNotA");
            loginPage.clickLoginBtn();
            Assert.IsTrue(loginPage.verifyDashboard());
            loginPage.closeBrowser();
        }
    }
}
