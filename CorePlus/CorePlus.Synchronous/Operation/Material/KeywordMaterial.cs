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
    public class KeywordMaterial : BaseMaterial
    {
        protected override string GetOperation()
        {
            return "KeywordMaterial";
        }

        protected override void DealRowData(string[] fields, SynCheckedDataInfoEntity entity)
        {
            long id = long.Parse(fields[2]);
            KeywordInfoEntity model = ParamHelper.wcfKeyword.FindByID(id);
            bool b = false;
            if (model == null)
            {
                b = true;
                model = new KeywordInfoEntity();
            }

            model.CampaignId = long.Parse(fields[0]);
            model.AdgroupId = long.Parse(fields[1]);
            model.ID = id;
            model.Keyword = fields[3];
            model.Price = fields[4] == "-" ? 0 : decimal.Parse(fields[4]);
            model.DestinationUrl = fields[5];
            model.MatchType = int.Parse(fields[6]);
            model.Pause = bool.Parse(fields[7]);
            model.Status = int.Parse(fields[8]);
            model.Quality = int.Parse(fields[9]);
            model.Temp = int.Parse(fields[10]);
            model.AccountName = entity.AccountName;

            if (b)
            {
                ParamHelper.wcfKeyword.Add(model);
            }
            else
            {
                ParamHelper.wcfKeyword.Update(model);
            }
        }
    }
}
