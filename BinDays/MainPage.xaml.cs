using System;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Collections.Generic;

namespace CMcG.BinDays
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        void Save(object sender, EventArgs e)
        {
            ((MainViewModel)DataContext).Save();
        }
    }
}