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
using JWT.Net.Encryption;
using JWT.Net.Exceptions;
using JWT.Net.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.Net
{
    /// <summary>
    /// JWTPackage
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JWTPackage<T> where T : class
    {
        public const string Prex = "Bearer ";

        protected Encoding _encoding;

        protected string _password;

        /// <summary>
        /// 头部
        /// </summary>
        public JWTHeader Header { get; set; } = new JWTHeader();
        /// <summary>
        /// 负载
        /// </summary>
        public JWTPayload<T> Payload { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Signature
        {
            get
            {
                var headerStr = Header.ToBase64Str(_encoding);

                var payloadStr = Payload.ToBase64Str(_encoding);

                var packageStr = $"{headerStr}.{payloadStr}";

                return $"{packageStr}.{Base64URL.Encode(HMACSHA256.Sign(_encoding.GetBytes(_password), _encoding.GetBytes(packageStr)))}";
            }
        }

        

        public JWTPackage() { }

        /// <summary>
        /// JWT包
        /// </summary>
        /// <param name="t">数据</param>
        /// <param name="timeOutSenconds">过期时间</param>
        /// <param name="password">密码</param>
        public JWTPackage(T t, int timeOutSenconds, string password) : this(new JWTPayload<T>(t, timeOutSenconds), password, Encoding.UTF8) { }


        /// <summary>
        /// JWT包
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="password"></param>
        /// <param name="encoding"></param>
        public JWTPackage(JWTPayload<T> payload, string password, Encoding encoding)
        {
            _encoding = encoding;

            _password = password;

            Payload = payload;
        }

        public JWTPackage(JWTHeader header, JWTPayload<T> payload, string password, Encoding encoding)
        {
            _encoding = encoding;

            _password = password;

            Header = header;

            Payload = payload;
        }

        /// <summary>
        /// 获取http header键值对
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<string, string> GetAuthorizationBearer()
        {
            return new KeyValuePair<string, string>("Authorization", $"{Prex}{Signature}");
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

            if (jwtPackage.Signature != signature) throw new SignatureVerificationException("JWT Package failed to parse signature");

            if (jwtPackage.Payload.IsExpired()) throw new TokenExpiredException("The token of jwtpackage has expired");

            return jwtPackage;
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

    }
}
