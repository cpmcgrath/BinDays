using System;
using System.Linq;

namespace BinDays.WinStore
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
    }
}
