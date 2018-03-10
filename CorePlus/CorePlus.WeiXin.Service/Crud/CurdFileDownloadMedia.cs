using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.WeiXin.Service
{
    public class CurdFileDownloadMedia : BaseGetCurd
    {
        protected override string GetUrl()
        {
            return ConstHelper.CURD_FILE_DOWNLOAD_MEDIA;
        }

        protected override string[] GetNamesOfMustParamter()
        {
            return new string[] { "media_id" };
        }
    }
}