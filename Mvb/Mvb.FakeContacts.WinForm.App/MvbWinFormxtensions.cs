using System;
using System.ComponentModel;

namespace Mvb.FakeContacts.WinForm.App
{
    public static class MvbWinFormxtensions
    {
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

        public static void MvbInvoke<T>(this T control, Action<T> call) where T : ISynchronizeInvoke
        {
            if (control.InvokeRequired) control.BeginInvoke(call, new object[] { control });
            else
                call(control);
        }
    }
}
