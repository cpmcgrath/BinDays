using System;
using System.Linq;
using System.Data.Linq;
using Microsoft.Phone.Data.Linq;

namespace CMcG.BinDays
{
    public partial class DataStoreContext : DataContext
    {
        public DataStoreContext(string connectionString = "Data Source=isostore:/DataStore.sdf")
            : base(connectionString)
        {
            new DataStoreUpdater().Upgrade(this);
        }

        public Table<Setup> Setup
        {
            get { return GetTable<Setup>(); }
        }

        public Table<RubbishBin> RubbishBins
        {
            get { return GetTable<RubbishBin>(); }
        }

        [Obsolete]
        public Setup CurrentSetup
        {
            get { return Setup.FirstOrDefault() ?? new Setup(); }
        }

        public RubbishBin[] NextBinDay
        {
            get
            {
                if (!RubbishBins.Any())
                    return new RubbishBin[0];

                var nextDay = RubbishBins.ToArray().Min(y => y.NextCollectionDate);
                return RubbishBins.ToArray().Where(x => x.NextCollectionDate == nextDay).ToArray();
            }
        }
    }
}
