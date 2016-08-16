using Mvb.Core;

namespace Mvb.Droid
{
    public class Mvb
    {
        public static void Init()
        {
            UiRunnerDispenser.RegisterRunner(()=> new DroidUiRunner());
        }
    }
}