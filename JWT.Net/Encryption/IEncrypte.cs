/****************************************************************************
*Copyright (c) 2022 Oceania All Rights Reserved.
*CLR版本： 4.0.30319.42000
*机器名称：WALLE
*公司名称：RiverLand
*命名空间：JWT.Net.Encryption
*文件名： IEncrypte
*版本号： V1.0.0.0
*唯一标识：262f7eb9-d1a1-4127-b1bd-34bb1554262d
*当前的用户域：WALLE
*创建人： yswen
*电子邮箱：walle.wen@tjingcai.com
*创建时间：2022/6/23 16:26:13
*描述：
*
*=================================================
*修改标记
*修改时间：2022/6/23 16:26:13
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
    public interface IEncrypte
    {
        string Sign(byte[] password, byte[] bytesToSign);
    }
}
