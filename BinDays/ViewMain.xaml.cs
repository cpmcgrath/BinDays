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
                var bins = context.NextBinDay;
                if (!bins.Any())
                    return;

                Days = new[] { new DayVM(bins) };
            }
        }

        public DayVM[] Days { get; set; }
    }

    public class DayVM
    {
        public DayVM(RubbishBin[] bins)
        {
            Bins = bins;
            var date = bins.First().NextCollectionDate;
            Weekday = date.DayOfWeek.ToString().ToUpper();
            Date = date.ToRelativeString("MMM").ToUpper();
        }

        public string       Weekday { get; set; }
        public string       Date    { get; set; }
        public RubbishBin[] Bins    { get; set; }

    }
}