using System;
using System.Linq;
using Microsoft.Phone.Shell;

namespace CMcG.BinDays
{
    public class LiveTileUpdater
    {
        public void UpdateTile()
        {
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
