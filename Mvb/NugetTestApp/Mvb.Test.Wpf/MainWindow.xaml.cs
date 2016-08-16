using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Mvb.Test.ModelBinders;

namespace Mvb.Test.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StockMb _mb;
        
        public MainWindow()
        {
            InitializeComponent();

            this._mb = new StockMb();

            this._mb.Binder.AddAction<StockMb>(t => t.Test, () =>
            {
                this.label.Content = "Ciao!";
            });

            this._mb.Binder.AddAction<StockMb>(t => t.Wait, () =>
            {
                this.label.Background = this._mb.Wait ? Brushes.Blue : Brushes.Transparent;

            });

            this._mb.Binder.AddActionForCollection<StockMb>(t => t.TestCollection, args =>
            {
                this.label.Content = args.Action == NotifyCollectionChangedAction.Add ? $"A: {this._mb.TestCollection.Count}" : $"R: {this._mb.TestCollection.Count}";
                ;
            });
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this._mb.Test = "cambia";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this._mb.TestCollection.Add("a");
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            this._mb.TestCollection.RemoveAt(this._mb.TestCollection.Count - 1);
        }

        private async void Button4_OnClick(object sender, RoutedEventArgs e)
        {
            await this._mb.LogTaskTest();
        }
    }
}
