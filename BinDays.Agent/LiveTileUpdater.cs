using System;
using System.Linq;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Info;
using CMcG.BinDays.Business.LiveTiles;

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
                var day = context.NextCollectionDay;

                if (!day.Bins.Any())
                    return;

                new TileExporter().Export(day);

                var tile  = ShellTile.ActiveTiles.First();
                tile.Update(new FlipTileData
                           {
                               Title                = "",
                               BackContent          = "",
                               BackgroundImage      = new Uri("isostore:/Shared/ShellContent/StandardTile.jpg", UriKind.Absolute),
                               SmallBackgroundImage = new Uri("isostore:/Shared/ShellContent/SmallTile.jpg",    UriKind.Absolute),
                               WideBackgroundImage  = new Uri("isostore:/Shared/ShellContent/WideTile.jpg",     UriKind.Absolute),
                           });
            }
        }
    }
}
