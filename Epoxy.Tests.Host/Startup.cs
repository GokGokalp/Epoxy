using System.Web.Http;
using System.Web.Http.ModelBinding;
using Owin;

namespace Epoxy.Tests.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();

            // Services.
            config.Services.Insert(typeof(ModelBinderProvider), 0, new EpoxyFromUriAndFromBodyBinderProvider());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }
    }
}