using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CorePlus.Synchronous
{
    public class SynDataDirector
    {
        public void Build(BaseBuilder builder)
        {
            builder.GetDealWithId();
            builder.CheckedDealWithId();
            builder.SynData();
        }
    }
}