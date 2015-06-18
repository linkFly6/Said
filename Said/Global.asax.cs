﻿using Said.Common;
using Said.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Said
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //加载配置文件
            ConfigTable.LoadConfig(Server.MapPath("~/config.json"));
            /*默认加载IP查询库*/
            Said.Helper.IP.Load(Server.MapPath(ConfigTable.Table[ConfigEnum.SourceDataIP]));

        }
    }

}