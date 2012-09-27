using System;
using System.Linq;

namespace CMcG.BinDays
{
    public class DayOfWeekViewModel
    {
        public DayOfWeekViewModel(DayOfWeek day)
        {
            Day = day;
        }
        public DayOfWeek Day { get; private set; }

        public override string ToString()
        {
            return DateTime.Today.DayOfWeek == Day ? "Today" : Day.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is DayOfWeekViewModel && Day.Equals(((DayOfWeekViewModel)obj).Day);
        }

        public override int GetHashCode()
        {
            return Day.GetHashCode();
        }
    }
}
