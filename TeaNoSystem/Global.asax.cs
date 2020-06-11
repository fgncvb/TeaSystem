using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TeaNoSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static Regex Regex_logUrl = new Regex(@"https?://.*?(?<!a)/logs\.html(/.*)?");

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// 访问错误日志权限处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //HttpApplication app = (HttpApplication)sender;
            ////动态加解密
            //// DynamicEncryptionHelper deh = null;

            //// 处理日志权限问题
            //string rawurl = Request.Url.ToString().ToLower(); // 获取原始请求路径

            //if (rawurl.Contains("logs.html")) // 如果请求的路径中保护logs.html字符
            //{
            //    Match match2 = Regex_logUrl.Match(rawurl);
            //    if (!match2.Success)
            //    {
            //        Response.Clear();
            //        Response.Redirect("~/404.html");
            //    }

            //    string clientIP = "";

            //    if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            //    {
            //        clientIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); // 提取I客户端真实P地址
            //    }
            //    else
            //    {
            //        clientIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(); // 提取客户端真实P地址
            //    }

            //    string accesselmahloglocation = ConfigurationManager.AppSettings["AccessElmahLogLocation"]; // 从配置文件加载允许访问错误日志文件的IP地址列表

            //    if (string.IsNullOrEmpty(accesselmahloglocation)) // 如果配置文件未读取到允许允许访问错误日志文件的IP地址列表
            //    {
            //        Response.Clear();
            //        Response.Redirect("~/404.html");
            //    }
            //    string key = ConfigTool.DynamicEncryptionKey;

            //    if (string.IsNullOrEmpty(key)) // 如果动态加密Key不存在
            //    {
            //        Response.Clear();
            //        Response.Redirect("~/404.html");
            //    }

            //    //deh = new DynamicEncryptionHelper(key); //取消动态解密

            //    string ipordomain_list = accesselmahloglocation; // deh.DecryptString(accesselmahloglocation); // 解密后的允许访问的IP地址列表

            //    if (string.IsNullOrEmpty(ipordomain_list))
            //    {
            //        Response.Clear();
            //        Response.Redirect("~/404.html");
            //    }

            //    if (!ipordomain_list.Contains(clientIP)) // 如果当前IP地址不在允许访问的列表中，就无权限访问
            //    {
            //        Response.Clear();
            //        Response.Redirect("~/404.html");
            //    }
            //}
        }

        /// <summary>
        /// 系统错误处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();

            if (lastError != null)
            {
                string strExceptionMessage = ""; //异常信息

                HttpException httpError = lastError as HttpException; // 对HTTP 404做额外处理，其他错误全部当成500服务器错误

                if (httpError != null)
                {
                    //int httpCode = httpError.GetHttpCode(); //获取错误代码
                    //if (httpCode == 400 || httpCode == 404)
                    //{
                    /// 此处注释掉判断Http状态码，意味着只要是Http错误都跳转到404错误
                    Server.ClearError();
                    strExceptionMessage = httpError.Message;
                    Response.Redirect("~/404.html");
                    //}
                }
                else
                {
                    Server.ClearError();
                    strExceptionMessage = lastError.Message;
                    Response.Redirect("~/500.html");
                }
            }
        }
    }
}
