using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharpAutomation.CustomReporter;
using System;
using System.Linq;

namespace RestSharpAutomation.ReportAttribute
{
    /*
     * 1.Create a class which inrerits from TestMethodAttribute
     * 2.Override the Execute method to provide the implementation
     * 3.Use the custom attribute with test method
     */

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestMethodWithReport : TestMethodAttribute
    {
        private readonly object syslock = new object();

        public override TestResult[] Execute(ITestMethod testMethod)
        {
            var name = testMethod.TestClassName + "." + testMethod.TestMethodName;
            var result = base.Execute(testMethod);
            var execution = result.FirstOrDefault();
            var status = execution.Outcome;
            var errorMessage = execution?.TestFailureException.Message;
            var trace = execution?.TestFailureException.StackTrace;

            lock (syslock)
            {
                CustomExtentReporter.GetInstance().AddToReport(name, "", status, errorMessage + "\n" + trace);
            }
            
            return result;
        }
    }
}