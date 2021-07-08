using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(GamePlace.Areas.Identity.IdentityHostingStartup))]
namespace GamePlace.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}