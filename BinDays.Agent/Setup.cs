using System;
using System.Data.Linq.Mapping;
using System.ComponentModel;

namespace CMcG.BinDays
{
    [Table]
    public class Setup : NotifyBase
    {
        int      m_id;
        DateTime m_dateOfCollection { get; set; }
        bool     m_isRecycling      { get; set; }

        [Column(IsPrimaryKey=true)]
        public int Id
        {
            get { return m_id; }
            set
            {
                if (m_id == value)
                    return;

                m_id = value;
                FirePropertyChanged("Id");
            }
        }

        [Column]
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

        [Column]
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
    }
}