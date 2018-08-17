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
            DockerRegistryClient dockerRegistryClient)
        {
            _registryClient = dockerRegistryClient;
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
                _logger.LogError("can't get tags for image: {0} - : ", imageName, ex);
                return Result<string, ImageTags>.ForLeft("can't get tags for image: " + imageName);
            }
        }

        public Result<string, string> DeleteImage(string imageName, string tagName)
        {
            try
            {
                var result = _registryClient.DeleteImage(imageName, tagName);

                return !result ? Result<string, string>.ForLeft("image: " + imageName + " tag: " + tagName + " was not deleted") 
                    : Result<string, string>.ForRight(imageName + ":" + tagName);
            }
            catch (Exception ex)
            {
                _logger.LogError("can't delete image: {0} tag: {1}", imageName, tagName);
                return Result<string, string>.ForLeft("can't delete image: " + imageName + " tag: " + tagName);
            }
        }

    }
}