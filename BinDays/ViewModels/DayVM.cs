using System.Linq;

namespace CMcG.BinDays
{
    public class DayVM
    {
        public DayVM(CollectionDay day)
        {
            Data    = day;
            Weekday = day.Date.DayOfWeek.ToString().ToUpper();
            Date    = day.Date.ToRelativeString("MMM").ToUpper();
        }

        public string        Weekday { get; set; }
        public string        Date    { get; set; }
        public CollectionDay Data    { get; set; }

        public RubbishBinVM[] Bins
        {
            get { return Data.Bins.Select(x => new RubbishBinVM(x)).ToArray(); }
        }
    }
}