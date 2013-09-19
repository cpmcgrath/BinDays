﻿using System;
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
    public partial class Small : UserControl
    {
        public Small()
        {
            InitializeComponent();
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

        public bool IsAdvertisement
        {
            get { return Advertisement.Visibility == Visibility.Visible; }
            set { Advertisement.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }

        public bool IsBold
        {
            get { return lblActualDay.FontWeight == FontWeights.Bold; }
            set { lblActualDay.FontWeight = value ? FontWeights.Bold : FontWeights.Normal; }
        }
    }
}
