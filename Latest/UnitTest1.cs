using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Latest
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";

        [TestMethod]
        public void GetRequestLatest()
        {
            int[] candies = new int[] { 2, 3, 5, 1, 4 };
            int extraCandies = 3;

            var result = KidsWithCandies(candies, extraCandies);

            //RestClient client = new RestClient();
            //RestRequest getRequest = new RestRequest(getUrl);
            //var response = client.ExecuteGet(getRequest);
        }

        //1431. Kids With the Greatest Number of Candies
        public IList<bool> KidsWithCandies(int[] candies, int extraCandies)
        {
            IList<bool> result = new List<bool>();

            for (int i = 0; i < candies.Length; i++)
            {
                int sum = candies[i] + extraCandies;
                bool hasGreatestCandies = true;

                for (int k = 0; k < candies.Length; k++)
                {
                    if (sum < candies[k])
                    {
                        hasGreatestCandies = false;
                        break;
                    }
                }

                result.Add(hasGreatestCandies);
            }

            return result;
        }
    }
}
