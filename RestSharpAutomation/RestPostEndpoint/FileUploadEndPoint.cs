using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Net;

namespace RestSharpAutomation.RestPostEndpoint
{
    [TestClass]
    public class FileUploadEndPoint
    {
        [TestMethod]
        public void Test_Upload_Of_File()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest()
            {
                Resource = "https://limitless-lake-55070.herokuapp.com/fileUpload/"
            };

            restRequest.AddFile("file", @"E:\RestSharpFramework\TextFile1.txt", "multipart/form-data");

            var response = restClient.Post(restRequest);
            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}