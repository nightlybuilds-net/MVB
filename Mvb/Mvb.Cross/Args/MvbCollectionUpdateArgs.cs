using System.Collections.Specialized;

namespace Mvb.Core.Args
{
    public class MvbCollectionUpdateArgs
    {
        public MvbUpdateAction MvbUpdateAction { get; set; }
        public NotifyCollectionChangedEventArgs NotifyCollectionChangedEventArgs { get; set; }
        public MvbCollectionItemChanged MvbCollectionItemChanged { get; set; }
    }
}