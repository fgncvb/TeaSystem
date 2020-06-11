using Common_Module.StringTool;
using System.Configuration;

namespace Common_Module.StringTool
{
    public static class ConfigHelper
    {

        /// <summary>
        /// 统计配置
        /// 陈宇龙
        /// 日期：2018年5月2日17:20:00
        /// </summary>
        public static string Tongji_Domain
        {
            get
            {
                string studentzone_domain = IsDebug == "true" ? ConfigurationManager.AppSettings["Debug_TongjiSiteUrl"] : ConfigurationManager.AppSettings["TongJi_Domain"];
                return studentzone_domain;
            }
        }

        /// <summary>
        /// 回调地址配置
        /// 陈宇龙
        /// 日期：2018年5月2日17:20:00
        /// </summary>
        public static string CallBack
        {
            get
            {
                string database = ConfigurationManager.AppSettings["CallBack"];
                return database;
            }
        }


        /// <summary>
        /// 回调地址配置
        /// 陈宇龙
        /// 日期：2018年5月2日17:20:00
        /// </summary>
        public static string chengxuurl
        {
            get
            {
                string database = ConfigurationManager.AppSettings["chengxuurl"];
                return database;
            }
        }


        /// <summary>
        /// 数据库配置
        /// 陈宇龙
        /// 日期：2018年5月2日17:19:49
        /// </summary>
        public static string IsRedis
        {
            get
            {
                string database = ConfigurationManager.AppSettings["IsRedis"];
                return database;
            }
        }

        /// <summary>
        /// 省
        /// </summary>
        public static int ProvinceID
        {
            get
            {
                int value = int.Parse(ConfigurationManager.AppSettings["ProvinceID"].ToString());
                return value;
            }
        }


        /// <summary>
        /// 县
        /// </summary>
        public static int CountyID
        {
            get
            {
                int value = int.Parse(ConfigurationManager.AppSettings["CountyID"].ToString());
                return value;
            }
        }


        /// <summary>
        /// 市
        /// </summary>
        public static int CityID
        {
            get
            {
                int value = int.Parse(ConfigurationManager.AppSettings["CityID"].ToString());
                return value;
            }
        }

        /// <summary>
        /// 视频封面图目录
        /// </summary>
        public static string ImageFilebasePath
        {
            get
            {
                string value = ConfigurationManager.AppSettings["ImageFilebasePath"];
                return value;
            }
        }
        /// <summary>
        /// ZIP文件解压目录
        /// </summary>
        public static string ExtractFilePath
        {
            get
            {
                string value = ConfigurationManager.AppSettings["ExtractFilePath"];
                return value;
            }
        }

        /// <summary>
        /// zip连接
        /// 
        /// 日期：2018年1月12日10:24:25
        /// </summary>
        public static string FTPUploadFilePath
        {
            get
            {
                string value = ConfigurationManager.AppSettings["FTPUploadFilePath"];
                return value;
            }
        }

        /// <summary>
        /// 是否开启访问权限验证
        /// </summary>
        public static bool IsCheckAccess
        {
            get
            {
                string value = ConfigurationManager.AppSettings["IsCheckAccess"];
                if (!string.IsNullOrEmpty(value) && value == "true")
                {
                    return true;
                }else if(value == "false")
                {
                    return false;
                }
                return false;
            }
        }

        /// <summary>
        /// 直播类型的资源，开始时间与结束时间之间至少的分钟数
        /// </summary>
        public static int LiveMinDuartion
        {
            get
            {
                string value = ConfigurationManager.AppSettings["LiveMinDuartion"];
                if(string.IsNullOrEmpty(value) || !value.IsNumber())
                {
                    return 20;
                }
                int i_value = int.Parse(value);
                return i_value;
            }
        }

        /// <summary>
        /// 监护人(家长)默认头像
        /// 于挺
        /// 日期：2016-10-21 14:25:20
        /// </summary>
        public static string Guardian_Default_HeadPortrait
        {
            get
            {
                string value = ConfigurationManager.AppSettings["Guardian_Default_HeadPortrait"];
                return value;
            }
        }

        /// <summary>
        /// 学生默认头像
        /// 于挺
        /// 日期：2016-10-21 14:24:39
        /// </summary>
        public static string Student_Default_HeadPortrait
        {
            get
            {
                string value = ConfigurationManager.AppSettings["Student_Default_HeadPortrait"];
                return value;
            }
        }

