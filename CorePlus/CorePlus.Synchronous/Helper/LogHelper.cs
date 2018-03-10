using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using Core;
using CorePlus.API;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public class LogHelper
    {
        public static void AddLog(LogInfoEntity entity)
        {
            string strIsLog = ConfigurationManager.AppSettings["IsLog"].ToString();
            bool isLog = bool.Parse(strIsLog);
            if (isLog)
            {
                ParamHelper.wcfLog.Add(entity);
            }
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
    }
}