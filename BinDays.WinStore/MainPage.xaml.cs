using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;

namespace BinDays.WinStore
{
    public sealed partial class MainPage : BinDays.WinStore.Common.LayoutAwarePage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            DefaultViewModel["Main"] = new MainViewModel();
        }

        void OnSave(object sender, RoutedEventArgs e)
        {
            ((MainViewModel)DefaultViewModel["Main"]).Save();
        }
    }
}
