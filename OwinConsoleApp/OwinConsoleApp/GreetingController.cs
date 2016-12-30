using System.Web.Http;

namespace OwinConsoleApp
{
    public class GreetingController : ApiController
    {
        public Greeting Get()
        {
            return new Greeting { Text = "Hello from Controller." };
        }
    }
}