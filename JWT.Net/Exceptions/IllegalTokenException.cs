/****************************************************************************
*项目名称：JWT.Net.Exceptions
*CLR 版本：4.0.30319.42000
*机器名称：WALLE-PC
*命名空间：JWT.Net.Exceptions
*类 名 称：IllegalTokenException
*版 本 号：V1.0.0.0
*创建人： yswenli
*电子邮箱：yswenli@outlook.com
*创建时间：2020/8/5 19:55:51
*描述：
*=====================================================================
*修改时间：2020/8/5 19:55:51
*修 改 人： yswenli
*版 本 号： V1.0.0.0
*描    述：
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.Net.Exceptions
{
    public class IllegalTokenException : Exception
    {
        public IllegalTokenException(string msg) : base(msg) { }

        public IllegalTokenException(string msg, Exception ex) : base(msg, ex) { }
    }
}
