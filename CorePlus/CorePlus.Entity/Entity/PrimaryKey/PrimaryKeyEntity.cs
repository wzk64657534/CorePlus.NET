using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.Entity
{
    public class PrimaryKeyEntity
    {
        public PrimaryKeyEntity()
        {
            IsMustPrefix = true;
            IsMustFillWithChar = true;
        }
        /// <summary>
        /// 前缀，在Memcached中作为Key，请务必保证唯一性
        /// </summary>
        public string Prefix { get; set; }
        /// <summary>
        /// 请求ID的类型
        /// 1 - GUID, 调用时不带任何参数，默认取GUID
        /// 2 - 日期精确到毫秒
        /// 3 - 自增量
        /// 4 - 日期精确到天，加自增量
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 填充字符，只允许一个字符
        /// </summary>
        public char FillChar { get; set; }
        /// <summary>
        /// 填充长度
        /// </summary>
        public int NumberLength { get; set; }
        /// <summary>
        /// 是否必须带前缀，默认必须
        /// </summary>
        public bool IsMustPrefix { get; set; }
        /// <summary>
        /// 是否必须填充，默认必须
        /// </summary>
        public bool IsMustFillWithChar { get; set; }
    }
}