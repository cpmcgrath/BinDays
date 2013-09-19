using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.IO.IsolatedStorage;

namespace CMcG.BinDays.Views
{
    public partial class ViewRateApp : PhoneApplicationPage
    {
        public ViewRateApp()
        {
            InitializeComponent();
        }

        void NotNow(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        void DontAskAgain(object sender = null, RoutedEventArgs e = null)
        {
            IsolatedStorageSettings.ApplicationSettings["HasReviewed"] = true;
            IsolatedStorageSettings.ApplicationSettings.Save();
            NavigationService.GoBack();
        }

        void RateApp(object sender, RoutedEventArgs e)
        {
            new MarketplaceReviewTask().Show();
            DontAskAgain();
        }
    }
}