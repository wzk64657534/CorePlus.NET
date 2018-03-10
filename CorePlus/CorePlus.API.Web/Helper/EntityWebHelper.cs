using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.Common;
using System.Web.Mvc;

namespace CorePlus.API.Web
{
    public class EntityWebHelper : EntityCommonHelper
    {
        public static IEnumerable<SelectListItem> GetMessageTypeList()
        {
            return new List<SelectListItem> 
            {
                new SelectListItem() { Text = "文本消息", Value = "Text" },
                new SelectListItem() { Text = "图片消息", Value = "Image" },
                new SelectListItem() { Text = "链接消息", Value = "Link" },
                new SelectListItem() { Text = "视频消息", Value = "Video" },
                new SelectListItem() { Text = "音频消息", Value = "Voice" },
                new SelectListItem() { Text = "地理位置消息", Value = "Location" },
                new SelectListItem() { Text = "菜单点击事件", Value = "MenuEvent" },
                new SelectListItem() { Text = "扫描事件", Value = "ScanEvent" },
                new SelectListItem() { Text = "地理位置事件", Value = "LocationEvent" },
                new SelectListItem() { Text = "关注/取消事件", Value = "SubscribeEvent" }
            };
        }

        public static IEnumerable<SelectListItem> GetReplyTypeList()
        {
            return new List<SelectListItem> 
            {
                new SelectListItem() { Text = "文本消息", Value = "Text" },
                new SelectListItem() { Text = "图文消息", Value = "ImageText" }
            };
        }

        public static IEnumerable<SelectListItem> GetWxMenuTypeList()
        {
            return new List<SelectListItem> 
            {
                new SelectListItem() { Text = "链接(VIEW)", Value = "view" },
                new SelectListItem() { Text = "菜单点击(CLICK)", Value = "click" }
            };
        }

        public static IEnumerable<SelectListItem> GetMatchTypeList()
        {
            return new List<SelectListItem> 
            {
                new SelectListItem { Selected = true, Text = "完全匹配", Value = "1" },
                new SelectListItem { Selected = false, Text = "包含匹配", Value = "2" }
            };
        }
    }
}