
using System;
using System.IO;
using System.Net;
using System.Data;
using System.Text;

namespace CloudEducation_Tool.FileTool
{
    public class HtmlFileHelper
    {
        ///<summary>
        ///直接读取页面生成静态
        ///</summary>
        /// <param name="Url">需要生成静态的页面</param>
        /// <param name="savePath">保存静态页面的地址</param>
        /// 生成成功返回true(字符串)，失败返回错误信息
        /// <returns>string</returns>
        public static string CreateHtmlFileFromUrl(string Url, string savePath)
        {
            string WebUrl = Url;
            string msg = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream stream = null;
            StreamReader reader = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(WebUrl);
                request.Method = "post";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = 0;
                response = (HttpWebResponse)request.GetResponse();
                stream = response.GetResponseStream();
                reader = new StreamReader(stream);
                string Result = reader.ReadToEnd();
                reader.Close();
                response.Close();
                StreamWriter fileWriter = new StreamWriter(savePath, false, Encoding.UTF8);
                fileWriter.Write(Result);
                fileWriter.Close();
                msg = "true";
            }catch(Exception ex)
            {
                msg = ex.Message;
                throw ex;
            }
            return msg;
        }


        ///<summary>
        ///直接读取模版生成静态
        ///</summary>
        /// <param name="templatePath">需要生成静态页面模版目录</param>
        /// <param name="savePath">保存静态页面的地址</param>
        /// 生成成功返回true(字符串)，失败返回错误信息
        /// <returns>string</returns>
        public static string CreateHtmlFileFromTemplate(string templatePath, string savePath)
        {
            string msg = null;
            if (!File.Exists(templatePath)) //判断模板文件是否存在
            {
                return "生成静态页面的模版不存在";
            }

            try
            {
                StreamReader Reader = new StreamReader(templatePath, Encoding.UTF8);  //实例化读取模板文件对象
                string TempLateValue = Reader.ReadToEnd();//读取模板文件内容，从头读到尾，放入一个变量
                Reader.Close(); //关闭对象
                DataTable dt = new DataTable();   //从数据库中读取指定的新闻列表
                foreach (DataRow row in dt.Rows)                //一个一个开始循环并取出内容
                {
                    StringBuilder Result = new StringBuilder(TempLateValue);          //建立字符串对象
                    Result = Result.Replace("{@Title}", row["Title"].ToString().Trim());     //替换标题标签
                    Result = Result.Replace("{@Content}", row["Contents"].ToString().Trim());//替换内容标签
                    string NewsId = row["ID"].ToString().Trim();                             //ID作为识别
                    StreamWriter WriteFile = new StreamWriter(savePath, false, Encoding.UTF8);     //写入
                    WriteFile.Write(Result.ToString().Trim());
                    WriteFile.Close();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                throw ex;
            }
            return msg;
        }

        /// <summary>
        /// 根据文件路径读取内容
        /// 日期：2015年3月13日 11:57:14
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>string</returns>
        public static string LoadFile(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            if (!fi.Exists)
            {
                return null;
            }
            string fileContent = "";
            try
            {
                fileContent = File.ReadAllText(filePath, Encoding.UTF8);
                if (string.IsNullOrEmpty(fileContent))
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                fileContent = "";
                throw ex;
            }
            return fileContent;
        }

        /// <summary>
        /// 写入文件
        /// 日期：2015年3月13日 12:07:40
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileContent"></param>
        /// <returns>bool</returns>
        public static bool WriteFile(string filePath,string fileName, string fileContent)
        {
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                File.WriteAllText(filePath + fileName, fileContent, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

    }
}
