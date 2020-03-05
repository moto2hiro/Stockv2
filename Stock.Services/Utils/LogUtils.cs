using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Stock.Services.Utils
{
    public class LogUtils
    {
        public static void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
        public static void Error(Exception ex)
        {
            var error = ex;
            var hasInnerException = true;
            while(hasInnerException)
            {
                System.Diagnostics.Debug.WriteLine(error.Message);
                System.Diagnostics.Debug.WriteLine(error.StackTrace);
                if (error.InnerException == null)
                    hasInnerException = false;
                else
                    error = error.InnerException;
            }
        }
    }
}
