using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.Entity;
using Core;

namespace CorePlus.Ws
{
    public interface IServant : IUserBase<ServantInfoEntity>
    {
        ServantInfoEntity GetByOwner(string id, string owner);

        bool UpdateWeiXin(long id, string openid, string weixinno);
    }
}