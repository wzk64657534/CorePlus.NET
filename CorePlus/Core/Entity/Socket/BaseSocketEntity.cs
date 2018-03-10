using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Core
{
    public class BaseSocketEntity : BaseEntity
    {
        public BaseSocketEntity()
        {
            Buffer = new byte[ConstHelper.BUFFER_SIZE];
        }

        public Socket Socket { get; set; }
        /// <summary>
        /// 缓冲区，用于储存发送或者接收的内容，默认大小16KB
        /// </summary>
        public byte[] Buffer { get; set; }

        public DateTime? ConnectedTime { get; set; }
    }
}