        /// <summary>
        /// 决定路径文件
        /// 陈宇龙
        /// 日期：2017年9月21日15:04:12
        /// </summary>
        public static string CoursewareFileDirectory
        {
            get
            {
                string value = ConfigurationManager.AppSettings["CoursewareFileDirectory"];
                return value;
            }
        }


        /// <summary>
        /// 决定路径
        /// 陈宇龙
        /// 日期：2017年9月21日15:04:12
        /// </summary>
        public static string CoursewareImageDirectory
        {
            get
            {
                string value = ConfigurationManager.AppSettings["CoursewareImageDirectory"];
                return value;
            }
        }

        public static string QuestionShi
        {
            get
            {
                string value = ConfigurationManager.AppSettings["QuestionShi"];
                return value;
            }
        }

        /// <summary>
        /// 教师默认头像
        /// 于挺
        /// 日期：2016-10-21 14:24:21
        /// </summary>
        public static string Teacher_Default_HeadPortrait
        {
            get
            {
                string value = ConfigurationManager.AppSettings["Teacher_Default_HeadPortrait"];
                return value;
            }
        }

        /// <summary>
        /// WWW Cookie作用域
        /// 于挺
        /// 日期：2016年9月23日18:14:14
        /// </summary>
        public static string WWW_Domain
        {
            get
            {
                string www_domain = ConfigurationManager.AppSettings["WWW_Domain"];
                return www_domain;
            }
        }

        /// <summary>
        /// 密钥
        /// 于挺
        /// 日期：2016年9月23日17:07:45
        /// </summary>
        public static string UserKey
        {
            get
            {
                string usekey = ConfigurationManager.AppSettings["UserKey"];
                return usekey;
            }
        }

        /// <summary>
        /// 静态文件网络路径
        /// 于挺
        /// 日期：2016年9月23日17:09:53
        /// </summary>
        public static string StaticUrl
        {
            get
            {
                string staticfilesurl = ConfigurationManager.AppSettings["StaticUrl"];
                return staticfilesurl;
            }
        }

        /// <summary>
        /// Excel保存地址
        /// 于挺
        /// 日期：2016年9月23日17:09:53
        /// </summary>
        public static string ExcelSaveUrl
        {
            get
            {
                string staticfilesurl = ConfigurationManager.AppSettings["ExcelSaveUrl"];
                return staticfilesurl;
            }
        }


        /// <summary>
        /// 网站后台网络地址
        /// 于挺
        /// 日期：2016年9月23日17:12:45
        /// </summary>
        public static string AdminSiteUrl
        {
            get
            {
                string adminsiteurl = IsDebug == "true" ? ConfigurationManager.AppSettings["Debug_AdminSiteUrl"] : ConfigurationManager.AppSettings["AdminSiteUrl"];
                return adminsiteurl;
            }
        }

        /// <summary>
        /// 网站后台网络地址
        /// 陈宇龙
        /// 日期：2019年10月12日10:06:32
        /// </summary>
        public static string BasicSiteUrl
        {
            get
            {
                string adminsiteurl = IsDebug == "true" ? ConfigurationManager.AppSettings["Debug_BasicSiteUrl"] : ConfigurationManager.AppSettings["BasicSiteUrl"];
                return adminsiteurl;
            }
        }


        /// <summary>
        /// 教师个人空间地址
        /// 廖博
        /// 日期：2016年11月23日 10:41:32
        /// </summary>
        public static string TeacherZoneSiteUrl
        {
            get
            {
                string teacherzonesiteurl = IsDebug == "true" ? ConfigurationManager.AppSettings["Debug_TeacherZoneSiteUrl"] : ConfigurationManager.AppSettings["TeacherZoneSiteUrl"];
                return teacherzonesiteurl;
            }
        }

        /// <summary>
        /// 学生个人空间地址
        /// 廖博
        /// 日期：2016年11月23日 10:41:32
        /// </summary>
        public static string StudentZoneSiteUrl
        {
            get
            {
                string studentzonesiteurl = IsDebug == "true" ? ConfigurationManager.AppSettings["Debug_StudentZoneSiteUrl"] : ConfigurationManager.AppSettings["StudentZoneSiteUrl"];
                return studentzonesiteurl;
            }
        }

