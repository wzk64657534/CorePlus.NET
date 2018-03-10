using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CorePlus.WeiXin.Entity;
using Core;

namespace CorePlus.WeiXin.Repository
{
    public class WxRepository<TEntity> : SimpleRepository<TEntity>
         where TEntity : BaseEntity, new()
    {

    }
}