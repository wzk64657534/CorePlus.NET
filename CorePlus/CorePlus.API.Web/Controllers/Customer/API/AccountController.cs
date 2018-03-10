using System;
using System.Data.Objects.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using Core;
using CorePlus.Entity;
using CorePlus.Repository;
using System.Reflection;

namespace CorePlus.API.Web
{
    [Authorize(Roles = "customer")]
    public class AccountController : SimpleController<AccountRepository, AccountInfoEntity>
    {
        public ActionResult List()
        {
            var typeList = ParamWebHelper.GetParamDtsInfo(2).Select(x => new { Type = x.ParamValue, Name = x.ParamDtsName }).ToList();
            ViewBag.TypeList = new SelectList(typeList, "Type", "Name");
            return View();
        }

        #region AccountList CRUD
        public ActionResult GetDataByPager()
        {
            int page = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int rows = Request["rows"] == null ? 15 : int.Parse(Request["rows"]);

            var models = Repository.GetAccountInfoByUserName(User.Identity.Name);

            int total = models.Count();

            var temp = models.OrderByDescending(x => x.ID).Skip((page - 1) * rows).Take(rows);

            foreach (var accountInfo in temp)
            {
                if (!string.IsNullOrEmpty(accountInfo.RegionTarget))
                {
                    accountInfo.RegionTarget = GetAreaById(accountInfo.RegionTarget);
                }
            }

            var result = new { total, rows = temp };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(AccountInfoEntity model)
        {
            if (ModelState.IsValid)
            {
                Create(model);
                return Content("ok");
            }
            return Content("false");
        }

        public ActionResult RemoveAccount(string ids)
        {
            string[] str = ids.Split(',');
            Repository.Delete(str);
            return Content("ok");
        }

        public override ActionResult Edit(long id)
        {
            var model = Repository.FindByID(id);

            //绑定DropdownList
            var typeList = ParamWebHelper.GetParamDtsInfo(2).Select(x => new { Type = x.ParamValue, Name = x.ParamDtsName }).ToList();
            ViewBag.TypeList = new SelectList(typeList, "Type", "Name");

            //解密
            model.Token = CryptHelper.DESDecode(model.Token);
            model.AccountPwd = CryptHelper.DESDecode(model.AccountPwd);

            if (!string.IsNullOrEmpty(model.RegionTarget))
            {
                model.RegionTarget = GetAreaById(model.RegionTarget);
            }

            return View(model);
        }

        [HttpPost]
        public override ActionResult Edit(long id, AccountInfoEntity model)
        {
            if (ModelState.IsValid)
            {
                Repository.Update(id, model);

                return Content("ok");
            }
            return Content("false");
        }
        #endregion

        //获取ckb列表
        public JsonResult GetCkbData(int id)
        {
            var result = ParamWebHelper.GetParamDtsInfo(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //获取Account下拉框类型
        public ActionResult LoadTypeData()
        {
            var temp = ParamWebHelper.GetParamDtsInfo(2);

            return Json(temp, JsonRequestBehavior.AllowGet);
        }
        //更具地区获取id
        private string GetParaValueByArea(string dtsName)
        {
            string[] paraStr = dtsName.Split(',');
            var idSb = new StringBuilder();
            foreach (var s in paraStr)
            {
                string id = ParamWebHelper.GetValueByDtsName(1, s);
                if (!string.IsNullOrEmpty(id))
                {
                    idSb.Append("||" + id);
                }

            }
            return idSb.Remove(0, 2).ToString();
        }
        //根据id获取地区
        private string GetAreaById(string paraValue)
        {
            //分割字符串

            string[] paraStr = paraValue.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            var areaSb = new StringBuilder();
            foreach (var s in paraStr)
            {
                string area = ParamWebHelper.GetDiscriptionById(1, s);
                if (!string.IsNullOrEmpty(area))
                {
                    areaSb.Append("," + area);
                }
            }
            return areaSb.Remove(0, 1).ToString();
        }
        //检验用户名是否存在
        public JsonResult CheckAccountName(string accountname)
        {
            var result = Repository.FindByExpression(x => x.AccountName == accountname).Any();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //检测id是否存在
        public JsonResult CheckAccountId(long accountid)
        {
            var result = Repository.FindByExpression(x => x.AccountId == accountid).Any();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}