/****************************************************************************
*项目名称：JWT.Net.Model
*CLR 版本：4.0.30319.42000
*机器名称：WALLE-PC
*命名空间：JWT.Net.Model
*类 名 称：JWTHeader
*版 本 号：V1.0.0.0
*创建人： yswenli
*电子邮箱：yswenli@outlook.com
*创建时间：2020/8/5 16:53:15
*描述：
*=====================================================================
*修改时间：2020/8/5 16:53:15
*修 改 人： yswenli
*版 本 号： V1.0.0.0
*描    述：
*****************************************************************************/
using JWT.Net.Encryption;
using JWT.Net.Newtonsoft.Json;
using JWT.Net.Newtonsoft.Json.Linq;
using System.Text;

namespace JWT.Net.Model
{
    public class JWTHeader : JWTBase
    {
        public JWTHeader()
        {
            TryAdd("alg", "HS256");
            TryAdd("typ", "JWT.Standard");
        }

        public static JWTHeader Parse(string base64Str, Encoding encoding)
        {
            var json = encoding.GetString(Base64URL.Decode(base64Str));

            JObject jsonArray = (JObject)JsonConvert.DeserializeObject(json);

            if (jsonArray.HasValues)
            {
                var header = new JWTHeader();

                foreach (JProperty item in jsonArray.Children())
                {
                    header[item.Name] = item.Value?.ToString();
                }
                return header;
            }
            return null;
        }

        public string ToBase64Str(Encoding encoding)
        {
            return Base64URL.Encode(encoding.GetBytes(this.ToString()));
        }
    }
}
