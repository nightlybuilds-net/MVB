using System;
using Mvb.Core;

namespace Mvb.Platform.MacOs
{
	public class MvbPlatform
	{
		public static void Init()
		{
			UiRunnerDispenser.RegisterRunner(() => new MacOsUiRunner());
		}
	}
}