        /// <summary>
        /// 县区网站地址
        /// 陈宇龙
        /// 日期：2017-3-3 14:51:25
        /// </summary>
        public static string CountyUrl
        {
            get
            {
                string countyurl = IsDebug == "true" ? ConfigurationManager.AppSettings["Debug_County_DomainUrl"] : ConfigurationManager.AppSettings["CountySiteUrl"];
                return countyurl;
            }
        }


        /// <summary>
        /// 网站地址
        /// 陈宇龙
        /// 日期：2017-3-3 14:51:25
        /// </summary>
        public static string SchoolSiteUrl
        {
            get
            {
                string countyurl = IsDebug == "true" ? ConfigurationManager.AppSettings["Debug_SchoolSiteUrl"] : ConfigurationManager.AppSettings["SchoolSiteUrl"];
                return countyurl;
            }
        }

        public static string GuardianZoneSiteUrl
        {
            get
            {
                string studentzonesiteurl = IsDebug == "true" ? ConfigurationManager.AppSettings["Debug_GuardianZoneSiteUrl"] : ConfigurationManager.AppSettings["GuardianZoneSiteUrl"];
                return studentzonesiteurl;
            }
        }

        /// <summary>
        /// Cookie全局作用域
        /// 陈宇龙
        /// 日期：2017-3-3 17:37:50
        /// </summary>
        public static string Global_Domain
        {
            get
            {
                string global_domain = IsDebug == "true" ? ConfigurationManager.AppSettings["Debug_WWW_Domain"] : ConfigurationManager.AppSettings["Global_Domain"];
                return global_domain;
            }

        }

        /// <summary>
        /// Admin Cookie作用域
        /// 于挺
        /// 日期：2016年9月23日17:06:21
        /// </summary>
        public static string Admin_Domain
        {
            get
            {
                string admin_domain = IsDebug == "true" ? ConfigurationManager.AppSettings["Debug_Admin_Domain"] : ConfigurationManager.AppSettings["Admin_Domain"];
                return admin_domain;
            }
        }

        /// <summary>
        /// Admin Cookie作用域
        /// 于挺
        /// 日期：2016年9月23日17:06:21
        /// </summary>
        public static string Basic_Domain
        {
            get
            {
                string admin_domain = IsDebug == "true" ? ConfigurationManager.AppSettings["Debug_Basic_Domain"] : ConfigurationManager.AppSettings["Basic_Domain"];
                return admin_domain;
            }
        }

        /// <summary>
        /// TeacherTzone Cookie作用域
        /// 廖博
        /// 日期：2016年11月23日 10:53:49
        /// </summary>
        public static string TeacherZone_Domain
        {
            get
            {
                string teacherzone_domain = IsDebug == "true" ? ConfigurationManager.AppSettings["Debug_TeacherZone_Domain"] : ConfigurationManager.AppSettings["TeacherZone_Domain"];
                return teacherzone_domain;
            }
        }

        /// <summary>
        /// County Cookie作用域
        /// 陈宇龙
        /// 日期：2017-3-3 11:13:56
        /// </summary>
        public static string County_Domain
        {
            get
            {
                string county_domain = IsDebug == "true" ? ConfigurationManager.AppSettings["Debug_County_Domain"] : ConfigurationManager.AppSettings["County_Domain"];
                return county_domain;
            }
        }


        /// <summary>
        /// Student Cookie作用域
        /// 廖博
        /// 日期：2016年11月23日 10:53:49
        /// </summary>
        public static string StudentZone_Domain
        {
            get
            {
                string studentzone_domain = IsDebug == "true" ? ConfigurationManager.AppSettings["Debug_StudentZone_Domain"] : ConfigurationManager.AppSettings["StudentZone_Domain"];
                return studentzone_domain;
            }
        }

        public static string GudianZone_Domain
        {
            get
            {
                string studentzone_domain = IsDebug == "true" ? ConfigurationManager.AppSettings["Debug_GuardianZone_Domain"] : ConfigurationManager.AppSettings["GuardianZone_Domain"];
                return studentzone_domain;
            }
        }

        /// <summary>
        /// 数据库配置
        /// 于挺
        /// 日期：2016年9月23日17:14:25
        /// </summary>
        public static string DefaultDataBase
        {
            get
            {
                string database = ConfigurationManager.AppSettings["DefaultDataBase"];
                return database;
            }
        }

