using System.Windows;
using Microsoft.Phone.Scheduler;
using System.Diagnostics;
using System.Windows.Controls;
using System;

namespace CMcG.BinDays.Agent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        static volatile bool m_classInitialized;

        public ScheduledAgent()
        {
            if (m_classInitialized)
                return;

            m_classInitialized = true;

            RunOnDispatcher(() => Application.Current.UnhandledException += GlobalUnhandledExceptionHandler);
        }

        void GlobalUnhandledExceptionHandler(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
                Debugger.Break();

            e.Handled = true;
            NotifyComplete();
        }

        protected override void OnInvoke(ScheduledTask task)
        {
            RunOnDispatcher(() =>
            {
                new LiveTileUpdater().UpdateTile();
                NotifyComplete();
            });
        }

        void RunOnDispatcher(Action action)
        {
            Deployment.Current.Dispatcher.BeginInvoke(action);
        }
    }
}