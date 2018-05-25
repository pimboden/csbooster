using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSBoosterObjectConvert
{
    public static class Helper
    {
        public static object SqlNULL(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return DBNull.Value;
            else
                return text.Trim();
        }

        public static object SqlGuid(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return DBNull.Value;
            else
                return new Guid(text.Trim());
        }
    }
}
