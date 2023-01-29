/****************************************************************************
*Copyright (c) 2023 RiverLand All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：JWT.Net.Common
*文件名： JsonHelper
*版本号： V1.0.0.0
*唯一标识：400078d6-dff0-4426-b153-3a2e6a75aa47
*当前的用户域：WALLE
*创建人： WALLE
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2023/1/29 15:51:53
*描述：json工具
*
*=================================================
*修改标记
*修改时间：2023/1/29 15:51:53
*修改人： yswen
*版本号： V1.0.0.0
*描述：json工具
*
*****************************************************************************/
using JWT.Net.Newtonsoft.Json;
using JWT.Net.Newtonsoft.Json.Linq;

namespace JWT.Net.Common
{
    /// <summary>
    /// json工具
    /// </summary>
    internal static class JsonHelper
    {
        static JsonSerializerSettings _settings;

        /// <summary>
        /// json工具
        /// </summary>
        static JsonHelper()
        {
            _settings = new JsonSerializerSettings();
            _settings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
            _settings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            _settings.NullValueHandling = NullValueHandling.Ignore;
            _settings.DefaultValueHandling = DefaultValueHandling.Ignore;
            _settings.Formatting = Formatting.None;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            if (obj == null) return null;
            return JsonConvert.SerializeObject(obj, _settings);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static JObject ToObj(this string json)
        {
            if (string.IsNullOrEmpty(json)) return null;
            return (JObject)JsonConvert.DeserializeObject(json, _settings);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ToObj<T>(this string json)
        {
            if (string.IsNullOrEmpty(json)) return default;
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }
    }
}
