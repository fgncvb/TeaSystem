using Microsoft.International.Converters.PinYinConverter;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Xml;

namespace Common_Module.StringTool
{
    public static class StringHelper
    {
        private static Regex RegTelePhone = new Regex(@"^\(?0\d{2,3}\)?[- ]?\d{7,8}|^0\d{2,3}[- ]?\d{7,8}|^1\d{10}|\(?\+?\d{2,3}\)?[- ]?0\d{2,3}[- ]?\d{7,8}|^\d{7,8}");
        private static Regex RegMobilePhone = new Regex(@"^1[3|4|5|7|8][0-9]\d{8}$"); 
        private static Regex RegURL = new Regex(@"(((([?](\w)+){1}[=]*))*((\w)+){1}([\&](\w)+[\=](\w)+)*)*");
        private static Regex RegQQ = new Regex("^[1-9]*[1-9][0-9]*$");
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegDecimal = new Regex("^[0-9]+(.[0-9]{1,2})?$"); //new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //等价于^[+-]?\d+[.]?\d+$
        private static Regex RegEmail = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
        private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");

        #region 数字字符串检查

        /// <summary>
        /// 匹配IP地址
        /// </summary>
        /// <param name="inputdata"></param>
        /// <returns></returns>
        public static bool IsIP(this string inputdata)
        {
            IPAddress ipadress;
            return IPAddress.TryParse(inputdata, out ipadress);
        }

