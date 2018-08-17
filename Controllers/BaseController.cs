using System;
using Microsoft.AspNetCore.Mvc;
using RegistryClient.Models;

namespace RegistryClient.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IActionResult RedirectError(string errorMessage)
        {

            var model = new ErrorViewModel()
            {
                ErrorMessage = errorMessage
            };

            return View("Error", model);
        }
    }
}