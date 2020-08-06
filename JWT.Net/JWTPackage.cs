/****************************************************************************
*项目名称：JWT.Net.Model
*CLR 版本：4.0.30319.42000
*机器名称：WALLE-PC
*命名空间：JWT.Net.Model
*类 名 称：JWTPackage
*版 本 号：V1.0.0.0
*创建人： yswenli
*电子邮箱：yswenli@outlook.com
*创建时间：2020/8/5 17:08:27
*描述：
*=====================================================================
*修改时间：2020/8/5 17:08:27
*修 改 人： yswenli
*版 本 号： V1.0.0.0
*描    述：
*****************************************************************************/
using JWT.Net.Common;
using JWT.Net.Encryption;
using JWT.Net.Exceptions;
using JWT.Net.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.Net
{
    /// <summary>
    /// JWT包
    /// </summary>
    public class JWTPackage
    {
        public const string Prex = "Bearer ";
        /// <summary>
        /// 头部
        /// </summary>
        public JWTHeader Header { get; set; } = new JWTHeader();
        /// <summary>
        /// 负载
        /// </summary>
        public JWTPayload Payload { get; set; }
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

        Encoding _encoding;

        string _password;

        public JWTPackage() { }

        /// <summary>
        /// JWT包
        /// </summary>
        /// <param name="jti">唯一身份标识</param>
        /// <param name="timeOutSenconds">过期时间</param>
        /// <param name="password">密码</param>
        public JWTPackage(string jti, int timeOutSenconds, string password) :
            this(string.Empty, string.Empty, string.Empty, DateTimeHelper.Now.AddSeconds(timeOutSenconds).GetTimeStampStr(), DateTimeHelper.Now.GetTimeStampStr(), DateTimeHelper.Now.GetTimeStampStr(), jti, password)
        { }

        /// <summary>
        /// JWT包
        /// </summary>
        /// <param name="iss">发行人</param>
        /// <param name="sub">主题</param>
        /// <param name="aud">接收方</param>
        /// <param name="exp">过期时间</param>
        /// <param name="nbf">生效时间</param>
        /// <param name="iat">签发时间</param>
        /// <param name="jti">唯一身份标识</param>
        /// <param name="password">密码</param>
        public JWTPackage(string iss, string sub, string aud, string exp, string nbf, string iat, string jti, string password) : this(new JWTPayload(iss, sub, aud, exp, nbf, iat, jti), password, Encoding.UTF8) { }

        /// <summary>
        /// JWT包
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="password"></param>
        /// <param name="encoding"></param>
        public JWTPackage(JWTPayload payload, string password, Encoding encoding)
        {
            _encoding = encoding;

            _password = password;

            Payload = payload;
        }

        public JWTPackage(JWTHeader header, JWTPayload payload, string password, Encoding encoding)
        {
            _encoding = encoding;

            _password = password;

            Header = header;

            Payload = payload;
        }

        /// <summary>
        /// 解析为JWTPackage
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="password"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static JWTPackage Parse(string signature, string password, Encoding encoding)
        {
            if (string.IsNullOrEmpty(signature)) throw new IllegalTokenException("参数不能为空");

            var arr = signature.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);

            if (arr == null || arr.Length != 3) throw new IllegalTokenException("JWTPackage解析token失败，token格式不正确");

            JWTPackage jwtPackage;

            try
            {
                jwtPackage = new JWTPackage(JWTHeader.Parse(arr[0], encoding), JWTPayload.Parse(arr[1], encoding), password, encoding);
            }
            catch (Exception ex)
            {
                throw new IllegalTokenException("JWTPackage解析token失败，token格式不正确", ex);
            }

            if (jwtPackage == null) throw new IllegalTokenException("JWTPackage解析token失败，token格式不正确");

            if (jwtPackage.Signature != signature) throw new SignatureVerificationException("JWTPackage解析token失败");

            if (jwtPackage.Payload.IsExpired()) throw new TokenExpiredException("JWTPackage的token已过期");

            return jwtPackage;
        }

        /// <summary>
        /// 解析为JWTPackage
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static JWTPackage Parse(string signature, string password)
        {
            return Parse(signature, password, Encoding.UTF8);
        }

    }

    public class JWTPackage<T> where T : class
    {
        public const string Prex = "Bearer ";

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

        Encoding _encoding;

        string _password;

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
            if (string.IsNullOrEmpty(signature)) throw new IllegalTokenException("参数不能为空");

            var arr = signature.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);

            if (arr == null || arr.Length != 3) throw new IllegalTokenException("JWTPackage解析token失败，token格式不正确");

            JWTPackage<T> jwtPackage;

            try
            {
                jwtPackage = new JWTPackage<T>(JWTHeader.Parse(arr[0], encoding), JWTPayload<T>.Parse(arr[1], encoding), password, encoding);
            }
            catch (Exception ex)
            {
                throw new IllegalTokenException("JWTPackage解析token失败，token格式不正确", ex);
            }

            if (jwtPackage == null) throw new IllegalTokenException("JWTPackage解析token失败，token格式不正确");

            if (jwtPackage.Signature != signature) throw new SignatureVerificationException("JWTPackage解析token失败");

            if (jwtPackage.Payload.IsExpired()) throw new TokenExpiredException("JWTPackage的token已过期");

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
