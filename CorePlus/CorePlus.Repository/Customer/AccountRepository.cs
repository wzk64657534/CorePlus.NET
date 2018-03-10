using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Core;
using CorePlus.Entity;
using System.Text;
using CorePlus.Common;

namespace CorePlus.Repository
{
    public class AccountRepository : SimpleRepository<AccountInfoEntity>
    {
        public IQueryable<AccountInfoEntity> GetAccountInfoByUserName(string userName)
        {
            var query = from x in this.DB.Set<AccountInfoEntity>()
                        join y in this.DB.Set<UserInfoEntity>()
                        on x.UserId equals y.ID
                        where y.UserName == userName
                        select x;

            return query;
        }

        protected override void BeforeAdd(AccountInfoEntity entity)
        {
            ProcessAccountModel(entity);
            base.BeforeAdd(entity);
        }

        protected override void BeforeUpdate(AccountInfoEntity entity, AccountInfoEntity uiEntity)
        {
            ProcessAccountModel(uiEntity);
            base.BeforeUpdate(entity, uiEntity);
        }

        //更具地区获取id
        private string GetParaValueByArea(string dtsName)
        {
            string[] paraStr = dtsName.Split(',');
            var idSb = new StringBuilder();
            foreach (var s in paraStr)
            {
                string id = ParamCommonHelper.GetValueByDtsName(1, s);
                if (!string.IsNullOrEmpty(id))
                {
                    idSb.Append("||" + id);
                }

            }
            return idSb.Remove(0, 2).ToString();
        }

        private void ProcessAccountModel(AccountInfoEntity model)
        {
            //加密密码
            model.AccountPwd = CryptHelper.DESEncode(model.AccountPwd);
            //加密密钥
            model.Token = CryptHelper.DESEncode(model.Token);
            model.LastUpdateTime = DateTime.Now;
            model.UserName = CookieCommonHelper.GetCookie("UserName");
            if (!string.IsNullOrEmpty(model.RegionTarget))
            {
                model.RegionTarget = GetParaValueByArea(model.RegionTarget);
            }
        }
    }
}