        /// <summary>
        /// 数据库配置
        /// 程亮明
        /// 日期：2018-06-22
        /// </summary>
        public static string ClassLessonDataBase
        {
            get
            {
                string database = ConfigurationManager.AppSettings["ClassLessonDataBase"];
                return database;
            }
        }

        /// <summary>
        /// MongoDB数据库配置
        /// 彭礼
        /// 日期：2019年3月25日 14:41:52
        /// </summary>
        public static string MongoDBConnectionString
        {
            get
            {
                string database = ConfigurationManager.AppSettings["MongoDBConnectionString"];
                return database;

            }
        }


        /// <summary>
        /// 数据库配置
        /// 程亮明
        /// 日期：2018-06-22
        /// </summary>
        public static string ZouBanPaiLessonDataBase
        {
            get
            {
                string database = ConfigurationManager.AppSettings["ZouBanPaiLessonDataBase"];
                return database;
            }
        }

        /// <summary>
        /// 是否开启调试模式
        /// 于挺
        /// 日期：2016-9-26 11:16:27
        /// </summary>
        public static string IsDebug
        {
            get
            {
                string isdebug = ConfigurationManager.AppSettings["IsDebug"];
                return isdebug;
            }
        }

        /// <summary>
        /// 学校列表分页数量
        /// 于挺
        /// 日期：2016年9月26日15:21:08
        /// </summary>
        public static int SchoolListPageSize
        {
            get
            {
                string schoollistpagesize = ConfigurationManager.AppSettings["SchoolListPageSize"];
                if(string.IsNullOrEmpty(schoollistpagesize) || !schoollistpagesize.IsNumber())
                {
                    return 20;
                }
                int i_schoollistpagesize = int.Parse(schoollistpagesize);
                return i_schoollistpagesize;
            }
        }

        /// <summary>
        /// 关注列表分页数量
        /// 廖博
        /// 日期：2016年12月28日 14:56:02
        /// </summary>
        public static int BeINTerestedInPageSize
        {
            get
            {
                string homeworklistpagesize = ConfigurationManager.AppSettings["BeINTerestedInPageSize"];
                if (string.IsNullOrEmpty(homeworklistpagesize) || !homeworklistpagesize.IsNumber())
                {
                    return 20;
                }
                int i_homeworklistpagesize = int.Parse(homeworklistpagesize);
                return i_homeworklistpagesize;
            }
        }

        /// <summary>
        /// 作业列表分页数量
        /// 廖博
        /// 日期：2016年11月29日 16:04:14
        /// </summary>
        public static int HomeWorkListPageSize
        {
            get
            {
                string homeworklistpagesize = ConfigurationManager.AppSettings["HomeWorkListPageSize"];
                if (string.IsNullOrEmpty(homeworklistpagesize) || !homeworklistpagesize.IsNumber())
                {
                    return 20;
                }
                int i_homeworklistpagesize = int.Parse(homeworklistpagesize);
                return i_homeworklistpagesize;
            }
        }


        /// <summary>
        /// 博客列表分页数量
        /// 廖博
        /// 日期：2016年12月2日 16:40:25
        /// </summary>
        public static int BlogListPageSize
        {
            get
            {
                string bloglistpagesize = ConfigurationManager.AppSettings["BlogListPageSize"];
                if (string.IsNullOrEmpty(bloglistpagesize) || !bloglistpagesize.IsNumber())
                {
                    return 20;
                }
                int i_bloglistpagesize = int.Parse(bloglistpagesize);
                return i_bloglistpagesize;
            }
        }

        /// <summary>
        /// 博客列表我的评论分页数量
        /// 廖博
        /// 日期：2016年12月5日 10:17:28
        /// </summary>
        public static int CommentListPageSize
        {
            get
            {
                string commentlistpagesize = ConfigurationManager.AppSettings["CommentListPageSize"];
                if (string.IsNullOrEmpty(commentlistpagesize) || !commentlistpagesize.IsNumber())
                {
                    return 20;
                }
                int i_commentlistpagesize = int.Parse(commentlistpagesize);
                return i_commentlistpagesize;
            }
        }

        
        /// <summary>
        /// 博客列表我的访客进行分页数量
        /// 廖博
        /// 日期:2016年12月7日 09:42:43
        /// </summary>
        public static int VisitorListPageSize
        {
            get
            {
                string visitorlistpagesize = ConfigurationManager.AppSettings["VisitorListPageSize"];
                if (string.IsNullOrEmpty(visitorlistpagesize) || !visitorlistpagesize.IsNumber())
                {
                    return 20;
                }
                int i_visitorlistpagesize = int.Parse(visitorlistpagesize);
                return i_visitorlistpagesize;
            }
        }


