using System;
using System.Data.Linq.Mapping;

namespace CMcG.BinDays
{
    [Table]
    public class Setup : NotifyBase
    {
        #region public int      Id               { get; set; }
        int m_id;
        [Column(IsPrimaryKey = true)]
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
        #endregion
        #region public DateTime DateOfCollection { get; set; }
        DateTime m_dateOfCollection;

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
        #endregion
        #region public bool     IsRecycling      { get; set; }
        bool m_isRecycling;

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
        #endregion

        public bool CalculateIfNextIsRecycling()
        {
            var from = DateOfCollection;
            var to   = DateTime.Today.Next(from.DayOfWeek);

            int weeks = ((int)(to - from).TotalDays) / 7;
            return (weeks % 2 == 1) ^ IsRecycling;
        }
    }
}