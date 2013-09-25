using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System.Windows;

namespace CMcG.BinDays.Views
{
    public partial class ViewGoPro : PhoneApplicationPage
    {
        public ViewGoPro()
        {
            InitializeComponent();
        }

        void GoToStore(object sender, RoutedEventArgs e)
        {
            new MarketplaceDetailTask { ContentIdentifier = "1aa723f3-51ca-43f2-94f9-44a7a17505cd" }.Show();
            NavigationService.GoBack();
        }

        private void NotNow(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}