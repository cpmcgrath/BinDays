using System;
using System.Linq;

namespace BinDays.WinStore
{
    public class MainViewModel : NotifyBase
    {
        Setup m_setup = new Setup();

        public MainViewModel()
        {
            CollectionDay = new DayOfWeekViewModel(m_setup.DateOfCollection.DayOfWeek);
            IsRecycling   = m_setup.CalculateIfNextIsRecycling();

        }

        public DayOfWeekViewModel[] DayList
        {
            get
            {
                return new[]
                {
                    DayOfWeek.Sunday,
                    DayOfWeek.Monday,
                    DayOfWeek.Tuesday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Friday,
                    DayOfWeek.Saturday,
                }.Select(x => new DayOfWeekViewModel(x)).ToArray();
            }
        }

        public DayOfWeekViewModel CollectionDay { get; set; }
        public bool               IsRecycling   { get; set; }

        public void Save()
        {
            m_setup.DateOfCollection = DateTime.Today.Next(CollectionDay.Day);
            m_setup.IsRecycling      = IsRecycling;
            m_setup.Save();
        }
    }
}