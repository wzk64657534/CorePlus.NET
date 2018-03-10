using System.Collections.Generic;
using System.Xml.Linq;
using Core;

namespace CorePlus.Ws
{
    public interface IBase<TEntity> where TEntity : BaseEntity
    {
        long Add(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        List<TEntity> GetAll();
        TEntity GetById(long id);
        bool DeleteById(long id);
    }
}