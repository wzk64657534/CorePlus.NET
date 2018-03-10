using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.Common;
using System.Configuration;
using System.Web.Mvc;
using Core;
using CorePlus.Entity;
using System.Data.Objects.SqlClient;

namespace CorePlus.API.Web
{
    public class ConstWebHelper : ConstCommonHelper
    {
        public static List<SelectListItem> Pause
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem { Value = "true", Text = "暂停" },
                    new SelectListItem { Value = "false", Text = "启用", Selected = true }
                };
            }
        }

        public static List<SelectListItem> ShowProb
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem { Value = "1", Text = "优选", Selected = true },
                    new SelectListItem { Value = "2", Text = "轮显" }
                };
            }
        }

        public static List<SelectListItem> RegionTarget
        {
            get
            {
                return new List<SelectListItem>()
                {
                    //new SelectListItem { Value = "9999999", Text = "全部区域", Selected=true },
                    new SelectListItem { Value = "1000", Text = "北京" },
                    new SelectListItem { Value = "2000", Text = "上海" },
                    new SelectListItem { Value = "3000", Text = "天津" },
                    new SelectListItem { Value = "4000", Text = "广东" },
                    new SelectListItem { Value = "5000", Text = "福建" },
                    new SelectListItem { Value = "8000", Text = "海南" },
                    new SelectListItem { Value = "9000", Text = "安徽" },
                    new SelectListItem { Value = "10000", Text = "贵州" },
                    new SelectListItem { Value = "11000", Text = "甘肃" },
                    new SelectListItem { Value = "12000", Text = "广西" },
                    new SelectListItem { Value = "13000", Text = "河北" },
                    new SelectListItem { Value = "14000", Text = "河南" },
                    new SelectListItem { Value = "15000", Text = "黑龙江" },
                    new SelectListItem { Value = "16000", Text = "湖北" },
                    new SelectListItem { Value = "17000", Text = "湖南" },
                    new SelectListItem { Value = "18000", Text = "吉林" },
                    new SelectListItem { Value = "19000", Text = "江苏" },
                    new SelectListItem { Value = "20000", Text = "江西" },
                    new SelectListItem { Value = "21000", Text = "辽宁" },
                    new SelectListItem { Value = "22000", Text = "内蒙古" },
                    new SelectListItem { Value = "23000", Text = "宁夏" },
                    new SelectListItem { Value = "24000", Text = "青海" },
                    new SelectListItem { Value = "25000", Text = "山东" },
                    new SelectListItem { Value = "26000", Text = "山西" },
                    new SelectListItem { Value = "27000", Text = "陕西" },
                    new SelectListItem { Value = "28000", Text = "四川" },
                    new SelectListItem { Value = "29000", Text = "西藏" },
                    new SelectListItem { Value = "30000", Text = "新疆" },
                    new SelectListItem { Value = "31000", Text = "云南" },
                    new SelectListItem { Value = "32000", Text = "浙江" },
                    new SelectListItem { Value = "33000", Text = "重庆" },
                    new SelectListItem { Value = "34000", Text = "香港" },
                    new SelectListItem { Value = "35000", Text = "台湾" },
                    new SelectListItem { Value = "36000", Text = "澳门" },
                    new SelectListItem { Value = "200000", Text = "日本" },
                    new SelectListItem { Value = "300000", Text = "其他国家（除日本外）" }
                };
            }
        }

        public static List<SelectListItem> Campaign
        {
            get
            {
                var db = CoreDBContext.GetContext();
                var query = from x in db.Set<CampaignInfoEntity>()
                            select new SelectListItem { Text = x.Name, Value = SqlFunctions.StringConvert((decimal)x.ID).Trim() };
                return query.ToList();
            }
        }

        public static List<SelectListItem> Adgroup
        {
            get
            {
                var db = CoreDBContext.GetContext();
                var query = from x in db.Set<AdgroupInfoEntity>()
                            select new SelectListItem { Text = x.Name, Value = SqlFunctions.StringConvert((decimal)x.ID).Trim() };
                return query.ToList();
            }
        }

        public static List<SelectListItem> MatchType
        {
            get
            {
                return new List<SelectListItem> 
                {
                    new SelectListItem() { Text="精确", Value="1" },
                    new SelectListItem() { Text="短语", Value="2" },
                    new SelectListItem() { Text="广泛", Value="3" }
                };
            }
        }
    }
}