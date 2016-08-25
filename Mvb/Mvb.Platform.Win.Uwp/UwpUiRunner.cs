using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Mvb.Core.Abstract;
using Mvb.Core.Args;

namespace Mvb.Platform.Win.Uwp
{
    public class UwpUiRunner : IUiRunner
    {
        public async void Run(Action action)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, action.Invoke);
        }

        public async void Run<T>(Action<T> action, T args)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                action.Invoke(args);
            });
        }
    }
}
