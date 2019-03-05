using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace UrlsAndRoutes
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvc(routes => {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}");//, defaults: new { action = "Index" });
                // example route for prefixed Urls
                // routes.MapRoute("Public/{controller=Home}/{action=Index}");
                // url patter that has both static and variable elements
                // routes.MapRoute("", "X{controller}/{action});

                // route that has controller replaced with another
                // routes.MapRoute(name: "ShopSchema", template: "Shop/{action}", defaults: new { controller = "Home" });
                // controller and action replaced
                // routes.MapRoute(name: "ShopSchema2", template: "Shop/OldAction", defaults: new { controller = "Home", action = "Index" });

                // defining custom segment variables
                // routes.MapRoute(name: "MyRoute", template: "{controller=Home}/{action=Index}/{id=DefaultId}");

                //specifying optional url segment
                // routes.MapRoute(name: "MyRoute", template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
