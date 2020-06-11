
namespace Common_Module.MediaTool
{
    public class FFmpegArguments
    {
        /// <summary>
        /// 从源视频截取视频段FFmpeg参数
        /// {0}位置：源视频文件路径
        /// {1}位置：源视频开始截取的时间点
        /// {2}位置：源视频结束截取的时间点
        /// {3}位置：截取后的新视频文件路径
        /// 
        /// 日期:2017年5月25日10:24:46
        /// </summary>
        public static string ExtractVideoArguments
        {
            get
            {
                // ffmpeg  -i ./plutopr.mp4 -vcodec copy -acodec copy -ss 00:00:10 -to 00:00:15 ./cutout1.mp4 -y
                // -ss time_off set the start time offset 设置从视频的哪个时间点开始截取，上文从视频的第10s开始截取
                // - to 截到视频的哪个时间点结束。上文到视频的第15s结束。截出的视频共5s.
                // 如果用 - t 表示截取多长的时间如 上文-to 换位 - t则是截取从视频的第10s开始，截取15s时长的视频。即截出来的视频共15s.

                // 注意的地方是：
                // 如果将 - ss放在 - i./ plutopr.mp4后面则 - to的作用就没了，跟 - t一样的效果了，变成了截取多长视频。一定要注意 - ss的位置。

                // 参数解析
                // -vcodec copy表示使用跟原视频一样的视频编解码器。
                // -acodec copy表示使用跟原视频一样的音频编解码器。

                // -i 表示源视频文件
                // - y 表示如果输出文件已存在则覆盖。

                // {0}位置：源视频文件路径
                // {1}位置：源视频开始截取的时间点
                // {2}位置：源视频结束截取的时间点
                // {3}位置：截取后的新视频文件路径
                return " -i {0} -vcodec copy -acodec copy -ss {1} -to {2} {3} -y";
            }
        }

        /// <summary>
        /// 从视频文件中提取指定时间点处的帧保存为图片（用于生成视频封面图片）
        /// 
        /// 日期：2017年5月25日11:50:11
        /// </summary>
        public static string ExtractImageArguments
        {
            get
            {
                // {0}位置：源视频文件路径
                // {1}位置：源视频开始截取的时间点
                // {2}位置：图片尺寸大小 格式：320x240
                // {3}位置：截取后图片文件保存路径
                //
                // 说明：
                // 通过指定 -ss，和-vframes也可以达到同样的效果。
                //这时候 - ss参数后跟的时间有两种写法,hh: mm: ss 或 直接写秒数:
                // ffmpeg - i test.asf - y - f  image2 - ss 00:01:00 - vframes 1  test1.jpg
                // or
                // ffmpeg - i test.asf - y - f  image2 - ss 60 - vframes 1  test1.jpg

                return " -i {0} -y -f image2 -ss {1} -t 0.001 -s {2} {3}";
            }
        }

        /// <summary>
        /// 转换视频基本参数（不带任何音视频参数值设定）
        /// 
        /// 日期：2017年5月25日15:12:30
        /// </summary>
        public static string ConvertVideoBaseArguments
        {
            get
            {
                // {0}位置：源视频文件路径
                // {1}位置：转换后视频文件保存路径
                // {2}位置：视频码率 格式：-b 500k
                // {3}位置：视频尺寸 格式：-s 800x680 

                //return " -i {0} -acodec aac -vcodec h264 {1}";
                return " -i {0} -c:v libx264 -c:a aac {2} {3} -y {1}";
            }
        }

        /// <summary>
        /// 从视频连续截图
        /// </summary>
        public static string ExtractImageListArguments
        {
            // 位置0 开始时间点
            // 位置1 视频源文件
            // 位置2 结束时间点
            // 位置3 图片尺寸 
            // 位置4 连续截图保存的目录  
            get
            {
                return " -ss {0} -i {1} -y -f image2 -r 0.1 -t {2} {3} {4}%3d.jpg";
            }
        }

