using Microsoft.Phone.Controls;
using System;

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
    }
}