using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OmniReader.Core.EMDK;
using OmniReader.GUI.Droid.Services;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AppService))]
namespace OmniReader.GUI.Droid.Services
{
    public class AppService : IAppService
    {
        public void CloseApp()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
        
        public void FinishAffinity()
        {
            Activity activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }

    }
}