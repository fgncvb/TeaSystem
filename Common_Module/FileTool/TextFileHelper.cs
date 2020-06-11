
using System;
using System.IO;
using System.Text;

namespace CloudEducation_Tool.FileTool
{
    public class TextFileHelper
    {
        /// <summary>
        /// 将信息写进文本文件
        /// 文本文件创建在程序所在目录
        /// 于挺
        /// 2013年8月18日 22:54:51
        /// </summary>
        /// <param name="msg"></param>
        /// <returns>void</returns>
        public static void WriteMsgToFile(String msg)
        {
            string fileName = string.Format("{0}_Log.txt", DateTime.Now.ToString("yyyy-MM-dd"));

            try
            {
                string dir = AppDomain.CurrentDomain.BaseDirectory + "Log\\";

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                string filePath = dir + fileName;

                bool exists = new FileInfo(filePath).Exists;

                var fs = new FileStream(filePath, exists ? FileMode.Append : FileMode.Create);

                //获得字节数组
                byte[] data = new UTF8Encoding().GetBytes(msg);
                //开始写入
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将信息写进文本文件
        /// 文本文件创建在程序所在目录
        /// 于挺
        /// 2013年8月18日 22:54:51
        /// </summary>
        /// <param name="msg"></param>
        /// <returns>void</returns>
        public static void WriteMsgToFile(string path, String msg)
        {
            try
            {
                bool exists = new FileInfo(path).Exists;

                var fs = new FileStream(path, exists ? FileMode.Append : FileMode.Create);

                //获得字节数组
                byte[] data = new UTF8Encoding().GetBytes(msg);
                //开始写入
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 读取文件到字符串
        /// 日期：2014年12月13日16:07:12
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>string</returns>
        public static string ReadFileToString(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
