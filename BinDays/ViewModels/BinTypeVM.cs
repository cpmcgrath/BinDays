using System;
using System.Linq;
using Microsoft.Phone.Shell;

namespace CMcG.BinDays
{
    public class BinTypeVM : NotifyBase
    {
        public static BinTypeVM[] GetVMs()
        {
            return new[] { BinType.GeneralWaste, BinType.Recycling }.Select(x => new BinTypeVM(x)).ToArray();
        }

        public BinTypeVM(BinType key)
        {
            Key = key;
        }

        public BinType Key { get; private set; }

        public override string ToString()
        {
            return Key == BinType.GeneralWaste ? "General Waste" : Key.ToString();
        }

        public override bool Equals(object obj)
        {
            var other = obj as BinTypeVM;
            return other != null && Key == other.Key;
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }
}