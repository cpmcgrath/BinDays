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
                case 0 : store.DeleteDatabase(); Create(store); break;

                default : return;
            }

            schema.DatabaseSchemaVersion = CURRENT_VERSION;
            schema.Execute();
        }
    }
}
