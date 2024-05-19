using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace StepikCourseDownloader
{
    internal class Authorization
    {
        private string authUrl;
        private string clientId;
        private string clientSecret;

        public Authorization(string authUrl, string clientId, string clientSecret)
        {
            this.authUrl = authUrl;
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }  

        public async Task<string?> GetToken()
        {
            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            var content = new FormUrlEncodedContent(values);

            var authenticationString = $"{clientId}:{clientSecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString));

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, authUrl);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            requestMessage.Content = content;

            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return JObject.Parse(responseBody)["access_token"]?.ToString();
        }
    }
}