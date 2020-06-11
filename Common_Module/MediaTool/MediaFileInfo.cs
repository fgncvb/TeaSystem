
using System;

namespace Common_Module.MediaTool
{
    public class MediaFileInfo
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSize { get; set; }

        /// <summary>
        /// 文件类型 图片1  音频2   视频3
        /// </summary>
        public int FileType { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 视频编码格式
        /// </summary>
        public string VideoFormat { get; set; }

        /// <summary>
        /// 视频码率
        /// </summary>
        public int VideoBitRate { get; set; }

        /// <summary>
        /// 音频编码格式
        /// </summary>
        public string AudioFormat { get; set; }

        /// <summary>
        /// 音频码率
        /// </summary>
        public int AudioBitRate { get; set; }

        /// <summary>
        /// 音频采样率
        /// </summary>
        public int AsamRate { get; set; }

        //声道数
        public string Channel { get; set; }

        //桢数
        public double FrameCount { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// 色彩
        /// </summary>
        public string ColorSpace { get; set; }


        #region 视频参数
        ///参数:Count  值:201
        //参数:Status 值
        //参数:StreamCount 值:1
        //参数:StreamKind 值:Video
        //参数:StreamKind/String 值:Video
        //参数:StreamKindID 值:0
        //参数:StreamKindPos 值:
        //参数:Inform 值:ID                                       : 1
        //Format                                   : MPEG-4 Visual
        //Format profile                           : Simple @L4a
        //Format settings, BVOP                    : No
        //Format settings, QPel                    : No
        //Format settings, GMC                     : No warppoints
        //Format settings, Matrix                  : Default(H.263)
        //Codec ID                                 : 20
        //Duration                                 : 52s 241ms
        //Bit rate mode                            : Variable
        //Bit rate                                 : 2 956 Kbps
        //Nominal bit rate                         : 2 700 Kbps
        //Maximum bit rate                         : 3 000 Kbps
        //Width                                    : 640 pixels
        //Height                                   : 480 pixels
        //Display aspect ratio                     : 4:3
        //Frame rate mode                          : Variable
        //Frame rate                               : 28.426 fps
        //Minimum frame rate                       : 14.925 fps
        //Maximum frame rate                       : 61.224 fps
        //Color space                              : YUV
        //Chroma subsampling                       : 4:2:0
        //Bit depth                                : 8 bits
        //Scan type                                : Progressive
        //Compression mode                         : Lossy
        //Bits/(Pixel* Frame)                       : 0.339
        //Stream size                              : 18.4 MiB(97%)
        //Encoded date                             : UTC 2011-09-02 02:58:59
        //Tagged date                              : UTC 2011-09-02 02:58:59

        //参数:ID 值:1
        //参数:ID/String 值:1
        //参数:UniqueID 值:
        //参数:UniqueID/String 值:
        //参数:MenuID 值:
        //参数:MenuID/String 值:
        //参数:Format 值:MPEG-4 Visual
        //参数:Format/Info 值:
        //参数:Format/Url 值:
        //参数:Format_Commercial 值:MPEG-4 Visual
        //参数:Format_Commercial_IfAny 值:
        //参数:Format_Version 值:
        //参数:Format_Profile 值:Simple @L4a
        //参数:Format_Compression 值:
        //参数:MultiView_BaseProfile 值:
        //参数:MultiView_Count 值:
        //参数:MultiView_Layout 值:
        //参数:Format_Settings 值:
        //参数:Format_Settings_BVOP 值:No
        //参数:Format_Settings_BVOP/String 值:No
        //参数:Format_Settings_QPel 值:No
        //参数:Format_Settings_QPel/String 值:No
        //参数:Format_Settings_GMC 值:0
        //参数:Format_Settings_GMC/String 值:No warppoints
        //参数:Format_Settings_Matrix 值:Default(H.263)
        //参数:Format_Settings_Matrix/String 值:Default(H.263)
        //参数:Format_Settings_Matrix_Data 值:
        //参数:Format_Settings_CABAC 值:
        //参数:Format_Settings_CABAC/String 值:
        //参数:Format_Settings_RefFrames 值:
        //参数:Format_Settings_RefFrames/String 值:
        //参数:Format_Settings_Pulldown 值:
        //参数:Format_Settings_FrameMode 值:
        //参数:Format_Settings_GOP 值:
        //参数:Format_Settings_FrameStructures 值:
        //参数:Format_Settings_Wrapping 值:
        //参数:InternetMediaType 值:video/MP4V-ES
        //参数:MuxingMode 值:
        //参数:CodecID 值:20
        //参数:CodecID/String 值:
        //参数:CodecID/Info 值:
        //参数:CodecID/Hint 值:
        //参数:CodecID/Url 值:
        //参数:CodecID_Description 值:
        //参数:Codec 值:MPEG-4V
        //参数:Codec/String 值:MPEG-4 Visual
        //参数:Codec/Family 值:MPEG-4V
        //参数:Codec/Info 值:
        //参数:Codec/Url 值:
        //参数:Codec/CC 值:20
        //参数:Codec_Profile 值:Simple @L4a
        //参数:Codec_Description 值:
        //参数:Codec_Settings 值:
        //参数:Codec_Settings_PacketBitStream 值:No
        //参数:Codec_Settings_BVOP 值:No
        //参数:Codec_Settings_QPel 值:No
        //参数:Codec_Settings_GMC 值:0
        //参数:Codec_Settings_GMC/String 值:No warppoints
        //参数:Codec_Settings_Matrix 值:Default(H.263)
        //参数:Codec_Settings_Matrix_Data 值:
        //参数:Codec_Settings_CABAC 值:
        //参数:Codec_Settings_RefFrames 值:
        //参数:Duration 值:52241
        //参数:Duration/String 值:52s 241ms
        //参数:Duration/String1 值:52s 241ms
        //参数:Duration/String2 值:52s 241ms
        //参数:Duration/String3 值:00:00:52.241
        //参数:Duration/String4 值:
        //参数:Duration_FirstFrame 值:
        //参数:Duration_FirstFrame/String 值:
        //参数:Duration_FirstFrame/String1 值:
        //参数:Duration_FirstFrame/String2 值:
        //参数:Duration_FirstFrame/String3 值:
        //参数:Duration_LastFrame 值:
        //参数:Duration_LastFrame/String 值:
        //参数:Duration_LastFrame/String1 值:
        //参数:Duration_LastFrame/String2 值:
        //参数:Duration_LastFrame/String3 值:
        //参数:BitRate_Mode 值:
        //参数:BitRate_Mode/String 值:Variable
        //参数:BitRate 值:2956434
        //参数:BitRate/String 值:2 956 Kbps
        //参数:BitRate_Minimum 值:
        //参数:BitRate_Minimum/String 值:
        //参数:BitRate_Nominal 值:2700000
        //参数:BitRate_Nominal/String 值:2 700 Kbps
        //参数:BitRate_Maximum 值:3000000
        //参数:BitRate_Maximum/String 值:3 000 Kbps
        //参数:Width 值:640
        //参数:Width/String 值:640 pixels
        //参数:Width_Offset 值:
        //参数:Width_Offset/String 值:
        //参数:Width_Original 值:
        //参数:Width_Original/String 值:
        //参数:Height 值:480
        //参数:Height/String 值:480 pixels
        //参数:Height_Offset 值:
        //参数:Height_Offset/String 值:
        //参数:Height_Original 值:
        //参数:Height_Original/String 值:
        //参数:PixelAspectRatio 值:1.000
        //参数:PixelAspectRatio/String 值:
        //参数:PixelAspectRatio_Original 值:
        //参数:PixelAspectRatio_Original/String 值:
        //参数:DisplayAspectRatio 值:1.333
        //参数:DisplayAspectRatio/String 值:4:3
        //参数:DisplayAspectRatio_Original 值:
        //参数:DisplayAspectRatio_Original/String 值:
        //参数:ActiveFormatDescription 值:
        //参数:ActiveFormatDescription/String 值:
        //参数:ActiveFormatDescription_MuxingMode 值:
        //参数:Rotation 值:0.000
        //参数:Rotation/String 值:
        //参数:FrameRate_Mode 值:VFR
        //参数:FrameRate_Mode/String 值:Variable
        //参数:FrameRate_Mode_Original 值:
        //参数:FrameRate_Mode_Original/String 值:
        //参数:FrameRate 值:28.426
        //参数:FrameRate/String 值:28.426 fps
        //参数:FrameRate_Original 值:
        //参数:FrameRate_Original/String 值:
        //参数:FrameRate_Minimum 值:14.925
        //参数:FrameRate_Minimum/String 值:14.925 fps
        //参数:FrameRate_Nominal 值:
        //参数:FrameRate_Nominal/String 值:
        //参数:FrameRate_Maximum 值:61.224
        //参数:FrameRate_Maximum/String 值:61.224 fps
        //参数:FrameCount 值:1485
        //参数:Standard 值:
        //参数:Resolution 值:8
        //参数:Resolution/String 值:8 bits
        //参数:Colorimetry 值:4:2:0
        //参数:ColorSpace 值:YUV
        //参数:ChromaSubsampling 值:4:2:0
        //参数:BitDepth 值:8
        //参数:BitDepth/String 值:8 bits
        //参数:ScanType 值:Progressive
        //参数:ScanType/String 值:Progressive
        //参数:ScanOrder 值:
        //参数:ScanOrder/String 值:
        //参数:Interlacement 值:PPF
        //参数:Interlacement/String 值:Progressive
        //参数:Compression_Mode 值:Lossy
        //参数:Compression_Mode/String 值:Lossy
        //参数:Compression_Ratio 值:
        //参数:Bits-(Pixel* Frame)  值:0.339
        //参数:Delay 值:
        //参数:Delay/String 值:
        //参数:Delay/String1 值:
        //参数:Delay/String2 值:
        //参数:Delay/String3 值:
        //参数:Delay/String4 值:
        //参数:Delay_Settings 值:
        //参数:Delay_Source 值:
        //参数:Delay_Source/String 值:
        //参数:Delay_Original 值:
        //参数:Delay_Original/String 值:
        //参数:Delay_Original/String1 值:
        //参数:Delay_Original/String2 值:
        //参数:Delay_Original/String3 值:
        //参数:Delay_Original/String4 值:
        //参数:Delay_Original_Settings 值:
        //参数:Delay_Original_Source 值:
        //参数:StreamSize 值:19305851
        //参数:StreamSize/String 值:18.4 MiB(97%)
        //参数:StreamSize/String1 值:18 MiB
        //参数:StreamSize/String2 值:18 MiB
        //参数:StreamSize/String3 值:18.4 MiB
        //参数:StreamSize/String4 值:18.41 MiB
        //参数:StreamSize/String5 值:18.4 MiB(97%)
        //参数:StreamSize_Proportion 值:0.96652
        //参数:Alignment 值:
        //参数:Alignment/String 值:
        //参数:Title 值:
        //参数:Encoded_Application 值:
        //参数:Encoded_Application/Url 值:
        //参数:Encoded_Library 值:
        //参数:Encoded_Library/String 值:
        //参数:Encoded_Library/Name 值:
        //参数:Encoded_Library/Version 值:
        //参数:Encoded_Library/Date 值:
        //参数:Encoded_Library_Settings 值:
        //参数:Language 值:
        //参数:Language/String 值:
        //参数:Language/String1 值:
        //参数:Language/String2 值:
        //参数:Language/String3 值:
        //参数:Language/String4 值:
        //参数:Language_More 值:
        //参数:Encoded_Date 值:UTC 2011-09-02 02:58:59
        //参数:Tagged_Date 值:UTC 2011-09-02 02:58:59
        //参数:Encryption 值:
        //参数:BufferSize 值:

        #endregion

        #region 音频参数
        //参数:Count 值:169
        //参数:Status 值:
        //参数:StreamCount 值:1
        //参数:StreamKind 值:Audio
        //参数:StreamKind/String 值:Audio
        //参数:StreamKindID 值:0
        //参数:StreamKindPos 值:
        //参数:Inform 值:Format                                   : MPEG Audio
        //Format version                           : Version 1
        //Format profile                           : Layer 3
        //Mode                                     : Joint stereo
        //Mode extension                           : MS Stereo
        //Duration                                 : 2mn 54s
        //Bit rate mode                            : Constant
        //Bit rate                                 : 112 Kbps
        //Channel(s)                               : 2 channels
        //Sampling rate                            : 44.1 KHz
        //Compression mode                         : Lossy
        //Stream size                              : 2.33 MiB(98%)

        //参数:ID 值:
        //参数:ID/String 值:
        //参数:UniqueID 值:
        //参数:UniqueID/String 值:
        //参数:MenuID 值:
        //参数:MenuID/String 值:
        //参数:Format 值:MPEG Audio
        //参数:Format/Info 值:
        //参数:Format/Url 值:
        //参数:Format_Commercial 值:MPEG Audio
        //参数:Format_Commercial_IfAny 值:
        //参数:Format_Version 值:Version 1
        //参数:Format_Profile 值:Layer 3
        //参数:Format_Compression 值:
        //参数:Format_Settings 值:Joint stereo / MS Stereo
        //参数:Format_Settings_SBR 值:
        //参数:Format_Settings_SBR/String 值:
        //参数:Format_Settings_PS 值:
        //参数:Format_Settings_PS/String 值:
        //参数:Format_Settings_Mode 值:Joint stereo
        //参数:Format_Settings_ModeExtension 值:MS Stereo
        //参数:Format_Settings_Emphasis 值:
        //参数:Format_Settings_Floor 值:
        //参数:Format_Settings_Firm 值:
        //参数:Format_Settings_Endianness 值:
        //参数:Format_Settings_Sign 值:
        //参数:Format_Settings_Law 值:
        //参数:Format_Settings_ITU 值:
        //参数:Format_Settings_Wrapping 值:
        //参数:InternetMediaType 值:audio/mpeg
        //参数:MuxingMode 值:
        //参数:MuxingMode_MoreInfo 值:
        //参数:CodecID 值:
        //参数:CodecID/String 值:
        //参数:CodecID/Info 值:
        //参数:CodecID/Hint 值:
        //参数:CodecID/Url 值:
        //参数:CodecID_Description 值:
        //参数:Codec 值:MPA1L3
        //参数:Codec/String 值:MPEG-1 Audio layer 3
        //参数:Codec/Family 值:
        //参数:Codec/Info 值:
        //参数:Codec/Url 值:
        //参数:Codec/CC 值:
        //参数:Codec_Description 值:
        //参数:Codec_Profile 值:Joint stereo
        //参数:Codec_Settings 值:
        //参数:Codec_Settings_Automatic 值:
        //参数:Codec_Settings_Floor 值:
        //参数:Codec_Settings_Firm 值:
        //参数:Codec_Settings_Endianness 值:
        //参数:Codec_Settings_Sign 值:
        //参数:Codec_Settings_Law 值:
        //参数:Codec_Settings_ITU 值:
        //参数:Duration 值:174524
        //参数:Duration/String 值:2mn 54s
        //参数:Duration/String1 值:2mn 54s 524ms
        //参数:Duration/String2 值:2mn 54s
        //参数:Duration/String3 值:00:02:54.524
        //参数:Duration_FirstFrame 值:
        //参数:Duration_FirstFrame/String 值:
        //参数:Duration_FirstFrame/String1 值:
        //参数:Duration_FirstFrame/String2 值:
        //参数:Duration_FirstFrame/String3 值:
        //参数:Duration_LastFrame 值:
        //参数:Duration_LastFrame/String 值:
        //参数:Duration_LastFrame/String1 值:
        //参数:Duration_LastFrame/String2 值:
        //参数:Duration_LastFrame/String3 值:
        //参数:BitRate_Mode 值:CBR
        //参数:BitRate_Mode/String 值:Constant
        //参数:BitRate 值:112000
        //参数:BitRate/String 值:112 Kbps
        //参数:BitRate_Minimum 值:
        //参数:BitRate_Minimum/String 值:
        //参数:BitRate_Nominal 值:
        //参数:BitRate_Nominal/String 值:
        //参数:BitRate_Maximum 值:
        //参数:BitRate_Maximum/String 值:
        //参数:Channel(s)  值:2
        //参数:Channel(s)/String 值:2 channels
        //参数:ChannelPositions 值:
        //参数:ChannelPositions/String2 值:
        //参数:ChannelLayout 值:
        //参数:SamplingRate 值:44100
        //参数:SamplingRate/String 值:44.1 KHz
        //参数:SamplingCount 值:7696512
        //参数:FrameRate 值:
        //参数:FrameRate/String 值:
        //参数:FrameCount 值:6681
        //参数:Resolution 值:
        //参数:Resolution/String 值:
        //参数:BitDepth 值:
        //参数:BitDepth/String 值:
        //参数:Compression_Mode 值:Lossy
        //参数:Compression_Mode/String 值:Lossy
        //参数:Compression_Ratio 值:
        //参数:Delay 值:
        //参数:Delay/String 值:
        //参数:Delay/String1 值:
        //参数:Delay/String2 值:
        //参数:Delay/String3 值:
        //参数:Delay/String4 值:
        //参数:Delay_Settings 值:
        //参数:Delay_Source 值:
        //参数:Delay_Source/String 值:
        //参数:Delay_Original 值:
        //参数:Delay_Original/String 值:
        //参数:Delay_Original/String1 值:
        //参数:Delay_Original/String2 值:
        //参数:Delay_Original/String3 值:
        //参数:Delay_Original/String4 值:
        //参数:Delay_Original_Settings 值:
        //参数:Delay_Original_Source 值:
        //参数:Video_Delay 值:
        //参数:Video_Delay/String 值:
        //参数:Video_Delay/String1 值:
        //参数:Video_Delay/String2 值:
        //参数:Video_Delay/String3 值:
        //参数:Video_Delay/String4 值:
        //参数:Video0_Delay 值:
        //参数:Video0_Delay/String 值:
        //参数:Video0_Delay/String1 值:
        //参数:Video0_Delay/String2 值:
        //参数:Video0_Delay/String3 值:
        //参数:Video0_Delay/String4 值:
        //参数:ReplayGain_Gain 值:
        //参数:ReplayGain_Gain/String 值:
        //参数:ReplayGain_Peak 值:
        //参数:StreamSize 值:2438637
        //参数:StreamSize/String 值:2.33 MiB(98%)
        //参数:StreamSize/String1 值:2 MiB
        //参数:StreamSize/String2 值:2.3 MiB
        //参数:StreamSize/String3 值:2.33 MiB
        //参数:StreamSize/String4 值:2.326 MiB
        //参数:StreamSize/String5 值:2.33 MiB(98%)
        //参数:StreamSize_Proportion 值:0.97918
        //参数:Alignment 值:
        //参数:Alignment/String 值:
        //参数:Interleave_VideoFrames 值:
        //参数:Interleave_Duration 值:
        //参数:Interleave_Duration/String 值:
        //参数:Interleave_Preload 值:
        //参数:Interleave_Preload/String 值:
        //参数:Title 值:
        //参数:Encoded_Library 值:
        //参数:Encoded_Library/String 值:
        //参数:Encoded_Library/Name 值:
        //参数:Encoded_Library/Version 值:
        //参数:Encoded_Library/Date 值:
        //参数:Encoded_Library_Settings 值:
        //参数:Language 值:
        //参数:Language/String 值:
        //参数:Language/String1 值:
        //参数:Language/String2 值:
        //参数:Language/String3 值:
        //参数:Language/String4 值:
        //参数:Language_More 值:
        //参数:Encoded_Date 值:
        //参数:Tagged_Date 值:
        //参数:Encryption 值:

        //参数:Count 值:278
        //参数:Status 值:
        //参数:StreamCount 值:1
        //参数:StreamKind 值:General
        //参数:StreamKind/String 值:General
        //参数:StreamKindID 值:0
        //参数:StreamKindPos 值:
        //参数:Inform 值:Complete name                            : F:\thunder\qie.rmvb
        //Format                                   : RealMedia
        //File size                                : 771 MiB
        //Duration                                 : 1h 58mn
        //Overall bit rate                         : 885 Kbps

        //参数:ID 值:
        //参数:ID/String 值:
        //参数:UniqueID 值:
        //参数:UniqueID/String 值:
        //参数:MenuID 值:
        //参数:MenuID/String 值:
        //参数:GeneralCount 值:
        //参数:VideoCount 值:1
        //参数:AudioCount 值:1
        //参数:TextCount 值:
        //参数:ChaptersCount 值:
        //参数:ImageCount 值:
        //参数:MenuCount 值:
        //参数:Video_Format_List 值:RealVideo 4
        //参数:Video_Format_WithHint_List 值:RealVideo 4
        //参数:Video_Codec_List 值:RealVideo 4
        //参数:Video_Language_List 值:
        //参数:Audio_Format_List 值:Cooker
        //参数:Audio_Format_WithHint_List 值:Cooker
        //参数:Audio_Codec_List 值:Cooker
        //参数:Audio_Language_List 值:
        //参数:Text_Format_List 值:
        //参数:Text_Format_WithHint_List 值:
        //参数:Text_Codec_List 值:
        //参数:Text_Language_List 值:
        //参数:Chapters_Format_List 值:
        //参数:Chapters_Format_WithHint_List 值:
        //参数:Chapters_Codec_List 值:
        //参数:Chapters_Language_List 值:
        //参数:Image_Format_List 值:
        //参数:Image_Format_WithHint_List 值:
        //参数:Image_Codec_List 值:
        //参数:Image_Language_List 值:
        //参数:Menu_Format_List 值:
        //参数:Menu_Format_WithHint_List 值:
        //参数:Menu_Codec_List 值:
        //参数:Menu_Language_List 值:
        //参数:CompleteName 值:F:\thunder\qie.rmvb
        //参数:FolderName 值:F:\thunder
        //参数:FileName 值:qie
        //参数:FileExtension 值:rmvb
        //参数:Format 值:RealMedia
        //参数:Format/String 值:RealMedia
        //参数:Format/Info 值:
        //参数:Format/Url 值:
        //参数:Format/Extensions 值:rm rmvb ra
        //参数:Format_Commercial 值:RealMedia
        //参数:Format_Commercial_IfAny 值:
        //参数:Format_Version 值:
        //参数:Format_Profile 值:
        //参数:Format_Compression 值:
        //参数:Format_Settings 值:
        //参数:InternetMediaType 值:application/vnd.rn-realmedia
        //参数:CodecID 值:
        //参数:CodecID/String 值:
        //参数:CodecID/Info 值:
        //参数:CodecID/Hint 值:
        //参数:CodecID/Url 值:
        //参数:CodecID_Description 值:
        //参数:Interleaved 值:
        //参数:Codec 值:RealMedia
        //参数:Codec/String 值:RealMedia
        //参数:Codec/Info 值:
        //参数:Codec/Url 值:
        //参数:Codec/Extensions 值:rm rmvb ra
        //参数:Codec_Settings 值:
        //参数:Codec_Settings_Automatic 值:
        //参数:FileSize 值:808682860
        //参数:FileSize/String 值:771 MiB
        //参数:FileSize/String1 值:771 MiB
        //参数:FileSize/String2 值:771 MiB
        //参数:FileSize/String3 值:771 MiB
        //参数:FileSize/String4 值:771.2 MiB
        //参数:Duration 值:7125405
        //参数:Duration/String 值:1h 58mn
        //参数:Duration/String1 值:1h 58mn 45s 405ms
        //参数:Duration/String2 值:1h 58mn
        //参数:Duration/String3 值:01:58:45.405
        //参数:Duration_Start 值:
        //参数:Duration_End 值:
        //参数:OverallBitRate_Mode 值:
        //参数:OverallBitRate_Mode/String 值:
        //参数:OverallBitRate 值:885095
        //参数:OverallBitRate/String 值:885 Kbps
        //参数:OverallBitRate_Minimum 值:
        //参数:OverallBitRate_Minimum/String 值:
        //参数:OverallBitRate_Nominal 值:
        //参数:OverallBitRate_Nominal/String 值:
        //参数:OverallBitRate_Maximum 值:
        //参数:OverallBitRate_Maximum/String 值:
        //参数:Delay 值:
        //参数:Delay/String 值:
        //参数:Delay/String1 值:
        //参数:Delay/String2 值:
        //参数:Delay/String3 值:
        //参数:StreamSize 值:20350217
        //参数:StreamSize/String 值:19.4 MiB(3%)
        //参数:StreamSize/String1 值:19 MiB
        //参数:StreamSize/String2 值:19 MiB
        //参数:StreamSize/String3 值:19.4 MiB
        //参数:StreamSize/String4 值:19.41 MiB
        //参数:StreamSize/String5 值:19.4 MiB(3%)
        //参数:StreamSize_Proportion 值:0.02516
        //参数:HeaderSize 值:
        //参数:DataSize 值:
        //参数:FooterSize 值:
        //参数:IsStreamable 值:
        //参数:Album_ReplayGain_Gain 值:
        //参数:Album_ReplayGain_Gain/String 值:
        //参数:Album_ReplayGain_Peak 值:
        //参数:Encryption 值:
        //参数:Title 值:
        //参数:Title/More 值:
        //参数:Title/Url 值:
        //参数:Domain 值:
        //参数:Collection 值:
        //参数:Season 值:
        //参数:Season_Position 值:
        //参数:Season_Position_Total 值:
        //参数:Movie 值:
        //参数:Movie/More 值:
        //参数:Movie/Country 值:
        //参数:Movie/Url 值:
        //参数:Album 值:
        //参数:Album/More 值:
        //参数:Album/Sort 值:
        //参数:Album/Performer 值:
        //参数:Album/Performer/Sort 值:
        //参数:Album/Performer/Url 值:
        //参数:Comic 值:
        //参数:Comic/More 值:
        //参数:Comic/Position_Total 值:
        //参数:Part 值:
        //参数:Part/Position 值:
        //参数:Part/Position_Total 值:
        //参数:Track 值:
        //参数:Track/More 值:
        //参数:Track/Url 值:
        //参数:Track/Sort 值:
        //参数:Track/Position 值:
        //参数:Track/Position_Total 值:
        //参数:Grouping 值:
        //参数:Chapter 值:
        //参数:SubTrack 值:
        //参数:Original/Album 值:
        //参数:Original/Movie 值:
        //参数:Original/Part 值:
        //参数:Original/Track 值:
        //参数:Compilation 值:
        //参数:Compilation/String 值:
        //参数:Performer 值:
        //参数:Performer/Sort 值:
        //参数:Performer/Url 值:
        //参数:Original/Performer 值:
        //参数:Accompaniment 值:
        //参数:Composer 值:
        //参数:Composer/Nationality 值:
        //参数:Arranger 值:
        //参数:Lyricist 值:
        //参数:Original/Lyricist 值:
        //参数:Conductor 值:
        //参数:Director 值:
        //参数:AssistantDirector 值:
        //参数:DirectorOfPhotography 值:
        //参数:SoundEngineer 值:
        //参数:ArtDirector 值:
        //参数:ProductionDesigner 值:
        //参数:Choregrapher 值:
        //参数:CostumeDesigner 值:
        //参数:Actor 值:
        //参数:Actor_Character 值:
        //参数:WrittenBy 值:
        //参数:ScreenplayBy 值:
        //参数:EditedBy 值:
        //参数:CommissionedBy 值:
        //参数:Producer 值:
        //参数:CoProducer 值:
        //参数:ExecutiveProducer 值:
        //参数:MusicBy 值:
        //参数:DistributedBy 值:
        //参数:OriginalSourceForm/DistributedBy 值:
        //参数:MasteredBy 值:
        //参数:EncodedBy 值:
        //参数:RemixedBy 值:
        //参数:ProductionStudio 值:
        //参数:ThanksTo 值:
        //参数:Publisher 值:
        //参数:Publisher/URL 值:
        //参数:Label 值:
        //参数:Genre 值:
        //参数:Mood 值:
        //参数:ContentType 值:
        //参数:Subject 值:
        //参数:Description 值:
        //参数:Keywords 值:
        //参数:Summary 值:
        //参数:Synopsis 值:
        //参数:Period 值:
        //参数:LawRating 值:
        //参数:LawRating_Reason 值:
        //参数:ICRA 值:
        //参数:Released_Date 值:
        //参数:Original/Released_Date 值:
        //参数:Recorded_Date 值:
        //参数:Encoded_Date 值:
        //参数:Tagged_Date 值:
        //参数:Written_Date 值:
        //参数:Mastered_Date 值:
        //参数:File_Created_Date 值:UTC 2011-08-23 12:40:55.765
        //参数:File_Created_Date_Local 值:2011-08-23 20:40:55.765
        //参数:File_Modified_Date 值:UTC 2011-08-24 03:47:48.843
        //参数:File_Modified_Date_Local 值:2011-08-24 11:47:48.843
        //参数:Recorded_Location 值:
        //参数:Written_Location 值:
        //参数:Archival_Location 值:
        //参数:Encoded_Application 值:
        //参数:Encoded_Application/Url 值:
        //参数:Encoded_Library 值:
        //参数:Encoded_Library/String 值:
        //参数:Encoded_Library/Name 值:
        //参数:Encoded_Library/Version 值:
        //参数:Encoded_Library/Date 值:
        //参数:Encoded_Library_Settings 值:
        //参数:Cropped 值:
        //参数:Dimensions 值:
        //参数:DotsPerInch 值:
        //参数:Lightness 值:
        //参数:OriginalSourceMedium 值:
        //参数:OriginalSourceForm 值:
        //参数:OriginalSourceForm/NumColors 值:
        //参数:OriginalSourceForm/Name 值:
        //参数:OriginalSourceForm/Cropped 值:
        //参数:OriginalSourceForm/Sharpness 值:
        //参数:Tagged_Application 值:
        //参数:BPM 值:
        //参数:ISRC 值:
        //参数:ISBN 值:
        //参数:BarCode 值:
        //参数:LCCN 值:
        //参数:CatalogNumber 值:
        //参数:LabelCode 值:
        //参数:Owner 值:
        //参数:Copyright 值:
        //参数:Copyright/Url 值:
        //参数:Producer_Copyright 值:
        //参数:TermsOfUse 值:
        //参数:ServiceName 值:
        //参数:ServiceChannel 值:
        //参数:Service/Url 值:
        //参数:ServiceProvider 值:
        //参数:ServiceProviderr/Url 值:
        //参数:ServiceType 值:
        //参数:NetworkName 值:
        //参数:OriginalNetworkName 值:
        //参数:Country 值:
        //参数:TimeZone 值:
        //参数:Cover 值:
        //参数:Cover_Description 值:
        //参数:Cover_Type 值:
        //参数:Cover_Mime 值:
        //参数:Cover_Data 值:
        //参数:Lyrics 值:
        //参数:Comment 值:
        //参数:Rating 值:
        //参数:Added_Date 值:
        //参数:Played_First_Date 值:
        //参数:Played_Last_Date 值:
        //参数:Played_Count 值:
        //参数:EPG_Positions_Begin 值:
        //参数:EPG_Positions_End 值:

        #endregion

        #region 图像参数
        //参数:Count 值:90
        //参数:Status 值:
        //参数:StreamCount 值:1
        //参数:StreamKind 值:Image
        //参数:StreamKind/String 值:Image
        //参数:StreamKindID 值:0
        //参数:StreamKindPos 值:
        //参数:Inform 值:Format                                   : JPEG
        //Width                                    : 640 pixels
        //Height                                   : 480 pixels
        //Chroma subsampling                       : 4:2:0
        //Bit depth                                : 8 bits
        //Compression mode                         : Lossy

        //参数:ID 值:
        //参数:ID/String 值:
        //参数:UniqueID 值:
        //参数:UniqueID/String 值:
        //参数:MenuID 值:
        //参数:MenuID/String 值:
        //参数:Title 值:
        //参数:Format 值:JPEG
        //参数:Format/Info 值:
        //参数:Format/Url 值:
        //参数:Format_Commercial 值:JPEG
        //参数:Format_Commercial_IfAny 值:
        //参数:Format_Version 值:
        //参数:Format_Profile 值:
        //参数:Format_Compression 值:
        //参数:Format_Settings 值:
        //参数:Format_Settings_Wrapping 值:
        //参数:InternetMediaType 值:image/jpeg
        //参数:CodecID 值:
        //参数:CodecID/String 值:
        //参数:CodecID/Info 值:
        //参数:CodecID/Hint 值:
        //参数:CodecID/Url 值:
        //参数:CodecID_Description 值:
        //参数:Codec 值:JPEG
        //参数:Codec/String 值:JPEG
        //参数:Codec/Family 值:
        //参数:Codec/Info 值:
        //参数:Codec/Url 值:
        //参数:Width 值:640
        //参数:Width/String 值:640 pixels
        //参数:Width_Offset 值:
        //参数:Width_Offset/String 值:
        //参数:Width_Original 值:
        //参数:Width_Original/String 值:
        //参数:Height 值:480
        //参数:Height/String 值:480 pixels
        //参数:Height_Offset 值:
        //参数:Height_Offset/String 值:
        //参数:Height_Original 值:
        //参数:Height_Original/String 值:
        //参数:PixelAspectRatio 值:
        //参数:PixelAspectRatio/String 值:
        //参数:PixelAspectRatio_Original 值:
        //参数:PixelAspectRatio_Original/String 值:
        //参数:DisplayAspectRatio 值:
        //参数:DisplayAspectRatio/String 值:
        //参数:DisplayAspectRatio_Original 值:
        //参数:DisplayAspectRatio_Original/String 值:
        //参数:ColorSpace 值:
        //参数:ChromaSubsampling 值:4:2:0
        //参数:Resolution 值:8
        //参数:Resolution/String 值:8 bits
        //参数:BitDepth 值:8
        //参数:BitDepth/String 值:8 bits
        //参数:Compression_Mode 值:Lossy
        //参数:Compression_Mode/String 值:Lossy
        //参数:Compression_Ratio 值:
        //参数:StreamSize 值:
        //参数:StreamSize/String 值:
        //参数:StreamSize/String1 值:
        //参数:StreamSize/String2 值:
        //参数:StreamSize/String3 值:
        //参数:StreamSize/String4 值:
        //参数:StreamSize/String5 值:
        //参数:StreamSize_Proportion 值:
        //参数:Encoded_Library 值:
        //参数:Encoded_Library/String 值:
        //参数:Encoded_Library/Name 值:
        //参数:Encoded_Library/Version 值:
        //参数:Encoded_Library/Date 值:
        //参数:Encoded_Library_Settings 值:
        //参数:Language 值:
        //参数:Language/String 值:
        //参数:Language/String1 值:
        //参数:Language/String2 值:
        //参数:Language/String3 值:
        //参数:Language/String4 值:
        //参数:Summary 值:
        //参数:Encoded_Date 值:
        //参数:Tagged_Date 值:
        //参数:Encryption 值:
        #endregion
    }
}
