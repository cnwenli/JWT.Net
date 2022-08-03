/****************************************************************************
*项目名称：JWT.Net.Common
*CLR 版本：4.0.30319.42000
*机器名称：WALLE-PC
*命名空间：JWT.Net.Common
*类 名 称：DateTimeHelper
*版 本 号：V1.0.0.0
*创建人： yswenli
*电子邮箱：yswenli@outlook.com
*创建时间：2020/8/5 17:42:06
*描述：
*=====================================================================
*修改时间：2020/8/5 17:42:06
*修 改 人： yswenli
*版 本 号： V1.0.0.0
*描    述：
*****************************************************************************/
using System;
using System.Collections.Generic;

namespace JWT.Net.Common
{
    public static class DateTimeHelper
    {
        public static DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        public static long GetTimeStamp(this DateTime dt, bool onlySeconds = true)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            DateTime nowTime = dt;
            if (onlySeconds)
            {
               return (long)Math.Round((nowTime - startTime).TotalSeconds, MidpointRounding.AwayFromZero);
            }
            else
            {
                return (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
            }
        }

        public static string GetTimeStampStr(this DateTime dt)
        {
            return dt.GetTimeStamp().ToString();
        }

        /// <summary>
        /// 是否已过期
        /// </summary>
        /// <param name="unixTickStr"></param>
        /// <returns></returns>
        public static bool IsExpired(string unixTickStr)
        {
            if (string.IsNullOrEmpty(unixTickStr)) return true;

            if (long.TryParse(unixTickStr, out long unixTick))
            {
                if (unixTick >= Now.GetTimeStamp())
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="str"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static bool Contains(this IEnumerable<string> strs, string str, StringComparison comparison)
        {
            if (string.IsNullOrEmpty(str)) return false;
            if (strs == null) return false;
            foreach (var item in strs)
            {
                if (!string.IsNullOrEmpty(item) && str.Equals(item, comparison)) return true;
            }
            return false;
        }
    }
}
