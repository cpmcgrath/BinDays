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

        public static DateTime ToNext(this DateTime instance, int interval)
        {
            var difference = DateTime.Today.AddDays(-1) - instance;
            var offset     = interval - (difference.Days + interval) % interval;
            return DateTime.Today.AddDays(offset - 1);
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
    }
}