        /// <summary>
        /// 压缩图片到GIF
        /// </summary>
        public static string CompressionImgToGifArguments
        {
            get
            {
                // 位置0 图片目录 
                // 位置1 循环播放次数
                // 位置2 GIF图片输出目录
                return " -delay 0 {0}*.jpg -loop {1} {2}"; // ImageMagick方式
            }
        }

        /// <summary>
        /// 从视频指定的时间点提取帧画面生成GIF图片（用于视频动画封面）
        /// 
        /// 日期：2017年5月26日09:50:32
        /// </summary>
        public static string ExtractGifForVideoArguments
        {
            get
            {
                // 位置{0} 开始时间点
                // 位置{1} 提取时长
                // 位置{2} 视频源文件
                // 位置{3} GIF图片尺寸 如：320x240
                // 位置{4} 帧率
                // 位置{5} GIF文件保存路径
                // -pix_fmt rgb24 输出RGB24位颜色gif图片 
                // 说明：通过ffmpeg生成的GIF图片颜色失真，
                // 使用ImageMagick软件（需要下载安装）进行将截成的jpeg图片转换为gif图片，同样是命令行模式的：每0.1秒一帧，循环（loop）5次
                // convert - delay 100 c:\*.jpeg - loop 5 c:\XXX.gif
                return " -ss {0} -t {1} -i {2} -pix_fmt rgb24 {3} -f gif -r {4} -y {5}";  // ffmpeg方式
            }
        }

        /// <summary>
        /// 合成GIF文件
        /// 
        /// 日期：2017年5月26日10:14:24
        /// </summary>
        public static string SyntheticGifArguments
        {
            get
            {
                // 位置{0} 指定合成GIF图片帧率
                // 位置{1} GIF缓存源文件（jpeg格式）
                // 位置{2} 合成后GIF文件的路径

                return " -f image2 -framerate {0} -i {1} {2}";
            }
        }

        /// <summary>
        /// 查看媒体文件信息
        /// 
        /// 日期：2017年5月26日14:32:29
        /// </summary>
        public static string ViewMediaInfoArguments
        {
            get
            {
                // 位置{0} 视频源文件
                return " -v quiet -print_format json -show_format -show_streams {0}";
            }
        }

        /// <summary>
        /// WMV格式转换到MP4格式所需参数
        /// 
        /// 日期：2017年5月26日17:47:32
        /// </summary>
        public static string ConvertWMVArguments
        {
            get
            {
                return " -i {0} -c:v libx264 -c:a aac -strict -2 {1}";
            }
        }

        /// <summary>
        /// RMVB格式转换到MP4格式所需参数
        /// 
        /// 日期：2017年5月27日09:22:37
        /// </summary>
        public static string ConvertRMVBArguments
        {
            get
            {
                // 位置{0} 视频源文件
                // 位置{1} 输出新文件的路径

                return " {0} -oac mp3lame -lameopts preset=64 -ovc xvid -xvidencopts bitrate=3600 -o {1}";
            }
        }

        /// <summary>
        /// mp4文件切片成m3u8列表文件
        /// 
        /// 日期：2017年6月30日14:59:20
        /// </summary>
        public static string M3u8ListArguments
        {
            //位置0 视频源文件
            //位置1 每个片段视频长度 单位：秒
            //位置2 m3u8文件名
            get
            {
                return " -i {0} -c:v libx264 -c:a aac -strict -{1} -f hls {2}";
            }
        }

        /// <summary>
        /// 将Rtmp流切片为m3u8
        /// 
        /// 日期：2017年6月30日15:37:46
        /// </summary>
        public static string RtmpToM3u8Arguments
        {
            //位置0 rtmp流地址
            //位置1 每个片段时长 单位：秒
            //位置2 m3u8文件目录
            get
            {
                return " -v verbose -i {0} -c:v libx264 -c:a aac -ac 1 -strict -2 -crf 20 -profile:v main -maxrate 800k -bufsize 1835k -pix_fmt yuv420p -flags -global_header -hls_time {1} -start_number 1 -f segment -segment_list {2} -segment_list_flags +live -segment_time 10 out%03d.ts";
            }
        }

    }
}
