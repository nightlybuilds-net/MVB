using System;

namespace Mvb.Cross.Args
{
    public class MvbCollectionItemChanged : MvbPropertyChanged
    {
        public MvbCollectionItemChanged(int index, string propertyName,object oldValue,object newValue) : base(propertyName,oldValue,newValue)
        {
            this.Index = index;
        }

        public int Index { get; }
    }

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