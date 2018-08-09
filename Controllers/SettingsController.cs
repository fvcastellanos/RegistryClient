using Microsoft.AspNetCore.Mvc;
using RegistryClient.Services;
using RegistryClient.Models.Settings;

namespace RegistryClient.Controllers
{
    [Route("Settings")]
    public class SettingsController : Controller
    {
        private readonly SettingsService _settingsService;

        public SettingsController(SettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public IActionResult Index()
        {
            var settings = _settingsService.GetRegistrySettings();

            var model = new SettingsViewModel()
            {
                URL = settings.URL,
                UserName = settings.UserName
            };

            return View("Index", model);
        }
    }
}