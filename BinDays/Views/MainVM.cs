using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace CMcG.BinDays
{
    public partial class ViewMain : PhoneApplicationPage
    {
        public ViewMain()
        {
            InitializeComponent();
            DataContext = new MainVM();
        }

    }

    public class MainVM
    {
        public MainVM()
        {
            using (var context = new DataStoreContext())
            {
                var days = context.CollectionDays;
                if (days.Any())
                    Days = days.Take(3).Select(x => new DayVM(x)).ToArray();

                Bins = context.RubbishBins.Select(x => new RubbishBinVM(x)).ToArray();
            }
        }

        public DayVM[]        Days { get; set; }
        public RubbishBinVM[] Bins { get; set; }

    }

    public class DayVM
    {
        public DayVM(CollectionDay day)
        {
            Data    = day;
            Weekday = day.Date.DayOfWeek.ToString().ToUpper();
            Date    = day.Date.ToRelativeString("MMM").ToUpper();
        }

        public string        Weekday { get; set; }
        public string        Date    { get; set; }
        public CollectionDay Data    { get; set; }

        public RubbishBinVM[] Bins
        {
            get { return Data.Bins.Select(x => new RubbishBinVM(x)).ToArray(); }
        }
    }

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
                return Data.BinType == BinDays.BinType.GeneralWaste ? "" : "";
            }
        }
    }
}