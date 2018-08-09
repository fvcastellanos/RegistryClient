using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RegistryClient.Domain;
using RegistryClient.Models.Catalog;
using RegistryClient.Services;

namespace RegistryClient.Controllers
{
    [Route("Catalog")]
    public class CatalogController : Controller
    {
        private RegistryService _registryService;

        public CatalogController(RegistryService registryService)
        {
            _registryService = registryService;
        }

        public IActionResult Index()
        {
            // var images = _registryService.getImages();

            var images = new List<DockerImage>();

            images.Add(new DockerImage() { Name = "cosa" });
            images.Add(new DockerImage() { Name = "casa" });
            images.Add(new DockerImage() { Name = "otra cosa" });
            images.Add(new DockerImage() { Name = "otra casa" });

            var names = from image in images
                select image.Name;

            var model = new ImageListViewModel()
            {
                Images = names
            };

            return View("Index", model);
        }

        [Route("{imageName}")]
        public IActionResult Tags(string imageName)
        {
            var model = new TagsViewModel(imageName);
            model.Tags = new List<string>();

            return View("Tags", model);
        }
    }
}