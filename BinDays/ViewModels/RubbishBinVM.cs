using System.Linq;

namespace CMcG.BinDays
{
    public class RubbishBinVM
    {
        public RubbishBinVM(RubbishBin data)
        {
            Data = data;
        }

        public RubbishBin Data { get; private set; }

        public string BinType
        {
            get
            {
                return Data.BinType == BinDays.BinType.GeneralWaste ? "General Waste" : Data.BinType.ToString();
            }
        }

        public string Icon
        {
            get
            {
                return Data.BinType == BinDays.BinType.GeneralWaste ? ""
                     : Data.BinType == BinDays.BinType.Recycling    ? ""
                                                                    : "⚘";
            }
        }
    }
}