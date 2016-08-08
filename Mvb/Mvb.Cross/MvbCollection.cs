using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Mvb.Cross
{
    public class MvbCollection<T> : ObservableCollection<T>
    {
        public MvbCollection()
            : base()
        {}

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
    }
}
