using System.Web.Mvc;
using Core;

namespace CorePlus.Web
{
    [Authorize(Roles = "administrator")]
    public class AdminBaseController<TRepository, TEntity> : SimpleController<TRepository, TEntity>
        where TRepository : SimpleRepository<TEntity>, new()
        where TEntity : BaseEntity, new()
    {

    }
}