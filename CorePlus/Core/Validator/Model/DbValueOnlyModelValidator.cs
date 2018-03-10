using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;

namespace Core
{
    public class DbValueOnlyModelValidator : ModelValidator
    {
        private DbValueOnlyAttribute attribute;
        public DbValueOnlyModelValidator(ModelMetadata metaData, ControllerContext context, DbValueOnlyAttribute attribute)
            : base(metaData, context)
        {
            this.attribute = attribute;
        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            if (container == null)
                yield break;

            ICoreController controller = this.ControllerContext.Controller as ICoreController;
            var action = Convert.ToString(this.ControllerContext.RouteData.Values["action"]);

            List<Tuple<string, ExpressionOperator, object>> list = new List<Tuple<string, ExpressionOperator, object>>();
            list.Add(new Tuple<string, ExpressionOperator, object>(Metadata.PropertyName, ExpressionOperator.Equal, Metadata.Model));

            if (attribute.DependOnProperty != null)
            {
                foreach (var dependOnProperty in attribute.DependOnProperty)
                {
                    PropertyInfo property = container.GetType().GetProperty(dependOnProperty);
                    if (property != null)
                    {
                        list.Add(new Tuple<string, ExpressionOperator, object>(dependOnProperty, ExpressionOperator.Equal, property.GetValue(container, null)));
                    }
                }
            }
            if (action == "Update")
            {
                PropertyInfo property = container.GetType().GetProperty("ID");
                list.Add(new Tuple<string, ExpressionOperator, object>(property.Name, ExpressionOperator.NotEqual, property.GetValue(container, null)));
            }
            if (action == "Add" || action == "Update")
            {
                if (controller.CoreRepository.Exists(list))
                {
                    yield return new ModelValidationResult()
                    {
                        Message = GetMessage()
                    };
                }
            }
            yield break;
        }

        private string GetMessage()
        {
            if (!string.IsNullOrEmpty(attribute.ErrorMessage))
            {
                return string.Format(attribute.ErrorMessage, Metadata.DisplayName, Metadata.Model);
            }
            return string.Format("[{0}]-'{1}' 已存在!", Metadata.DisplayName, Metadata.Model);
        }
    }
}
