﻿/****************************************************************************
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
using System;
using System.Text;

using JWT.Net.Common;
using JWT.Net.Exceptions;
using JWT.Net.Model;

namespace JWT.Net
{
    /// <summary>
    /// JWT包
    /// </summary>
    public class JWTPackage : JWTPackage<string>
    {

        /// <summary>
        /// JWT包
        /// </summary>
        /// <param name="jti">唯一身份标识</param>
        /// <param name="timeOutSenconds">过期时间</param>
        /// <param name="password">密码</param>
        public JWTPackage(string jti, int timeOutSenconds, string password) :
            this(string.Empty, string.Empty, string.Empty, DateTimeHelper.Now.AddSeconds(timeOutSenconds).GetTimeStamp(), DateTimeHelper.Now.GetTimeStamp(), DateTimeHelper.Now.GetTimeStamp(), jti, password)
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
        public JWTPackage(string iss, string sub, string aud, long exp, long nbf, long iat, string jti, string password) : 
            this(new JWTPayload(iss, sub, aud, exp, nbf, iat, jti), password, Encoding.UTF8) { 
        
        }

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

        /// <summary>
        /// JWT包
        /// </summary>
        /// <param name="header"></param>
        /// <param name="payload"></param>
        /// <param name="password"></param>
        /// <param name="encoding"></param>
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
        /// <param name="token"></param>
        /// <param name="password"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public new static JWTPackage Parse(string token, string password, Encoding encoding)
        {
            if (string.IsNullOrEmpty(token)) throw new IllegalTokenException("Parameter cannot be empty");

            var arr = token.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);

            if (arr == null || arr.Length != 3) throw new IllegalTokenException("JWT Package failed to parse signature, signature format is incorrect");

            JWTPackage jwtPackage;

            try
            {
                jwtPackage = new JWTPackage(JWTHeader.Parse(arr[0], encoding), JWTPayload.Parse(arr[1], encoding), password, encoding);
            }
            catch (Exception ex)
            {
                throw new IllegalTokenException("JWT Package failed to parse signature, signature format is incorrect", ex);
            }

            if (jwtPackage == null) throw new IllegalTokenException("JWT Package failed to parse signature, signature format is incorrect");

            if (jwtPackage.GetToken() != token) throw new SignatureVerificationException("JWT Package failed to parse signature");

            if (jwtPackage.Payload.IsExpired()) throw new TokenExpiredException("The token of jwtpackage has expired");

            return jwtPackage;
        }

        /// <summary>
        /// 解析为JWTPackage
        /// </summary>
        /// <param name="token"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public new static JWTPackage Parse(string token, string password)
        {
            return Parse(token, password, Encoding.UTF8);
        }

        /// <summary>
        /// 解析为JWTPackage
        /// </summary>
        /// <param name="token"></param>
        /// <param name="password"></param>
        /// <param name="package"></param>
        /// <returns></returns>
        public static bool TryParse(string token, string password, out JWTPackage package)
        {
            package = null;
            try
            {
                package = Parse(token, password);
                if (package != null)
                    return true;
            }
            catch { }
            return false;
        }

    }
    
}