        /// <summary>
        /// 匹配手机号码
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(this string inputData)
        {
            Match m = RegMobilePhone.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 匹配座机号码
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsTelePhone(this string inputData)
        {
            Match m = RegTelePhone.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 匹配QQ号码
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsQQ(this string inputData)
        {
            Match m = RegQQ.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否数字字符串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumber(this string inputData)
        {
            Match m = RegNumber.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否数字字符串 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumberSign(this string inputData)
        {
            Match m = RegNumberSign.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// 是否是浮点数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimal(this string inputData)
        {
            Match m = RegDecimal.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// 是否是浮点数 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimalSign(this string inputData)
        {
            Match m = RegDecimalSign.Match(inputData);
            return m.Success;
        }

        #endregion

        #region 中文检测

        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsHasCHZN(this string inputData)
        {
            Match m = RegCHZN.Match(inputData);
            return m.Success;
        }

        #endregion

        #region 邮件地址
        /// <summary>
        /// 是否是合法的邮箱地址
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsEmail(this string inputData)
        {
            Match m = RegEmail.Match(inputData);
            return m.Success;
        }

        #endregion

        #region 日期格式判断
        /// <summary>
        /// 日期格式字符串判断
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateTime(this string str)
        {
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    DateTime.Parse(str);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 检测URL地址
        /// <summary>
        /// 判断URL格式是否正确
        /// 日期：2013年10月8日21:57:31
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsURL(this string url)
        {
            Match match = RegURL.Match(url.Trim());
            if (match.Success)
            {
                return true;
            }
            return false;

        }
        #endregion

        #region 其他

        /// <summary>
        /// 将字符串根据分割符号转成字符串数组
        /// 日期：2013年10月14日20:22:20
        /// </summary>
        /// <param name="strList"></param>
        /// <param name="separatorIn"></param>
        /// <returns></returns>
        public static string[] Split(this string strList, char separatorIn)
        {
            char[] separator = { separatorIn };
            return strList.Split(separator);
        }

        public static string DelHTML(this string Htmlstring, int _int)//将?HTML去¨￡¤除y
        {
            Htmlstring = DeleteHtml(Htmlstring);
            Htmlstring = GetLenStr(Htmlstring, _int);
            return Htmlstring;

        }

        /// <summary>
        /// 完全去除Html标签
        /// 日期：2015年3月29日 10:55:43
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string DeleteHtml(this string Htmlstring)
        {
            #region
            //删|?除y脚?本à? 
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<STYLE[^>]*?>.*?</STYLE>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<style[^>]*?>.*?</style>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<style>([^>]*)</style>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //删|?除yHTML
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"-->", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<!--.*", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring =System.Text.RegularExpressions. Regex.Replace(Htmlstring,@"<A>.*</A>","");
            //Htmlstring =System.Text.RegularExpressions. Regex.Replace(Htmlstring,@"<[a-zA-Z]*=\.[a-zA-Z]*\?[a-zA-Z]+=\d&\w=%[a-zA-Z]*|[A-Z0-9]","");
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(amp|#38);", "&", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(lt|#60);", "<", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(gt|#62);", ">", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&#(\d+);", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = Htmlstring.Replace("<", "");
            Htmlstring = Htmlstring.Replace(">", "");
            Htmlstring = Htmlstring.Replace("\r\n", "");
            Htmlstring = Htmlstring.Replace("\n", "");
            Htmlstring = Htmlstring.Replace(" ", "");
            Htmlstring = Htmlstring.Replace("  ", "");
            //Htmlstring=HttpContext.Current.Server.HtmlEncode(Htmlstring);
            #endregion
            return Htmlstring;
        }

        public static string GetLenStr(this string str, int length)
        {
            if (str != "")
            {
                int i = 0, j = 0;
                foreach (char chr in str)
                {
                    if ((int)chr > 127)
                    {
                        i += 2;
                    }
                    else
                    {
                        i++;
                    }
                    if (i > length)
                    {
                        str = str.Substring(0, j) + "...";
                        break;
                    }
                    j++;
                }

            }
            return str;

        }

        public static string DelHTML(this string Htmlstring)
        {
            Htmlstring = DeleteHtml(Htmlstring);
            return Htmlstring;
        }

        public static string DeleteHTML(string Htmlstring)
        {
            string temp = DeleteHtml(Htmlstring);
            return temp;
        }


        /// <summary>
        /// 检查字符串最大长度，返回指定长度的串
        /// </summary>
        /// <param name="sqlInput">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>			
        public static string SqlText(this string sqlInput, int maxLength)
        {
            if (sqlInput != null && sqlInput != string.Empty)
            {
                if (sqlInput.Length > maxLength)//按最大长度截取字符串
                    sqlInput = sqlInput.Substring(0, maxLength);
            }
            return sqlInput;
        }

        //字符串清理
        public static string InputText(this string inputString, int maxLength)
        {
            StringBuilder retVal = new StringBuilder();

            // 检查是否为空
            if ((inputString != null) && (inputString != String.Empty))
            {
                //检查长度
                if (inputString.Length > maxLength)
                    inputString = inputString.Substring(0, maxLength);

                //替换危险字符
                for (int i = 0; i < inputString.Length; i++)
                {
                    switch (inputString[i])
                    {
                        case '"':
                            retVal.Append("&quot;");
                            break;
                        case '<':
                            retVal.Append("&lt;");
                            break;
                        case '>':
                            retVal.Append("&gt;");
                            break;
                        default:
                            retVal.Append(inputString[i]);
                            break;
                    }
                }
                retVal.Replace("'", " ");// 替换单引号
            }
            return retVal.ToString().Trim();

        }
        /// <summary>
        /// 转换成 HTML code
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>string</returns>
        public static string Encode(this string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }
        /// <summary>
        ///解析html成 普通文本
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>string</returns>
        public static string Decode(this string str)
        {
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }

        public static string SqlTextClear(this string sqlText)
        {
            if (sqlText == null)
            {
                return null;
            }
            if (sqlText == "")
            {
                return "";
            }
            sqlText = sqlText.Replace(",", "");//去除,
            sqlText = sqlText.Replace("<", "");//去除<
            sqlText = sqlText.Replace(">", "");//去除>
            sqlText = sqlText.Replace("--", "");//去除--
            sqlText = sqlText.Replace("'", "");//去除'
            sqlText = sqlText.Replace("\"", "");//去除"
            sqlText = sqlText.Replace("=", "");//去除=
            sqlText = sqlText.Replace("%", "");//去除%
            sqlText = sqlText.Replace(" ", "");//去除空格
            return sqlText;
        }
        #endregion

        #region 是否由特定字符组成
        public static bool IsContainSameChar(this string strInput)
        {
            string charInput = string.Empty;
            if (!string.IsNullOrEmpty(strInput))
            {
                charInput = strInput.Substring(0, 1);
            }
            return IsContainSameChar(strInput, charInput, strInput.Length);
        }

        public static bool IsContainSameChar(string strInput, string charInput, int lenInput)
        {
            if (string.IsNullOrEmpty(charInput))
            {
                return false;
            }
            else
            {
                Regex RegNumber = new Regex(string.Format("^([{0}])+$", charInput));
                //Regex RegNumber = new Regex(string.Format("^([{0}]{{1}})+$", charInput,lenInput));
                Match m = RegNumber.Match(strInput);
                return m.Success;
            }
        }
        #endregion

        #region 检查输入的参数是不是某些定义好的特殊字符：这个方法目前用于密码输入的安全检查
        /// <summary>
        /// 检查输入的参数是不是某些定义好的特殊字符：这个方法目前用于密码输入的安全检查
        /// </summary>
        public static bool IsContainSpecChar(this string strInput)
        {
            string[] list = new string[] { "123456", "654321" };
            bool result = new bool();
            for (int i = 0; i < list.Length; i++)
            {
                if (strInput == list[i])
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 检测是否含有非法字符
        /// </summary>
        /// <param name="badword"></param>
        /// <returns></returns>
        public static bool CheckSafeWord(this string badword)
        {
            string[] bw = new string[15];
            bw[0] = "'"; 
            bw[1] = "\""; 
            bw[2] = ";"; 
            bw[3] = "--"; 
            bw[4] = ","; 
            bw[5] = "!"; 
            bw[6] = "~"; 
            bw[7] = "@"; 
            bw[8] = "#"; 
            bw[9] = "$"; 
            bw[10] = "%"; 
            bw[11] = "^"; 
            bw[12] = "&"; 
            bw[13] = "  "; 
            bw[14] = "_"; 
 
            bool isok = false;
            foreach (string str in bw)
            {
                if (badword.IndexOf(str) > -1)
                {
                    isok = true;
                    return isok;
                }
            }
            return isok;
        } 

        /// <summary>
        /// 根据日期产生数据编码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CreateGUIDByDateTime()
        {

            return DateTime.Now.ToString("yyMMddHHmmss");
        }

        /// <summary>
        /// 根据日期产生数据编码 ToFileTimeUtc
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CreateGUIDByDateTimeFromFileTimeUtc()
        {

            return DateTime.Now.ToFileTimeUtc().ToString();
        }

        public static string CreateGUID(string preFix)
        {
            string guid = preFix.ToUpper();
            string randomCode = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int randomCodeLength = randomCode.Length;
            string datetime = DateTime.Now.ToString("yy{0}{1}MM{2}{3}dd{4}{5}HH{6}{7}mm{8}{9}ss");
            Random random = new Random();
            guid = guid + string.Format(datetime,
                randomCode.Substring(random.Next(randomCodeLength), 1),
                randomCode.Substring(random.Next(randomCodeLength), 1),
                randomCode.Substring(random.Next(randomCodeLength), 1),
                randomCode.Substring(random.Next(randomCodeLength), 1),
                randomCode.Substring(random.Next(randomCodeLength), 1),
                randomCode.Substring(random.Next(randomCodeLength), 1),
                randomCode.Substring(random.Next(randomCodeLength), 1),
                randomCode.Substring(random.Next(randomCodeLength), 1),
                randomCode.Substring(random.Next(randomCodeLength), 1),
                randomCode.Substring(random.Next(randomCodeLength), 1)
                );
            return guid;
        }

        /// <summary>
        /// 生成数据库中数据ID
        /// 于挺
        /// 2013年1月7日 10:40:32
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CreateGUID()
        {
            return Guid.NewGuid().ToString("N").Trim();
        }

        /// <summary>
        /// 分析用户请求是否正常
        /// </summary>
        /// <param name="Str">传入用户提交数据</param>
        /// <returns>返回是否含有SQL注入式攻击代码</returns>
        public static bool ProcessSqlStr(this string Str)
        {
            bool ReturnValue = true;
            try
            {
                if (Str != "" && Str != null)
                {
                    string SqlStr = "";
                    if (SqlStr == "" || SqlStr == null)
                    {
                        SqlStr = "'|and|exec|insert|select|delete|update|count|mid|master|truncate|char|declare";
                    }
                    string[] anySqlStr = SqlStr.Split('|');
                    foreach (string ss in anySqlStr)
                    {
                        if (Str.IndexOf(ss) >= 0)
                        {
                            ReturnValue = false;
                        }
                    }
                }
            }
            catch
            {
                ReturnValue = false;
            }
            return ReturnValue;
        }

        /// <summary>
        /// 把字符串“1,2,3”变成“'1','2','3'”
        /// </summary>
        /// <param name="String"></param>
        /// <returns></returns>
        public static string GetSqlString(string String)
        {
            if (String.Length > 0)
            {
                String = String.Replace(",", "','");
                String = "'" + String + "'";
            }
            return String;
        }

        /// <summary>
        /// 去除Html标签
        /// 日期：2013年10月2日18:15:51
        /// </summary>
        /// <param name="str"></param>
        /// <returns>string</returns>
        public static string ReplaceHTML(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return str.Replace("<", "&lt;").Replace(">", "&gt;").Replace("''", "&quot;").Replace(";", "；");
            }
            return "";
        }

        /// <summary>
        /// 将全角字符转换成半角字符
        /// 日期：2014年12月3日 18:35:34
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string ConvertToDBC(this string inputStr)
        {
            char[] c = inputStr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new String(c);
        }

        /// <summary>
        /// 将半角字符转换成全角字符
        /// 日期：2014年12月3日 18:37:21
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string ConvertToSBC(this string inputStr)
        {
            // 半角转全角：
            char[] c = inputStr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new String(c);
        }



        /// <summary>
        /// 得到随机哈希加密字符串
        /// </summary>
        /// <returns></returns>
        public static string GetSecurity()
        {
            string Security = HashEncoding(GetRandomValue());
            return Security;
        }

        /// <summary>
        /// 得到一个随机数值
        /// </summary>
        /// <returns></returns>
        public static string GetRandomValue()
        {
            Random Seed = new Random();
            string RandomVaule = Seed.Next(1, int.MaxValue).ToString().Trim();
            return RandomVaule;
        }
        /// <summary>
        /// 随机生成字符串
        /// </summary>
        /// <returns></returns>
        public static string GetRandom(int strLength)
        {
            string strSep = ",";
            char[] chrSep = strSep.ToCharArray();
            string strChar = "1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            string[] aryChar = strChar.Split(chrSep, strChar.Length);
            string strRandom = string.Empty;
            Random Rnd = new Random();

            //生成随机字符串
            for (int i = 0; i < strLength; i++)
            {
                strRandom += aryChar[Rnd.Next(aryChar.Length)];
            }
            return strRandom;

        }
        /// <summary>
        /// 哈希加密一个字符串
        /// </summary>
        /// <param name="Security"></param>
        /// <returns></returns>
        private static string HashEncoding(string Security)
        {
            byte[] Value;
            UnicodeEncoding Code = new UnicodeEncoding();
            byte[] Message = Code.GetBytes(Security);
            SHA512Managed Arithmetic = new SHA512Managed();
            Value = Arithmetic.ComputeHash(Message);
            Security = "";
            foreach (byte o in Value)
            {
                Security += (int)o + "O";
            }
            return Security;
        }

        ///   <summary> 
        ///   MD5加密 
        ///   </summary> 
        ///   <param   name= "strString "> </param> 
        ///   <param   name= "strKey "> </param> 
        ///   <param   name= "encoding "> </param> 
        ///   <returns> </returns>  
        public static string MD5(this string str)
        {
            byte[] bValue;
            byte[] bHash;
            string result = null;
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();

            bValue = System.Text.Encoding.UTF8.GetBytes(str);

            bHash = MD5.ComputeHash(bValue);

            MD5.Clear();

            for (int i = 0; i < bHash.Length; i++)
            {
                if (bHash[i].ToString("x").Length == 1)
                {
                    //如果返回来是07这样的值，0会被省掉，所以强制加了一个0   
                    result += "0" + bHash[i].ToString("x");
                }
                else
                {
                    result += bHash[i].ToString("x");
                }

            }
            return result.ToUpper();
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="value">加密字符串</param>
        /// <param name="code">16还是32位</param>
        /// <returns></returns>
        public static String GetMD5(String value)
        {
            //if (code == 16) //16位MD5加密（取32位加密的9~25字符）
            //{
            //    return  FormsAuthentication.HashPasswordForStoringInConfigFile(value, "MD5").ToLower().Substring(8, 16);
            //}
            //else if (code == 32)
            //{
            //    return FormsAuthentication.HashPasswordForStoringInConfigFile(value, "MD5").ToLower();
            //}
            //else
            //{
            //    return "00000000000000000000000000000000";
            //}
            byte[] data = Encoding.UTF8.GetBytes(value);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] OutBytes = md5.ComputeHash(data);
            string OutString = "";
            for (int i = 0; i < OutBytes.Length; i++)
            {
                OutString += OutBytes[i].ToString("x2");
            }
            // return OutString.ToUpper();
            return OutString.ToLower();
        }
        
        /// <summary>
        /// json转xml
        /// </summary>
        /// <param name="sJson"></param>
        /// <returns></returns>
        public static XmlDocument Json2Xml(string sJson)
        {
            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            Dictionary<string, object> Dic =null;
            try
            {
                Dic = (Dictionary<string, object>)oSerializer.DeserializeObject(sJson);
            }
            catch (Exception ex)
            {
                //return null; 
                throw ex;
            }
            
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDec;
            xmlDec = doc.CreateXmlDeclaration("1.0", "gb2312", "yes");
            doc.InsertBefore(xmlDec, doc.DocumentElement);
            XmlElement nRoot = doc.CreateElement("root");
            doc.AppendChild(nRoot);
            foreach (KeyValuePair<string, object> item in Dic)
            {
                XmlElement element = doc.CreateElement(item.Key);
                KeyValue2Xml(element, item);
                nRoot.AppendChild(element);
            }
            return doc;
        }
        private static void KeyValue2Xml(XmlElement node, KeyValuePair<string, object> Source)
        {
            object kValue = Source.Value;
            if (kValue == null)
            {
                kValue = "";
            }

            if (kValue.GetType() == typeof(Dictionary<string, object>))
            {
                foreach (KeyValuePair<string, object> item in kValue as Dictionary<string, object>)
                {
                    XmlElement element = node.OwnerDocument.CreateElement(item.Key);
                    KeyValue2Xml(element, item);
                    node.AppendChild(element);
                }
            }
            else if (kValue.GetType() == typeof(object[]))
            {
                object[] o = kValue as object[];
                for (int i = 0; i < o.Length; i++)
                {
                    XmlElement xitem = node.OwnerDocument.CreateElement("Item");
                    KeyValuePair<string, object> item = new KeyValuePair<string, object>("Item", o[i]);
                    KeyValue2Xml(xitem, item);
                    node.AppendChild(xitem);
                }

            }
            else
            {
                XmlText text = node.OwnerDocument.CreateTextNode(kValue.ToString());
                node.AppendChild(text);
            }
        }
        /// <summary>
        /// SHA256加密，不可逆转
        /// </summary>
        /// <param name="str">string str:被加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string SHA256Encrypt(string str)
        {
            System.Security.Cryptography.SHA256 s256 = new System.Security.Cryptography.SHA256Managed();
            byte[] byte1;
            byte1 = s256.ComputeHash(Encoding.Default.GetBytes(str));
            
            s256.Clear();
            return Convert.ToBase64String(byte1);
        }
        public static string HashHmac(string signatureString, string secretKey)
        {
            var enc = Encoding.UTF8;
            HMACSHA256 hmac = new HMACSHA256(enc.GetBytes(secretKey));
            hmac.Initialize();
            byte[] buffer = enc.GetBytes(signatureString);
            return Convert.ToBase64String(hmac.ComputeHash(buffer));
        }


        //加密鑰匙
        private static byte[] DESKey = new byte[] { 11, 23, 93, 102, 72, 41, 18, 12 };
        //解密鑰匙
        private static byte[] DESIV = new byte[] { 75, 158, 46, 97, 78, 57, 17, 36 };
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(this string strInput)
        {

            return "";
        }


        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns>解密后的字符串</returns>

        public static string Decrypt(this string strInput)
        {
            return "";
        }


        /// <summary>
        /// 根据URL获取域名
        /// 日期：2015年3月1日 14:05:26
        /// </summary>
        /// <param name="Url"></param>
        /// <returns>string</returns>
        public static string ExtractDomainNameFromURL(this string Url)
        {
            if (!Url.Contains("://"))
                Url = "http://" + Url;

            return new Uri(Url).Host;
        }


        /// <summary>
        /// 产生指定长度的短信验证码
        /// 日期：2015年4月21日18:41:03
        /// </summary>
        /// <param name="length"></param>
        /// <returns>string</returns>
        public static string CreateSMSValidateCode(int length)
        {
            string randomVaule = "";
            Random Seed = new Random();
            for (int i = 1; i <= length; i++)
            {
                randomVaule = randomVaule + Seed.Next(0, 9).ToString().Trim();
            }

            return randomVaule;
        }

        /// <summary>
        /// 产生指定个数的英文字符
        /// 日期：2017-12-28 15:19:58
        /// </summary>
        /// <param name="length"></param>
        /// <returns>string</returns>
        public static string CreateRademoCode(int length)
        {
            string randomVaule = "";
            Random Seed = new Random();
            for (int i = 1; i <= length; i++)
            {
                //randomVaule = randomVaule + Seed.Next(0, 25).ToString().Trim();
                randomVaule += gg(Seed.Next(0, 25));
            }

            return randomVaule;
        }

        private static string  gg(int index)
        {
            string code = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return code.Substring(index, 1);
        }


        /// <summary>
        /// 将unicode字符转为中文
        /// </summary>
        /// <param name="str_input"></param>
        /// <returns></returns>
        public static string UnicodeToChar(this string str_input)
        {
            string r = "";
            MatchCollection mc = Regex.Matches(str_input, @"\\u([\w]{2})([\w]{2})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            byte[] bts = new byte[2];
            foreach (Match m in mc)
            {
                bts[0] = (byte)int.Parse(m.Groups[2].Value, NumberStyles.HexNumber);
                bts[1] = (byte)int.Parse(m.Groups[1].Value, NumberStyles.HexNumber);
                r += Encoding.Unicode.GetString(bts);
            }
            return r;

        }

        /// <summary>
        /// 将繁体转为简体
        /// </summary>
        /// <param name="str_input"></param>
        /// <returns></returns>
        public static string SimplifiedChinese(this string str_input)
        {
            str_input = Strings.StrConv(str_input, VbStrConv.SimplifiedChinese, 0);
            return str_input;
        }

        /// <summary>
        /// 将简体转换为繁体
        /// </summary>
        /// <param name="str_input"></param>
        /// <returns></returns>
        public static string TraditionalChinese(this string str_input)
        {
            str_input = Strings.StrConv(str_input, VbStrConv.TraditionalChinese, 0);
            return str_input;
        }


        /// <summary>
        /// 转换汉字为拼音
        /// </summary>
        /// <param name="str_input"></param>
        /// <returns></returns>
        public static string PinYin(this string str_input)
        {
            string r = "";
            foreach (char obj in str_input)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, t.Length - 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r;
        }


        /// <summary>
        /// 转换数字为汉字，如将 5 转换为汉字 五
        /// 于挺
        /// 日期：2017年1月12日 18:02:58
        /// </summary>
        /// <param name="input"></param>
        /// <returns>string</returns>
        public static string GetHanziForNumber(this int input)
        {
            return HanziForNumverList[input];
        }

        /// <summary>
        /// 汉字数字列表,可根据需要自行添加
        /// 于挺
        /// 日期：2017年1月12日 18:03:43
        /// </summary>
        public static List<string> HanziForNumverList
        {
            get
            {
                List<string> list = new List<string>();
                list.Add("〇");
                list.Add("一");
                list.Add("二");
                list.Add("三");
                list.Add("四");
                list.Add("五");
                list.Add("六");
                list.Add("七");
                list.Add("八");
                list.Add("九");
                list.Add("十");
                return list;
            }
        }

        /// <summary>
        /// 生成A-Z、0-9、a-z的随机数
        /// 陈宇龙
        /// 日期：2017年6月6日09:37:45
        /// </summary>
        /// <returns></returns>
        public static string RandomCode(int length)
        {
            string str = "0123456789abcdefghigklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZ";

            if (length < 1)
            {
                length = 4;
            }
            if(length > 61)
            {
                length = 61;
            }

            Random rabdom = new Random();
            string code = "";
            for (int i = 0; i < length; i++)
            {
                code += str.Substring(0 + rabdom.Next(str.Length), 1);
            }
            return code;

        }
        /// <summary>
        /// DateTime时间转换成Unix时间戳
        /// </summary>
        /// <returns></returns>
        public static int ConvertDateTimeInt()
        {

            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(DateTime.Now - startTime).TotalSeconds;
        }
    }
}
