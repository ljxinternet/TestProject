using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using BroadcastTest.Broadcasts;
using BroadcastTest.Services;
using Java.Lang;

namespace BroadcastTest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        #region Field

        private TextView _txtView;
        private TestReceiver _receiver;

        
       
        


        #endregion


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //
            Init();
        }

        /// <summary>
        /// The init.
        /// </summary>
        private void Init()
        {
            
           
            //LocalBroadcastManager localBM = LocalBroadcastManager.GetInstance(this);
             _receiver=new TestReceiver();
            _txtView = FindViewById<TextView>(Resource.Id.txtView);
            _receiver.Alert += (s) =>
            {
                RunOnUiThread(() => { _txtView.Text = s; });
            };

            Button btnRegisterBroadcast = FindViewById<Button>(Resource.Id.btnRegisterBroadcast);
            btnRegisterBroadcast.Click += (send, e) =>
            {
                //RegisterBroadcast();
                var intentFilter = new IntentFilter("Xamarin.Broadcast.Test");
                RegisterReceiver(_receiver, intentFilter);
            };
            Button btnUnRegisterBroadcast = FindViewById<Button>(Resource.Id.btnUnRegisterBroadcast);
            btnUnRegisterBroadcast.Click += (send, e) =>
            {
                UnregisterReceiver(_receiver);
                //Intent intent=new Intent("Xamarin.Broadcast.Test");
                //SendBroadcast(intent);
            };
            Button btnStartServce = FindViewById<Button>(Resource.Id.btnStartServce);
            btnStartServce.Click += (send, e) =>
            {
                Intent testService = new Intent(this, typeof(TestService));
                StartService(testService);
            };
            Button btnStopService = FindViewById<Button>(Resource.Id.btnStopService);
            btnStopService.Click += (send, e) => { StopService(new Intent(this, typeof(TestService))); };
        }

        /// <summary>
        /// The register broadcast.
        /// </summary>
        void RegisterBroadcast()
        {
            var intentFilter = new IntentFilter("Xamarin.Broadcast.Test");
            TestReceiver testReceiver = new Broadcasts.TestReceiver();
            RegisterReceiver(testReceiver, intentFilter);
        }
    }
}

