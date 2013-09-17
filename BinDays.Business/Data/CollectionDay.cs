using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMcG.BinDays
{
    public class CollectionDay
    {
        public CollectionDay()
        {
            Date = DateTime.Today;
            Bins = new RubbishBin[0];
        }

        public DateTime     Date { get; set; }
        public RubbishBin[] Bins { get; set; }
    }
}
