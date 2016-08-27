using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Mvb.Core.Abstract;
using Mvb.Core.Args;

namespace Mvb.Core.Base
{
    /// <summary>
    ///     Implementation of IMvbNotifyPropertyChanged.
    /// </summary>
    public abstract class MvbBindable : IMvbNotifyPropertyChanged
    {
       
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<MvbPropertyChanged> MvbPropertyChanged;
       
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (Equals(storage, value)) return false;

            var temp = storage; 
            storage = value;

            if (propertyName != null)
            {
                this.OnPropertyChanged(propertyName);
                this.MvbPropertyChanged?.Invoke(this, new MvbPropertyChanged(propertyName, temp, value));
            }

            return true;
        }

        
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
