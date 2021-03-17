using System;
using DDACAssignment.Areas.Identity.Data;
using DDACAssignment.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(DDACAssignment.Areas.Identity.IdentityHostingStartup))]
namespace DDACAssignment.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DDACAssignmentContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DDACAssignmentContextConnection")));

                services.AddDefaultIdentity<DDACAssignmentUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<DDACAssignmentContext>();
            });
        }
    }
}