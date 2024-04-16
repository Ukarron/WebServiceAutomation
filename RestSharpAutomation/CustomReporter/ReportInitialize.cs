using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestSharpAutomation.CustomReporter
{
    [TestClass]
    public class ReportInitialize
    {
        [AssemblyCleanup]
        public static void CleanUp()
        {
            CustomExtentReporter.GetInstance().WriteToReport();
        }
    }
}
