using System;
using Windows.Storage;

namespace BinDays
{
    public class Setup : NotifyBase
    {
        public Setup()
        {
            IsRecycling = (bool)(ApplicationData.Current.LocalSettings.Values["IsRecycling"] ?? false);
            string date = (string)ApplicationData.Current.LocalSettings.Values["DateOfCollection"] ?? DateTime.Today.ToString();
            DateOfCollection = DateTime.Parse(date);
        }

        DateTime m_dateOfCollection;
        bool     m_isRecycling;

        public DateTime DateOfCollection
        {
            get { return m_dateOfCollection; }
            set
            {
                if (m_dateOfCollection == value)
                    return;

                m_dateOfCollection = value;
                FirePropertyChanged("DateOfCollection");
            }
        }

        public bool IsRecycling
        {
            get { return m_isRecycling; }
            set
            {
                if (m_isRecycling == value)
                    return;

                m_isRecycling = value;
                FirePropertyChanged("IsRecycling");
            }
        }

        public bool CalculateIfNextIsRecycling()
        {
            var from = DateOfCollection;
            var to   = DateTime.Today.Next(from.DayOfWeek);

            int weeks = ((int)(to - from).TotalDays) / 7;
            return (weeks % 2 == 1) ^ IsRecycling;
        }

        public void Save()
        {
            ApplicationData.Current.LocalSettings.Values["IsRecycling"]      = IsRecycling;
            ApplicationData.Current.LocalSettings.Values["DateOfCollection"] = DateOfCollection.ToString();
        }
    }
}