using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RegistryClient.Client
{    
    public class HttpClientFactory
    {
        public static HttpClient createClient(string baseUrl, string user, string password)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);
            var token = buildBasicAuthToken(user, password);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);

            return httpClient;
        }

        private static string buildBasicAuthToken(string user, string password)
        {
            var token = user + ":" + password;
            var tokenBytes = System.Text.Encoding.UTF8.GetBytes(token);
            return System.Convert.ToBase64String(tokenBytes);
        }        
    }
}