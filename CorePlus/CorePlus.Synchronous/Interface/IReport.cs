using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public interface IReport
    {
        void GetReportId(DateTime dt, SynDataInfoEntity entity);
    }
}