        /// <summary>
        /// 私信列表(收信)(发信)进行分页数量
        /// 廖博
        /// 日期: 2017年2月8日 15:02:41
        /// </summary>
        public static int ReceiveMsgListPageSize
        {
            get
            {
                string visitorlistpagesize = ConfigurationManager.AppSettings["ReceiveMsgListPageSize"];
                if (string.IsNullOrEmpty(visitorlistpagesize) || !visitorlistpagesize.IsNumber())
                {
                    return 20;
                }
                int i_visitorlistpagesize = int.Parse(visitorlistpagesize);
                return i_visitorlistpagesize;
            }
        }


        /// <summary>
        /// 后台列表页面查询关键词最大长度
        /// 于挺
        /// 日期：2016-10-21 15:54:00
        /// </summary>
        public static int KeywordMaxLength
        {
            get
            {
                string keywordMaxLength = ConfigurationManager.AppSettings["KeywordMaxLength"];
                if (string.IsNullOrEmpty(keywordMaxLength) || !keywordMaxLength.IsNumber())
                {
                    return 12;
                }
                int i_keywordMaxLength = int.Parse(keywordMaxLength);
                return i_keywordMaxLength;
            }
        }

        /// <summary>
        /// 学校列表分页数量
        /// 于挺
        /// 日期：2016年10月14日15:48:58
        /// </summary>
        public static int SystemUserListPageSize
        {
            get
            {
                string systemuserlistpagesize = ConfigurationManager.AppSettings["SystemUserListPageSize"];
                if (string.IsNullOrEmpty(systemuserlistpagesize) || !systemuserlistpagesize.IsNumber())
                {
                    return 20;
                }
                int i_systemuserlistpagesize = int.Parse(systemuserlistpagesize);
                return i_systemuserlistpagesize;
            }
        }



        /// <summary>
        /// 教师列表分页数量
        /// 于挺
        /// 日期：2016-10-18 16:27:44
        /// </summary>
        public static int TeacherListPageSize
        {
            get
            {
                string teacherlistpagesize = ConfigurationManager.AppSettings["TeacherListPageSize"];
                if (string.IsNullOrEmpty(teacherlistpagesize) || !teacherlistpagesize.IsNumber())
                {
                    return 20;
                }
                int i_teacherlistpagesize = int.Parse(teacherlistpagesize);
                return i_teacherlistpagesize;
            }
        }

        /// <summary>
        /// 教材版本列表分页数量
        /// 于挺
        /// 日期：2017年9月20日14:16:27
        /// </summary>
        public static int TeachingMaterialListPageSize
        {
            get
            {
                string teacherlistpagesize = ConfigurationManager.AppSettings["TeachingMaterialListPageSize"];
                if (string.IsNullOrEmpty(teacherlistpagesize) || !teacherlistpagesize.IsNumber())
                {
                    return 20;
                }
                int i_teacherlistpagesize = int.Parse(teacherlistpagesize);
                return i_teacherlistpagesize;
            }
        }

        /// <summary>
        /// 监护人列表分页数量
        /// 于挺
        /// 日期：2016-10-26 16:38:54
        /// </summary>
        public static int GuardianListPageSize
        {
            get
            {
                string guardianlistpagesize = ConfigurationManager.AppSettings["GuardianListPageSize"];
                if (string.IsNullOrEmpty(guardianlistpagesize) || !guardianlistpagesize.IsNumber())
                {
                    return 20;
                }
                int i_guardianlistpagesize = int.Parse(guardianlistpagesize);
                return i_guardianlistpagesize;
            }
        }
        
        /// <summary>
        /// 监护人列表分页数量(主要用户更改学生监护人页面的查询监护人功能)
        /// 于挺
        /// 日期：2016-10-26 16:41:40
        /// </summary>
        public static int GuardianListPageSizeForChangeGuardian
        {
            get
            {
                string guardianlistpagesizeforchangeguardian = ConfigurationManager.AppSettings["GuardianListPageSizeForChangeGuardian"];
                if (string.IsNullOrEmpty(guardianlistpagesizeforchangeguardian) || !guardianlistpagesizeforchangeguardian.IsNumber())
                {
                    return 20;
                }
                int i_guardianlistpagesizeforchangeguardian = int.Parse(guardianlistpagesizeforchangeguardian);
                return i_guardianlistpagesizeforchangeguardian;
            }
        }

