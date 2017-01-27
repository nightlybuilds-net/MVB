using Mvb.Core;

namespace Mvb.Platform.Droid
{
    public class MvbPlatform
    {
        public static void Init()
        {
            Dispenser.RegisterRunner(()=> new DroidUiRunner());
        }
    }
}