/****************************************************************************
*项目名称：JWT.Net.Encryption
*CLR 版本：4.0.30319.42000
*机器名称：WALLE-PC
*命名空间：JWT.Net.Encryption
*类 名 称：Base64URL
*版 本 号：V1.0.0.0
*创建人： yswenli
*电子邮箱：yswenli@outlook.com
*创建时间：2020/8/5 16:20:44
*描述：
*=====================================================================
*修改时间：2020/8/5 16:20:44
*修 改 人： yswenli
*版 本 号： V1.0.0.0
*描    述：
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.Net.Encryption
{
    /// <summary>
    /// Base64URL
    /// 基于JWT规范的Base64编码/解码实现
    /// </summary>
    public static class Base64URL
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Encode(byte[] input)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input));
            if (input.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(input));

            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0]; 
            output = output.Replace('+', '-');
            output = output.Replace('/', '_'); 
            return output;
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] Decode(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException(nameof(input));

            var output = input;
            output = output.Replace('-', '+');
            output = output.Replace('_', '/'); 
            switch (output.Length % 4) 
            {
                case 0:
                    break; 
                case 2:
                    output += "==";
                    break; 
                case 3:
                    output += "=";
                    break;
                default:
                    throw new FormatException("非法 base64url 字符串。");
            }            
            return Convert.FromBase64String(output); 
        }
    }
}
