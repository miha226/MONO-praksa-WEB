using CarDealership.WebAPI.App_Start;
using System.Web.Http;

namespace CarDealership.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutofacConfig.StartDI();
        }
    }
}
