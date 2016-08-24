using System;
using System.ComponentModel;

namespace Mvb.Platform.Win.WinForms
{
    public static class MvbWinFormExtensions
    {
        /// <summary>
        /// Invoke func on control
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="control"></param>
        /// <param name="call"></param>
        /// <returns></returns>
        public static TResult MvbInvoke<T, TResult>(this T control, Func<T, TResult> call) where T : ISynchronizeInvoke
        {
            if (control.InvokeRequired)
            {
                var asyncResult = control.BeginInvoke(call, new object[] { control });
                var result = control.EndInvoke(asyncResult);
                return (TResult)result;
            }
            else
                return call(control);
        }

        /// <summary>
        /// Invoke action on control
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="call"></param>
        public static void MvbInvoke<T>(this T control, Action<T> call) where T : ISynchronizeInvoke
        {
            if (control.InvokeRequired) control.BeginInvoke(call, new object[] { control });
            else
                call(control);
        }
    }
}
