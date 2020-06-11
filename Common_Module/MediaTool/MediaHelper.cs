
using System;
using System.Text.RegularExpressions;

namespace Common_Module.MediaTool
{
    public class MediaHelper:BaseCommon
    {
        /// <summary>
        /// 从源视频文件中提取一段视频
        /// 
        /// 日期：2017年5月25日10:10:23
        /// <param name="sourcefile"></param>
        /// <param name="starttimepoint"></param>
        /// <param name="endtimepoint"></param>
        /// <param name="newvideofilepath"></param>
        /// <returns>MediaOperationStatus</returns>
        public MediaOperationStatus ExtractVideo(string sourcefile,string starttimepoint, string endtimepoint, string newvideofilepath)
        {
            //组合截取视频所需参数
            string arguments = string.Format(FFmpegArguments.ExtractVideoArguments, sourcefile, starttimepoint, endtimepoint, newvideofilepath);

            //启动转换线程(后台执行，自动结束，执行完成后自动退出线程)
            string apppath = GetFFmpegPath();
            StartProcess(arguments, apppath);

            MediaOperationStatus mos = new MediaOperationStatus();
            mos.FFmpegUrl = newvideofilepath;
            
            return mos;
        }

        /// <summary>
        /// 从源视频文件中指定时间点处的帧保存为图片（用于生成视频封面图片）
        /// 如果不设定图片尺寸，默认尺寸为320x240
        /// 
        /// 日期：2017年5月25日14:22:09
        /// </summary>
        /// <param name="sourcefile"></param>
        /// <param name="starttimepoint"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="picturepath"></param>
        /// <returns>MediaOperationStatus</returns>
        public MediaOperationStatus ExtractImageFormVideo(string sourcefile, string starttimepoint, int width, int height, string picturepath)
        {
            
            string picture_size = "320x240"; //默认图片尺寸
            if(width > 0 && height > 0)
            {
                picture_size = string.Format("{0}x{1}", width, height); //设置图片尺寸
            }

            //组合截取图片所需参数
            string arguments = string.Format(FFmpegArguments.ExtractImageArguments, sourcefile, starttimepoint, picture_size, picturepath);

            //启动转换线程(后台执行，自动结束，执行完成后自动退出线程)
            string apppath = GetFFmpegPath();
            StartProcess(arguments, apppath);

            MediaOperationStatus mos = new MediaOperationStatus();
            mos.FFmpegUrl = picturepath;

            return mos;
        }

        /// <summary>
        /// 转换视频（默认参数，不带任何音视频设定参数）
        /// 
        /// 日期：2017年5月25日15:16:17
        /// </summary>
        /// <param name="sourcefile"></param>
        /// <param name="outputfilepath"></param>
        /// <param name="bitrate">码率</param>
        /// <param name="width">视频宽度</param>
        /// <param name="height">视频高度</param>
        /// <returns>MediaOperationStatus</returns>
        public MediaOperationStatus ConvertVideoToMP4(string sourcefile, string outputfilepath, int bitrate, int width, int height)
        {

            //组合转换视频所需参数
            string base_arguments = FFmpegArguments.ConvertVideoBaseArguments;
            string apppath = GetFFmpegPath();

            // 当前FFmpeg版本支持常见媒体格式，统一转换命令参数
            //FileInfo fi = new FileInfo(sourcefile);
            //if(fi.Extension == ".wmv")
            //{
            //    base_arguments = FFmpegArguments.ConvertWMVArguments;

            //}else if(fi.Extension == ".rmvb") // 如果是RMVB格式，则调用Mencoder.exe程序 
            //{
            //    base_arguments = FFmpegArguments.ConvertRMVBArguments;
            //    apppath = GetMencoderPath();
            //}

            //如果码率小于0 ，则采用默认码率参数
            string malv = "";
            if(bitrate > 0)
            {
                malv = string.Format("-b {0}k", bitrate);
            }

            //如果视频宽度或高度任何一个参数小于0 ，则采用默认视频尺寸
            string size = "";
            if(width > 0 && height > 0)
            {
                size = string.Format("-s {0}x{1}", width, height);
            }

            string arguments = string.Format(base_arguments, sourcefile, outputfilepath, malv, size);

            //启动转换线程(后台执行，自动结束，执行完成后自动退出线程)
            
            StartProcess(arguments, apppath);

            MediaOperationStatus mos = new MediaOperationStatus();
            mos.FFmpegUrl = outputfilepath;

            return mos;
        }


