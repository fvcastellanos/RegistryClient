using System.Net.Http;
using Newtonsoft.Json;
using RegistryClient.Client.Model;
using static RegistryClient.Client.HttpClientFactory;

namespace RegistryClient.Client
{
    public class DockerRegistryClient
    {
        private readonly HttpClient _httpClient;

        public DockerRegistryClient(string url, string userName, string password)
        {
            _httpClient = createClient(url, userName, password);
        }

        public Catalog GetCatalog()
        {
            var stringTask = _httpClient.GetStringAsync("_catalog");

            var catalog = JsonConvert.DeserializeObject<Catalog>(stringTask.Result);

            return catalog;
        }

        public ImageTags GetTags(string imageName)
        {
            var stringTask = _httpClient.GetStringAsync(imageName + "/tags/list");

            var tags = JsonConvert.DeserializeObject<ImageTags>(stringTask.Result);

            return tags;
        }
    }
}