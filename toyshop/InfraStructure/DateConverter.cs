using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace toyshop.InfraStructure
{
    public static class DateConverter
    {
        public static string PersianDate(this PersianCalendar persian, DateTime date)
        {
            var result = $"{persian.GetHour(date).ToString().PadLeft(2, '0')}:{persian.GetMinute(date).ToString().PadLeft(2, '0')} , {persian.GetYear(date)}/{persian.GetMonth(date).ToString().PadLeft(2, '0')}/{persian.GetDayOfMonth(date).ToString().PadLeft(2, '0')}";
            return result;
        }
        public static DateTime ToMiladiDate(this string PersianDate)
        {
            int year = Convert.ToInt32(PersianDate.Substring(0, 4));
            int month = Convert.ToInt32(PersianDate.Substring(5, 2).PadLeft(2, '0'));
            int day = Convert.ToInt32(PersianDate.Substring(8, 2).PadLeft(2, '0'));
            DateTime Milady = new DateTime(year, month, day, new System.Globalization.PersianCalendar());
            
            return Milady;
        }
    }
}
