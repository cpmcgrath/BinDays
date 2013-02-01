using System;
using System.Linq;
using Microsoft.Phone.Shell;

namespace CMcG.BinDays
{
    public class MainViewModel : NotifyBase
    {
        public MainViewModel()
        {
            Status = new AppStatus { AutoRemove = true };
            using (var context = new DataStoreContext())
            {
                var bins = context.NextBinDay;
                if (!bins.Any())
                    return;

                CollectionDay = new DayOfWeekViewModel(bins.First().NextCollectionDate.DayOfWeek);
                IsRecycling   = bins.Any(x => x.BinType == BinType.Recycling);
            }
        }

        public DayOfWeekViewModel[] DayList
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

        public DayOfWeekViewModel CollectionDay { get; set; }
        public bool               IsRecycling   { get; set; }
        public AppStatus          Status        { get; private set; }

        public void Save()
        {
            using (var context = new DataStoreContext())
            {
                var normal = context.RubbishBins.FirstOrDefault(x => x.BinType == BinType.GeneralWaste);
                bool hasNormal = normal != null;
                normal = normal ?? new RubbishBin { BinType = BinType.GeneralWaste };
                normal.Interval = 7;
                normal.OriginDate = DateTime.Today.Next(CollectionDay.Day);

                if (!hasNormal)
                    context.RubbishBins.InsertOnSubmit(normal);

                var recycle = context.RubbishBins.FirstOrDefault(x => x.BinType == BinType.Recycling);
                bool hasRecycle = recycle != null;
                recycle = recycle ?? new RubbishBin { BinType = BinType.Recycling };
                recycle.Interval = 14;
                recycle.OriginDate = DateTime.Today.Next(CollectionDay.Day);
                if (IsRecycling)
                    recycle.OriginDate = recycle.OriginDate.AddDays(7);


                if (!hasRecycle)
                    context.RubbishBins.InsertOnSubmit(recycle);

                context.SubmitChanges();
            }

            new LiveTileUpdater().UpdateTile();
            Status.SetAction("Saved successfully.", true);
        }
    }
}