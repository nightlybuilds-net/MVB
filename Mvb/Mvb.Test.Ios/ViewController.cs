using System;

using UIKit;
using System.Collections.Specialized;
using Mvb.Test.ModelBinders;

namespace Mvb.Test.Ios
{
	public partial class ViewController : UIViewController
	{
		StockMb _mb;

		public ViewController (IntPtr handle) : base (handle)
		{
			this._mb = new StockMb ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			this._mb.Binder.AddAction<StockMb>(t => t.Test, () =>
				{
					label.Text = "Cambio!";
				});

			this._mb.Binder.AddAction<StockMb>(t => t.Wait, () =>
				{ 
					if(this._mb.Wait)
						label.BackgroundColor = UIColor.Red;
					else
						label.BackgroundColor = UIColor.Orange;
				});

			this._mb.Binder.AddActionForCollection<StockMb>(t => t.TestCollection, args =>
				{
					
					label.Text = args.Action == NotifyCollectionChangedAction.Add ? $"A: {this._mb.TestCollection.Count}" : $"R: {this._mb.TestCollection.Count}";
				});

		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		partial void UIButton4_TouchUpInside (UIButton sender)
		{
			this._mb.Test = "Cambio";
		}

		partial void UIButton5_TouchUpInside (UIButton sender)
		{
			this._mb.TestCollection.Add("");
		}

		partial void UIButton6_TouchUpInside (UIButton sender)
		{
			this._mb.TestCollection.RemoveAt(this._mb.TestCollection.Count - 1);
		}

		partial void UIButton7_TouchUpInside (UIButton sender)
		{
			this._mb.LogTaskTest();
		}
	}
}

