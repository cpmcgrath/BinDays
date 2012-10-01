using System;
using System.Linq;

namespace CMcG.BinDays
{
    public static class Extensions
    {
        public static DateTime Next(this DateTime instance, DayOfWeek day)
        {
            int fromDiff = ((int)day - (int)instance.DayOfWeek) % 6;
            return instance.AddDays(fromDiff);
        }

        public static string ToRelativeString(this DayOfWeek day)
        {
            return DateTime.Today.DayOfWeek            == day ? "Today" 
                 : DateTime.Today.AddDays(1).DayOfWeek == day ? "Tomorrow"
                                                              : day.ToString();
        }
    }
}