using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

namespace VideoPlayer
{
    using Android.Content.PM;
    using Android.Net;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true,ConfigurationChanges = ConfigChanges.Keyboard|ConfigChanges.KeyboardHidden|ConfigChanges.Orientation|ConfigChanges.ScreenSize)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //

        }
        

    }
}

