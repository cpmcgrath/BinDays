using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace CMcG.BinDays.Business.LiveTiles
{
    public partial class Wide : UserControl
    {
        public Wide()
        {
            InitializeComponent();
        }

        public string ActualDate
        {
            get { return lblActualDate.Text; }
            set { lblActualDate.Text = value; }
        }

        public string ActualDay
        {
            get { return lblActualDay.Text; }
            set { lblActualDay.Text = value; }
        }

        public bool HasGeneral
        {
            get { return General.Visibility == Visibility.Visible; }
            set { General.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }

        public bool HasRecycling
        {
            get { return Recycling.Visibility == Visibility.Visible; }
            set { Recycling.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }

        public bool HasGreen
        {
            get { return Green.Visibility == Visibility.Visible; }
            set { Green.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }
    }
}
