using System.Web;

using Core;

namespace CorePlus.Repository
{
    public class AshxSimpleRepository<TEntity> : SimpleRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        public override TEntity NewEntity(object anyObj)
        {
            HttpContext context = (HttpContext)anyObj;
            // 业务逻辑，处理js提交的访问数据
            // 获取Json格式的参数
            string data = context.Request.Params["params"] == null ? string.Empty : context.Request.Params.Get("params");
            // 没有数据退出，不显示任何提示
            if (string.IsNullOrWhiteSpace(data)) { return null; }
            // 做进一步逻辑处理
            TEntity entity = FillEntity(context, data);

            return entity;
        }

        public virtual TEntity FillEntity(HttpContext context, string data)
        {
            return null;
        }
    }
}