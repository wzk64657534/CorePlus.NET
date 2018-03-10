using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class CoreMapping<TEntity> : BaseMapping<TEntity>, IMapping
          where TEntity : BaseEntity, new()
    {
        public CoreMapping()
        {
            HasKey(x => x.ID);
        }
    }
}