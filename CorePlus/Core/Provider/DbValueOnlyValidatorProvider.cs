using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Core
{
    public class DbValueOnlyValidatorProvider : AssociatedValidatorProvider
    {
        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            foreach (DbValueOnlyAttribute attr in attributes.OfType<DbValueOnlyAttribute>())
            {
                yield return new DbValueOnlyModelValidator(metadata, context, attr);
            }
        }
    }
}