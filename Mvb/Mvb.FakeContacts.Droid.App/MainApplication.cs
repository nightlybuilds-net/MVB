using System;

using Android.App;
using Android.OS;
using Android.Runtime;
using DryIoc;
using Mvb.FakeContacts.Abstract;
using Mvb.FakeContacts.Concrete;
using Mvb.FakeContacts.Droid.Services;
using Mvb.FakeContacts.ModelBinders;
using Plugin.CurrentActivity;

namespace Mvb.FakeContacts.Droid.App
{
	//You can specify additional application information in this attribute
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public static IContainer Ioc;

        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          :base(handle, transer)
        {}

        public override void OnCreate()
        {
            base.OnCreate();
            this.RegisterActivityLifecycleCallbacks(this);

            Mvb.Droid.Mvb.Init();

            //Init IOC
            Ioc = new Container();

            //Services
            Ioc.Register<IContactServices, DroidContactsServices>();
            Ioc.Register<IAvatarServices, CommonAvatarServices>();

            //ModelBinders
            Ioc.Register<ContactsSummaryModelBinders>(Reuse.Singleton);
            Ioc.Register<ContactsModelBinders>(Reuse.Singleton);
            

        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            this.UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}