        /// <summary>
        /// 后台教师列表页面查询条件学校名称长度
        /// 于挺
        /// 日期：2016-10-18 16:29:40
        /// </summary>
        public static int TeacherListNameMaxLength
        {
            get
            {
                string teacherlistnamemaxlength = ConfigurationManager.AppSettings["TeacherListNameMaxLength"];
                if (string.IsNullOrEmpty(teacherlistnamemaxlength) || !teacherlistnamemaxlength.IsNumber())
                {
                    return 12;
                }
                int i_teacherlistnamemaxlength = int.Parse(teacherlistnamemaxlength);
                return i_teacherlistnamemaxlength;
            }
        }


        /// <summary>
        /// 后台系统用户列表页面查询条件用户名长度
        /// 于挺
        /// 日期：2016年10月14日15:46:46
        /// </summary>
        public static int SystemUserNameMaxLength
        {
            get
            {
                string systemusernamemaxlength = ConfigurationManager.AppSettings["SystemUserNameMaxLength"];
                if (string.IsNullOrEmpty(systemusernamemaxlength) || !systemusernamemaxlength.IsNumber())
                {
                    return 12;
                }
                int i_systemusernamemaxlength = int.Parse(systemusernamemaxlength);
                return i_systemusernamemaxlength;
            }
        }

        /// <summary>
        /// 教师空间课件资源页面分页数量
        /// 于挺
        /// 日期：2016年12月19日16:41:50
        /// </summary>
        public static int TzoneCoursewareListPageSize
        {
            get
            {
                string tzoneCoursewareListPageSize = ConfigurationManager.AppSettings["TzoneCoursewareListPageSize"];
                if (string.IsNullOrEmpty(tzoneCoursewareListPageSize) || !tzoneCoursewareListPageSize.IsNumber())
                {
                    return 20;
                }
                int i_tzoneCoursewareListPageSize = int.Parse(tzoneCoursewareListPageSize);
                return i_tzoneCoursewareListPageSize;
            }
        }

        /// <summary>
        /// 教师空间慕课课时选择视频资源文件分页数量
        /// 于挺
        /// 日期：2017年5月23日15:09:10
        /// </summary>
        public static int TzoneSelectResourceListPageSize
        {
            get
            {
                string tzoneSelectResourceListPageSize = ConfigurationManager.AppSettings["TzoneSelectResourceListPageSize"];
                if (string.IsNullOrEmpty(tzoneSelectResourceListPageSize) || !tzoneSelectResourceListPageSize.IsNumber())
                {
                    return 20;
                }
                int i_tzoneSelectResourceListPageSize = int.Parse(tzoneSelectResourceListPageSize);
                return i_tzoneSelectResourceListPageSize;
            }
        }

        /// <summary>
        /// 教师空间题库资源页面分页数量
        /// 于挺
        /// 日期：2016年12月20日11:18:01
        /// </summary>
        public static int TzoneQuestionBankListPageSize
        {
            get
            {
                string tzoneQuestionBankListPageSize = ConfigurationManager.AppSettings["TzoneQuestionBankListPageSize"];
                if (string.IsNullOrEmpty(tzoneQuestionBankListPageSize) || !tzoneQuestionBankListPageSize.IsNumber())
                {
                    return 20;
                }
                int i_tzoneQuestionBankListPageSize = int.Parse(tzoneQuestionBankListPageSize);
                return i_tzoneQuestionBankListPageSize;
            }
        }

        /// <summary>
        /// 学生空间慕课页面显示数量
        /// 陈宇龙
        /// 日期：2017年7月31日10:01:24
        /// </summary>
        public static int SzonMuKeListPageSize
        {
            get
            {
                string mukelistpagesize = ConfigurationManager.AppSettings["MuKeListPageSize"];
                if (string.IsNullOrEmpty(mukelistpagesize) || !mukelistpagesize.IsNumber())
                {
                    return 12;
                }
                int i_mukelistpagesize = int.Parse(mukelistpagesize);
                return i_mukelistpagesize;
            }
        }

