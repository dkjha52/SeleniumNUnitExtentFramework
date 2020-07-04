using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using System.Threading;
using System.Runtime.CompilerServices;

namespace SeleniumNUnitExtentFramework.Report
{
    class ExtentTestManager
    {
       
        public static ThreadLocal<ExtentTest> extentTest = new ThreadLocal<ExtentTest>();
        public static ExtentReports extent = ExtentManager.getExtent();
        public static ExtentTest test;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest getTest()
        {
            return extentTest.Value;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest createTest(String name, String description,String deviceId)
        {
            test = extent.CreateTest(name, name).AssignCategory(deviceId);
            test.AssignDevice(description);
            extentTest.Value=test;
            return getTest();
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest createTest(String name, String description)
        {
            return createTest(name, description, Thread.CurrentThread.Name);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
         public static ExtentTest createTest(String name)
        {
            return createTest(name, "Sample Test");
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void logger(String message)
        {
            getTest().Log(Status.Info, message + "<br>");
        }
    }
}
