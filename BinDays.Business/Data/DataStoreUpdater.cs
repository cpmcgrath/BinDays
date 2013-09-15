using System;
using System.Linq;
using System.Data.Linq;
using Microsoft.Phone.Data.Linq;

namespace CMcG.BinDays
{
    public class DataStoreUpdater
    {
        const int CURRENT_VERSION = 1;

        void Create(DataStoreContext store)
        {
            store.CreateDatabase();
            var schemaUpdater = store.CreateDatabaseSchemaUpdater();
            schemaUpdater.DatabaseSchemaVersion = CURRENT_VERSION;
            schemaUpdater.Execute();
        }

        public void Upgrade(DataStoreContext store)
        {
            if (!store.DatabaseExists())
            {
                Create(store);
                return;
            }

            var schema  = store.CreateDatabaseSchemaUpdater();
            int version = schema.DatabaseSchemaVersion;

            switch (version)
            {
                case 0 : UpgradeToVersion1(store, schema); break;

                default : return;
            }

            schema.DatabaseSchemaVersion = CURRENT_VERSION;
            schema.Execute();
        }

        void UpgradeToVersion1(DataStoreContext store, DatabaseSchemaUpdater schema)
        {
            schema.AddTable<RubbishBin>();
            schema.Execute();

            if (store.Setup.Any())
            {
                var general   = new RubbishBin { BinType = BinType.GeneralWaste, Interval = 7, OriginDate = store.CurrentSetup.DateOfCollection };
                var recycling = new RubbishBin { BinType = BinType.Recycling, Interval = 14, OriginDate = store.CurrentSetup.DateOfCollection.AddDays(store.CurrentSetup.IsRecycling ? 0 : 7) };

                store.RubbishBins.InsertAllOnSubmit(new[] { general, recycling });
                store.Setup.DeleteAllOnSubmit(store.Setup);
                store.SubmitChanges();
            }

        }
    }
}
