using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.IO;
using System.Threading.Tasks;

namespace RestSharpAutomation.DropBoxAPI
{
    [TestClass]
    public class TestDropBoxAPI
    {
        private const string ListEndPointUrl = "https://api.dropboxapi.com/2/files/list_folder";
        private const string CreateEndPointUrl = "https://api.dropboxapi.com/2/files/create_folder_v2";
        private const string DownloadEndPointUrl = "https://content.dropboxapi.com/2/files/download";
        private const string AccessToken = ""; //my access token
        
        [TestMethod]
        public void TestListFolder()
        {
            string body = "{\"path\": \"\",\"recursive\": false,\"include_media_info\": false,\"include_deleted\": false,\"include_has_explicit_shared_members\": false,\"include_mounted_folders\": true,\"include_non_downloadable_files\": true}";

            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = ListEndPointUrl
            };


           request.AddHeader("Authorization", "Bearer " + AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(body);

            var response = client.Post<ListFolderModel.RootObject>(request);
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [TestMethod]
        public void TestCreateFolder()
        {
            string body = "{\"path\": \"/TestFolder\",\"autorename\": true}";
            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = CreateEndPointUrl
            };

            request.AddHeader("Authorization", "Bearer " + AccessToken);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(body);
            var response = client.Post(request);
            Assert.AreEqual(200, (int)response.StatusCode);
        }


        [TestMethod]
        public void TestDownloadFile()
        {
            string location = "{\"path\": \"/Delegates and lambda expressions.ppt\"}";
            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = DownloadEndPointUrl
            };

            request.AddHeader("Authorization", "Bearer " + AccessToken);
            request.AddHeader("Dropbox-API-Arg", location);
            request.RequestFormat = DataFormat.Json;
            var dataInByte = client.DownloadData(request);
            File.WriteAllBytes("Delegates and lambda expressions.ppt", dataInByte);
        }

        [TestMethod]
        public void TestFileDownloadParaller()
        {
            string locationOfFile1 = "{\"path\": \"/Delegates and lambda expressions.ppt\"}";
            string locationOfFile2 = "{\"path\": \"/Structs.zip\"}";

            IRestRequest file1 = new RestRequest()
            {
                Resource = DownloadEndPointUrl
            };
            file1.AddHeader("Authorization", "Bearer " + AccessToken);
            file1.AddHeader("Dropbox-API-Arg", locationOfFile1);

            IRestRequest file2 = new RestRequest()
            {
                Resource = DownloadEndPointUrl
            };
            file2.AddHeader("Authorization", "Bearer " + AccessToken);
            file2.AddHeader("Dropbox-API-Arg", locationOfFile2);


            IRestClient client = new RestClient();

            byte[] downloadDataFile1 = null;
            byte[] downloadDataFile2 = null;

            var task1 = Task.Factory.StartNew(() =>
            {
                downloadDataFile1 = client.DownloadData(file1);
            });

            var task2 = Task.Factory.StartNew(() =>
            {
                downloadDataFile2 = client.DownloadData(file2);
            });

            task1.Wait();
            task2.Wait();

            if(null != downloadDataFile1)
                File.WriteAllBytes("Delegates and lambda expressions.ppt", downloadDataFile1);
            if(null != downloadDataFile2)
                File.WriteAllBytes("Structs.zip", downloadDataFile2);
        }
    }
}