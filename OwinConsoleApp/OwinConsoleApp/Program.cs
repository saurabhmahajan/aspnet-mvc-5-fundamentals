using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string uri = "http://localhost:5000";

            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine("Started!");
                Console.ReadKey();
                Console.WriteLine("Stopped!");
            }
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.Run(ctx => ctx.Response.WriteAsync("Hello, World!!"));
            app.Use<HelloWorldComponent>();
        }
    }

    public class HelloWorldComponent
    {
        private readonly Func<IDictionary<string, object>, Task> _next;

        public HelloWorldComponent(Func<IDictionary<string, object>, Task> next)
        {
            _next = next;
        }

        public Task Invoke(IDictionary<string, object> environment)
        {
            var stream = environment["owin.ResponseBody"] as Stream;
            using (StreamWriter writer = new StreamWriter(stream))
            {
                return writer.WriteAsync("Hello, World!");
            }
        }
    }
}