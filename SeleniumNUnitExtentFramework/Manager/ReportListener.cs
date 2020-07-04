using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Configuration;
using System.Collections;

namespace SeleniumNUnitExtentFramework.Manager
{
    [SetUpFixture]
    public abstract class ReportListener
    {
        public IWebDriver driver;

        [OneTimeSetUp]
        protected void Setup()
        {

            Report.ExtentManager.getExtent();
        }
        [OneTimeTearDown]
        protected void TearDown()
        {
            Report.ExtentManager.getExtent().Flush();
        }
        [SetUp]
        public void BeforeTest()
        {
            String browserName = ConfigurationManager.AppSettings["browser"];
            BrowserManager brmanager = new BrowserManager();
            driver = brmanager.getWebDriver(browserName);
            String methodName = TestContext.CurrentContext.Test.MethodName;
            Report.ExtentTestManager.createTest(TestContext.CurrentContext.Test.Name, browserName, getTestCategories(methodName));
        }
        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    DateTime time = DateTime.Now;
                    String methodName = TestContext.CurrentContext.Test.MethodName;
                    String fileName = methodName+"_" + time.ToString("h_mm_ss") + ".png";
                    String screenShotPath = Capture(driver, fileName);
                    Report.ExtentTestManager.getTest().Log(Status.Fail, "<b style=\"color: Red; \">Fail</b>");
                    //Report.ExtentTestManager.getTest().Log(Status.Fail, "Snapshot below: " + Report.ExtentTestManager.getTest().AddScreenCaptureFromPath("Screenshots\\" + fileName));
                    Report.ExtentTestManager.getTest().AddScreenCaptureFromPath("Screenshots\\" + fileName);
                    Report.ExtentTestManager.getTest().Log(logstatus, "Test ended with: " + stacktrace);
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    Report.ExtentTestManager.getTest().Log(Status.Warning, "<b style=\"color: Tomato; \">Warning</b>");
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    Report.ExtentTestManager.getTest().Log(Status.Skip, "<b style=\"color: Orange; \">Skipped</b>");
                    break;
                default:
                    logstatus = Status.Pass;
                    Report.ExtentTestManager.getTest().Log(Status.Pass, "<b style=\"color: MediumSeaGreen; \">Pass</b>");
                    break;
            }

            //Report.ExtentTestManager.getTest().Log(logstatus, "Test ended with: " + stacktrace);
            Report.ExtentManager.getExtent().Flush();
            driver.Quit();
        }
        public IWebDriver GetDriver()
        {
            return driver;
        }
        public static string Capture(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            var pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            var reportPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(reportPath + "Reports\\" + "Screenshots");
            var finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "Reports\\Screenshots\\" + screenShotName;
            var localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
            return reportPath;
        }
        public String getTestCategories(String methodName)
        {
            var myAttribute = this.GetType().GetMethod(methodName).GetCustomAttributes(true).OfType<CategoryAttribute>().FirstOrDefault();
            return myAttribute.Name;
        }
    }
}
