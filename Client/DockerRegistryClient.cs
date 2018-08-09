using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RegistryClient.Client.Model;
using static RegistryClient.Client.HttpClientFactory;

namespace RegistryClient.Client
{
    public class DockerRegistryClient
    {
        private readonly HttpClient _httpClient;

        public DockerRegistryClient(string url, string userName, string password) {
            _httpClient = createClient(url, userName, password);
        }

        public Catalog getCatalog() {
            // var serializer = new DataContractJsonSerializer(typeof(Catalog));
            var stringTask = _httpClient.GetStringAsync("_catalog");

            var catalog = JsonConvert.DeserializeObject<Catalog>(stringTask.Result);
            // var streamTask = _httpClient.GetStreamAsync("_catalog");

            // var repositories = serializer.ReadObject(streamTask.Result) as Catalog;

            return catalog;
        }
    }
}