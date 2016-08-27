using System;
using System.ComponentModel;
using Mvb.Core.Args;

namespace Mvb.Core.Abstract
{
    public interface IMvbNotifyPropertyChanged : INotifyPropertyChanged
    {
        event EventHandler<MvbPropertyChanged> MvbPropertyChanged;
    }
}