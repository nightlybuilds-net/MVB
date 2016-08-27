using Mvb.Core;

namespace Mvb.Platform.Win.Uwp
{
    public class MvbPlatform
    {
        public static void Init()
        {
            UiRunnerDispenser.RegisterRunner(() => new UwpUiRunner());
        }
    }
}
