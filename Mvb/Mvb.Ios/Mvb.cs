using Mvb.Core;

namespace Mvb.Ios
{
    public class Mvb
    {
        public static void Init()
        {
            UiRunnerDispenser.RegisterRunner(() => new IosUiRunner());
        }
    }
}
