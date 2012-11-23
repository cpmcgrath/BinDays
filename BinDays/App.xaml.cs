using System;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Info;

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
                long result = (long)DeviceExtendedProperties.GetValue("ApplicationWorkingSetLimit");
                return result < 94371840L;
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