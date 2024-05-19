using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace StepikCourseDownloader
{
    internal class Helpers
    {
        private string apiHost;
        private string token;
        private static HttpClient httpClient = new HttpClient();

        public Helpers(string apiHost, string token)
        {
            this.apiHost = apiHost;
            this.token = token;
        }

        public async Task<JToken> FetchObject(string objClass, JToken objId)
        {
            var apiUrl = $"{apiHost}/api/{objClass}s/{objId}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl);

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return JObject.Parse(responseBody)[$"{objClass}s"][0];
        }

        public async Task<List<JToken>> FetchObjects(string objClass, JToken objIds)
        {
            var ids = objIds.ToObject<int[]>() ?? new int[0];
            var objs = new List<JToken>();
            var stepSize = 30;

            for (var i = 0; i < ids.Length; i += stepSize)
            {
                var objIdsSlice = objIds.ToList().GetRange(i, Math.Min(stepSize, ids.Length - i)).Select(ids => $"ids[]={ids}");
                var apiUrl = string.Format("{0}/api/{1}s?{2}", apiHost, objClass, string.Join("&", objIdsSlice));

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl);

                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                objs.AddRange(JObject.Parse(responseBody)[$"{objClass}s"]);
            }

            return objs;
        }
    }
}
