﻿/****************************************************************************
*项目名称：JWT.Net.Model
*CLR 版本：4.0.30319.42000
*机器名称：WALLE-PC
*命名空间：JWT.Net.Model
*类 名 称：JWTBase
*版 本 号：V1.0.0.0
*创建人： yswenli
*电子邮箱：yswenli@outlook.com
*创建时间：2020/8/5 16:38:06
*描述：
*=====================================================================
*修改时间：2020/8/5 16:38:06
*修 改 人： yswenli
*版 本 号： V1.0.0.0
*描    述：
*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace JWT.Net.Model
{
    public abstract class JWTBase : JWTBase<string, string>
    {
        public JWTBase() : base(StringComparer.OrdinalIgnoreCase)
        {

        }

        public new string ToString()
        {
            var count = Count();

            if (count > 0)
            {
                int i = 0;
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                foreach (var item in this)
                {
                    i++;
                    if (i == 1)
                        sb.Append($"\"{item.Key}\":\"{item.Value}\"");
                    else
                        sb.Append($",\"{item.Key}\":\"{item.Value}\"");
                }
                sb.Append("}");
                return sb.ToString();
            }
            return base.ToString();
        }
    }

    public abstract class JWTBase<TKey, TVal> : IEnumerable<KeyValuePair<TKey, TVal>>
    {
        Dictionary<TKey, TVal> _dic;

        public TVal this[TKey key]
        {
            get
            {
                if (_dic.ContainsKey(key))
                    return _dic[key];
                else
                    return default(TVal);
            }
            set
            {
                _dic[key] = value;
            }
        }


        public JWTBase(IEqualityComparer<TKey> comparer)
        {
            _dic = new Dictionary<TKey, TVal>(comparer);
        }

        public IEnumerator<KeyValuePair<TKey, TVal>> GetEnumerator()
        {
            return _dic.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsEmput
        {
            get
            {
                return _dic.Count == 0;
            }
        }

        public void TryAdd(TKey key, TVal val)
        {
            if (!_dic.ContainsKey(key))
            {
                _dic.Add(key, val);
            }
        }

        public void TryRemove(TKey key)
        {
            if (_dic.ContainsKey(key))
            {
                _dic.Remove(key);
            }
        }

        public int Count()
        {
            return _dic.Count;
        }
    }

}
