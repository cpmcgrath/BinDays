using System;
using System.Collections.Generic;
using System.Linq;

namespace CMcG.BinDays
{
    public static class Extensions
    {
        public static DateTime Next(this DateTime instance, DayOfWeek day)
        {
            int fromDiff = ((int)day - (int)instance.DayOfWeek + 7) % 7;
            return instance.AddDays(fromDiff);
        }

        public static DateTime ToNext(this DateTime instance, int interval, DateTime from)
        {
            var difference = from.AddDays(-1) - instance;
            var offset     = interval - (difference.Days + interval) % interval;
            return from.AddDays(offset - 1);
        }

        public static string Aggregate(this IEnumerable<string> source, string join)
        {
            return string.Join(join, source.ToArray());
        }

        public static string ToRelativeString(this DayOfWeek day)
        {
            return DateTime.Today.DayOfWeek            == day ? "Today" 
                 : DateTime.Today.AddDays(1).DayOfWeek == day ? "Tomorrow"
                                                              : day.ToString();
        }

        public static string ToRelativeString(this DateTime date, string monthFormat = "MMMM")
        {
            return DateTime.Today == date            ? "Today"
                 : DateTime.Today.AddDays(1) == date ? "Tomorrow"
                                                     : string.Format("{0}{2} {1:" + monthFormat + "}", date.Day, date, GetDaySuffix(date.Day));
        }

        static string GetDaySuffix(int day)
        {
            switch (day)
            {
                case 1:
                case 21:
                case 31: return "st";
                case 2:
                case 22: return "nd";
                case 3:
                case 23: return "rd";
                default: return "th";
            }
        }
    }
}