using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common_Module.MediaTool
{
    public class MediaOperationStatus
    {
        /// <summary>
        /// 视频名称
        /// </summary>
        public string FFmpegName { get; set; }

        /// <summary>
        /// 视频封面图片
        /// </summary>
        public string FFmpegImage { get; set; }

        /// <summary>
        /// 视频路径
        /// </summary>
        public string FFmpegUrl { get; set;}

        /// <summary>
        /// 视频创建时间
        /// </summary>
        public DateTime FFmpegCreateDate { get; set; }




    }
}
