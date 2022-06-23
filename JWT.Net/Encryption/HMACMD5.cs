/****************************************************************************
*Copyright (c) 2022 Oceania All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：JWT.Net.Encryption
*文件名： MD5
*版本号： V1.0.0.0
*唯一标识：fe629897-b640-4c47-a18b-ae7b0a7747d6
*当前的用户域：WALLE
*创建人： yswen
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/6/23 16:29:54
*描述：
*
*=================================================
*修改标记
*修改时间：2022/6/23 16:29:54
*修改人： yswen
*版本号： V1.0.0.0
*描述：
*
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.Net.Encryption
{
    public class HMACMD5 : IEncrypte
    {
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="password"></param>
        /// <param name="bytesToSign"></param>
        /// <returns></returns>
        public string Sign(byte[] password, byte[] bytesToSign)
        {
            using (var sha = new System.Security.Cryptography.HMACMD5(password))
            {
                return Convert.ToBase64String(sha.ComputeHash(bytesToSign));
            }
        }
    }
}
