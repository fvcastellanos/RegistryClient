using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using RegistryClient.Client;
using RegistryClient.Domain;

namespace RegistryClient.Services
{
    public class RegistryService
    {
        private readonly ILogger _logger;
        private readonly DockerRegistryClient _registryClient;

        public RegistryService(ILogger<RegistryService> logger,
            SettingsService settingsService)
        {
            var settings = settingsService.GetRegistrySettings();
            _registryClient = new DockerRegistryClient(settings.URL, settings.UserName, settings.Password);
            _logger = logger;
        }

        public Result<string, IEnumerable<DockerImage>> GetImages()
        {
            try
            {
                var catalog = _registryClient.GetCatalog();

                var imageList = from imageName in catalog.Repositories
                    select new DockerImage()
                    {
                        Name = imageName
                    };

                return Result<string, IEnumerable<DockerImage>>.ForRight(imageList);
            }
            catch (Exception ex)
            {
                _logger.LogError("can't get images: ", ex);
                return Result<string, IEnumerable<DockerImage>>.ForLeft("can't get images list");
            }
        }

        public Result<string, ImageTags> GetTags(string imageName)
        {
            try
            {
                var imageTags = _registryClient.GetTags(imageName);

                var tags = new ImageTags()
                {
                    ImageName = imageTags.Name,
                    TagList = imageTags.Tags
                };

                return Result<string, ImageTags>.ForRight(tags);

            }
            catch (Exception ex)
            {
                _logger.LogError("can't get tags for image: %s : ", imageName, ex);
                return Result<string, ImageTags>.ForLeft("can't get tags for image: " + imageName);
            }
        }

    }
}