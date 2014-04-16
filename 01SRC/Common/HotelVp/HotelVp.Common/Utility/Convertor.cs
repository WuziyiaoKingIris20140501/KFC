using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using HotelVp.Common.Utilities.String;

namespace HotelVp.Common.Utilities
{
    public static class Convertor
    {
        public static string ToString(DateTime dateValue)
        {
            if (dateValue == null || dateValue == DateTime.MinValue)
            {
                return null;
            }
            return dateValue.ToString("yyyy-MM-dd HH:mm:ss");
        }


        public static DateTime ToDateTime(string dateValue)
        {
            DateTime tempDate = DateTime.MinValue;
            if (!StringHelper.IsNullOrEmpty(dateValue))
            {
                try
                {
                    tempDate = DateTime.Parse(dateValue.Trim());
                }
                catch (System.FormatException)
                {
                    tempDate = DateTime.Parse("01/01/1900 00:00:00");
                }
            }
            else
            {
                tempDate = DateTime.Parse("01/01/1900 00:00:00");
            }

            return tempDate;
        }
    }
}
