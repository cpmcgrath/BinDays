using System;
using System.Linq;
using Microsoft.Phone.Shell;

namespace CMcG.BinDays
{
    public class BinEditVM : NotifyBase
    {
        public BinEditVM()
        {
            Status = new AppStatus { AutoRemove = true };
            BinType = BinDays.BinType.GeneralWaste;
            OriginDate = DateTime.Today;
            WeekInterval = 1;
        }

        public BinType[] BinTypeList
        {
            get { return Enum.GetValues(typeof(BinType)).Cast<BinType>().ToArray(); }
        }

        public BinType   BinType      { get; set; }
        public DateTime  OriginDate   { get; set; }
        public int       WeekInterval { get; set; }
        public AppStatus Status       { get; private set; }

        public void Save()
        {
            using (var context = new DataStoreContext())
            {
                var bin = new RubbishBin
                {
                    BinType    = BinType,
                    OriginDate = OriginDate,
                    Interval   = WeekInterval * 7
                };
                context.RubbishBins.InsertOnSubmit(bin);
                context.SubmitChanges();
            }

            new LiveTileUpdater().UpdateTile();
            Status.SetAction("Saved successfully.", true);
        }
    }
}