        /// <summary>
        /// 从视频源文件提取GIF图片
        /// 
        /// 日期：2017年5月26日14:25:13
        /// -pix_fmt rgb24 输出RGB24位颜色gif图片 
        /// 说明：通过ffmpeg生成的GIF图片颜色失真，
        /// 使用ImageMagick软件（需要下载安装）进行将截成的jpeg图片转换为gif图片，同样是命令行模式的：每0.1秒一帧，循环（loop）5次
        /// convert - delay 100 c:\*.jpeg - loop 5 c:\XXX.gif
        /// </summary>
        /// <param name="starttimepoint"></param>
        /// <param name="starttimepoint"></param>
        /// <param name="filesource"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="framerate"></param>
        /// <param name="temppath"></param>
        /// <param name="gifpath"></param>
        /// <returns>MediaOperationStatus</returns>
        public MediaOperationStatus ExtractGifForVideoArguments(string starttimepoint, string endtimepoint, string filesource, int width,int height, string temppath, string gifpath)
        {
            string picture_size = ""; //默认图片尺寸
            if (width > 0 && height > 0)
            {
                picture_size = string.Format(" -s {0}x{1} ", width, height); //设置图片尺寸
            }

            //组合转换视频所需参数
            string arguments = string.Format(FFmpegArguments.ExtractImageListArguments, starttimepoint, filesource, endtimepoint, picture_size, temppath);

            //启动转换线程(后台执行，自动结束，执行完成后自动退出线程)
            string apppath = GetFFmpegPath();
            //StartProcess(arguments, apppath);


            //Thread.Sleep(6000);

            //DateTime dt1 = DateTime.Now;
            //DateTime dt2 = DateTime.Now.AddMinutes(3);
            //bool isrun = true;

            //while (isrun)
            //{

            //}

            arguments = string.Format(FFmpegArguments.CompressionImgToGifArguments, temppath, 0, gifpath);
            apppath = GetImageMagickStartPath();
            StartProcess(arguments, apppath);

            MediaOperationStatus mos = new MediaOperationStatus();
            mos.FFmpegUrl = gifpath;

            return mos;
        }

        /// <summary>
        /// 重设MP4文件头部信息
        /// 
        /// 日期：2017-6-23 10:54:18
        /// <param name="sourcefile"></param>
        /// <param name="newvideofilepath"></param>
        /// <returns>MediaOperationStatus</returns>
        public MediaOperationStatus SetDataMete(string sourcefile, string newvideofilepath)
        {
            //组合截取视频所需参数
            string arguments = string.Format("{0} {1}", sourcefile, newvideofilepath);

            //启动转换线程(后台执行，自动结束，执行完成后自动退出线程)
            string apppath = GetQtFastStartPath();
            StartProcess(arguments, apppath);

            MediaOperationStatus mos = new MediaOperationStatus();
            mos.FFmpegUrl = newvideofilepath;

            return mos;
        }

        /// <summary>
        /// 获取音视频文件播放时长
        /// 单位：秒
        /// 
        /// 日期：2017年5月26日17:03:11
        /// </summary>
        /// <param name="filesource"></param>
        /// <returns>string 单位：秒</returns>
        public int GetMediaDuartion(string filesource)
        {
            string result = ViewMediaInfo(filesource);

            var temp = Regex.Match(result, @"(?<=format[\s\S]+duration:).*(?=,)", RegexOptions.Multiline).Value;

            string[] infolist = temp.Split(',');
            if(infolist == null || infolist.Length < 1)
            {
                return 0;
            }

            double duartion_temp = double.Parse(infolist[0]);
            int duartion = (int)duartion_temp;

            return duartion;
        }

