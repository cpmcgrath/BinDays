using System;
using System.ComponentModel;

namespace CMcG.BinDays
{
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged  = delegate { };

        protected void FirePropertyChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
