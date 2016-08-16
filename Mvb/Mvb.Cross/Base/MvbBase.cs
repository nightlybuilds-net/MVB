using Mvb.Cross.Components;

namespace Mvb.Cross.Base
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
