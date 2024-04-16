using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace RestSharpLatest.GetRequest
{
    [TestClass]
    public class TestGetRequestLatest
    {
        private readonly string getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";

        [TestMethod]
        public void GetRequestLatest()
        {
            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest();
            var response = client.ExecuteGet(getRequest);
        }
    }
}