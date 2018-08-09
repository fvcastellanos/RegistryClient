using RegistryClient.Domain;

using static System.Environment;

namespace RegistryClient.Services
{
    public class SettingsService
    {
        public Settings GetRegistrySettings()
        {
            return new Settings() 
            {
                URL = GetEnvironmentVariable("REGISTRY_URL"),
                UserName = GetEnvironmentVariable("REGISTRY_USERNAME"),
                Password = GetEnvironmentVariable("REGISTRY_PASSWORD")
            };
        }
    }
}