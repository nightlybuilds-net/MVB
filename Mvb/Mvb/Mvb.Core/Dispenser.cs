using System;
using Mvb.Core.Abstract;
using Mvb.Core.Components;

namespace Mvb.Core
{
    public static class Dispenser
    {
        private static IMvbMessenger _defaultMessenger = new MvbMessenger();
        private static Func<IUiRunner> _uiRunner;
        private static Func<IMvbMessenger> _messenger;

        /// <summary>
        /// Register UiRunner
        /// </summary>
        /// <param name="theRunner"></param>
        public static void RegisterRunner(Func<IUiRunner> theRunner)
        {
            _uiRunner = theRunner;
        }

        /// <summary>
        /// Register custom messenger
        /// </summary>
        /// <param name="messenger"></param>
        public static void RegisterMessenger(Func<IMvbMessenger> messenger)
        {
            _messenger = messenger;
        }

        /// <summary>
        /// Get uirunner
        /// </summary>
        /// <returns></returns>
        internal static IUiRunner GetRunner()
        {
            if(_uiRunner == null)
                throw new Exception("You need to register a UIRunner! Use platform specific Mvb.Init");

            return _uiRunner.Invoke();   
        }

        /// <summary>
        /// Get custom messenger
        /// </summary>
        /// <returns></returns>
        internal static IMvbMessenger GetMessenger()
        {
            if (_messenger == null)
                return _defaultMessenger;

 ;           return _messenger.Invoke();
        }
    }
}