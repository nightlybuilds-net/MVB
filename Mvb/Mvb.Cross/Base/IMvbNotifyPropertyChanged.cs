using System;
using Mvb.Cross.Args;

namespace Mvb.Cross.Base
{
    public interface IMvbNotifyPropertyChanged 
    {
        event EventHandler<MvbPropertyChanged> MvbPropertyChanged;
    }
}