        /// <summary>
        /// 将mp4文件切片为m3u8列表
        /// 
        /// 日期：2017年6月30日15:05:54
        /// </summary>
        /// <param name="sourcefile"></param>
        /// <param name="hls_time"></param>
        /// <param name="m3u8name"></param>
        /// <returns>MediaOperationStatus</returns>
        public MediaOperationStatus CreateM3u8List(string sourcefile,int hls_time, string m3u8path)
        {
            //组合截取视频所需参数
            string arguments = string.Format(FFmpegArguments.M3u8ListArguments, sourcefile, hls_time, m3u8path);

            //启动转换线程(后台执行，自动结束，执行完成后自动退出线程)
            string apppath = GetFFmpegPath();
            StartProcess(arguments, apppath);

            MediaOperationStatus mos = new MediaOperationStatus();
            mos.FFmpegUrl = "";

            return mos;
        }

        /// <summary>
        /// 将rtmp流媒体保存为m3u8列表
        /// 
        /// 日期：2017年6月30日15:05:54
        /// </summary>
        /// <param name="sourcefile"></param>
        /// <param name="hls_time"></param>
        /// <param name="m3u8name"></param>
        /// <returns>MediaOperationStatus</returns>
        public MediaOperationStatus ConvertRtmpToM3u8(string rtmpurl, int hls_time, string m3u8path)
        {
            //组合截取视频所需参数
            string arguments = string.Format(FFmpegArguments.RtmpToM3u8Arguments, rtmpurl, hls_time, m3u8path);

            //启动转换线程(后台执行，自动结束，执行完成后自动退出线程)
            string apppath = GetFFmpegPath();
            StartProcess(arguments, apppath);

            MediaOperationStatus mos = new MediaOperationStatus();
            mos.FFmpegUrl = "";

            return mos;
        }

        /// <summary>
        /// 启动流媒体程序(用于直播)
        /// 日期：2017年7月21日10:32:02
        /// </summary>
        /// <returns></returns>
        public MediaOperationStatus StartLiveApp()
        {
            string arguments = ""; // string.Format(FFmpegArguments.RtmpToM3u8Argument);

            //启动转换线程(后台执行，自动结束，执行完成后自动退出线程)
            string apppath = GetLiveAppPath();
            StartProcess(arguments, apppath);

            MediaOperationStatus mos = new MediaOperationStatus();
            mos.FFmpegUrl = "";

            return mos;
        }

        /// <summary>
        /// 查看媒体文件信息
        /// 文件路径如果有中文将会出现乱码，尽量避开中文路径
        /// 
        /// 日期：2017年5月26日14:35:01
        /// </summary>
        /// <param name="filesource"></param>
        /// <returns>json格式字符串</returns>
        private string ViewMediaInfo(string filesource)
        {
            //组合转换视频所需参数
            string arguments = string.Format(FFmpegArguments.ViewMediaInfoArguments, filesource);

            //启动转换线程(后台执行，自动结束，执行完成后自动退出线程)
            string apppath = GetFFprobePath();
            string result = StartProcess(arguments, apppath);

            //Regex regexObj = new Regex(@"\r\n", RegexOptions.Multiline);

            try
            {
                result = Regex.Replace(result, @"\r\n", "", RegexOptions.Multiline);
                result = Regex.Replace(result, "\"", "", RegexOptions.Multiline);
                result = result.Replace("\\\\", @"\").Replace(" ","");
            }
            catch (Exception ex)
            {
                result = "";
                throw ex;
            }


            return result;
        }
    }
}
