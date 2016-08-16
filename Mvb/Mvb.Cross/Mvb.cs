using Mvb.Core.Abstract;

namespace Mvb.Core
{
    public class Mvb
    {
        /// <summary>
        /// Register Null Ui Runner
        /// Just invoke actions
        /// </summary>
        public static void NullInit()
        {
            UiRunnerDispenser.RegisterRunner(() => new NullUiRunner());
        }
    }
}
