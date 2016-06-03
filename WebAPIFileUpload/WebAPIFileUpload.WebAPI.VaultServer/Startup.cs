using Owin;
using System.Web.Http;

namespace WebAPIFileUpload.WebAPI.VaultServer
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            

            appBuilder.UseWebApi(config);
        }
    }
}
