using CommForAdolph.ChartExChange;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CommForAdolph.TypeExtend
{
    public static class TypeExtend
    {
        #region 集合扩展
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enum"></param>
        /// <param name="mapFunction"></param>
        public static void ForEach<T>(this IEnumerable<T> @enum, Action<T> mapFunction)
        {
            foreach (var item in @enum) mapFunction(item);
        }
        #endregion


        #region 字符串扩展
        /// <summary>
        /// 字符串格式化传参
        /// </summary>
        /// <param name="text"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatWith(this string text, params object[] args)
        {
            return string.Format(text, args);
        }

        /// <summary>
        /// 取字符串首字母
        /// </summary>
        /// <param name="text"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string ToPY(this string text)
        {
            return PinYin.GetCodstring(text);
        }

        /// <summary>
        /// 字符串转换为枚举
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// 正则表达式扩展
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsMatch(this string s, string pattern)
        {
            if (s == null) return false;
            else return Regex.IsMatch(s, pattern);
        }

        /// <summary>
        /// 正则表达式扩展
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string Match(this string s, string pattern)
        {
            if (s == null) return "";
            return Regex.Match(s, pattern).Value;
        }

        /// <summary>
        /// 转换为数字
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt(this string s, int defaultValue)
        {
            int i = defaultValue;
            int.TryParse(s, out i);
            return i;
        }

        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToInt(this string s, DateTime defaultValue)
        {
            DateTime i = defaultValue;
            DateTime.TryParse(s, out i);
            return i;
        }


        public static bool IsNull(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// IsNull
        /// </summary>
        /// <param name="s"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static string IsNull(this string s, string s2)
        {
            //扩展方法不会NullReferenceException
            if (string.IsNullOrEmpty(s))
                return s2;
            return s;

        }

        public static long ToLong0(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            long result;
            if (long.TryParse(value, out result))
                return result;
            else
                return 0;

        }

        public static long ToDouleLong(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            try
            {
                return Convert.ToInt64(Math.Round(Convert.ToDecimal(value), 0));
            }
            catch
            {
                return 0;
            }
        }


        public static int ToInt(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            int vl = 0;
            int.TryParse(value, out vl);
            return vl;
        }
        /// <summary>
        /// 返回短整型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static short ToShort(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            short vl = 0;
            short.TryParse(value, out vl);
            return vl;
        }
        /// <summary>
        /// 返回为int? 类型扩展
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int? ToNullInt(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            return int.Parse(value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToLongTime(this string value)
        {
            string basedate = "2014-1-1";
            if (value.IsNull())
                value = basedate;
            value = value.Trim();
            try
            {
                return DateTime.Parse(basedate + " " + value);
            }
            catch
            {
                return DateTime.Parse(basedate);
            }
        }
        /// <summary>
        /// 转换位可空日期格式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string value)
        {
            if (value.IsNull())
                return null;

            try
            {
                DateTime? dt = DateTime.Parse(value);
                if (dt.Value.Year < 1573 || dt.Value.Year > 9999)
                {
                    return null;
                }
                else
                {
                    return dt;
                }

            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 转换位日期格式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime0(this string value)
        {
            if (value.IsNull())
                value = "2000-1-1";
            value = value.Trim();

            if ((value.IndexOf('-') < 0 && value.IndexOf('/') < 0) && value.Length >= 8)
            {
                value = value.Insert(4, "-");
                value = value.Insert(7, "-");
            }
            try
            {
                return DateTime.Parse(value);
            }
            catch
            {
                return DateTime.Parse("2000-1-1");
            }
        }

        //public static DateTime? ToNullDateTime(this string value)
        //{            
        //    return DateTime.Parse(value);
        //}


        /// <summary>
        ///  分析ajaxRequest请求的参数，默认&为分隔符
        ///  例如：name1=value1&name2=value2
        /// </summary>
        /// <param name="value">ajaxRequest 请求回发的值</par   am>
        /// <returns>名值对字典</returns>
        public static StringDictionary AnalysisAjaxRequestArgument(this string value)
        {
            return AnalysisAjaxRequestArgument(value, '&');
        }


        /// <summary>
        /// 把int? 类型转换为字符串，有值为该值，否则为字符串空
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToString(this int? value)
        {
            return value.HasValue ? value.Value.ToString() : string.Empty;
        }

        /// <summary>
        /// 删除第一次匹配的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="removeString"></param>
        /// <returns></returns>
        public static string RemoveFirstMatchString(this string str, string removeString)
        {
            if (str.IndexOf(removeString) > -1)
            {
                return str.Remove(str.IndexOf(removeString), removeString.Length);
            }
            return str;
        }

        /// <summary>
        /// 去降空格并转换为大写.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimAndUpper(this string str)
        {
            if (str.IsNull()) return str;
            return str.Trim().ToUpper();

        }
        /// <summary>
        /// 去降空格并转换为小写.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimAndLower(this string str)
        {
            if (str.IsNull()) return str;
            return str.Trim().ToLower();

        }
        /// <summary>
        /// 截断
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubStr(this string str, int length)
        {
            if (str.Length <= length) return str;
            return str.SubStr(length, true);
        }
        /// <summary>
        /// 截断拼接
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <param name="isSpilth"></param>
        /// <returns></returns>
        public static string SubStr(this string str, int length, bool isSpilth)
        {
            if (str.IsNull()) return string.Empty;
            if (str.Length <= length)
                return str;
            return str.Substring(0, length) + (isSpilth ? "...." : "");
        }

        /// <summary>
        /// 是否包含字符串
        /// </summary>
        /// <param name="inputstr"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ContainsNull(this string inputstr, string str)
        {
            try
            {
                if (inputstr.IsNull()) return false;
                return inputstr.Contains(str);
            }
            catch
            {
                return false;

            }
        }

        public static bool ToBoolString(this string inputstr)
        {
            try
            {
                return inputstr == "1" ? true : false;
            }
            catch
            {
                return false;

            }
        }
        public static string ToBoolString(this bool inputstr)
        {
            try
            {
                return inputstr ? "1" : "0";
            }
            catch
            {
                return "0";
            }
        }
        ///// <summary>
        ///// MD5加密32位
        ///// </summary>
        ///// <param name="txt"></param>
        ///// <returns></returns>
        //public static string ToMD5Encode32(this string txt)
        //{
        //    if (txt.IsNull()) return string.Empty;
        //    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txt, "MD5");

        //}
        /// <summary>
        /// MD5加密16位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToMD5Encode16(this string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }

        /// <summary>
        /// 转换位字符串集合
        /// </summary>
        /// <param name="str"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string[] ToStrArray(this string str, char split)
        {
            if (str.IsNull()) return new string[] { };

            return str.Split(new char[] { split }, StringSplitOptions.RemoveEmptyEntries);
        }

        //public static System.Drawing.Color getRgbColor(this string name)
        //{
        //    if (name.IsNull() || name.Length != 8)
        //    {
        //        name = "ffffffff";
        //    }
        //    int a = Convert.ToInt32(name.Substring(0, 2), 16);
        //    int r = Convert.ToInt32(name.Substring(2, 2), 16);
        //    int g = Convert.ToInt32(name.Substring(4, 2), 16);
        //    int b = Convert.ToInt32(name.Substring(6, 2), 16);
        //    return System.Drawing.Color.FromArgb(a, r, g, b);
        //}

        /// <summary>
        /// 是否整形
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsInteger(this string s)
        {
            string pattern = @"^-?\d+$";
            return Regex.IsMatch(s, pattern);
        }
        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string value)
        {
            return Regex.IsMatch(value, @"^(-?\d+)(\.\d+)?$");
        }



        /// <summary>
        /// 是否一个字符串是数字
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumber(this string s)
        {
            if (s.IsNull()) return false;
            long result = 0;
            return (long.TryParse(s, out result));
        }


        /// <summary>
        /// 每隔2位转换为16进制数字
        /// 例如0230900005 输出结果为 02 1E 5A 00 05
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToHex(this string value)
        {
            if (!value.IsNumber()) return null;
            if ((value.Length % 2) != 0)
                value = "0" + value;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < value.Length; i = i + 2)
            {
                string s = value.Substring(i, 2);
                sb.Append(int.Parse(s).ToString("X2"));
                //sb.Append(Convert.ToString(int.Parse(s), 16).PadLeft(2,'0').ToUpper());
            }
            return sb.ToString();
        }
        /// <summary>
        /// ToHex的逆向函数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FromHex(this string value)
        {
            if ((value.Length % 2) != 0)
                value = "0" + value;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < value.Length; i = i + 2)
            {
                string aa = value.Substring(i, 2);
                string s = Convert.ToUInt16(aa, 16).ToString();
                if (s.Length > 2)
                    throw new ApplicationException("FromHex函数溢出，参数为" + value);
                sb.Append(s.PadLeft(2, '0'));
            }
            return sb.ToString().ToUpper();
        }

        public static string ToHexNumber32(this int value)
        {
            return Convert.ToString(value, 16).ToUpper();
        }
        public static string ToHexNumber64(this long value)
        {
            return Convert.ToString(value, 16).ToUpper();
        }
        public static int FromHexNumber32(this string value)
        {
            return Convert.ToInt32(value, 16);
        }
        public static long FromHexNumber64(this string value)
        {
            return Convert.ToInt64(value, 16);
        }
        /// <summary>
        /// 忽略大小写判等
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetStr"></param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string value, string targetStr)
        {
            return string.Compare(value, targetStr, true) == 0 ? true : false;
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNull(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static double ToDouble(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0.00;
            double result;
            if (double.TryParse(value, out result))
                return result;
            else
                return 0.00;
        }

        public static long? ToLong(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            long result;
            if (long.TryParse(value, out result))
                return result;
            else
                return null;

        }
        /// <summary>
        /// 转换为Decimal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal? ToDecimal(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            decimal result = 0;
            if (decimal.TryParse(value, out result))
                return result;
            else
                return 0;
        }
        /// <summary>
        /// 判断字符串非空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToNullString(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";
            else
                return value;
        }

        /// <summary>
        /// 得到AJAX请求参数
        /// </summary>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static StringDictionary AnalysisAjaxRequestArgument(this string value, char separator)
        {
            StringDictionary argumentPairs = new StringDictionary();
            if (value.IsNull()) return argumentPairs;
            string[] firstSpilt = value.Split(separator);
            foreach (string frist in firstSpilt)
            {
                string[] secondSpilt = frist.Split('=');
                if (secondSpilt.Length > 1)
                {
                    argumentPairs.Add(secondSpilt[0], secondSpilt[1]);
                }
                else if (secondSpilt.Length == 1)
                {
                    argumentPairs.Add(secondSpilt[0], string.Empty);
                }
            }
            return argumentPairs;
        }
        /// <summary>
        /// 去掉所有空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimAllSpace(this string str)
        {
            if (str.IsNull()) return str;

            return Regex.Replace(str, @"\s*", "", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 字符串转换为bool
        /// </summary>
        /// <param name="inputstr"></param>
        /// <returns></returns>
        public static bool ToBool(this string inputstr)
        {
            try
            {
                return bool.Parse(inputstr);
            }
            catch
            {
                return false;

            }
        }
        /// <summary>
        /// MD5编码
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string ToMD5Encode32(this string txt)
        {
            if (txt.IsNull()) return string.Empty;
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txt, "MD5");

        }
        /// <summary>
        /// 字符串按指定字符分割返回List
        /// </summary>
        /// <param name="str"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static List<string> ToStrList(this string str, char split)
        {
            if (str.IsNull()) return new List<string>();
            return new List<string>(str.ToStrArray(split));
        }
        /// <summary>
        /// 匹配HTML中的IMG标签并返回集合
        /// </summary>
        /// <param name="sHtmlText"></param>
        /// <returns></returns>
        public static string[] GetHtmlImageUrlList(this string sHtmlText)
        {
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>
                                        [^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            MatchCollection matches = regImg.Matches(sHtmlText);

            int i = 0;
            string[] sUrlList = new string[matches.Count];

            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;

            return sUrlList;
        }

        public static string StripHT(this string strHtml)
        {
            strHtml = Regex.Replace(strHtml, @"[^\u4e00-\u9fa5]+", "");
            return strHtml;
        }
        /// <summary>
        /// IP字符串验证
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(this string ip)
        {
            string num = "(25[0-5]|2[0-4]//d|[0-1]//d{2}|[1-9]?//d)";
            return Regex.IsMatch(ip, ("^" + num + "//." + num + "//." + num + "//." + num + "$"));
        }
        /// <summary>
        /// 邮箱验证
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(this string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email,
                    @"^([/w-/.]+)@((/[[0-9]{1,3}/.[0-9] {1,3}/.[0-9]{1,3}/.)|(([/w-]+/.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(/)?]$");

        }
        /// <summary>
        /// 电话号码验证
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool IsPhone(this string phone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^(0|86|17951)?(13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}$");

        }
        /// <summary>
        /// 身份证验证
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsICard(this string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email,
                    @"(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)");

        }
        /// <summary>
        /// 判断字符是否为URL地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsUrl(this string url)
        {

            return System.Text.RegularExpressions.Regex.IsMatch(url, @"http(s)?://([/w-]+/.)+[/w-]+(/[/w- ./?%&=]*)?");

        }

        public static string WipeScript(this string html)
        {
            html = html.Trim();
            if (string.IsNullOrEmpty(html))
                return string.Empty;
            html = Regex.Replace(html, "<head[^>]*>(?:.|[\r\n])*?</head>", "");
            html = Regex.Replace(html, "<script[^>]*>(?:.|[\r\n])*?</script>", "");
            html = Regex.Replace(html, "<style[^>]*>(?:.|[\r\n])*?</style>", "");
            html = Regex.Replace(html, "[\\s]{2,}", " "); //两个或多个空格替换为一个
            html = Regex.Replace(html, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", ""); //<br>
            html = Regex.Replace(html, "\\&[a-zA-Z]{1,10};", "");
            html = Regex.Replace(html, "<[^>]*>", "");

            html = Regex.Replace(html, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", ""); //&nbsp;
            html = Regex.Replace(html, "<(.|\\n)*?>", string.Empty); //其它任何标记

            html = html.Replace("'", "''");
            html = html.Replace("\r\n", "");
            html = html.Replace("  ", "");
            html = html.Replace("\t", "");

            return html;
        }
        /// <summary>
        /// 转换为字符分割，数据库查询格式“‘1’，‘2’，‘3’”
        /// </summary>
        /// <param name="sString"></param>
        /// <param name="sChar"></param>
        /// <returns></returns>
        public static string ToWhereString(this string sString, char sChar = ',')
        {
            string[] stringCollection = sString.Split(new char[] { sChar }, StringSplitOptions.RemoveEmptyEntries);
            string str = string.Empty;
            foreach (string item in stringCollection)
            {
                str += "'";
                str += item.ToString();
                str += "',";
            }
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Remove(str.Length - 1);
            }
            if (string.IsNullOrEmpty(str))
                return "''";
            return str;
        }
        /// <summary>
        ///  获取byte长度
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int GetByteLength(this string s)
        {
            if (s.IsNull()) return 0;
            return Encoding.GetEncoding("gb2312").GetByteCount(s);
        }
        /// <summary>
        /// 得到string在指定编码下的byte[]长度
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static int GetByteLength(this string s, Encoding encoding)
        {
            return encoding.GetByteCount(s);
        }
        #endregion


        #region 数字扩展
        /// <summary> 
        /// Kilobytes 
        /// </summary> 
        /// <param name="value"></param> 
        /// <returns></returns> 
        public static int KB(this int value)
        {
            return value * 1024;
        }

        /// <summary> 
        /// Megabytes 
        /// </summary> 
        /// <param name="value"></param> 
        /// <returns></returns> 
        public static int MB(this int value)
        {
            return value.KB() * 1024;
        }

        /// <summary> 
        /// Gigabytes 
        /// </summary> 
        /// <param name="value"></param> 
        /// <returns></returns> 
        public static int GB(this int value)
        {
            return value.MB() * 1024;
        }

        /// <summary> 
        /// Terabytes 
        /// </summary> 
        /// <param name="value"></param> 
        /// <returns></returns> 
        public static long TB(this int value)
        {
            return (long)value.GB() * (long)1024;
        }
        #endregion


        #region 百分比扩展

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this int number, int percent)
        {
            return (decimal)(number * percent / 100);
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this int position, int total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = (decimal)position / (decimal)total * 100;
            return result;
        }
        public static decimal PercentOf(this int? position, int total)
        {
            if (position == null) return 0;

            decimal result = 0;
            if (position > 0 && total > 0)
                result = (decimal)((decimal)position / (decimal)total * 100);
            return result;
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this int number, float percent)
        {
            return (decimal)(number * percent / 100);
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this int position, float total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = (decimal)((decimal)position / (decimal)total * 100);
            return result;
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this int number, double percent)
        {
            return (decimal)(number * percent / 100);
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this int position, double total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = (decimal)((decimal)position / (decimal)total * 100);
            return result;
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this int number, decimal percent)
        {
            return (decimal)(number * percent / 100);
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this int position, decimal total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = (decimal)position / (decimal)total * 100;
            return result;
        }

        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this int number, long percent)
        {
            return (decimal)(number * percent / 100);
        }

        /// <summary>
        /// Percentage of the number.
        /// </summary>
        /// <param name="percent">The percent</param>
        /// <param name="number">The Number</param>
        /// <returns>The result</returns>
        public static decimal PercentOf(this int position, long total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = (decimal)position / (decimal)total * 100;
            return result;
        }

        #endregion

    }
}
