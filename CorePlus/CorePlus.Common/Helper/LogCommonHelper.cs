using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Core;
using CorePlus.Entity;
using System.Configuration;

namespace CorePlus.Common
{
    public class LogCommonHelper : Core.LogHelper
    {
        public static void AddLog(LogInfoEntity entity)
        {
            var db = CoreDBContext.GetContext();
            db.Set<LogInfoEntity>().Add(entity);
        }

        public static void AddLog(string errorMsg, string accountName = "System", string operation = null, string tag = null)
        {
            LogInfoEntity entity = new LogInfoEntity();
            entity.AccountName = accountName;
            entity.ErrorMsg = errorMsg;
            entity.ErrorDate = DateTime.Now;
            entity.Operation = operation;
            entity.Tag = tag;

            AddLog(entity);
        }

        public static void AddLog(string errorMsg, string operation)
        {
            AddLog(errorMsg, "System", operation, null);
        }

        public static void WriteLog(string record, string prefix = null)
        {
            bool isNeed = ConfigurationManager.AppSettings["IsLog"] == null ? false : bool.Parse(ConfigurationManager.AppSettings["IsLog"].ToString());
            WriteLog(record, bool.Parse(ConfigurationManager.AppSettings["IsLog"].ToString()), null);
        }
    }
}