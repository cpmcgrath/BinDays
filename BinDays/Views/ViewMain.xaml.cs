using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System;
using System.Windows;

namespace CMcG.BinDays
{
    public partial class ViewMain : PhoneApplicationPage
    {
        public ViewMain()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataContext = new MainVM();
        }

        void OnAdd(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/BinEdit.xaml", UriKind.Relative));
        }

        void EditBin(object sender, System.Windows.RoutedEventArgs e)
        {
            var context = (RubbishBinVM)((FrameworkElement)sender).DataContext;
            NavigationService.Navigate(new Uri("/Views/BinEdit.xaml?id=" + context.Data.Id, UriKind.Relative));
        }

        void OnGoPro(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/ViewGoPro.xaml", UriKind.Relative));
        }

        void OnRateApp(object sender, EventArgs e)
        {
            new MarketplaceReviewTask().Show();
        }
    }
}