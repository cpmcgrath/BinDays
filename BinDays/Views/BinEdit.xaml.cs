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
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode != NavigationMode.New)
                return;

            if (NavigationContext.QueryString.ContainsKey("id"))
                DataContext = new BinEditVM(int.Parse(NavigationContext.QueryString["id"]));
            else
                DataContext = new BinEditVM();

        }

        void Save(object sender, EventArgs e)
        {
            this.FinishBinding();
            ((BinEditVM)DataContext).Save();
            NavigationService.GoBack();
        }

        void Delete(object sender, EventArgs e)
        {
            ((BinEditVM)DataContext).Delete();
            NavigationService.GoBack();
        }
    }
}