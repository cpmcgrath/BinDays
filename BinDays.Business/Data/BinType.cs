using System;
using System.ComponentModel;

namespace CMcG.BinDays
{
    [Flags]
    public enum BinType
    {
        [Description("General Waste")]
        GeneralWaste = 1,
        Recycling    = 2,
        Green        = 4
    }
}
