using System;
using System.Collections.Generic;
using System.Text;

namespace CotTools
{
    public static class Extensions
    {
        public static int ToNetValue(this object obj)
        {
            return Convert.ToInt32(obj.ToString().Replace(".", ""));
        }
    }
}
