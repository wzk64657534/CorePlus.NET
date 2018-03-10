using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.Entity
{
    public class VisitorDatagridEntity
    {
        public string NAME { get; set; }
        // 浏览量
        public int PV { get; set; }
        // 访客数
        public int UV { get; set; }
        //访问次数
        public int VC { get; set; }
        //pv占比
        public string PVPRE { get; set; }
        //uv占比
        public string UVPRE { get; set; }
        //vc占比
        public string VCPRE { get; set; }

        public int Count { get; set; }
    }
}
