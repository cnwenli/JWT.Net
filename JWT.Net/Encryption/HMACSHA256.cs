/****************************************************************************
*项目名称：JWT.Net.Encryption
*CLR 版本：4.0.30319.42000
*机器名称：WALLE-PC
*命名空间：JWT.Net.Encryption
*类 名 称：HMACSHA256
*版 本 号：V1.0.0.0
*创建人： yswenli
*电子邮箱：yswenli@outlook.com
*创建时间：2020/8/5 16:30:33
*描述：
*=====================================================================
*修改时间：2020/8/5 16:30:33
*修 改 人： yswenli
*版 本 号： V1.0.0.0
*描    述：
*****************************************************************************/

using System;

namespace JWT.Net.Encryption
{
    /// <summary>
    /// HMACSHA256加密
    /// </summary>
    public class HMACSHA256: IEncrypte
    {
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="password"></param>
        /// <param name="bytesToSign"></param>
        /// <returns></returns>
        public string Sign(byte[] password, byte[] bytesToSign)
        {
            using (var sha = new System.Security.Cryptography.HMACSHA256(password))
            {
                return Convert.ToBase64String(sha.ComputeHash(bytesToSign));
            }
        }
    }
}
