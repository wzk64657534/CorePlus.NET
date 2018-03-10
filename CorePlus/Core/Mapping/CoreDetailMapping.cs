using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;


namespace Core
{
    public class CoreDetailMapping<TEntity> : BaseMapping<TEntity>
        where TEntity : DetailEntity
    {
        public CoreDetailMapping()
        {
            HasKey(x => new { x.ID, x.SEQ });
        }
    }
}
