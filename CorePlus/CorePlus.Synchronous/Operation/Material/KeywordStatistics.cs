using System;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public class KeywordStatistics : BaseMaterial
    {
        protected override string GetOperation()
        {
            return "KeywordStatistics";
        }

        protected override void DealRowData(string[] fields, SynCheckedDataInfoEntity entity)
        {
            KeywordStatisticsEntity model = new KeywordStatisticsEntity();
            // 日期
            model.SynchroDate = DateTime.Parse(fields[0]);
            // 账户ID
            model.AccountId = long.Parse(fields[1]);
            // 推广计划ID
            model.CampaignId = long.Parse(fields[3]);
            // 推广单元ID
            model.AdgroupId = long.Parse(fields[5]);
            // 关键词ID
            model.KeywordId = long.Parse(fields[7]);
            // 推广方式
            model.ExpandType = fields[9];
            // 消费
            model.TotalCost = decimal.Parse(DataHelper.SetDefault(fields[10]));
            // 平均点击价格
            model.AvgClickedPrice = decimal.Parse(DataHelper.SetDefault(fields[11]));
            // 点击量
            model.Clicked = long.Parse(DataHelper.SetDefault(fields[12]));
            // 展现量
            model.ShowCnt = long.Parse(DataHelper.SetDefault(fields[13]));
            // 点击率
            model.ClickedRate = decimal.Parse(DataHelper.SetDefault(fields[14]).Replace("%", ""));
            // 千次展现消费
            model.ThousandCost = decimal.Parse(DataHelper.SetDefault(fields[15]));
            //平均排名
            model.AvgRang = decimal.Parse(DataHelper.SetDefault(fields[16]));
            // 转化
            model.TransformCnt = int.Parse(DataHelper.SetDefault(fields[17]));
            ParamHelper.wcfKeywordStatistics.Add(model);
            // 日期	账户ID	账户	推广计划ID	推广计划	推广单元ID	推广单元	关键词keywordID	关键词	推广方式	消费	平均点击价格	点击量	展现量	点击率	千次展现消费	平均排名	转化
        }
    }
}
