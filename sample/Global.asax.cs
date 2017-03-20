using System.Web;
using System.Web.Http;

namespace HalSample
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(HalConfig.Register);
        }
    }
}