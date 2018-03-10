using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CorePlus.Synchronous
{
    public class RealTimeDataBuilder : BaseBuilder
    {
        public override void GetDealWithId()
        {

        }

        public override void CheckedDealWithId()
        {

        }

        public override void SynData()
        {
            ParamHelper.managerRealTime.Update("folder");
        }
    }
}