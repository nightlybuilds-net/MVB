using System;

using UIKit;
using Mvb.Test.ViewModels;
using System.Collections.Specialized;

namespace Mvb.Test.Ios
{
	public partial class ViewController : UIViewController
	{
		TestVm _vm;

		public ViewController (IntPtr handle) : base (handle)
		{
			this._vm = new TestVm ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			this._vm.Binder.AddAction<TestVm>(t => t.Test, () =>
				{
					label.Text = "Cambio!";
				});

			this._vm.Binder.AddAction<TestVm>(t => t.Wait, () =>
				{ 
					if(this._vm.Wait)
						label.BackgroundColor = UIColor.Red;
					else
						label.BackgroundColor = UIColor.Orange;
				});

			this._vm.Binder.AddActionForCollection<TestVm>(t => t.TestCollection, args =>
				{
					
					label.Text = args.Action == NotifyCollectionChangedAction.Add ? $"A: {this._vm.TestCollection.Count}" : $"R: {this._vm.TestCollection.Count}";
				});

		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		partial void UIButton4_TouchUpInside (UIButton sender)
		{
			this._vm.Test = "Cambio";
		}

		partial void UIButton5_TouchUpInside (UIButton sender)
		{
			this._vm.TestCollection.Add("");
		}

		partial void UIButton6_TouchUpInside (UIButton sender)
		{
			this._vm.TestCollection.RemoveAt(this._vm.TestCollection.Count - 1);
		}

		partial void UIButton7_TouchUpInside (UIButton sender)
		{
			this._vm.LogTaskTest();
		}
	}
}

