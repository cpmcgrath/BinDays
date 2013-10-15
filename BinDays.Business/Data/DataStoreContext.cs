using System;
using System.Linq;
using System.Data.Linq;
using Microsoft.Phone.Data.Linq;
using System.Collections.Generic;

namespace CMcG.BinDays
{
    public partial class DataStoreContext : DataContext
    {
        public DataStoreContext(string connectionString = "Data Source=isostore:/DataStore.sdf")
            : base(connectionString)
        {
            new DataStoreUpdater().Upgrade(this);
        }

        public Table<RubbishBin> RubbishBins
        {
            get { return GetTable<RubbishBin>(); }
        }

        public CollectionDay NextCollectionDay
        {
            get { return CollectionDays.FirstOrDefault() ?? new CollectionDay(); }
        }

        public IEnumerable<CollectionDay> CollectionDays
        {
            get
            {
                if (!RubbishBins.Any())
                    yield break;

                var origin = DateTime.Today;
                for (int i = 0; i < 10; i++)
                {
                    var nextDay = RubbishBins.ToArray().Min(y => y.NextAfter(origin));
                    var bins = RubbishBins.ToArray().Where(x => x.NextAfter(origin) == nextDay).ToArray();
                    yield return new CollectionDay { Date = nextDay, Bins = bins };
                    origin = nextDay.AddDays(1);
                }
            }
        }

    }
}
