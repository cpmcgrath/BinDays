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

        private void GoToStore(object sender, RoutedEventArgs e)
        {
            new MarketplaceDetailTask { ContentIdentifier = "" }.Show();
        }
    }
}