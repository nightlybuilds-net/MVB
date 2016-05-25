using System;

namespace Mvb.Cross
{
    public abstract class MvbBase : Bindable
    {
        public MvBinder Binder { get; private set; }

        protected void InitBinder()
        {
            this.Binder = new MvBinder(this);
        }
    }
}
