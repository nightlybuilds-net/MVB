using System;
using System.Collections.Specialized;
using Mvb.Core.Args;

namespace Mvb.Core.Abstract
{
    public interface IMvbCollection : INotifyCollectionChanged
    {
        event EventHandler<MvbCollectionItemChanged> MvbItemCollectionChanged;
    }
}