using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Core
{
    public class BaseHttpHandler<TRepository> : IHttpHandler
        where TRepository : BaseRepository, new()
    {
        private TRepository repository;

        public TRepository Repository
        {
            get { return this.repository; }
        }

        public BaseHttpHandler()
        {
            repository = new TRepository();
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/javascript";
            string callback = context.Request.Params["callback"];

            MessageResult strReturn = Operation(context);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string msg = serializer.Serialize(strReturn);

            string result = callback + "(" + msg + ")";
            context.Response.Write(result);
        }

        public virtual MessageResult Operation(HttpContext context)
        {
            return new MessageResult() { Code = 0, Message = "" };
        }

        public virtual bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class BaseHttpHandler<TRepository, TEntity> : BaseHttpHandler<TRepository>
        where TRepository : BaseRepository<TEntity>, new()
        where TEntity : BaseEntity
    {

    }
}