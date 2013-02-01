using System;
using System.Data.Linq.Mapping;

namespace CMcG.BinDays
{
    [Table]
    public class RubbishBin : NotifyBase
    {
        int      m_id;
        BinType  m_binType;
        DateTime m_originDate;
        int      m_interval;

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
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
        public BinType BinType
        {
            get { return m_binType; }
            set
            {
                if (m_binType == value)
                    return;

                m_binType = value;
                FirePropertyChanged("BinType");
            }
        }

        [Column]
        public DateTime OriginDate
        {
            get { return m_originDate; }
            set
            {
                if (m_originDate == value)
                    return;

                m_originDate = value;
                FirePropertyChanged("OriginDate");
            }
        }

        [Column]
        public int Interval
        {
            get { return m_interval; }
            set
            {
                if (m_interval == value)
                    return;

                m_interval = value;
                FirePropertyChanged("Interval");
            }
        }

        public DateTime NextCollectionDate
        {
            get { return m_originDate.ToNext(Interval); }
        }
    }
}
