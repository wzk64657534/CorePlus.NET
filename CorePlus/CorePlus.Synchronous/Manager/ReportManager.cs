using System.Collections.Generic;
using System;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public class ReportManager
    {
        Dictionary<string, IReport> manager = null;
        public ReportManager()
        {
            manager = new Dictionary<string, IReport>();
            manager.Add("account", new AccountReport());
            manager.Add("campaign", new CampaignReport());
            manager.Add("adgroup", new AdgroupReport());
            manager.Add("keyword", new KeywordReport());
            manager.Add("creative", new CreativeReport());
        }

        public void GetReportId(string key, DateTime dt, SynDataInfoEntity entity)
        {
            if (manager.ContainsKey(key))
            {
                manager[key].GetReportId(dt, entity);
            }
        }
    }
}