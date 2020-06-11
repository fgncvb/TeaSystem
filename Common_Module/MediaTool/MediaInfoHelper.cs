
using System;
using System.IO;

namespace Common_Module.MediaTool
{
    public class MediaInfoHelper
    {
        /// <summary>
        /// 获取视频信息
        /// 
        /// 日期：2017年6月27日10:16:18
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns>MediaFileInfo</returns>
        public MediaFileInfo GetVideoInfo(string filepath)
        {
            MediaFileInfo mfi = new MediaFileInfo();

            MediaInfo MI = new MediaInfo();
            MI.Open(filepath);

            FileInfo fi = new FileInfo(filepath);
            if(fi.Exists)
            {
                mfi.FilePath = filepath;
                mfi.FileName = fi.Name;
                mfi.FileSize = GetSize(fi.Length);
            }else
            {
                mfi.FilePath = "";
                mfi.FileName = "";
                mfi.FileSize = "";
            }

            string colorspace = MI.Get(StreamKind.Video, 0, "ColorSpace");

            string video = MI.Get(StreamKind.Video, 0, "Format");
            mfi.VideoFormat = video.ToLower().Trim();

            string framecout = MI.Get(StreamKind.Video, 0, "FrameCount");
            mfi.FrameCount = Convert.ToDouble(framecout);

            string audio = MI.Get(StreamKind.Audio, 0, "Format");
            mfi.AudioFormat = audio.ToLower().Trim();

            string audiobmode = MI.Get(StreamKind.General, 0, "OverallBitRate_Mode");
            string genral = MI.Get(StreamKind.General, 0, "Video_Format_List");

            string width = MI.Get(StreamKind.Video, 0, "Width");//视频width6    
            string height = MI.Get(StreamKind.Video, 0, "Height");

            mfi.Width = Convert.ToInt32(width);
            mfi.Height = Convert.ToInt32(height);

            //视频码率
            string videobitrate = MI.Get(StreamKind.Video, 0, "BitRate");
            if (string.IsNullOrEmpty(videobitrate))
            {
                videobitrate = "0";
            }
            mfi.VideoBitRate = Convert.ToInt32(videobitrate) / 1000;

            string duration = MI.Get(StreamKind.Video, 0, "Duration");
            TimeSpan t = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(duration));

            mfi.Duration = t;

            //采 样 数
            string asamrate = MI.Get(StreamKind.Audio, 0, "SamplingRate");
            if (string.IsNullOrEmpty(asamrate))
            {
                asamrate = "0";
            }
            mfi.AsamRate = Convert.ToInt32(asamrate);

            //音频码率
            string audiobitrate = MI.Get(StreamKind.Audio, 0, "BitRate");
            if(string.IsNullOrEmpty(audiobitrate))
            {
                audiobitrate = "0";
            }
            mfi.AudioBitRate = Convert.ToInt32(audiobitrate) / 1000;

            string chanel = MI.Get(StreamKind.Audio, 0, "Channel(s)");
            mfi.Channel = chanel;

            MI.Close();

            return mfi;
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public string GetSize(long b)
        {
            if (b.ToString().Length <= 10)
                return GetMB(b);
            if (b.ToString().Length >= 11 && b.ToString().Length <= 12)
                return GetGB(b);
            if (b.ToString().Length >= 13)
                return GetTB(b);
            return String.Empty;
        }

        /// <summary>
        /// 将B转换为TB
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private string GetTB(long b)
        {
            for (int i = 0; i < 4; i++)
            {
                b /= 1024;
            }
            return b + "TB";
        }

        /// <summary>
        /// 将B转换为GB
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private string GetGB(long b)
        {
            for (int i = 0; i < 3; i++)
            {
                b /= 1024;
            }
            return b + "GB";
        }

        /// <summary>
        /// 将B转换为MB
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private string GetMB(long b)
        {
            for (int i = 0; i < 2; i++)
            {
                b /= 1024;
            }
            return b + "MB";
        }
    }
}
