/****************************************************************************
*项目名称：JWT.Net.Model
*CLR 版本：4.0.30319.42000
*机器名称：WALLE-PC
*命名空间：JWT.Net.Model
*类 名 称：StandardPayload
*版 本 号：V1.0.0.0
*创建人： yswenli
*电子邮箱：yswenli@outlook.com
*创建时间：2020/8/6 10:43:26
*描述：
*=====================================================================
*修改时间：2020/8/6 10:43:26
*修 改 人： yswenli
*版 本 号： V1.0.0.0
*描    述：
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.Net.Model
{
    /// <summary>
    /// jwt payload标准模型
    /// </summary>
    public class StandardPayload
    {
        /// <summary>
        /// 发行人
        /// </summary>
        public string iss { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string sub { get; set; }
        /// <summary>
        /// 接收方
        /// </summary>
        public string aud { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public string exp { get; set; }
        /// <summary>
        /// 生效时间
        /// </summary>
        public string nbf { get; set; }
        /// <summary>
        /// 签发时间
        /// </summary>
        public string iat { get; set; }
        /// <summary>
        /// 唯一身份标识
        /// </summary>
        public string jti { get; set; }
    }
}
