using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Net;

namespace RestSharpAutomation.JWT_Token.Model
{
    [TestClass]
    public class TestJwtToken
    {
        private string RegisterUrl = "https://jobapplicationjwt.herokuapp.com/users/sign-up";
        private string AuthenticateUrl = "https://jobapplicationjwt.herokuapp.com/users/authenticate";
        private string GetAllUrl = "https://jobapplicationjwt.herokuapp.com/auth/webapi/all";
        private IRestClient client;
        private IRestRequest request;
        private string token;
        private string user = "{ \"password\": \"qAz1@3\",  \"username\": \"somerandomuser\"}";

        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient();

            // Registration
            request = new RestRequest()
            {
                Resource = RegisterUrl
            };
            request.AddJsonBody(user);

            var response = client.Post(request);
            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            // Generate the token
            request = new RestRequest()
            {
                Resource = AuthenticateUrl
            };
            request.AddJsonBody(user);
            var responseToken = client.Post<JwtToken>(request);
            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(responseToken.Data.token); //JWT Token

            token = responseToken.Data.token;
        }

        [TestMethod]
        public void TestGetWithJwt()
        {
            var request = new RestRequest()
            {
                Resource = GetAllUrl
            };
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);

            var response = client.Get(request);
            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}