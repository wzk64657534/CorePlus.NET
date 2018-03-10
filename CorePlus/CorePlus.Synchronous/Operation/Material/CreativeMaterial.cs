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
    public class CreativeMaterial : BaseMaterial
    {
        protected override string GetOperation()
        {
            return "CreativeMaterial";
        }

        protected override void DealRowData(string[] fields, SynCheckedDataInfoEntity entity)
        {
            long id = long.Parse(fields[2]);
            CreativeInfoEntity model = ParamHelper.wcfCreative.FindByID(id);
            bool b = false;
            if (model == null)
            {
                b = true;
                model = new CreativeInfoEntity();
            }

            model.CampaignId = long.Parse(fields[0]);
            model.AdgroupId = long.Parse(fields[1]);
            model.ID = id;
            model.Title = fields[3];
            model.Description1 = fields[4];
            model.Description2 = fields[5];
            model.DestinationUrl = fields[6];
            model.DisplayUrl = fields[7];
            model.Pause = bool.Parse(fields[8]);
            model.Status = int.Parse(fields[9]);
            model.Temp = int.Parse(fields[10]);
            model.AccountName = entity.AccountName;

            if (b)
            {
                ParamHelper.wcfCreative.Add(model);
            }
            else
            {
                ParamHelper.wcfCreative.Update(model);
            }
        }
    }
}