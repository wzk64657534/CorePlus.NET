using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CorePlus.WeiXin.Entity;
using CorePlus.Entity;

namespace CorePlus.WeiXin.Repository
{
    public class WxIndexRepository : WxMessageBaseRepository<WxEntity>
    {
        public List<WxAccountEntity> GetManyCompanyTokenFalse()
        {
            var query = from x in this.DB.Set<WxAccountEntity>()
                        where x.TokenStatus == 0
                        select x;

            return query.ToList();
        }
    }
}