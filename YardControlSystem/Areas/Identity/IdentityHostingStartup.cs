using System;
using YardControlSystem.Areas.Identity.Data;
using YardControlSystem.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(YardControlSystem.Areas.Identity.IdentityHostingStartup))]
namespace YardControlSystem.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<YardControlSystemIdentityContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("YardControlSystemIdentityContextConnection")));

                services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
               
                    .AddEntityFrameworkStores<YardControlSystemIdentityContext>();
            });
        }
    }
}