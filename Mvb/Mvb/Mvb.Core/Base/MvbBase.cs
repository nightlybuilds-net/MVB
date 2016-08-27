using Mvb.Core.Components;

namespace Mvb.Core.Base
{
    public abstract class MvbBase : MvbBindable
    {
        public MvBinder Binder { get; private set; }

        protected void InitBinder()
        {
            this.Binder = new MvBinder(this);
        }
    }
}
