using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public interface IMaterial
    {
        void Update(SynCheckedDataInfoEntity entity);
    }
}