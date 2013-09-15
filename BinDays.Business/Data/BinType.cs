using System;

namespace CMcG.BinDays
{
    [Flags]
    public enum BinType
    {
        GeneralWaste = 1,
        Recycling    = 2,
        Green        = 4
    }
}
