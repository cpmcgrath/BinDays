using System;
using System.Linq;
using Microsoft.Phone.Shell;

namespace CMcG.BinDays
{
    public class BinEditVM : NotifyBase
    {
        public BinEditVM()
        {
            Status       = new AppStatus { AutoRemove = true };
            BinType      = BinDays.BinType.GeneralWaste;
            OriginDate   = DateTime.Today;
            WeekInterval = 1;
            EditType     = "NEW";
            Id           = -1;
        }

        public BinEditVM(int id) : this()
        {
            using (var store = new DataStoreContext())
            {
                var bin      = store.RubbishBins.First(x => x.Id == id);
                BinType      = bin.BinType;
                OriginDate   = bin.OriginDate;
                WeekInterval = bin.Interval / 7;
            }
            EditType = "EDIT";
            Id       = id;
        }

        public BinType[] BinTypeList
        {
            get { return Enum.GetValues(typeof(BinType)).Cast<BinType>().ToArray(); }
        }

        public int       Id           { get; set; }
        public string    EditType     { get; set; }
        public BinType   BinType      { get; set; }
        public DateTime  OriginDate   { get; set; }
        public int       WeekInterval { get; set; }
        public AppStatus Status       { get; private set; }

        public void Save()
        {
            using (var store = new DataStoreContext())
            {
                var bin = store.RubbishBins.FirstOrDefault(x => x.Id == Id);
                if (bin != null)
                {
                    store.RubbishBins.DeleteOnSubmit(bin);
                    store.SubmitChanges();
                }

                bin = new RubbishBin
                {
                    BinType    = BinType,
                    OriginDate = OriginDate,
                    Interval   = WeekInterval * 7
                };
                store.RubbishBins.InsertOnSubmit(bin);
                store.SubmitChanges();
            }

            new LiveTileUpdater().UpdateTile();
            Status.SetAction("Saved successfully.", true);
        }

        public void Delete()
        {
            using (var store = new DataStoreContext())
            {
                var bin = store.RubbishBins.FirstOrDefault(x => x.Id == Id);
                if (bin != null)
                {
                    store.RubbishBins.DeleteOnSubmit(bin);
                    store.SubmitChanges();
                }
            }

            new LiveTileUpdater().UpdateTile();
            Status.SetAction("Delete successfully.", true);
        }
    }
}