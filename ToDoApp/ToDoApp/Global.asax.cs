using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ToDo.Web.App_Start;

namespace ToDo.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void Application_Error(object sender, EventArgs e)
        {
            var exc = Server.GetLastError();
            Server.ClearError();
            Response.Clear();
            Response.Write("<h2>Error</h2>\n");
        }
    }
}