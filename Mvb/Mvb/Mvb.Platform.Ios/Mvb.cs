using Mvb.Core;

namespace Mvb.Platform.Ios
{
    public class MvbPlatform
    {
        public static void Init()
        {
            UiRunnerDispenser.RegisterRunner(() => new IosUiRunner());
        }
    }
}
