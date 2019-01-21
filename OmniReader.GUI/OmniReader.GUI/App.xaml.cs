using OmniReader.Core.Generic;
using OmniReader.Core.Manager;
using OmniReader.Data.Database;
using OmniReader.Data.Database.Model;
using OmniReader.Data.Database.Repository;
using OmniReader.GUI.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms.PlatformConfiguration;
using System.Collections.Generic;
using System.Threading.Tasks;
using OmniReader.Core.App;
using OmniReader.GUI.Helper;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OmniReader.GUI
{
    public partial class App : Application
    {
        private AppInitializer AppInitializer = null;

        public App()
        {
            InitializeComponent();

            AppInitializer = new AppInitializer();
            AppInitializer.InitalizeDatabase();
            AppInitializer.InitalizeGlobalObjects();
            AppInitializer.InitializeConfig();

            /* Set css theme version */
            ViewHelper.SetAppGlobalTheme(Resources, "1");
            
            //old
            MainPage = new NavigationPage(new LoginView());
        }

        /// <summary>
        /// OnStart() method calls when your application is started at first. When the application starts, it reads all the code written in OnStart method
        /// </summary>
        protected override void OnStart()
        {
            //
        }

        /// <summary>
        /// OnSleep() method calls when your application is in sleep state, i.e., there is no work going on in your application. Sleep method calls when user hides the application. In this form, your application is open in background and is in sleep state.
        /// </summary>
        protected override void OnSleep()
        {
            //
        }

        /// <summary>
        /// OnResume() Method is called when user comes back into application after sleep state.
        /// </summary>
        protected override void OnResume()
        {
            //
        }
    }
}
