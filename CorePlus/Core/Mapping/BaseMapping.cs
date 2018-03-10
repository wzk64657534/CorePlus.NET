using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace Core
{
    public class BaseMapping<TEntity> : EntityTypeConfiguration<TEntity>, IMapping
        where TEntity : class
    {
        public void RegistTo(ConfigurationRegistrar configurationRegistrar)
        {
            configurationRegistrar.Add(this);
        }
    }
}
