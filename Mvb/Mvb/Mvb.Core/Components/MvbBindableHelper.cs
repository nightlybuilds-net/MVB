using Mvb.Core.Base;

namespace Mvb.Core.Components
{
    public class MvbBindableHelper<T> : MvbBindable
    {
        private T _item;

        public T Item
        {
            get { return this._item; }
            protected set { this.SetProperty(ref this._item, value); }
        }

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
