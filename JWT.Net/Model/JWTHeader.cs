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
*描述：JWTHeader
*=====================================================================
*修改时间：2020/8/5 16:53:15
*修 改 人： yswenli
*版 本 号： V1.0.0.0
*描    述：JWTHeader
*****************************************************************************/
using System.Text;

using JWT.Net.Common;
using JWT.Net.Encryption;
using JWT.Net.Newtonsoft.Json.Linq;

namespace JWT.Net.Model
{
    /// <summary>
    /// JWTHeader
    /// </summary>
    public class JWTHeader : JWTBase
    {
        /// <summary>
        /// JWTHeader
        /// </summary>
        /// <param name="encryptType"></param>
        public JWTHeader(EncryptType encryptType)
        {
            EncryptType = encryptType;
            TryAdd("alg", encryptType.ToString());
            TryAdd("typ", "JWT");
        }

        /// <summary>
        /// EncryptType
        /// </summary>
        public EncryptType EncryptType
        {
            private set; get;
        }

        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="base64Str"></param>
        /// <param name="encoding"></param>
        /// <param name="encryptType"></param>
        /// <returns></returns>
        public static JWTHeader Parse(string base64Str, Encoding encoding, EncryptType encryptType = EncryptType.HS256)
        {
            var json = encoding.GetString(Base64URL.Decode(base64Str));

            JObject jsonArray = JsonHelper.ToObj(json);

            if (jsonArray.HasValues)
            {
                var header = new JWTHeader(encryptType);

                foreach (JProperty item in jsonArray.Children())
                {
                    header[item.Name] = item.Value?.ToString();
                }
                return header;
            }
            return null;
        }

        /// <summary>
        /// ToBase64Str
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public string ToBase64Str(Encoding encoding)
        {
            return Base64URL.Encode(encoding.GetBytes(this.ToString()));
        }
    }
}
