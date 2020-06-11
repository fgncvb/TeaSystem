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
        /// ���ʴ�����־Ȩ�޴���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //HttpApplication app = (HttpApplication)sender;
            ////��̬�ӽ���
            //// DynamicEncryptionHelper deh = null;

            //// ������־Ȩ������
            //string rawurl = Request.Url.ToString().ToLower(); // ��ȡԭʼ����·��

            //if (rawurl.Contains("logs.html")) // ��������·���б���logs.html�ַ�
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
            //        clientIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); // ��ȡI�ͻ�����ʵP��ַ
            //    }
            //    else
            //    {
            //        clientIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(); // ��ȡ�ͻ�����ʵP��ַ
            //    }

            //    string accesselmahloglocation = ConfigurationManager.AppSettings["AccessElmahLogLocation"]; // �������ļ�����������ʴ�����־�ļ���IP��ַ�б�

            //    if (string.IsNullOrEmpty(accesselmahloglocation)) // ��������ļ�δ��ȡ������������ʴ�����־�ļ���IP��ַ�б�
            //    {
            //        Response.Clear();
            //        Response.Redirect("~/404.html");
            //    }
            //    string key = ConfigTool.DynamicEncryptionKey;

            //    if (string.IsNullOrEmpty(key)) // �����̬����Key������
            //    {
            //        Response.Clear();
            //        Response.Redirect("~/404.html");
            //    }

            //    //deh = new DynamicEncryptionHelper(key); //ȡ����̬����

            //    string ipordomain_list = accesselmahloglocation; // deh.DecryptString(accesselmahloglocation); // ���ܺ��������ʵ�IP��ַ�б�

            //    if (string.IsNullOrEmpty(ipordomain_list))
            //    {
            //        Response.Clear();
            //        Response.Redirect("~/404.html");
            //    }

            //    if (!ipordomain_list.Contains(clientIP)) // �����ǰIP��ַ����������ʵ��б��У�����Ȩ�޷���
            //    {
            //        Response.Clear();
            //        Response.Redirect("~/404.html");
            //    }
            //}
        }

        /// <summary>
        /// ϵͳ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();

            if (lastError != null)
            {
                string strExceptionMessage = ""; //�쳣��Ϣ

                HttpException httpError = lastError as HttpException; // ��HTTP 404�����⴦����������ȫ������500����������

                if (httpError != null)
                {
                    //int httpCode = httpError.GetHttpCode(); //��ȡ�������
                    //if (httpCode == 400 || httpCode == 404)
                    //{
                    /// �˴�ע�͵��ж�Http״̬�룬��ζ��ֻҪ��Http������ת��404����
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
