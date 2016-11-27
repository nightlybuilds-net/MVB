using System;
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
            Dispenser.RegisterRunner(() => new NullUiRunner());
        }

        /// <summary>
        /// Register Custom MvbMessenger
        /// </summary>
        public static void RegisterMvbMessenger(Func<IMvbMessenger> mvbMessenger)
        {
            Dispenser.RegisterMessenger(mvbMessenger);
        }
    }
}
