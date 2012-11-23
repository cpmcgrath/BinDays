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
                long result = (long)DeviceExtendedProperties.GetValue("ApplicationWorkingSetLimit");
                return result < 94371840L;
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
                var setup = context.CurrentSetup;
                var tile  = ShellTile.ActiveTiles.First();
                tile.Update(new StandardTileData
                           {
                               Title           = setup.DateOfCollection.DayOfWeek.ToRelativeString(),
                               BackContent     = "",//setup.CalculateIfNextIsRecycling() ? "recycling" : "not recycling",
                               BackgroundImage = new Uri(setup.CalculateIfNextIsRecycling() ? "/Images/TileRecycle.png" : "/Images/TileBlackBin.png", UriKind.Relative)
                           });
            }
        }
    }
}
