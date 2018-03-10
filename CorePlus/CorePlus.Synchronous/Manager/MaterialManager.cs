using System.Collections.Generic;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public class MaterialManager
    {
        Dictionary<string, IMaterial> manager = null;
        public MaterialManager()
        {
            manager = new Dictionary<string, IMaterial>();
            manager.Add("account-material", new AccountMaterial());
            manager.Add("campaign-material", new CampaignMaterial());
            manager.Add("adgroup-material", new AdgroupMaterial());
            manager.Add("keyword-material", new KeywordMaterial());
            manager.Add("creative-material", new CreativeMaterial());
            manager.Add("sublink-material", new SublinkMaterial());
            manager.Add("account-statistics", new AccountStatistics());
            manager.Add("campaign-statistics", new CampaignStatistics());
            manager.Add("adgroup-statistics", new AdgroupStatistics());
            manager.Add("keyword-statistics", new KeywordStatistics());
            manager.Add("creative-statistics", new CreativeStatistics());
        }

        public void Update(string key, SynCheckedDataInfoEntity entity)
        {
            if (manager.ContainsKey(key))
            {
                manager[key].Update(entity);
            }
        }
    }
}