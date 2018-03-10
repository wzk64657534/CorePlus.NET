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
    public class AdgroupMaterial : BaseMaterial
    {
        protected override string GetOperation()
        {
            return "AdgroupMaterial";
        }

        protected override void DealRowData(string[] fields, SynCheckedDataInfoEntity entity)
        {
            long id = long.Parse(fields[1]);
            AdgroupInfoEntity model = ParamHelper.wcfAdgroup.FindByID(id);
            bool b = false;
            if (model == null)
            {
                b = true;
                model = new AdgroupInfoEntity();
            }

            model.CampaignId = long.Parse(fields[0]);
            model.ID = id;
            model.Name = fields[2];
            model.MaxPrice = fields[3] == "-" ? 0 : decimal.Parse(fields[3]);
            model.NegativeWords = fields[4];
            model.ExactNegativeWords = fields[5];
            model.Pause = bool.Parse(fields[6]);
            model.Status = int.Parse(fields[7]);
            model.AccountName = entity.AccountName;

            if (b)
            {
                ParamHelper.wcfAdgroup.Add(model);
            }
            else
            {
                ParamHelper.wcfAdgroup.Update(model);
            }
        }
    }
}