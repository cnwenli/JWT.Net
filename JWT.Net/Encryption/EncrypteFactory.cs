/****************************************************************************
*Copyright (c) 2022 Oceania All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：JWT.Net.Encryption
*文件名： EncrypteFactory
*版本号： V1.0.0.0
*唯一标识：ec568a92-a6d5-442b-8d98-e84ae53c5847
*当前的用户域：WALLE
*创建人： yswen
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/6/23 16:20:36
*描述：
*
*=================================================
*修改标记
*修改时间：2022/6/23 16:20:36
*修改人： yswen
*版本号： V1.0.0.0
*描述：
*
*****************************************************************************/

namespace JWT.Net.Encryption
{
    /// <summary>
    /// EncrypteFactory
    /// </summary>
    public static class EncrypteFactory
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="encryptType"></param>
        /// <returns></returns>
        public static IEncrypte Create(EncryptType encryptType)
        {
            switch (encryptType)
            {
                case EncryptType.HS256:
                default:
                    return new HMACSHA256();
                case EncryptType.MD5:
                    return new HMACMD5();
            }
        }
    }
}
