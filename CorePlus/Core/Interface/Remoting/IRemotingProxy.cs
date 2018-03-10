using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Interface
{
    public interface IRemotingProxy
    {
        DataStruct RunProc(string db, DataStruct dataStruct, out string strMsg);
    }
}