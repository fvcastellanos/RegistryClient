using System.Collections.Generic;
using RegistryClient.Domain;
using RegistryClient.Client;
using System.Linq;

namespace RegistryClient.Services
{
    public class RegistryService
    {
        private readonly DockerRegistryClient _registryClient;

        public RegistryService(SettingsService settingsService)
        {
            var settings = settingsService.GetRegistrySettings();
            _registryClient = new DockerRegistryClient(settings.URL, settings.UserName, settings.Password);
        }

        public IEnumerable<DockerImage> getImages()
        {
            var catalog = _registryClient.getCatalog();

            var imageList = from imageName in catalog.Repositories
                select new DockerImage()
                {
                    Name = imageName
                };

            return imageList;
        }

    }
}