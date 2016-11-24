using Mvb.Core;

namespace Mvb.Platform.Win.Wpf
{
    public class MvbPlatform
    {
        public static void Init()
        {
            Dispenser.RegisterRunner(() => new WpfUiRunner());
        }
    }
}
