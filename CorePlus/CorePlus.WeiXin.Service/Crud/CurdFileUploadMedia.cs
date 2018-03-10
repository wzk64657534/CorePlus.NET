using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace CorePlus.WeiXin.Service
{
    public class CurdFileUploadMedia : BasePostCurd
    {
        protected override string GetUrl()
        {
            return ConstHelper.CURD_FILE_UPLOAD_MEDIA;
        }

        protected override string[] GetNamesOfMustParamter()
        {
            return new string[] { "type", "media" };
        }
    }
}