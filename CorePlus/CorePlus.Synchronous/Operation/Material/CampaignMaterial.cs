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
    public class CampaignMaterial : BaseMaterial
    {
        protected override string GetOperation()
        {
            return "CampaignMaterial";
        }

        protected override void DealRowData(string[] fields, SynCheckedDataInfoEntity entity)
        {
            long id = long.Parse(fields[0]);
            CampaignInfoEntity model = ParamHelper.wcfCampaign.FindByID(id);
            bool b = false;
            if (model == null)
            {
                b = true;
                model = new CampaignInfoEntity();
            }

            model.ID = id;
            model.Name = fields[1];
            model.Budget = fields[2] == "-" ? 0 : decimal.Parse(fields[2]);
            model.RegionTarget = fields[3];
            model.ExcludeIp = fields[4];
            model.NegativeWords = fields[5];
            model.ExactNegativeWords = fields[6];
            model.Schedule = fields[7];
            model.BudgetOfflineTime = fields[8];
            model.ShowProb = fields[9] == "-" ? 0 : int.Parse(fields[9]);
            model.Pause = fields[10] == "-" ? false : bool.Parse(fields[10]);
            model.JoinContent = fields[11] == "-" ? false : bool.Parse(fields[11]);
            model.ContentPrice = fields[12] == "-" ? 0 : decimal.Parse(fields[12]);
            model.Status = int.Parse(fields[13]);
            model.AccountName = entity.AccountName;

            if (b)
            {
                ParamHelper.wcfCampaign.Add(model);
            }
            else
            {
                ParamHelper.wcfCampaign.Update(model);
            }
        }
    }
}