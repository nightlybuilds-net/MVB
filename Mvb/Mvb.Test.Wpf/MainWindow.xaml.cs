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
using Mvb.Test.ViewModels;

namespace Mvb.Test.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TestVm _vm;
        
        public MainWindow()
        {
            InitializeComponent();

            this._vm = new TestVm();

            this._vm.Binder.AddAction<TestVm>(t => t.Test, () =>
            {
                this.label.Content = "Ciao!";
            });

            this._vm.Binder.AddAction<TestVm>(t => t.Wait, () =>
            {
                this.label.Background = this._vm.Wait ? Brushes.Blue : Brushes.Transparent;

            });

            this._vm.Binder.AddActionForCollection<TestVm>(t => t.TestCollection, args =>
            {
                this.label.Content = args.Action == NotifyCollectionChangedAction.Add ? $"A: {this._vm.TestCollection.Count}" : $"R: {this._vm.TestCollection.Count}";
                ;
            });
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this._vm.Test = "cambia";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this._vm.TestCollection.Add("a");
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            this._vm.TestCollection.RemoveAt(this._vm.TestCollection.Count - 1);
        }

        private async void Button4_OnClick(object sender, RoutedEventArgs e)
        {
            await this._vm.LogTaskTest();
        }
    }
}
