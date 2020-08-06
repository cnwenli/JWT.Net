/****************************************************************************
*项目名称：JWT.Net.Exceptions
*CLR 版本：4.0.30319.42000
*机器名称：WALLE-PC
*命名空间：JWT.Net.Exceptions
*类 名 称：TokenExpiredException
*版 本 号：V1.0.0.0
*创建人： yswenli
*电子邮箱：yswenli@outlook.com
*创建时间：2020/8/5 19:35:58
*描述：
*=====================================================================
*修改时间：2020/8/5 19:35:58
*修 改 人： yswenli
*版 本 号： V1.0.0.0
*描    述：
*****************************************************************************/
using JWT.Net.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.Net.Exceptions
{
    public class TokenExpiredException : SignatureVerificationException
    {
        private const string PayloadDataKey = "PayloadData";
        private const string ExpirationKey = "Expiration";


        public TokenExpiredException(string message)
             : base(message)
        {
        }

        public JWTPayload PayloadData
        {
            get => GetOrDefault<JWTPayload>(PayloadDataKey);
            internal set => this.Data.Add(PayloadDataKey, value);
        }


        public DateTime? Expiration
        {
            get => GetOrDefault<DateTime?>(ExpirationKey);
            internal set => this.Data.Add(ExpirationKey, value);
        }
    }
}
