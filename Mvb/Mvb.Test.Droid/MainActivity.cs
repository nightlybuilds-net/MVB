using System;
using System.Collections.Specialized;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Mvb.Core.Args;
using Mvb.Test.ModelBinders;
using Debug = System.Diagnostics.Debug;

namespace Mvb.Test.Droid
{
    [Activity(Label = "Mvb.Test.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        private StockMb _mb;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            this.SetContentView(Resource.Layout.Main);

            var editText = this.FindViewById<EditText>(Resource.Id.editText1);
            var changeBtn = this.FindViewById<Button>(Resource.Id.button1);
            var addBtn = this.FindViewById<Button>(Resource.Id.button2);
            var removeBtn = this.FindViewById<Button>(Resource.Id.button3);
            var longBtn = this.FindViewById<Button>(Resource.Id.button4);
            var addRangeBtn = this.FindViewById<Button>(Resource.Id.button5);
            var changeItemZero = this.FindViewById<Button>(Resource.Id.button6);

            changeBtn.Click += (sender, args) =>
            {
                this._mb.Test = "Cambio!";
            };

            addBtn.Click += (sender, args) =>
            {
                this._mb.TestCollection.Add(new Stock()
                {
                    Name = "ofewinf"
                });
            };

            removeBtn.Click += (sender, args) =>
            {
                this._mb.TestCollection.RemoveAt(this._mb.TestCollection.Count - 1);
            };

            longBtn.Click += async (sender, args) =>
            {
                await this._mb.LogTaskTest();
            };

            addRangeBtn.Click += (sender, args) =>
            {
                var newList = Enumerable.Range(1, 50);

                this._mb.TestCollection.AddRange(newList.Select(s => new Stock
                {
                    Name = s.ToString()
                }));
            };

            changeItemZero.Click += (sender, args) =>
            {
                this._mb.TestCollection[0].Name = "Pippo2";
            };


            this._mb = new StockMb();

            this._mb.Binder.AddAction<StockMb>(t => t.Test, () =>
            {
                editText.Text = "Cambiato!";
            });

            this._mb.Binder.AddAction<StockMb>(t => t.Wait, () =>
            { editText.SetBackgroundColor(this._mb.Wait ? Color.Red : Color.Transparent); });

            this._mb.Binder.AddActionForCollection<StockMb>(t => t.TestCollection, args =>
            {
                if (args.MvbUpdateAction == MvbUpdateAction.CollectionChanged)
                {
                    if (args.NotifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Add)
                    {
                        var t = args.NotifyCollectionChangedEventArgs.NewItems.Count;

                        foreach (var newItem in args.NotifyCollectionChangedEventArgs.NewItems)
                        {
                            Debug.WriteLine(newItem);
                        }
                    }

                    editText.Text = args.NotifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Add ? $"A: {this._mb.TestCollection.Count}" : $"R: {this._mb.TestCollection.Count}";
                }

                if (args.MvbUpdateAction == MvbUpdateAction.ItemChanged)
                {
                    editText.Text = $"[{args.MvbCollectionItemChanged.Index}] from {args.MvbCollectionItemChanged.NewValue} to {args.MvbCollectionItemChanged.OldValue}";
                }
                
            });


        }
    }
}

