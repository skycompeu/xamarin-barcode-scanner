using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Widget;
using Android.OS;
using Android;
using System.Threading.Tasks;
using OmniReader.GUI.Droid.Emdk;
using FreshMvvm;
using OmniReader.Core.EMDK;

namespace OmniReader.GUI.Droid
{
    [Activity(Label = "OmniReader.GUI", Icon = "@mipmap/or_logo", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private App m_App = null;
        private IEMDKScanner Scanner = null;
        
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            
            await TryToGetPermissions();
            
            SetupScanner();
            
            LoadApplication(m_App = new App());
        }

        private void SetupScanner()
        {
            Scanner = new EMDKScanner(this);
            Scanner.StartScanner();

            FreshIOC.Container.Register(Scanner);
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (Scanner != null)
                Scanner.ResumeScanner();
        }
        
        protected override void OnPause()
        {
            base.OnPause();
            if (Scanner != null)
                Scanner.PauseScanner();
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (Scanner != null)
                Scanner.DestroyScanner();
        }
        

        #region RuntimePermissions
        async Task TryToGetPermissions()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                await GetPermissionsAsync();
                return;
            }
        }

        const int RequestLocationId = 0;
        
        readonly string[] PermissionsGroupLocation =
        {
            Android.Manifest.Permission.WriteExternalStorage,
            Android.Manifest.Permission.ReadExternalStorage
        };
        
        async Task GetPermissionsAsync()
        {
            const string permission = Manifest.Permission.AccessFineLocation;

            if (CheckSelfPermission(permission) == (int)Android.Content.PM.Permission.Granted)
            {
                //TODO change the message to show the permissions name
                Toast.MakeText(this, "Special permissions granted", ToastLength.Short).Show();
                return;
            }

            if (ShouldShowRequestPermissionRationale(permission))
            {
                //set alert for executing the task
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Permissions Needed");
                alert.SetMessage("The application need special permissions to continue");
                alert.SetPositiveButton("Request Permissions", (senderAlert, args) =>
                {
                    RequestPermissions(PermissionsGroupLocation, RequestLocationId);
                });

                alert.SetNegativeButton("Cancel", (senderAlert, args) =>
                {
                    Toast.MakeText(this, "Cancelled!", ToastLength.Short).Show();
                });

                Dialog dialog = alert.Create();
                dialog.Show();


                return;
            }

            RequestPermissions(PermissionsGroupLocation, RequestLocationId);

        }

        private void GetPermissions()
        {
            const string permission = Manifest.Permission.AccessFineLocation;

            if (CheckSelfPermission(permission) == (int)Android.Content.PM.Permission.Granted)
            {
                //TODO change the message to show the permissions name
                Toast.MakeText(this, "Special permissions granted", ToastLength.Short).Show();
                return;
            }

            if (ShouldShowRequestPermissionRationale(permission))
            {
                //set alert for executing the task
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Permissions Needed");
                alert.SetMessage("The application need special permissions to continue");
                alert.SetPositiveButton("Request Permissions", (senderAlert, args) =>
                {
                    RequestPermissions(PermissionsGroupLocation, RequestLocationId);
                });

                alert.SetNegativeButton("Cancel", (senderAlert, args) =>
                {
                    Toast.MakeText(this, "Cancelled!", ToastLength.Short).Show();
                });

                Dialog dialog = alert.Create();
                dialog.Wait();
                dialog.Show();


                return;
            }

            RequestPermissions(PermissionsGroupLocation, RequestLocationId);

        }
        
        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestLocationId:
                {
                    if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
                    {
                        Toast.MakeText(this, "Special permissions granted", ToastLength.Short).Show();

                    }
                    else
                    {
                        Toast.MakeText(this, "Special permissions denied", ToastLength.Short).Show();
                    }
                }

                break;
            }
        }

        #endregion
    }
}