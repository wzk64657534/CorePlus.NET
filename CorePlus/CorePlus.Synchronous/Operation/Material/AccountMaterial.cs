using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;

using CorePlus.API;
using Core;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public class AccountMaterial : BaseMaterial
    {
        protected override string GetOperation()
        {
            return "AccountMaterial";
        }

        protected override void DealRowData(string[] fields, SynCheckedDataInfoEntity entity)
        {
            AccountInfoEntity model = ParamHelper.wcfAccount.FindByExpression(x => x.AccountName == entity.AccountName).FirstOrDefault();
            if (model != null)
            {
                model.AccountId = long.Parse(fields[0]);
                model.Balance = fields[1] == "-" ? 0 : decimal.Parse(fields[1]);
                model.Cost = fields[2] == "-" ? 0 : decimal.Parse(fields[2]);
                model.Payment = fields[3] == "-" ? 0 : decimal.Parse(fields[3]);
                model.Budget = fields[4] == "-" ? 0 : decimal.Parse(fields[4]);
                model.RegionTarget = fields[5];
                model.ExcludeIp = fields[6];
                model.OpenDomains = fields[7];
                ParamHelper.wcfAccount.Update(model);
            }
        }
    }
}