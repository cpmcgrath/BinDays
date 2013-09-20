using System;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Info;
using System.IO.IsolatedStorage;

namespace CMcG.BinDays
{
    public partial class App : Application
    {
        public PhoneApplicationFrame RootFrame { get; private set; }

        public new static App Current
        {
            get { return (App)Application.Current; }
        }

        public App()
        {
            InitializeComponent();
            InitializePhoneApplication();

            if (!IsolatedStorageSettings.ApplicationSettings.Contains("HasReviewed"))
                IsolatedStorageSettings.ApplicationSettings["HasReviewed"] = false;

            if (!IsolatedStorageSettings.ApplicationSettings.Contains("StartCount"))
                IsolatedStorageSettings.ApplicationSettings["StartCount"] = 0;

            int count = (int)IsolatedStorageSettings.ApplicationSettings["StartCount"];
            IsolatedStorageSettings.ApplicationSettings["StartCount"] = ++count;

            bool hasRated = (bool)IsolatedStorageSettings.ApplicationSettings["HasReviewed"];
            ShowRateApp  = !hasRated && (count > 3 && count % 4 == 0);
            ShowGoPro = LicenseInformation.IsTrial && (count > 3 && count % 4 == 2);


            if (System.Diagnostics.Debugger.IsAttached)
                ShowGraphicsProfiling();
            StartBackgroundAgent();
        }

        void ShowGraphicsProfiling()
        {
            Current.Host.Settings.EnableFrameRateCounter = true;
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;

            // Show the areas of the app that are being redrawn in each frame.
            //Current.Host.Settings.EnableRedrawRegions = true;

            // Shows areas of a page that are handed off to GPU with a colored overlay.
            //Current.Host.Settings.EnableCacheVisualization = true;
        }

        public bool ShowRateApp { get; set; }
        public bool ShowGoPro   { get; set; }

        void OnInitialLaunch(object sender, LaunchingEventArgs e)
        {
        }

        void OnReactivated(object sender, ActivatedEventArgs e)
        {
        }

        void OnDeactivated(object sender, DeactivatedEventArgs e)
        {
        }

        void OnClosing(object sender, ClosingEventArgs e)
        {
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debugger.Break();
        }

        void GlobalUnhandledExceptionHandler(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debugger.Break();
        }

        public static bool IsLowMemDevice
        {
            get
            {
                return (long)DeviceExtendedProperties.GetValue("DeviceTotalMemory") <= 268435456;
            }
        }

        void StartBackgroundAgent()
        {
            try
            {
                if (IsLowMemDevice)
                    return;
                var taskName = "BinDays.Agent";

                if (ScheduledActionService.Find(taskName) is PeriodicTask)
                    ScheduledActionService.Remove(taskName);

                ScheduledActionService.Add(new PeriodicTask(taskName)
                {
                    Description = "Updates whether it's a recycling week or not"
                });
            }
            catch
            {
            }

            //ScheduledActionService.LaunchForTest(taskName, TimeSpan.FromSeconds(15));
        }

        #region Phone application initialization

        // Avoid double-initialization
        bool phoneApplicationInitialized;

        // Do not add any additional code to this method
        void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            RootFrame.NavigationFailed += OnNavigationFailed;
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}