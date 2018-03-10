using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using CorePlus.WeiXin.Entity;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.WeiXin.Repository
{
    public class WxMessageBaseRepository<TEntity> : WxRepository<TEntity>
         where TEntity : WxEntity, new()
    {

    }
}