        /// <summary>
        /// 教师空间直播任务页面列表分页数量
        /// 于挺
        /// 日期：2017年6月2日16:11:14
        /// </summary>
        public static int TzoneLiveTaskListPageSize
        {
            get
            {
                string tzoneLiveTaskListPageSize = ConfigurationManager.AppSettings["TzoneLiveTaskListPageSize"];
                if (string.IsNullOrEmpty(tzoneLiveTaskListPageSize) || !tzoneLiveTaskListPageSize.IsNumber())
                {
                    return 12;
                }
                int i_tzoneLiveTaskListPageSize = int.Parse(tzoneLiveTaskListPageSize);
                return i_tzoneLiveTaskListPageSize;
            }
        }


        /// <summary>
        /// 教师空间首页最新提问分页数量
        /// 于挺
        /// 日期：2017年3月22日14:23:09
        /// </summary>
        public static int TzoneNewAskListPageSize
        {
            get
            {
                string tzonenewasklistpagesize = ConfigurationManager.AppSettings["TzoneNewAskListPageSize"];
                if (string.IsNullOrEmpty(tzonenewasklistpagesize) || !tzonenewasklistpagesize.IsNumber())
                {
                    return 1;
                }
                int i_tzonenewasklistpagesize = int.Parse(tzonenewasklistpagesize);
                return i_tzonenewasklistpagesize;
            }
        }

        /// <summary>
        /// 教师空间首页最新课件答疑分页数量
        /// 于挺
        /// 日期：2017年3月23日15:17:09
        /// </summary>
        public static int TzoneNewResourceListPageSize
        {
            get
            {
                string tzonenewresourcelistpagesize = ConfigurationManager.AppSettings["TzoneNewResourceListPageSize"];
                if (string.IsNullOrEmpty(tzonenewresourcelistpagesize) || !tzonenewresourcelistpagesize.IsNumber())
                {
                    return 20;
                }
                int i_tzonenewresourcelistpagesize = int.Parse(tzonenewresourcelistpagesize);
                return i_tzonenewresourcelistpagesize;
            }
        }

        /// <summary>
        /// 教师空间首页学生动态分页数量
        /// 于挺
        /// 日期：2017年3月27日14:11:36
        /// </summary>
        public static int TzoneStudent_TrendListPageSize
        {
            //get
            //{
            //    string tzonestudent_trendlistpagesize = ConfigurationManager.AppSettings["TzoneStudent_TrendListPageSize"];
            //    if (string.IsNullOrEmpty(tzonestudent_trendlistpagesize) || !tzonestudent_trendlistpagesize.IsNumber())
            //    {
            //        return 1;
            //    }
            //    int i_tzonestudent_trendlistpagesize = int.Parse(tzonestudent_trendlistpagesize);
            //    return i_tzonestudent_trendlistpagesize;
            //}
            get
            {
                return 1;
            }

        }

        /// <summary>
        /// 教师空间首页学生动态博客点赞数量
        /// 于挺
        /// 日期：2017年3月27日15:09:20
        /// </summary>
        public static int StudentBlogTopCountForTzone
        {
            get
            {
                string studentblogtopcountfortzone = ConfigurationManager.AppSettings["StudentBlogTopCountForTzone"];
                if (string.IsNullOrEmpty(studentblogtopcountfortzone) || !studentblogtopcountfortzone.IsNumber())
                {
                    return 12;
                }
                int i_studentblogtopcountfortzone = int.Parse(studentblogtopcountfortzone);
                return i_studentblogtopcountfortzone;
            }
        }

        /// <summary>
        /// 视频存放目录
        /// 于挺
        /// 日期：2017年7月7日14:09:19
        /// </summary>
        public static string VideoFileBasePath
        {
            get
            {
                string videofilebasepath = ConfigurationManager.AppSettings["videofilebasepath"];
                return videofilebasepath;
            }
        }

        /// <summary>
        /// 我的班级分页显示数量
        /// 昂青本
        /// 日期：2017年10月18日16:37:20
        /// </summary>
        public static int StudentClassIDPageSize
        {
            get
            {
                string studentclassidpagesize = ConfigurationManager.AppSettings["StudentClassIDPageSize"];
                if (string.IsNullOrEmpty(studentclassidpagesize) || !studentclassidpagesize.IsNumber())
                {
                    return 6;
                }
                int i_studentclassidpagesize = int.Parse(studentclassidpagesize);
                return i_studentclassidpagesize;
            }
        }



    }
}
