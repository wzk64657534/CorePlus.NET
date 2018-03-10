using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Security.Principal;

using CorePlus.API;
using Core;
using CorePlus.Entity;

namespace CorePlus.API.Web
{
    [Authorize(Roles = "customer")]
    public class BaiduController : Controller
    {
        [ChildActionOnly]
        public ActionResult PartAccountInfo()
        {
            var db = CoreDBContext.GetContext();
            string name = User.Identity.Name;
            var model = db.Set<AccountInfoEntity>().Where(x => x.UserName == name).FirstOrDefault();
            try
            {
                if (model != null)
                {
                    // 调用一次接口同步数据
                    BaiduV2AccountService BaiduV2AccountService =
                        new CorePlus.API.BaiduV2AccountService(model.AccountName, CryptHelper.DESDecode(model.AccountPwd), CryptHelper.DESDecode(model.Token));
                    CorePlus.API.BaiduAPI_V2_AccountService.AccountInfoType account = BaiduV2AccountService.GetAccountInfo();
                    if (account != null)
                    {
                        model.Balance = (decimal)account.balance;
                        model.Budget = (decimal)account.budget;
                        model.Cost = (decimal)account.cost;
                        model.Payment = (decimal)account.payment;
                        model.Type = account.type;

                        db.Entry(model).State = System.Data.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    LogWebHelper.AddLog("账户不存在", "Baidu_PartAccountInfo");
                }
            }
            catch (Exception ex)
            {
                // 接口同步失败记录日志
                LogWebHelper.AddLog(ex.Message, "Baidu_PartAccountInfo");
            }
            // 目标区域中文
            string regionsCN = string.Empty;
            if (!string.IsNullOrEmpty(model.RegionTarget))
            {
                string[] regions = model.RegionTarget.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                int max = regions.Length > 2 ? 2 : regions.Length;

                for (int i = 0; i < max; i++)
                {
                    string value = regions[i];
                    regions[i] = db.Set<ParamDtsInfoEntity>().Where(x => x.ID == 1 && x.ParamValue == value).FirstOrDefault().ParamDtsName;
                }

                foreach (var item in regions)
                {
                    regionsCN += item + " ";
                }
            }

            ViewBag.RegionsCn = string.IsNullOrEmpty(regionsCN) ? "没有数据" : (regionsCN + "...");

            return PartialView(model);
        }

        public ActionResult Index(long id, string arg)
        {
            CookieHelper.SetCookie("AccountId", id.ToString());
            CookieHelper.SetCookie("AccountName", arg);
            return View();
        }
    }
}