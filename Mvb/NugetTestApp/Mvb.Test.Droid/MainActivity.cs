using System;
using System.Collections.Specialized;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Mvb.Test.ModelBinders;

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
            SetContentView(Resource.Layout.Main);



            var editText = FindViewById<EditText>(Resource.Id.editText1);
            var changeBtn = FindViewById<Button>(Resource.Id.button1);
            var addBtn = FindViewById<Button>(Resource.Id.button2);
            var removeBtn = FindViewById<Button>(Resource.Id.button3);
            var longBtn = FindViewById<Button>(Resource.Id.button4);

            changeBtn.Click += (sender, args) =>
            {
                this._mb.Test = "Cambio!";
            };

            addBtn.Click += (sender, args) =>
            {
                this._mb.TestCollection.Add("ofewinf");
            };

            removeBtn.Click += (sender, args) =>
            {
                this._mb.TestCollection.RemoveAt(this._mb.TestCollection.Count - 1);
            };

            longBtn.Click += async (sender, args) =>
            {
                await this._mb.LogTaskTest();
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
                editText.Text = args.Action == NotifyCollectionChangedAction.Add ? $"A: {this._mb.TestCollection.Count}" : $"R: {this._mb.TestCollection.Count}";
            });


        }
    }
}

