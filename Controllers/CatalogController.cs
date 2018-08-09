using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
            var images = _registryService.getImages();

            var names = from image in images
                select image.Name;

            var model = new ImageListViewModel()
            {
                Images = names
            };

            return View("Index", model);
        }
    }
}