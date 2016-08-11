using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Mvb.Cross
{
    public class MvbCollection<T> : ObservableCollection<T>, IMvbCollection
    {
        public MvbCollection()
            : base()
        {
            base.CollectionChanged += this.OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (T item in notifyCollectionChangedEventArgs.OldItems)
                {
                    if(!(item is INotifyPropertyChanged)) continue;

                    //Removed items
                    ((INotifyPropertyChanged)item).PropertyChanged -= this.OnPropertyChanged;
                }
            }

            if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (T item in notifyCollectionChangedEventArgs.NewItems)
                {
                    if (!(item is INotifyPropertyChanged)) continue;

                    //Added items
                    ((INotifyPropertyChanged)item).PropertyChanged += this.OnPropertyChanged;
                }
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var index = this.IndexOf((T) sender);
            this.IndexUpdate?.Invoke(this,new IndexUpdateArgs(index));
        }

        public MvbCollection(IEnumerable<T> collection)
            : base(collection)
        {}

        public MvbCollection(List<T> list)
            : base(list)
        {}

		/// <summary>
		/// Adds the range.
		/// </summary>
		/// <returns>The range.</returns>
		/// <param name="range">Range.</param>
        public void AddRange(IEnumerable<T> range)
        {
            var startIndexd = this.Items.Count-1;
		    var enumerable = range as T[] ?? range.ToArray();
		    foreach (var item in enumerable)
            {
                this.Items.Add(item);
            }

            this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, enumerable.ToList(), startIndexd));
        }

		/// <summary>
		/// Reset the collection and addrange the specified range.
		/// This will fire a NotifyCollectionChanged for reset followed by a NotifyCollectionChanged for add
		/// </summary>
		/// <param name="range">Range.</param>
		public void Reset(IEnumerable<T> range)
        {
            this.Items.Clear();
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			this.AddRange(range);
        }

        public event EventHandler<IndexUpdateArgs> IndexUpdate;
    }

    public interface IMvbCollection : INotifyCollectionChanged
    {
        event EventHandler<IndexUpdateArgs> IndexUpdate;
    }

    public class IndexUpdateArgs : EventArgs
    {
        public int Index { get; private set; }

        public IndexUpdateArgs(int index)
        {
            this.Index = index;
        }
    }
}
