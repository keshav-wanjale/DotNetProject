using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiExample.WebApp.Areas.HelpPage
{
    public class HelpPageAreaRegistration
    {
        public string AreaName => "HelpPage";

        public void RegisterArea(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "HelpPage_Default",
                pattern: "Help/{action}/{apiId?}",
                defaults: new { controller = "Help", action = "Index" });
        }
    }
}