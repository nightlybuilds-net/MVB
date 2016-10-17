using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mvb.Core.Abstract;
using Mvb.Core.Args;

namespace Mvb.Core.Base
{
    /// <summary>
    ///     Implementation of IMvbNotifyPropertyChanged.
    /// </summary>
    public abstract class MvbBindable : IMvbBindable
    {
       
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<MvbPropertyChanged> MvbPropertyChanged;
        public void ClearHandler()
        {
            //clear propertychanged
            var invocationList = this.PropertyChanged?.GetInvocationList();
            if (invocationList != null)
            {
                foreach (var d in invocationList)
                    this.PropertyChanged -= d as PropertyChangedEventHandler;
            }

            //clear mvbpropertychanged
            var mvbInvocationList = this.MvbPropertyChanged?.GetInvocationList();
            if (mvbInvocationList != null)
            {
                foreach (var d in mvbInvocationList)
                    this.MvbPropertyChanged -= d as EventHandler<MvbPropertyChanged>;
            }

            var thisType = this.GetType().GetTypeInfo();

            // Clear MvbCollectionItemChanged on children
            foreach (var info in thisType.DeclaredProperties)
            {
                var isMvbCollection =
                    info.PropertyType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IMvbCollection));

                if (!isMvbCollection) continue;

                var obserableProp = (IMvbCollection)info.GetValue(this, null);
                if (obserableProp == null) continue;

                obserableProp.ClearMvbCollectionItemChanged();
            }

        }

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
