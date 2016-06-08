using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvb.Cross
{
    public class MvbCollection<T> : ObservableCollection<T>
    {
        public MvbCollection()
            : base()
        {
        }

        public MvbCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public MvbCollection(List<T> list)
            : base(list)
        {
        }

        public void AddRange(IEnumerable<T> range)
        {
            var startIndexd = this.Items.Count-1;
            foreach (var item in range)
            {
                Items.Add(item);
            }

            this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            //this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, range.ToList(), startIndexd));
        }

        public void Reset(IEnumerable<T> range)
        {
            this.Items.Clear();
            this.AddRange(range);
        }
    }
}
