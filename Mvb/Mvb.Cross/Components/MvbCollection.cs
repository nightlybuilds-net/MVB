using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Mvb.Core.Abstract;
using Mvb.Core.Args;
using Mvb.Core.Base;

namespace Mvb.Core.Components
{
    public class MvbCollection<T> : ObservableCollection<T>, IMvbCollection
    {
        public event EventHandler<MvbCollectionItemChanged> MvbItemCollectionChanged;


        public MvbCollection()
            : base()
        {
            base.CollectionChanged += this.OnCollectionChanged;
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

        /// <summary>
        /// On Collection Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="notifyCollectionChangedEventArgs"></param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (T item in notifyCollectionChangedEventArgs.OldItems)
                {
                    if (!(item is IMvbNotifyPropertyChanged)) continue;

                    //Removed items
                    ((IMvbNotifyPropertyChanged)item).MvbPropertyChanged -= this.OnPropertyChanged;
                }
            }

            if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (T item in notifyCollectionChangedEventArgs.NewItems)
                {
                    if (!(item is IMvbNotifyPropertyChanged)) continue;

                    //Added items
                    ((IMvbNotifyPropertyChanged)item).MvbPropertyChanged += this.OnPropertyChanged;
                }
            }
        }

        /// <summary>
        /// Item Property Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnPropertyChanged(object sender, MvbPropertyChanged args)
        {
            var index = this.IndexOf((T)sender);
            this.MvbItemCollectionChanged?.Invoke(this, new MvbCollectionItemChanged(index, args.PropertyName, args.OldValue, args.NewValue));
        }

    }
}
