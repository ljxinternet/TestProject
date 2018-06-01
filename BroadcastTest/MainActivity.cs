using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using BroadcastTest.Broadcasts;
using BroadcastTest.Services;
using Exception = Java.Lang.Exception;

namespace BroadcastTest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        #region Field

        private EditText etTextTime;
        private TestReceiver _receiver;
        private AlarmManager _alarmManager;


        //private readonly string SERVICE_TEST = "Xamarin.Service.Test";
        private readonly string BROADCAST_TEST = "Xamarin.Broadcast.Test";



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
            etTextTime = FindViewById<EditText>(Resource.Id.etTextTime);

            Button btnRegisterBroadcast = FindViewById<Button>(Resource.Id.btnRegisterBroadcast);
            btnRegisterBroadcast.Click += (send, e) =>
            {
                //RegisterBroadcast();
                var intentFilter = new IntentFilter(BROADCAST_TEST);
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
                Intent testService = new Intent(this, typeof(AlarmService));
                try
                {
                    int mTime = int.Parse(etTextTime.Text);
                    testService.PutExtra("Time", mTime);
                    StartService(testService);
                }
                catch (Exception exception)
                {
                    Toast.MakeText(this, exception.Message,ToastLength.Short).Show();
                    //Console.WriteLine(exception);
                    //throw;
                }
                
            };
            Button btnStopService = FindViewById<Button>(Resource.Id.btnStopService);
            btnStopService.Click += (send, e) => { StopService(new Intent(this, typeof(AlarmService))); };

            //
            Button btnStartAlarm = FindViewById<Button>(Resource.Id.btnStartAlarm);
            btnStartAlarm.Click += BtnStartAlarm_Click;

            //
            Button btnStopAlarm = FindViewById<Button>(Resource.Id.btnStopAlarm);
            btnStopAlarm.Click += BtnStopAlarm_Click;
            //
            Button btnTest = FindViewById<Button>(Resource.Id.btnTest);
            btnTest.Click += BtnTest_Click;

        }

        private void BtnTest_Click(object sender, EventArgs e)
        {
            var intent=new Intent("Xamarin.Broadcast.Test2");
            SendBroadcast(intent);
            //throw new NotImplementedException();
        }

        private void BtnStopAlarm_Click(object sender, EventArgs e)
        {
            if (_alarmManager!=null)
            {
                PendingIntent pIntent = PendingIntent.GetService(Application.Context, 0, new Intent(Android.App.Application.Context, typeof(AlarmService)), 0);
                _alarmManager.Cancel(pIntent);
                _alarmManager = null;
                Toast.MakeText(this, "定时任务被取消", ToastLength.Short).Show();

            }
            //throw new NotImplementedException();
        }

        private void BtnStartAlarm_Click(object sender, EventArgs e)
        {
            int mTime = int.Parse(etTextTime.Text);
            _alarmManager = (AlarmManager)GetSystemService(AlarmService);
            long triggerAtMills = SystemClock.ElapsedRealtime() + mTime * 1000;
            PendingIntent pIntent = PendingIntent.GetService(Application.Context, 0, new Intent(Android.App.Application.Context, typeof(AlarmService)), 0);
            //_alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, triggerAtMills, pIntent);
            _alarmManager.SetRepeating(AlarmType.ElapsedRealtimeWakeup, SystemClock.CurrentThreadTimeMillis(), mTime * 1000, pIntent);
            //_alarmManager.SetWindow(AlarmType.ElapsedRealtimeWakeup,SystemClock.CurrentThreadTimeMillis(),mTime*1000,pIntent);
            Toast.MakeText(this, $"定时任务启动,间隔{mTime}秒",ToastLength.Short).Show();
        }

        /// <summary>
        /// The register broadcast.
        /// </summary>
        void RegisterBroadcast()
        {
            var intentFilter = new IntentFilter(BROADCAST_TEST);
            TestReceiver testReceiver = new Broadcasts.TestReceiver();
            RegisterReceiver(testReceiver, intentFilter);
        }
    }
}

