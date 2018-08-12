using System;
using System.Collections.Generic;
using System.Linq;
using RegistryClient.Client;
using RegistryClient.Domain;

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

        public IEnumerable<DockerImage> GetImages()
        {
            var catalog = _registryClient.GetCatalog();

            var imageList = from imageName in catalog.Repositories
                select new DockerImage()
                {
                    Name = imageName
                };

            return imageList;
        }

        public IEnumerable<ImageTags> GetTags(string imagename)
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