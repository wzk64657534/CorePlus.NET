using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class WarningException : Exception
    {
        public string FieldName { get; private set; }

        public object Model { get; private set; }

        public WarningException(object model, string fieldName, string message, params object[] args)
            : base(string.Format(message, args))
        {
            this.FieldName = fieldName;
            this.Model = model;
        }

        public WarningException(string message, params object[] args)
            : base(string.Format(message, args))
        {

        }
    }
}