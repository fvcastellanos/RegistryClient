using System.Collections.Generic;
using RegistryClient.Domain;
using RegistryClient.Client;
using System.Linq;
using System;

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
            var catalog = _registryClient.GetCatalog();

            var imageList = from imageName in catalog.Repositories
                select new DockerImage()
                {
                    Name = imageName
                };

            return imageList;
        }

        public IEnumerable<ImageTags> getTags(string imagename)
        {
            try
            {

            }
            catch (Exception ex)
            {
                // do something
            }
            var tags = _registryClient.GetTags(imagename);
            return null;
        }

    }
}