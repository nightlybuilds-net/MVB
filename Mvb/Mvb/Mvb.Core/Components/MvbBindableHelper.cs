using Mvb.Core.Abstract;
using Mvb.Core.Base;

namespace Mvb.Core.Components
{
    public class MvbBindableHelper<T> : MvbBindable
    {
        public T Item { get; private set; }

        public void SetItem(T item)
        {
            this.Clear();
            this.Item = item;
        }

        /// <summary>
        /// Reset the state on SetItem
        /// </summary>
        public virtual void Clear()
        {}
    }
}
