namespace Mvb.Core.Args
{
    public class MvbCollectionItemChanged : MvbPropertyChanged
    {
        public MvbCollectionItemChanged(int index, string propertyName,object oldValue,object newValue) : base(propertyName,oldValue,newValue)
        {
            this.Index = index;
        }

        public int Index { get; }
    }
}