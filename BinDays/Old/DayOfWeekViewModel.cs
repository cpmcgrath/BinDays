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
            return Day.ToRelativeString();
        }

        public override bool Equals(object obj)
        {
            return obj is DayOfWeekViewModel && Day.Equals(((DayOfWeekViewModel)obj).Day);
        }

        public override int GetHashCode()
        {
            return Day.GetHashCode();
        }

        public static DayOfWeekViewModel[] DayList
        {
            get
            {
                return new[]
                {
                    DayOfWeek.Sunday,
                    DayOfWeek.Monday,
                    DayOfWeek.Tuesday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Friday,
                    DayOfWeek.Saturday,
                }.Select(x => new DayOfWeekViewModel(x)).ToArray();
            }
        }
    }
}
