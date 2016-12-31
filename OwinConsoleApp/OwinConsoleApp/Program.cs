using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;

namespace OwinConsoleApp
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    //public class Program
    //{
    //    private static void Main(string[] args)
    //    {
    //        string uri = "http://localhost:5000";

    //        using (WebApp.Start<Startup>(uri))
    //        {
    //            Console.WriteLine("Started!");
    //            Console.ReadKey();
    //            Console.WriteLine("Stopped!");
    //        }
    //    }
    //}

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(async (environment, next) =>
            {
                Console.WriteLine($"Request path : {environment.Request.Path}");
                await next();
                Console.WriteLine($"Response Status : {environment.Response.StatusCode}");
            });

            ConfigureWebApi(app);

            app.UseHelloWorld();
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();
            configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "Greeting", id = RouteParameter.Optional });

            app.UseWebApi(configuration);
        }
    }

    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseHelloWorld(this IAppBuilder app)
        {
            return app.Use<HelloWorldComponent>();
        }
    }

    public class HelloWorldComponent
    {
        private readonly AppFunc _next;

        public HelloWorldComponent(AppFunc next)
        {
            _next = next;
        }

        public Task Invoke(IDictionary<string, object> environment)
        {
            var stream = environment["owin.ResponseBody"] as Stream;
            using (StreamWriter writer = new StreamWriter(stream))
            {
                return writer.WriteAsync($"Hello, World!");
            }
        }
    }
}