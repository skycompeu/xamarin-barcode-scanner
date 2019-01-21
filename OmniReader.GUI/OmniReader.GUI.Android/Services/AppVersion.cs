using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OmniReader.Core.EMDK;
using OmniReader.GUI.Droid.Services;

[assembly: Xamarin.Forms.Dependency(typeof(AppVersion))]
namespace OmniReader.GUI.Droid.Services
{
    public class AppVersion : IAppVersion
    {
        PackageInfo m_appInfo;

        public AppVersion()
        {
            var context = Android.App.Application.Context;
            m_appInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
        }
        
        public string GetVersion()
        {
            return m_appInfo.VersionName;
        }
    }
}