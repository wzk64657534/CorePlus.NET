using System;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public class AccountStatistics : BaseMaterial
    {
        protected override string GetOperation()
        {
            return "AccountStatistics";
        }

        protected override void DealRowData(string[] fields, SynCheckedDataInfoEntity entity)
        {
            AccountStatisticsEntity model = new AccountStatisticsEntity();
            // 日期
            model.SynchroDate = DateTime.Parse(fields[0]);
            // 账户ID
            model.AccountId = long.Parse(fields[1]);
            // 推广方式
            model.ExpandType = fields[3];
            // 消费
            model.TotalCost = decimal.Parse(DataHelper.SetDefault(fields[4]));
            // 平均点击价格
            model.AvgClickedPrice = decimal.Parse(DataHelper.SetDefault(fields[5]));
            // 点击量
            model.Clicked = long.Parse(DataHelper.SetDefault(fields[6]));
            // 展现量
            model.ShowCnt = long.Parse(DataHelper.SetDefault(fields[7]));
            // 点击率
            model.ClickedRate = decimal.Parse(DataHelper.SetDefault(fields[8]).Replace("%", ""));
            // 千次展现消费
            model.ThousandCost = decimal.Parse(DataHelper.SetDefault(fields[9]));
            // 转化
            model.TransformCnt = int.Parse(DataHelper.SetDefault(fields[10]));
            ParamHelper.wcfAccountStatistics.Add(model);
            // 日期	账户ID	账户	推广方式	消费	平均点击价格	点击量	展现量	点击率	千次展现消费	转化
        }
    }
}