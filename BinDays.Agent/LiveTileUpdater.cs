using System;
using System.Linq;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Info;

namespace CMcG.BinDays
{
    public class LiveTileUpdater
    {
        public static bool IsLowMemDevice
        {
            get
            {
                try
                {
                    return (long)DeviceExtendedProperties.GetValue("DeviceTotalMemory") <= 268435456;
                }
                catch
                {
                    return false;
                }
            }
        }

        public void UpdateTile()
        {
            if (IsLowMemDevice)
                return;

            using (var context = new DataStoreContext())
            {
                var bins = context.NextBinDay;

                if (!bins.Any())
                    return;

                var tileImage = "/Images/Tile" + bins.OrderBy(x => x.BinType).Select(x => x.BinType.ToString()).Aggregate("") + ".png";

                var tile  = ShellTile.ActiveTiles.First();
                tile.Update(new FlipTileData
                           {
                               Title                = bins.First().NextCollectionDate.DayOfWeek.ToRelativeString(),
                               BackContent          = "",
                               BackgroundImage      = new Uri(tileImage, UriKind.Relative),
                               SmallBackgroundImage = new Uri(tileImage, UriKind.Relative),
                           });
            }
        }
    }
}
