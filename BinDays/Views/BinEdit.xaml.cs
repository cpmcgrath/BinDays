using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace CMcG.BinDays.Views
{
    public partial class BinEdit : PhoneApplicationPage
    {
        public BinEdit()
        {
            InitializeComponent();
            DataContext = new BinEditVM();
        }

        void Save(object sender, EventArgs e)
        {
            this.FinishBinding();
            ((BinEditVM)DataContext).Save();
            NavigationService.GoBack();
        }
    }
}