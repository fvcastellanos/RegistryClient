using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RegistryClient.Client.Model;
using static RegistryClient.Client.HttpClientFactory;

namespace RegistryClient.Client
{
    public class DockerRegistryClient
    {
        private readonly HttpClient _httpClient;

        public DockerRegistryClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Catalog GetCatalog()
        {
            var stringTask = _httpClient.GetStringAsync("_catalog");

            var catalog = JsonConvert.DeserializeObject<Catalog>(stringTask.Result);

            if (stringTask.IsCompletedSuccessfully)
            {
                return catalog;
            }

            throw stringTask.Exception;
        }

        public ImageTags GetTags(string imageName)
        {
            var stringTask = _httpClient.GetStringAsync(imageName + "/tags/list");

            var tags = JsonConvert.DeserializeObject<ImageTags>(stringTask.Result);

            return tags;
        }

        public bool DeleteImage(string imageName, string tagName)
        {
            var task = _httpClient.DeleteAsync(imageName + "/manifests/" + tagName);
            var responseMessage = task.Result;

            return responseMessage.IsSuccessStatusCode;
        }
    }
}