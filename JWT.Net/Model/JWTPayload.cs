/****************************************************************************
*项目名称：JWT.Net.Model
*CLR 版本：4.0.30319.42000
*机器名称：WALLE-PC
*命名空间：JWT.Net.Model
*类 名 称：JWTPayload
*版 本 号：V1.0.0.0
*创建人： yswenli
*电子邮箱：yswenli@outlook.com
*创建时间：2020/8/5 16:56:00
*描述：
*=====================================================================
*修改时间：2020/8/5 16:56:00
*修 改 人： yswenli
*版 本 号： V1.0.0.0
*描    述：
*****************************************************************************/
using JWT.Net.Common;
using JWT.Net.Encryption;
using JWT.Net.Newtonsoft.Json;
using JWT.Net.Newtonsoft.Json.Linq;

using System.Text;

namespace JWT.Net.Model
{
    /// <summary>
    /// JWTPayload
    /// </summary>
    public class JWTPayload<T> : JWTBase where T : class
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T Data
        {
            get; set;
        }

        /// <summary>
        /// JWTPayload
        /// </summary>
        public JWTPayload()
        {

        }

        /// <summary>
        /// JWTPayload
        /// </summary>
        /// <param name="t"></param>
        /// <param name="timeoutSencond"></param>
        public JWTPayload(T t, int timeoutSencond)
        {
            this.Data = t;
            this["exp"] = DateTimeHelper.Now.AddSeconds(timeoutSencond).GetTimeStampStr();
            if (t != null)
            {
                var type = t.GetType();
                if (type != typeof(string))
                {
                    var propertites = type.GetProperties();
                    foreach (var item in propertites)
                    {
                        var val = item.GetValue(t);
                        if (val == null)
                        {
                            this[item.Name] = null;
                        }
                        else
                        {
                            this[item.Name] = val;
                        }
                    }
                }
                else
                {
                    this["jti"] = t.ToString();
                }
            }
        }


        /// <summary>
        /// JWTPayload
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="base64Str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static JWTPayload<T> Parse(string base64Str, Encoding encoding)
        {
            var json = encoding.GetString(Base64URL.Decode(base64Str));

            JObject jsonArray = (JObject)JsonConvert.DeserializeObject(json);

            if (jsonArray.HasValues)
            {
                var payload = new JWTPayload<T>();

                var names = StandardPayload.GetNames();

                foreach (JProperty item in jsonArray.Children())
                {
                    if (item.Value != null)
                    {
                        if (item.Value.Type.ToString().Equals("string", System.StringComparison.InvariantCultureIgnoreCase))
                        {
                            payload[item.Name] = item.Value.ToString();
                        }
                        else
                        {
                            payload[item.Name] = (long)item.Value;
                        }
                    }
                    else
                    {
                        payload[item.Name] = "";
                    }
                }
                return payload;
            }
            return null;
        }

        public string ToBase64Str(Encoding encoding)
        {
            return Base64URL.Encode(encoding.GetBytes(this.ToString()));
        }

        public bool IsExpired()
        {
            return DateTimeHelper.IsExpired(this["exp"].ToString());
        }
    }

    /// <summary>
    /// JWTPayload
    /// </summary>
    public class JWTPayload : JWTPayload<string>
    {
        /// <summary>
        /// JWTPayload
        /// </summary>
        public JWTPayload()
        {

        }

        /// <summary>
        /// JWTPayload
        /// </summary>
        /// <param name="jti"></param>
        /// <param name="timeoutSencond"></param>
        public JWTPayload(string jti, int timeoutSencond) : base(jti, timeoutSencond)
        {

        }
        /// <summary>
        /// JWTPayload
        /// </summary>
        /// <param name="standard"></param>
        public JWTPayload(StandardPayload standard) : this(standard.iss, standard.sub, standard.aud, standard.exp, standard.nbf, standard.iat, standard.jti)
        {

        }


        /// <summary>
        /// JWTPayload
        /// </summary>
        /// <param name="iss">发行人</param>
        /// <param name="sub">主题</param>
        /// <param name="aud">接收方</param>
        /// <param name="exp">过期时间</param>
        /// <param name="nbf">生效时间</param>
        /// <param name="iat">签发时间</param>
        /// <param name="jti">唯一身份标识</param>
        public JWTPayload(string iss, string sub, string aud, long exp, long nbf, long iat, string jti)
        {
            if (!string.IsNullOrEmpty(iss))
                TryAdd("iss", iss);
            if (!string.IsNullOrEmpty(sub))
                TryAdd("sub", sub);
            if (!string.IsNullOrEmpty(aud))
                TryAdd("aud", aud);

            if (exp > 0)
                TryAdd("exp", exp);
            if (nbf > 0)
                TryAdd("nbf", nbf);
            if (iat > 0)
                TryAdd("iat", iat);

            if (!string.IsNullOrEmpty(jti))
                TryAdd("jti", jti);
        }

        public static new JWTPayload Parse(string base64Str, Encoding encoding)
        {
            var json = encoding.GetString(Base64URL.Decode(base64Str));

            JObject jsonArray = (JObject)JsonConvert.DeserializeObject(json);

            if (jsonArray.HasValues)
            {
                var payload = new JWTPayload();

                StringBuilder data = new StringBuilder();
                data.Append("{");

                var names = StandardPayload.GetNames();

                foreach (JProperty item in jsonArray.Children())
                {
                    if (item.Value != null)
                    {
                        if (item.Value.Type.ToString().Equals("string", System.StringComparison.InvariantCultureIgnoreCase))
                        {
                            payload[item.Name] = item.Value.ToString();
                        }
                        else
                        {
                            payload[item.Name] = (long)item.Value;
                        }
                    }
                    else
                    {
                        payload[item.Name] = "";
                    }
                    if (!names.Contains(item.Name, System.StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (item.Value != null)
                        {
                            if (item.Value.Type.ToString().Equals("string", System.StringComparison.InvariantCultureIgnoreCase))
                            {
                                data.Append($"\"{item.Name}\":\"{item.Value}\",");
                            }
                            else
                            {
                                data.Append($"\"{item.Name}\":{item.Value},");
                            }
                        }
                        else
                        {
                            data.Append($"\"{item.Name}\":\"\",");
                        }
                    }
                }
                data.Remove(data.Length - 1, 1);
                data.Append("}");
                payload.Data = data.ToString();
                return payload;
            }
            return null;
        }
    }
}
