using AppKit;
using DryIoc;
using Foundation;
using Mvb.Core;
using Mvb.FakeContacts.Abstract;
using Mvb.FakeContacts.Cocoa.Services;
using Mvb.FakeContacts.Concrete;
using Mvb.FakeContacts.ModelBinders;

namespace Mvb.FakeContacts.Cocoa.App
{
	[Register("AppDelegate")]
	public class AppDelegate : NSApplicationDelegate
	{
		public static IContainer Ioc;

		public AppDelegate()
		{
		}

		public override void DidFinishLaunching(NSNotification notification)
		{
			// Insert code here to initialize your application


		}

		public override void WillTerminate(NSNotification notification)
		{
			// Insert code here to tear down your application
		}
	}
}

