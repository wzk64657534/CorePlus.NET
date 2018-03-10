using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.API.Web
{
    public class ApiMapping<TEntity> : CoreMapping<TEntity>
        where TEntity : BaseEntity, new()
    {
        public ApiMapping()
        {
            HasKey(x => x.ID);
            this.Property(p => p.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}