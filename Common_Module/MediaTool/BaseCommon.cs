
using System;
using System.Diagnostics;
using System.IO;

namespace Common_Module.MediaTool
{
    public class BaseCommon
    {
        /// <summary>
        /// 获取流媒体程序路径
        /// 
        /// 日期：2017年7月21日10:25:49
        /// </summary>
        /// <returns>string</returns>
        protected string GetLiveAppPath()
        {
            int systemwei = Environment.Is64BitOperatingSystem == true ? 64 : 32;//获取系统位数

            string basedirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = basedirectory + string.Format("LiveApp\\nginx_live.exe", systemwei); //获取程序的根目录

            if (File.Exists(path))
            {
                return path;
            }

            return path;
        }

        /// <summary>
        /// 获取FFmpeg程序路径
        /// 
        /// 日期：2017年5月25日11:25:33
        /// </summary>
        /// <returns>string</returns>
        protected string GetFFmpegPath()
        {
            int systemwei = Environment.Is64BitOperatingSystem == true ? 64 : 32;//获取系统位数

            string basedirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = basedirectory + string.Format("ConvertTool\\ffmpeg{0}\\ffmpeg.exe", systemwei); //获取程序的根目录

            if (File.Exists(path))
            {
                return path;
            }

            return path;
        }

        /// <summary>
        /// 获取FFprobe程序路径
        /// 
        /// 日期：2017年5月26日14:19:04
        /// </summary>
        /// <returns>string</returns>
        protected string GetFFprobePath()
        {
            int systemwei = Environment.Is64BitOperatingSystem == true ? 64 : 32;//获取系统位数

            string basedirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = basedirectory + string.Format("ConvertTool\\ffmpeg{0}\\ffprobe.exe", systemwei); //获取程序的根目录

            if (File.Exists(path))
            {
                return path;
            }

            return path;
        }

        /// <summary>
        /// 获取Mencoder程序路径
        /// 
        /// 日期：2017年5月27日09:42:36
        /// </summary>
        /// <returns>string</returns>
        protected string GetMencoderPath()
        {
            string basedirectory = System.Environment.CurrentDirectory;
            string path = basedirectory + "ConvertTool\\mplayer\\mencoder.exe"; //获取程序的根目录

            if (File.Exists(path))
            {
                return path;
            }

            return path;
        }

        /// <summary>
        /// qtfaststart
        /// 
        /// 日期：2017年6月23日10:49:21
        /// </summary>
        /// <returns>string</returns>
        protected string GetQtFastStartPath()
        {
            string basedirectory = System.Environment.CurrentDirectory;
            string path = basedirectory + "ConvertTool\\qtfaststart\\qtfaststart.exe"; //获取程序的根目录

            if (File.Exists(path))
            {
                return path;
            }

            return path;
        }

        /// <summary>
        /// ImageMagick
        /// 
        /// 日期：2017-7-5 14:43:51
        /// </summary>
        /// <returns>string</returns>
        protected string GetImageMagickStartPath()
        {
            string basedirectory = System.Environment.CurrentDirectory;
            string path = basedirectory + "ImageMagick\\convert_q16.exe"; //获取程序的根目录

            if (File.Exists(path))
            {
                return path;
            }

            return path;
        }

        /// <summary>
        /// 
        /// 启动媒体工具程序
        /// 日期：2017年5月25日09:47:33
        /// </summary>
        /// <returns>ProcessStartInfo</returns>
        //private ProcessStartInfo CreateProcessStartInfo(string apppath)
        //{
        //    ProcessStartInfo psi = new ProcessStartInfo();

        //    psi.FileName = apppath;
        //    psi.UseShellExecute = false; //不使用操作系统外壳程序启动线程(一定为FALSE, 详细的请看MSDN)

        //    // 把外部程序错误输出写到StandardError流中
        //    //注意,FFMPEG的所有输出信息,都为错误输出流,用StandardOutput是捕获不到任何消息的  mencoder就是用standardOutput来捕获的
        //    psi.RedirectStandardError = true;
        //    psi.RedirectStandardOutput = true;
        //    psi.RedirectStandardInput = true;

        //    psi.CreateNoWindow = true; //不创建进程窗口

        //    return psi;

        //}

        /// <summary>
        /// 启动转换线程
        /// 
        /// 日期：2017年5月25日11:23:52
        /// </summary>
        /// <param name="arguments"></param>
        protected string StartProcess(string arguments, string apppath)
        {
            ProcessStartInfo psi = new ProcessStartInfo();

            psi.FileName = apppath;
            psi.UseShellExecute = false; //不使用操作系统外壳程序启动线程(一定为FALSE, 详细的请看MSDN)

            // 把外部程序错误输出写到StandardError流中
            //注意,FFMPEG的所有输出信息,都为错误输出流,用StandardOutput是捕获不到任何消息的  mencoder就是用standardOutput来捕获的
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;

            psi.CreateNoWindow = true; //不创建进程窗口

            psi.Arguments = arguments; //参数(这里是FFMPEG的参数)

            Process proc = new Process();
            proc.StartInfo = psi;   // ProcessStartInfo对象不能再外部创建，只能在这里创建。否则程序将不能正常执行。
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;

            proc.Start();

            //StreamReader sr = proc.StandardOutput;

            //string result = sr.ReadToEnd();

            //sr.Close();
            //sr.Dispose();

            //关闭处理程序
            //proc.Close();

            return "";
        }
    }
}
