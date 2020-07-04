using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System.IO;
namespace SeleniumNUnitExtentFramework.Manager
{
    class BrowserManager
    {
        IWebDriver driver;
        public IWebDriver getWebDriver(String browser_name)
        {
            Platform current = Platform.CurrentPlatform;
            String brname = browser_name.ToUpper();
            String path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            String actualPath = path.Substring(0, path.LastIndexOf("bin"));
            String projectPath = new Uri(actualPath).LocalPath;
            String chromepath = projectPath + "Lib\\chromedriver.exe";
            String geckopath = projectPath + "Lib\\geckodriver.exe";
            String edgepath = projectPath + "Lib\\msedgedriver.exe";

            switch (Enum.Parse(typeof(WebBrowserType), brname))
            {
                case WebBrowserType.FIREFOX:
                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService("webdriver.gecko.driver", geckopath);
                    driver = new FirefoxDriver(service);
                    driver.Manage().Window.Maximize();
                    return driver;
                case WebBrowserType.EDGE:
                    EdgeDriverService edservice = EdgeDriverService.CreateDefaultService("webdriver.edge.driver", edgepath);
                    driver = new EdgeDriver(edservice);
                    driver.Manage().Window.Maximize();
                    return driver;
                case WebBrowserType.CHROME:
                    ChromeDriverService chservice = ChromeDriverService.CreateDefaultService("webdriver.chrome.driver", chromepath);
                    driver = new ChromeDriver(chservice);
                    driver.Manage().Window.Maximize();
                    return driver;
                default:
                    throw new Exception("Browser type unsupported");
            }

        }

        public enum WebBrowserType
        {
            FIREFOX, IE, CHROME, SAFARI, EDGE
        }
    }
}
