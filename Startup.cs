using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistryClient.Client;
using RegistryClient.Controllers;
using RegistryClient.Services;

using static RegistryClient.Client.HttpClientFactory;

namespace RegistryClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Custom configurations
            var settingsService = new SettingsService();
            var settings = settingsService.GetRegistrySettings();
            var httpClient = CreateHttpClientBasicAuth(settings.URL, settings.UserName, settings.Password);
            
            services.AddMvc();

            services.AddLogging();

            services.AddSingleton<SettingsService, SettingsService>(provider => settingsService);

            services.AddSingleton<HttpClient, HttpClient>(provider => httpClient);
            
            services.AddSingleton<DockerRegistryClient, DockerRegistryClient>();

            services.AddSingleton<RegistryService, RegistryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UsePathBase(Routes.Root);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
