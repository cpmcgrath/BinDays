using Microsoft.Phone.Controls;
using System;

namespace CMcG.BinDays
{
    public partial class ViewMain : PhoneApplicationPage
    {
        public ViewMain()
        {
            InitializeComponent();
            DataContext = new MainVM();
        }

        void OnAdd(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/BinEdit.xaml", UriKind.Relative));
        }
    }
}