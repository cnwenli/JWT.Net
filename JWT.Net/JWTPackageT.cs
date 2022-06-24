/****************************************************************************
*项目名称：JWT.Net
*CLR 版本：4.0.30319.42000
*机器名称：WALLE-PC
*命名空间：JWT.Net
*类 名 称：JWTPackageT
*版 本 号：V1.0.0.0
*创建人： yswenli
*电子邮箱：yswenli@outlook.com
*创建时间：2020/8/6 17:22:42
*描述：
*=====================================================================
*修改时间：2020/8/6 17:22:42
*修 改 人： yswenli
*版 本 号： V1.0.0.0
*描    述：
*****************************************************************************/
using System;
using System.Text;

using JWT.Net.Encryption;
using JWT.Net.Exceptions;
using JWT.Net.Model;

namespace JWT.Net
{
    /// <summary>
    /// JWTPackage
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JWTPackage<T> where T : class
    {

        /// <summary>
        /// Bearer+空格
        /// </summary>
        public const string Prex = "Bearer ";

        /// <summary>
        /// http header Authorization
        /// </summary>
        public const string HEADERKEY = "Authorization";

        protected Encoding _encoding;

        protected string _password;

        EncryptType _encryptType;

        /// <summary>
        /// 头部
        /// </summary>
        public JWTHeader Header { get; set; }
        /// <summary>
        /// 负载
        /// </summary>
        public JWTPayload<T> Payload { get; set; }

        /// <summary>
        /// 包内容
        /// </summary>
        public string PackageBase64
        {
            get
            {
                var headerStr = Header.ToBase64Str(_encoding);

                var payloadStr = Payload.ToBase64Str(_encoding);

                return $"{headerStr}.{payloadStr}";
            }
        }

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature
        {
            get
            {
                return $"{EncrypteFactory.Create(_encryptType).Sign(_encoding.GetBytes(_password), _encoding.GetBytes(PackageBase64))}";
            }
        }

        /// <summary>
        /// JWT包
        /// </summary>
        public JWTPackage()
        {
            _encryptType = EncryptType.HS256;

            Header = new JWTHeader(_encryptType);
        }

        /// <summary>
        /// JWT包
        /// </summary>
        /// <param name="t">数据</param>
        /// <param name="timeOutSenconds">过期时间</param>
        /// <param name="password">密码</param>
        public JWTPackage(T t, int timeOutSenconds, string password, EncryptType encryptType = EncryptType.HS256) :
            this(new JWTPayload<T>(t, timeOutSenconds), password, Encoding.UTF8)
        { }


        /// <summary>
        /// JWT包
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="password"></param>
        /// <param name="encoding"></param>
        public JWTPackage(JWTPayload<T> payload, string password, Encoding encoding, EncryptType encryptType = EncryptType.HS256)
            : this(new JWTHeader(encryptType), payload, password, encoding)
        {

        }
        /// <summary>
        /// JWT包
        /// </summary>
        /// <param name="header"></param>
        /// <param name="payload"></param>
        /// <param name="password"></param>
        /// <param name="encoding"></param>
        public JWTPackage(JWTHeader header, JWTPayload<T> payload, string password, Encoding encoding)
        {
            _encoding = encoding;

            _password = password;

            Header = header;

            Payload = payload;

            _encryptType = header.EncryptType;
        }

        /// <summary>
        /// 获取http header键值对
        /// </summary>
        /// <returns></returns>
        public ValueTuple<string, string> GetAuthorizationBearer()
        {
            return new ValueTuple<string, string>(JWTPackage.HEADERKEY, $"{Prex}{GetBearerToken()}");
        }

        /// <summary>
        /// 解析为JWTPackage
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="password"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static JWTPackage<T> Parse(string signature, string password, Encoding encoding)
        {
            if (string.IsNullOrEmpty(signature)) throw new IllegalTokenException("Parameter cannot be empty");

            var arr = signature.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);

            if (arr == null || arr.Length != 3) throw new IllegalTokenException("JWT Package failed to parse signature, signature format is incorrect");

            JWTPackage<T> jwtPackage;

            try
            {
                jwtPackage = new JWTPackage<T>(JWTHeader.Parse(arr[0], encoding), JWTPayload<T>.Parse(arr[1], encoding), password, encoding);
            }
            catch (Exception ex)
            {
                throw new IllegalTokenException("JWT Package failed to parse signature, signature format is incorrect", ex);
            }

            if (jwtPackage == null) throw new IllegalTokenException("JWT Package failed to parse signature, signature format is incorrect");

            if (jwtPackage.GetToken() != signature) throw new SignatureVerificationException("JWT Package failed to parse signature");

            if (jwtPackage.Payload.IsExpired()) throw new TokenExpiredException("The token of jwtpackage has expired");

            return jwtPackage;
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            return $"{PackageBase64}.{Signature}";
        }

        /// <summary>
        /// 获取Token,带格式：Bearer+空格+token
        /// </summary>
        public string GetBearerToken()
        {
            return $"{Prex}{GetToken()}";
        }


        /// <summary>
        /// 解析为JWTPackage
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static JWTPackage<T> Parse(string signature, string password)
        {
            return Parse(signature, password, Encoding.UTF8);
        }

        /// <summary>
        /// 解析为JWTPackage
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="password"></param>
        /// <param name="package"></param>
        /// <returns></returns>
        public static bool TryParse(string signature, string password, out JWTPackage<T> package)
        {
            package = null;
            try
            {
                package = Parse(signature, password);
                if (package != null)
                    return true;
            }
            catch { }
            return false;
        }

    }
}
