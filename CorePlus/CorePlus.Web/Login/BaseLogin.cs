using System.Linq;
using System.Web.Security;
using Core;
using Core.Entity;
using CorePlus.Entity;
using CorePlus.WeiXin.Entity;

namespace CorePlus.Web
{
    public abstract class BaseLogin<TEntity> : ILogin
        where TEntity : UserEntity
    {
        public string[] Login(string name, string pwd)
        {
            var db = CoreDBContext.GetContext();
            string md5Pwd = CryptHelper.MD5(pwd);

            var entity = (from x in db.Set<TEntity>()
                          where x.UserName == name
                          && x.UserPwd == md5Pwd
                          select x).FirstOrDefault();

            if (entity != null)
            {
                if (string.IsNullOrWhiteSpace(entity.RoleName))
                {
                    return new string[] { "LogOn", "Home" };
                }

                CookieHelper.Clear();
                CookieHelper.SetCookie("UserId", entity.ID.ToString());
                CookieHelper.SetCookie("UserName", entity.UserName);
                CookieHelper.SetCookie("RoleName", entity.RoleName);
                this.ExtraCookies(entity);
                FormsAuthentication.SetAuthCookie(entity.UserName, false);

                //return new string[]{ action, controller }
                return RedirectToAction();
            }

            return null;
        }

        protected virtual void ExtraCookies(TEntity entity)
        {

        }

        protected abstract string[] RedirectToAction();
    }
}