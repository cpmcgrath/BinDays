using System;
using System.Linq;
using System.Data.Linq;

namespace CMcG.BinDays
{
    public partial class DataStoreContext : System.Data.Linq.DataContext
    {
        public void CreateIfNotExists()
        {
            if (!DatabaseExists())
                CreateDatabase();
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

        public Setup CurrentSetup
        {
            get { return Setup.FirstOrDefault() ?? new Setup(); }
        }
    }
}
