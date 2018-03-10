using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;
using CorePlus.Entity;
using CorePlus.Repository;
using System.Data.Objects.SqlClient;

namespace CorePlus.Repository
{
      public class VisitTrackRepository : VisitRepository<VisitBannerEntity>
      {
            public string GetTrackJs()
            {
                  var uid = CookieHelper.GetCookie("UserId");
                  var host = ConfigurationHelper.Get("jsHost");
                  StringBuilder sb = new StringBuilder();
                  sb.AppendLine("<script type=\"text/javascript\">");
                  sb.AppendLine("var _fyProtocol = ((\"https:\" == document.location.protocol) ? \" https://\" : \" http://\");");
                  sb.AppendLine("document.write(unescape(\"%3Cscript src='\" + _fyProtocol + \"{0}/js/jquery-1.5.1.min.js'  type='text/javascript'%3E%3C/script%3E\"));");
                  sb.AppendLine("document.write(unescape(\"%3Cscript src='\" + _fyProtocol + \"{0}/js/data.js' id='srtVisit' uid='{1}' type='text/javascript'%3E%3C/script%3E\")); ");
                  sb.AppendLine("</script>");

                  return string.Format(sb.ToString(), host, uid);
            }
      }
}