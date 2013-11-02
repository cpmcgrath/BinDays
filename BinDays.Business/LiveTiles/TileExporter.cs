using System;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CMcG.BinDays.Business.LiveTiles
{
    public class TileData
    {
        public bool IsGeneralWaste { get; set; }
        public bool IsRecycling    { get; set; }
        public bool IsGreen        { get; set; }

        public DateTime Date { get; set; }
    }

    public class TileExporter
    {
        public void Export(CollectionDay day)
        {
            var data = new TileData
            {
                Date           = day.Date,
                IsGeneralWaste = day.Bins.Any(x => x.BinType == BinType.GeneralWaste),
                IsRecycling    = day.Bins.Any(x => x.BinType == BinType.Recycling),
                IsGreen        = day.Bins.Any(x => x.BinType == BinType.Green)
            };
            ExportSmall(data);
            ExportStandard(data);
            ExportWide(data);
        }

        public void ExportSmall(TileData data)
        {
            var tile = new Small
            {
                ActualDay       = data.Date.DayOfWeek.ToString().Substring(0, 3).ToUpper(),
                HasGeneral      = data.IsGeneralWaste,
                HasRecycling    = data.IsRecycling,
                HasGreen        = data.IsGreen,
                IsBold          = data.Date == DateTime.Today || data.Date == DateTime.Today.AddDays(1),
                IsAdvertisement = LicenseInformation.IsTrial
            };
            Export(tile, 159, 159, "/Shared/ShellContent/SmallTile.jpg");
        }

        public void ExportStandard(TileData data)
        {
            var tile = new Standard
            {
                ActualDay    = data.Date.DayOfWeek.ToRelativeString().ToUpper(),
                HasGeneral   = data.IsGeneralWaste,
                HasRecycling = data.IsRecycling,
                HasGreen     = data.IsGreen
            };
            Export(tile, 336, 336, "/Shared/ShellContent/StandardTile.jpg");
        }

        public void ExportWide(TileData data)
        {
            var tile = new Wide
            {
                ActualDate   = data.Date.ToRelativeString().ToUpper(),
                ActualDay    = data.Date.DayOfWeek.ToString().ToUpper(),
                HasGeneral   = data.IsGeneralWaste,
                HasRecycling = data.IsRecycling,
                HasGreen     = data.IsGreen
            };
            Export(tile, 691, 336, "/Shared/ShellContent/WideTile.jpg");
        }

        void Export(UserControl tile, int width, int height, string filename)
        {
            tile.Measure(new Size(width, height));
            tile.Arrange(new Rect(0, 0, width, height));

            var bmp = new WriteableBitmap(width, height);
            bmp.Render(tile, null);
            bmp.Invalidate();

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            using (var stream = store.OpenFile(filename, System.IO.FileMode.OpenOrCreate))
            {
                bmp.SaveJpeg(stream, width, height, 0, 100);
            }
        }
    }
}
