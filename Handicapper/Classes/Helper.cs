using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Handicapper
{
    public class Helper
    {

        public static bool IsDate(object Expression)
        {
            if (Expression != null)
            {
                if (Expression is DateTime)
                {
                    return true;
                }
                if (Expression is string)
                {
                    DateTime time1;
                    return DateTime.TryParse((string)Expression, out time1);
                }
            }
            return false;
        }
        public static bool IsNumeric(string anyString)
        {
            if (anyString == null)
            {
                anyString = "";
            }

            if (anyString.Length > 0)
            {
                double dummyOut = new double();
                System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-US", true);
                return Double.TryParse(anyString, System.Globalization.NumberStyles.Any, cultureInfo.NumberFormat, out dummyOut);
            }
            else
            {
                return false;
            }
        }

        public enum FileStatus
        {
            Open = 0,       // File is open
            Closed = 1,     // File is closed
            Writing = 2,
            Error = 3
        }


        public enum RegistratationType
        {
            Expired = -1,
            Registered = 0,
            NotRegisteredYet = 1
        }

    }
}
