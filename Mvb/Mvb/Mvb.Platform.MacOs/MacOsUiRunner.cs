using System;
using Foundation;
using Mvb.Core.Abstract;

namespace Mvb.Platform.MacOs
{
	public class MacOsUiRunner : NSObject, IUiRunner
	{
		public void Run(Action action)
		{
			base.InvokeOnMainThread(action);
		}


		public void Run<T>(Action<T> action, T args)
		{
			base.InvokeOnMainThread(() => { action.Invoke(args); });
		}
	}
}

