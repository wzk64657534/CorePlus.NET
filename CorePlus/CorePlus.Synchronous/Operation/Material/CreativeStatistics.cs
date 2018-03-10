using System;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public class CreativeStatistics : BaseMaterial
    {
        protected override string GetOperation()
        {
            return "CreativeStatistics";
        }

        protected override void DealRowData(string[] fields, SynCheckedDataInfoEntity entity)
        {
            CreativeStatisticsEntity model = new CreativeStatisticsEntity();
            // 日期
            model.SynchroDate = DateTime.Parse(fields[0]);
            // 账户ID
            model.AccountId = long.Parse(fields[1]);
            // 推广计划ID
            model.CampaignId = long.Parse(fields[3]);
            // 推广单元ID
            model.AdgroupId = long.Parse(fields[5]);
            // 创意ID
            model.CreativeId = long.Parse(fields[7]);
            // 推广方式
            model.ExpandType = fields[12];
            // 消费
            model.TotalCost = decimal.Parse(DataHelper.SetDefault(fields[13]));
            // 平均点击价格
            model.AvgClickedPrice = decimal.Parse(DataHelper.SetDefault(fields[14]));
            // 点击量
            model.Clicked = long.Parse(DataHelper.SetDefault(fields[15]));
            // 展现量
            model.ShowCnt = long.Parse(DataHelper.SetDefault(fields[16]));
            // 点击率
            model.ClickedRate = decimal.Parse(DataHelper.SetDefault(fields[17]).Replace("%", "")); 
            // 千次展现消费
            model.ThousandCost = decimal.Parse(DataHelper.SetDefault(fields[18]));
            // 平均排名
            model.AvgRang = decimal.Parse(DataHelper.SetDefault(fields[19]));
            // 转化
            //model.TransformCnt = int.Parse(DataHelper.SetDefault(fields[19]));
            ParamHelper.wcfCreativeStatistics.Add(model);
            // 日期	账户ID	账户	推广计划ID	推广计划	推广单元ID	推广单元	创意ID	创意标题	创意描述1	创意描述2	显示URL	推广方式	消费	平均点击价格	点击量	展现量	点击率	千次展现消费	平均排名
        }
    }
}