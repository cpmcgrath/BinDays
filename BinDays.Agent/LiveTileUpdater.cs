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
                return (long)DeviceExtendedProperties.GetValue("DeviceTotalMemory") <= 268435456;
            }
        }

        public void UpdateTile()
        {
            try
            {
                if (IsLowMemDevice)
                    return;
            }
            catch
            {
            }

            using (var context = new DataStoreContext())
            {
                var bins = context.NextBinDay;

                if (!bins.Any())
                    return;

                var tileImage = "/Images/Tile" + bins.OrderBy(x => x.BinType).Select(x => x.BinType.ToString()).Aggregate("") + ".png";

                var tile  = ShellTile.ActiveTiles.First();
                tile.Update(new StandardTileData
                           {
                               Title           = bins.First().NextCollectionDate.DayOfWeek.ToRelativeString(),
                               BackContent     = "",//setup.CalculateIfNextIsRecycling() ? "recycling" : "not recycling",
                               BackgroundImage = new Uri(tileImage, UriKind.Relative)
                           });
            }
        }
    }
}
