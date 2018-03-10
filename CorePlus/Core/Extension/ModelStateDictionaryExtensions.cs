using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel;

namespace Core
{
    public static class ModelStateDictionaryExtensions
    {
        public static void CheckError(this ModelStateDictionary modelState)
        {
            foreach (KeyValuePair<string, ModelState> item in modelState)
            {
                if (item.Value.Errors.Count > 0)
                {
                    throw new WarningException(item.Value.Errors[0].ErrorMessage);
                }
            }
        }

        public static bool CheckModel(this ModelStateDictionary modelState)
        {
            foreach (KeyValuePair<string, ModelState> item in modelState)
            {
                if (item.Value.Errors.Count > 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}