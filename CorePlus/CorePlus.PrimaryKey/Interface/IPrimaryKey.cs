using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.Entity;

namespace CorePlus.PrimaryKey
{
    public interface IPrimaryKey
    {
        string Get(PrimaryKeyEntity entity);
    }
}