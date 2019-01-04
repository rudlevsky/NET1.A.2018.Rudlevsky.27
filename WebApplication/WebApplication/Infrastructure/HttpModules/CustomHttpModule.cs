using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Infrastructure.HttpHandlers;

namespace WebApplication.Infrastructure.HttpModules
{
    public class CustomHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PostResolveRequestCache += new EventHandler(app_AcquireRequestState);
        }

        public void app_AcquireRequestState(object o, EventArgs ea)
        {
            HttpApplication httpApp = (HttpApplication)o;
            
            object controller = httpApp.Context.Request.RequestContext.RouteData.Values["controller"];
            object id = httpApp.Context.Request.RequestContext.RouteData.Values["id"];

            if ((string.Equals(controller.ToString(), "Image", StringComparison.OrdinalIgnoreCase) && id != null))
            {
                httpApp.Context.RemapHandler(new CustomHttpHandler());
            }      
        }

        public void Dispose()
        {
        }
    }
}