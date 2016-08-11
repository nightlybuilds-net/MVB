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
using Mvb.Test.ViewModels;
using Debug = System.Diagnostics.Debug;

namespace Mvb.Test.Droid
{
    [Activity(Label = "Mvb.Test.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        private TestVm _vm;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);



            var editText = FindViewById<EditText>(Resource.Id.editText1);
            var changeBtn = FindViewById<Button>(Resource.Id.button1);
            var addBtn = FindViewById<Button>(Resource.Id.button2);
            var removeBtn = FindViewById<Button>(Resource.Id.button3);
            var longBtn = FindViewById<Button>(Resource.Id.button4);
            var addRangeBtn = FindViewById<Button>(Resource.Id.button5);
            var changeItemZero = FindViewById<Button>(Resource.Id.button6);

            changeBtn.Click += (sender, args) =>
            {
                this._vm.Test = "Cambio!";
            };

            addBtn.Click += (sender, args) =>
            {
                this._vm.TestCollection.Add(new TestObservable()
                {
                    Name = "ofewinf"
                });
            };

            removeBtn.Click += (sender, args) =>
            {
                this._vm.TestCollection.RemoveAt(this._vm.TestCollection.Count - 1);
            };

            longBtn.Click += async (sender, args) =>
            {
                await this._vm.LogTaskTest();
            };

            addRangeBtn.Click += (sender, args) =>
            {
                var newList = Enumerable.Range(1, 50);

                this._vm.TestCollection.AddRange(newList.Select(s => new TestObservable
                {
                    Name = s.ToString()
                }));
            };

            changeItemZero.Click += (sender, args) =>
            {
                this._vm.TestCollection[0].Name = "Pippo2";
            };


            this._vm = new TestVm();

            this._vm.Binder.AddAction<TestVm>(t => t.Test, () =>
            {
                editText.Text = "Cambiato!";
            });

            this._vm.Binder.AddAction<TestVm>(t => t.Wait, () =>
            { editText.SetBackgroundColor(this._vm.Wait ? Color.Red : Color.Transparent); });

            this._vm.Binder.AddActionForCollection<TestVm>(t => t.TestCollection, args =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    var t = args.NewItems.Count;

                    foreach (var newItem in args.NewItems)
                    {
                        Debug.WriteLine(newItem);
                    }
                }

                editText.Text = args.Action == NotifyCollectionChangedAction.Add ? $"A: {this._vm.TestCollection.Count}" : $"R: {this._vm.TestCollection.Count}";
            });


        }
    }
}

