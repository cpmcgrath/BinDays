using System.Linq;

namespace CMcG.BinDays
{
    public class MainVM
    {
        public MainVM()
        {
            using (var context = new DataStoreContext())
            {
                var days = context.CollectionDays;
                if (days.Any())
                    Days = days.Take(3).Select(x => new DayVM(x)).ToArray();

                Bins = context.RubbishBins.Select(x => new RubbishBinVM(x)).ToArray();
            }
        }

        public DayVM[]        Days { get; set; }
        public RubbishBinVM[] Bins { get; set; }
    }
}