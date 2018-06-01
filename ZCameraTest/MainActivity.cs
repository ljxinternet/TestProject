using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

namespace ZCameraTest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //
            InitUi();
        }

        private void InitUi()
        {
            Button btnCamera = FindViewById<Button>(Resource.Id.btnCameraTest);
            btnCamera.Click += BtnCamera_Click;
            //throw new System.NotImplementedException();
        }

        private void BtnCamera_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(CameraActivity));
            StartActivity(intent);
            //throw new System.NotImplementedException();
        }
    }
}

