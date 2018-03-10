using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CorePlus.Entity;
using Core;
using CorePlus.Ws;

namespace CorePlus.Ws.Service
{
    /// <summary>
    /// ServantService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ServantService : UserBaseService<ServantInfoEntity>, IServant
    {
        [WebMethod]
        public ServantInfoEntity GetByOwner(string id, string owner)
        {
            long lngId = 0;
            if (!long.TryParse(id, out lngId)) { lngId = 0; }
            long highId = 0;
            if (!long.TryParse(owner, out highId)) { highId = 0; }

            var db = CoreDBContext.GetContext();
            var query = (from x in db.Set<ServantInfoEntity>()
                         where (x.ID == lngId && x.UserId == highId)
                         || (x.WeiXinNo == owner && x.OpenId == id)
                         select x).FirstOrDefault();
            return query;
        }

        [WebMethod]
        public ServantInfoEntity GetByOpenId(string openId)
        {
            var db = CoreDBContext.GetContext();
            var query = (from x in db.Set<ServantInfoEntity>()
                         where x.OpenId == openId
                         select x).FirstOrDefault();
            return query;
        }

        [WebMethod]
        public bool UpdateWeiXin(long id, string openid, string weixinno)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                entity.OpenId = openid;
                entity.WeiXinNo = weixinno;
                return Update(entity);
            }

            return false;
        }
    }
}