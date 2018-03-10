using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CorePlus.API;
using Core;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public interface IDealWithType
    {
        void GetDealWithId(DateTime dt, SynDataInfoEntity entity);
        void SynData(SynCheckedDataInfoEntity entity);
        void CheckedData(SynCheckedDataInfoEntity entity, SynDataInfoEntity model);
    }
}