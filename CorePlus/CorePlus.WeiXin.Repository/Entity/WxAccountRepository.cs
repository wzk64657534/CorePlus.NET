using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using Core;
using CorePlus.WeiXin.Entity;
using System.Data.Objects.SqlClient;
using CorePlus.Common;
using CorePlus.Entity;

namespace CorePlus.WeiXin.Repository
{
    public class WxAccountRepository : WxUserNameRepository<WxAccountEntity>
    {
        public override WxAccountEntity NewEntity(object anyObj = null)
        {
            var entity = base.NewEntity(anyObj);
            entity.TokenStatus = 0;
            entity.IsAdvanced = false;
            entity.IsSaveRecord = false;
            entity.ExpiresIn = 7200;
            return entity;
        }

        public override WxAccountEntity Update(long id, WxAccountEntity uiEntity)
        {
            var entity = FindByID(id);
            if (entity == null)
            {
                Add(uiEntity);
            }
            else
            {
                CheckNotNull(entity);
                CheckNotNull(uiEntity);
                Update(entity, uiEntity);
            }
            return entity;
        }

        public string ResetWeiXinValidating(string username)
        {
            var entity = FindByExpression(x => x.UserName == username).FirstOrDefault();
            entity.WeiXinNo = null;
            entity.TokenStatus = 0;
            entity = Update(entity.ID, entity);
            return entity.TokenStatus == 0 ? "重置成功" : "重置失败";
        }
    }
}