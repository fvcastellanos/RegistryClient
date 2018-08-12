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
        private readonly RegistryService _registryService;

        public CatalogController(RegistryService registryService)
        {
            _registryService = registryService;
        }

        public IActionResult Index()
        {
            var result = _registryService.GetImages();

            if (result.IsLeft())
            {
                // do something about the error
            }
            
            var names = from image in result.Right
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
            var result = _registryService.GetTags(imageName);

            if (result.IsLeft())
            {
                // do something
            }

            var imageTags = result.Right;
            var model = new TagsViewModel(imageTags.ImageName)
            {
                Tags = imageTags.TagList
            };
   
            return View("Tags", model);
        }
    }
}