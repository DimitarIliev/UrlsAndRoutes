using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.DependencyInjection;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("weekday", typeof(WeekDayConstraint));
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = true;
            });
            //services.Configure<RouteOptions>(options => options.ConstraintMap.Add("weekday", typeof(WeekDayConstraint)));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}");

                routes.Routes.Add(new LegacyRoute(
                    app.ApplicationServices,
                    "/articles/Windows_3.1_Overview.html",
                    "/old/.NET_1.0_Class_Library"));
                //routes.MapRoute(
                //    name: "NewRoute",
                //    template: "App/Do{action}",
                //    defaults: new { controller = "Home" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "out",
                    template: "outbound/{controller=Home}/{action=Index}");
            });


            //app.UseMvcWithDefaultRoute();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}");//, defaults: new { action = "Index" });
            //    Constraints
            //    routes.MapRoute(name: "MyRoute", template: "{controller}/{action}/{id?}", defaults: new { controller = "Home", action = "Index" }, constraints: new { id = new IntRouteConstraint() });
            //    regex
            //    routes.MapRoute(name: "MyRoute", template: "{controller:regex(^H.*)=Home}/{action=Index}/{id?}");
            //    routes.MapRoute(name: "MyRoute", template: "{controller:regex(^H.*)=Home}/" + "{action:regex(^Index$|^About$)=Index}/{id?}");
            //    routes.MapRoute(name: "MyRoute", template: "{controller=Home}/{action=Index}/{id:range(10,20)?}");
            //    routes.MapRoute(name: "MyRoute", template: "{controller=Home}/{action=Index}" + "{id:alpha:minlength(6)?}");
            //    routes.MapRoute(name: "MyRoute", template: "{controller}/{action}/{id?}", defaults: new { controller = "Home", action = "Index" }, constraints: new
            //    {
            //        id = new CompositeRouteConstraint(
            //            new IRouteConstraint[]
            //            {
            //                new AlphaRouteConstraint(),
            //                new MinLengthRouteConstraint(6)
            //            })
            //    });
            //    routes.MapRoute(name: "MyRoute", template: "{controller}/{action}/{id:weekday?}");
            //    routes.MapRoute(name: "MyRoute", template: "{controller}/{action}/{id?}", defaults: new { controller = "Home", action = "Index" }, constraints: new
            //    {
            //        id = new WeekDayConstraint()
            //    });
            //    routes.MapRoute(name: "MyRoute", template: "{controller=Home}/{action=Index}/{id:int?}");
            //    Catchall variable
            //    routes.MapRoute(name: "MyRoute", template: "{controller=Home}/{action=Index}/{id?}/{*catchall}");
            //    example route for prefixed Urls

            //    routes.MapRoute("Public/{controller=Home}/{action=Index}");
            //    url patter that has both static and variable elements
            //     routes.MapRoute("", "X{controller}/{action});

            //     route that has controller replaced with another
            //     routes.MapRoute(name: "ShopSchema", template: "Shop/{action}", defaults: new { controller = "Home" });
            //    controller and action replaced
            //     routes.MapRoute(name: "ShopSchema2", template: "Shop/OldAction", defaults: new { controller = "Home", action = "Index" });

            //    defining custom segment variables
            //     routes.MapRoute(name: "MyRoute", template: "{controller=Home}/{action=Index}/{id=DefaultId}");

            //    specifying optional url segment
            //     routes.MapRoute(name: "MyRoute", template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
