using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
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
            app.Run(ctx => ctx.Response.WriteAsync("Hello, World!!"));
        }
    }
}