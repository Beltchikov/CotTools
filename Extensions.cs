using System;
using System.Collections.Generic;
using System.Text;

namespace CotTools
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// ToNetValue
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToNetValue(this object obj)
        {
            return Convert.ToInt32(obj.ToString().Replace(".", ""));
        }
    }
}
