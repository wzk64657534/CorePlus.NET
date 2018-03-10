using System;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public class CampaignStatistics : BaseMaterial
    {
        protected override string GetOperation()
        {
            return "CampaignStatistics";
        }

        protected override void DealRowData(string[] fields, SynCheckedDataInfoEntity entity)
        {
            CampaignStatisticsEntity model = new CampaignStatisticsEntity();
            // 日期
            model.SynchroDate = DateTime.Parse(fields[0]);
            // 账户ID
            model.AccountId = long.Parse(fields[1]);
            // 推广计划ID
            model.CampaignId = long.Parse(fields[3]);
            // 推广方式
            model.ExpandType = fields[5];
            // 消费
            model.TotalCost = decimal.Parse(DataHelper.SetDefault(fields[6]));
            // 平均点击价格
            model.AvgClickedPrice = decimal.Parse(DataHelper.SetDefault(fields[7]));
            // 点击量
            model.Clicked = long.Parse(DataHelper.SetDefault(fields[8]));
            // 展现量
            model.ShowCnt = long.Parse(DataHelper.SetDefault(fields[9]));
            // 点击率
            model.ClickedRate = decimal.Parse(DataHelper.SetDefault(fields[10]).Replace("%", ""));
            // 千次展现消费
            model.ThousandCost = decimal.Parse(DataHelper.SetDefault(fields[11]));
            // 转化
            model.TransformCnt = int.Parse(DataHelper.SetDefault(fields[12]));
            ParamHelper.wcfCampaignStatistics.Add(model);
            //日期	账户ID	账户	推广计划ID	推广计划	推广方式	消费	平均点击价格	点击量	展现量	点击率	千次展现消费	转化
        }
    }
}