using System;

namespace Mvb.Core.Args
{
    public class MvbPropertyChanged : EventArgs
    {
        public MvbPropertyChanged(string propertyName, object oldValue, object newValue)
        {
            this.PropertyName = propertyName;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public string PropertyName { get; }
        public object OldValue { get; }
        public object NewValue { get; }
    }
}