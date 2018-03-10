using System;
using System.Web;
using Core;
using CorePlus.Entity;
using CorePlus.Repository;

namespace CorePlus.Visit
{
    public class VisitHttpHandler : BaseHttpHandler<VisitAshxRepository, VisitInfoEntity>
    {
        public override MessageResult Operation(HttpContext context)
        {
            try
            {
                VisitInfoEntity entity = Repository.NewEntity(context);
                if (entity != null)
                {
                    Repository.Add(entity);
                }
            }
            catch (Exception ex)
            {
                return new MessageResult() { Code = -1, Message = ex.Message };
            }

            return new MessageResult() { Code = 0, Message = "" };
        }
    }
}