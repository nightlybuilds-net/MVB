// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Mvb.FakeContacts.iOS.App
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView ContactList { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton LoadBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ShakeBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SummaryLbl { get; set; }

        [Action ("LoadBtnTouchDown:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void LoadBtnTouchDown (UIKit.UIButton sender);

        [Action ("ShakeNamesBtnTouchDown:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ShakeNamesBtnTouchDown (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (ContactList != null) {
                ContactList.Dispose ();
                ContactList = null;
            }

            if (LoadBtn != null) {
                LoadBtn.Dispose ();
                LoadBtn = null;
            }

            if (ShakeBtn != null) {
                ShakeBtn.Dispose ();
                ShakeBtn = null;
            }

            if (SummaryLbl != null) {
                SummaryLbl.Dispose ();
                SummaryLbl = null;
            }
        }
    }
}