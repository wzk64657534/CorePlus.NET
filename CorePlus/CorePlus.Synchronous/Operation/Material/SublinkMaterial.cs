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
    public class SublinkMaterial : BaseMaterial
    {
        protected override string GetOperation()
        {
            return "SublinkMaterial";
        }

        protected override void DealRowData(string[] fields, SynCheckedDataInfoEntity entity)
        {
            long id = long.Parse(fields[2]);
            SublinkInfoEntity model = ParamHelper.wcfSublink.FindByID(id);
            bool b = false;
            if (model == null)
            {
                b = true;
                model = new SublinkInfoEntity();
            }

            model.CampaignId = long.Parse(fields[0]);
            model.AdgroupId = long.Parse(fields[1]);
            model.ID = id;
            model.Pause = bool.Parse(fields[3]);
            model.Status = int.Parse(fields[4]);
            model.Temp = int.Parse(fields[5]);
            model.AccountName = entity.AccountName;

            string subinfos = string.Empty;
            for (int i = 6; i < fields.Length; i += 2)
            {
                subinfos += string.Format("{0}**{1}||", fields[i], fields[i + 1]);
            }
            model.SublinkInfos = subinfos.Trim('|');

            if (b)
            {
                ParamHelper.wcfSublink.Add(model);
            }
            else
            {
                ParamHelper.wcfSublink.Update(model);
            }
        }
    }
}
