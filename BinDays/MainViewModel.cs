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
                CollectionDay = new DayOfWeekViewModel(context.CurrentSetup.DateOfCollection.DayOfWeek);
                IsRecycling   = context.CurrentSetup.CalculateIfNextIsRecycling();
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
                var setup              = context.CurrentSetup;
                setup.DateOfCollection = DateTime.Today.Next(CollectionDay.Day);
                setup.IsRecycling      = IsRecycling;

                if (!context.Setup.Any())
                    context.Setup.InsertOnSubmit(setup);

                context.SubmitChanges();
            }

            new LiveTileUpdater().UpdateTile();
            Status.SetAction("Saved successfully.", true);
        }
    }
}