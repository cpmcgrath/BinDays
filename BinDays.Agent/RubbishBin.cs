using System;
using System.Data.Linq.Mapping;

namespace CMcG.BinDays
{
    [Table]
    public class RubbishBin : NotifyBase
    {
        #region public int      Id         { get; set; }
        int      m_id;

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
        #endregion
        #region public BinType  BinType    { get; set; }
        BinType  m_binType;

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
        #endregion
        #region public DateTime OriginDate { get; set; }
        DateTime m_originDate;

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
        #endregion
        #region public int      Interval   { get; set; }
        int      m_interval;
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
        #endregion

        public DateTime NextCollectionDate
        {
            get { return m_originDate.ToNext(Interval); }
        }
    }
}
