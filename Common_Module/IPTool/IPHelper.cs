
using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace Common_Module.IPTool
{
    public class IPHelper
    {
        public static IPLocation GetIPLocation(string ipdatafilepath, string ip_str)
        {
            QQWry qq = new QQWry(ipdatafilepath);
            IPLocation location = new IPLocation();
            try
            {
                location = qq.SearchIPLocation(ip_str);
            }
            catch (Exception ex)
            {
                
                location.country = "未知";
                location.area = "未知";
                throw ex;
            }
            return location;
        }

        public static string GetIPLocation(string ip)
        {
            string WebUrl = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=json&ip=" + ip;
            string msg = "0-0-0";
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream stream = null;
            StreamReader reader = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(WebUrl);
                request.Method = "get";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = 0;
                response = (HttpWebResponse)request.GetResponse();
                stream = response.GetResponseStream();
                reader = new StreamReader(stream);
                string jsonText = reader.ReadToEnd();
                reader.Close();
                response.Close();

                JavaScriptSerializer js = new JavaScriptSerializer();   //实例化一个能够序列化数据的类
                ToMyJson list = js.Deserialize<ToMyJson>(jsonText);    //将json数据转化为对象类型并赋值给list

                string country = list.country;
                string province = list.province;
                string city = list.city;

                msg = country + "-" + province + "-" + city;
            }
            catch (Exception ex)
            {
                msg = "0-0-0";
                throw ex;
            }
            return msg;
        }
    }
    public struct ToMyJson
    {
        public string ret { get; set; }  //属性的名字，必须与json格式字符串中的"key"值一样。
        public string start { get; set; }
        public string end { get; set; }

        public string country { get; set; }

        public string province { get; set; }

        public string city { get; set; }

        public string district { get; set; }

        public string isp { get; set; }

        public string type { get; set; }

        public string desc { get; set; }
    }
}
