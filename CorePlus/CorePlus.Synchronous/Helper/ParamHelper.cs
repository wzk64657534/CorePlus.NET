using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CorePlus.API;
using System.Threading;
using CorePlus.Entity;
using CorePlus.Repository;

namespace CorePlus.Synchronous
{
    public class ParamHelper
    {
        public static AccountRepository wcfAccount = null;
        public static CampaignRepository wcfCampaign = null;
        public static AdgroupRepository wcfAdgroup = null;
        public static KeywordRepository wcfKeyword = null;
        public static CreativeRepository wcfCreative = null;
        public static SublinkRepository wcfSublink = null;
        public static FolderRepository wcfFolder = null;
        public static MonitorRepository wcfMonitor = null;

        public static AccountStatisticsRepository wcfAccountStatistics = null;
        public static CampaignStatisticsRepository wcfCampaignStatistics = null;
        public static AdgroupStatisticsRepository wcfAdgroupStatistics = null;
        public static KeywordStatisticsRepository wcfKeywordStatistics = null;
        public static CreativeStatisticsRepository wcfCreativeStatistics = null;

        public static SynCheckedDataRepository wcfSynCheckedData = null;
        public static SynDataRepository wcfSynData = null;
        public static LogRepository wcfLog = null;

        public static object objLock = new object();
        public static string[] MaterialKeys = new string[] { "account", "campaign", "adgroup", "keyword", "creative", "sublink" };

        public static List<SynDataInfoEntity> SynDatas = null;
        public static List<SynCheckedDataInfoEntity> SynCheckedDatas = null;
        public static int SynDataMax = 0;
        public static int SynCheckedDataMax = 0;

        public static DealWithManager managerDealWith = null;
        public static MaterialManager managerMaterail = null;
        public static ReportManager managerReport = null;
        public static RealTimeManager managerRealTime = null;

        public static string GetMaterialKey(string valString)
        {
            foreach (var item in MaterialKeys)
            {
                if (valString.Contains(item))
                {
                    return item;
                }
            }

            return null;
        }

        public static DateTime PreviousDate { get { return SettingsHelper.GetPreviousDate(); } }
        public static DateTime Current { get { return Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")); } }

        public static bool IsRun()
        {
            if (Current <= PreviousDate)
            {
                LogHelper.AddLog("当前不需要发送处理请求", "GetFileId");
                return false;
            }

            return true;
        }
    }
}