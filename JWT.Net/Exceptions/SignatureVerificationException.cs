/****************************************************************************
*项目名称：JWT.Net.Exceptions
*CLR 版本：4.0.30319.42000
*机器名称：WALLE-PC
*命名空间：JWT.Net.Exceptions
*类 名 称：SignatureVerificationException
*版 本 号：V1.0.0.0
*创建人： yswenli
*电子邮箱：yswenli@outlook.com
*创建时间：2020/8/5 19:34:56
*描述：
*=====================================================================
*修改时间：2020/8/5 19:34:56
*修 改 人： yswenli
*版 本 号： V1.0.0.0
*描    述：
*****************************************************************************/
using System;

namespace JWT.Net.Exceptions
{
    public class SignatureVerificationException : Exception
    {
        private const string ExpectedKey = "Expected";
        private const string ReceivedKey = "Received";


        public SignatureVerificationException(string message)
            : base(message)
        {
        }

        public SignatureVerificationException(string decodedCrypto, params string[] decodedSignatures)
            : this("Invalid signature")
        {
            this.Expected = decodedCrypto;
            this.Received = $"{String.Join(",", decodedSignatures)}";
        }


        public string Expected
        {
            get => GetOrDefault<string>(ExpectedKey);
            internal set => this.Data.Add(ExpectedKey, value);
        }

        public string Received
        {
            get => GetOrDefault<string>(ReceivedKey);
            internal set => this.Data.Add(ReceivedKey, value);
        }


        protected T GetOrDefault<T>(string key) =>
            this.Data.Contains(key) ? (T)this.Data[key] : default(T);
    }
}
