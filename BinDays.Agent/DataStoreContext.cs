using System;
using System.Linq;
using System.Data.Linq;
using Microsoft.Phone.Data.Linq;

namespace CMcG.BinDays
{
    public partial class DataStoreContext : System.Data.Linq.DataContext
    {
        public void CreateIfNotExists()
        {
            if (!DatabaseExists())
            {
                CreateDatabase();
                var schemaUpdater = this.CreateDatabaseSchemaUpdater();
                schemaUpdater.DatabaseSchemaVersion = 1;
                schemaUpdater.Execute();
            }
            else
            {
                var schemaUpdater = this.CreateDatabaseSchemaUpdater();
                int version       = schemaUpdater.DatabaseSchemaVersion;

                if (version == 0)
                {
                    schemaUpdater.AddTable<RubbishBin>();
                    schemaUpdater.DatabaseSchemaVersion = 1;
                    schemaUpdater.Execute();

                    if (Setup.Any())
                    {
                        var general   = new RubbishBin { BinType = BinType.GeneralWaste, Interval =  7, OriginDate = CurrentSetup.DateOfCollection };
                        var recycling = new RubbishBin { BinType = BinType.Recycling,    Interval = 14, OriginDate = CurrentSetup.DateOfCollection.AddDays(CurrentSetup.IsRecycling ? 0 : 7) };

                        RubbishBins.InsertAllOnSubmit(new[] { general, recycling });
                        Setup.DeleteAllOnSubmit(Setup);
                        SubmitChanges();
                    }
                }
            }
        }

        public DataStoreContext(string connectionString = "Data Source=isostore:/DataStore.sdf")
            : base(connectionString)
        {
            CreateIfNotExists();
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
            get { return RubbishBins.Where(x => x.NextCollectionDate == RubbishBins.Max(y => y.NextCollectionDate)).ToArray(); }
        }
    }
}
