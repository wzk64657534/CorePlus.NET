using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public static class ExceptionHelper
    {
        public static Exception GetInnerException(Exception exception)
        {
            if (exception.InnerException != null)
            {
                return GetInnerException(exception.InnerException);
            }
            return exception;
        }
    }
}
