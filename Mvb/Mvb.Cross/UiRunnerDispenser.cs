using System;
using Mvb.Cross.Abstract;

namespace Mvb.Cross
{
    public static class UiRunnerDispenser
    {
        private static Func<IUiRunner> _dispenser;

        public static void RegisterRunner(Func<IUiRunner> theRunner)
        {
            _dispenser = theRunner;
        }

        internal static IUiRunner GetRunner()
        {
            if(_dispenser == null)
                throw new Exception("You need to register a UIRunner! Use UiRunnerDispenser.RegisterRunner method or platform specific Mvb.Init");

            return _dispenser.Invoke();   
        }
    }
}