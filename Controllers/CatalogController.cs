using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RegistryClient.Models.Catalog;
using RegistryClient.Services;

namespace RegistryClient.Controllers
{
    [Route("Catalog")]
    public class CatalogController : BaseController
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
                return RedirectError("Can't get image catalog");
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
                return RedirectError("Can't get tag list from image: " + imageName);
            }

            var imageTags = result.Right;
            var model = new TagsViewModel(imageTags.ImageName)
            {
                Tags = imageTags.TagList
            };
   
            return View("Tags", model);
        }

        [Route("DeleteTag/{imageName}/{tagName}")]
        public IActionResult ConfirmDeletion(string imageName, string tagName)
        {
            var model = BuildViewModel(imageName, tagName);

            return View("ConfirmDelete", model);
        }
        
        [HttpPost]
        [Route("DeleteTag")]
        public IActionResult DeleteImage(DeleteTagRequestView requestView)
        {
            if (!ModelState.IsValid)
            {
                return RedirectError("Can't delete image tag");
            }
            
            var result = _registryService.DeleteImage(requestView.ImageName, requestView.TagName);

            if (result.IsLeft())
            {
                var model = BuildViewModel(requestView.ImageName, requestView.TagName, result.Left);
                return View("ConfirmDelete", model);
            }

            return Redirect(Routes.Catalog + "/" + requestView.ImageName);
        }

        private ConfirmDeleteViewModel BuildViewModel(string imageName, string tagName)
        {
            return BuildViewModel(imageName, tagName, string.Empty);
        }
        
        private ConfirmDeleteViewModel BuildViewModel(string imageName, string tagName, string errorMessage)
        {
            return new ConfirmDeleteViewModel()
            {
                ImageName = imageName,
                TagName = tagName,
                ErrorMessage = errorMessage
            };
        }
    }
}