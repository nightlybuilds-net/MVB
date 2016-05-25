using System;
using System.Collections.Specialized;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Mvb.Test.ViewModels;

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

            changeBtn.Click += (sender, args) =>
            {
                this._vm.Test = "Cambio!";
            };

            addBtn.Click += (sender, args) =>
            {
                this._vm.TestCollection.Add("ofewinf");
            };

            removeBtn.Click += (sender, args) =>
            {
                this._vm.TestCollection.RemoveAt(this._vm.TestCollection.Count - 1);
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
                editText.Text = args.Action == NotifyCollectionChangedAction.Add ? $"A: {this._vm.TestCollection.Count}" : $"R: {this._vm.TestCollection.Count}";
            });


        }
    }
}

