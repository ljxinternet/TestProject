using System;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.Media;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using BroadcastTest.Broadcasts;
using BroadcastTest.Services;
using SufeiUtil;
using Exception =System.Exception;
//
using ZXing;
using ZXing.Mobile;

namespace BroadcastTest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        #region Field

        private EditText etTextTime;
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
            etTextTime = FindViewById<EditText>(Resource.Id.etTextTime);

            Button btnRegisterBroadcast = FindViewById<Button>(Resource.Id.btnRegisterBroadcast);
            btnRegisterBroadcast.Click += (send, e) =>
            {
            };
            Button btnUnRegisterBroadcast = FindViewById<Button>(Resource.Id.btnUnRegisterBroadcast);
            btnUnRegisterBroadcast.Click += (send, e) =>
            {
            };
            Button btnStartServce = FindViewById<Button>(Resource.Id.btnStartServce);
            btnStartServce.Click += (send, e) =>
            {

            };
            Button btnStopService = FindViewById<Button>(Resource.Id.btnStopService);
            btnStopService.Click += (send, e) => {  };

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

        private async void BtnTest_Click(object sender, EventArgs e)
        {
            var intent=new Intent("Xamarin.Broadcast.Test2");
            SendBroadcast(intent);
            //
           ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);
            ZXing.Mobile.MobileBarcodeScanner scanner=new MobileBarcodeScanner();
            try
            {
                var result = await scanner.Scan();
                if (!string.IsNullOrEmpty(result.Text))
                {
                    ScanResultHandle(result);
                }
            }
            catch (Exception exception)
            {
                //Console.WriteLine(exception);
                //throw;
            }
            

            //throw new NotImplementedException();
        }

        /// <summary>
        /// 获取扫描结果的处理
        /// </summary>
        private void ScanResultHandle(ZXing.Result result)
        {
            string url = result.Text;
            if (!string.IsNullOrEmpty(url))
            {
                Toast.MakeText(this,url,ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "扫描取消", ToastLength.Short).Show();
            }
        }
        
        private void BtnStopAlarm_Click(object sender, EventArgs e)
        {
                PendingIntent pIntent = PendingIntent.GetBroadcast(Application.Context, 0, new Intent(Android.App.Application.Context, typeof(AlarmReceiver)), 0);

         AlarmManager _alarmManager =(AlarmManager) Android.App.Application.Context.GetSystemService(AlarmService);
        _alarmManager.Cancel(pIntent);
            Intent alarmService = new Intent(this, typeof(AlarmTestService));
            StopService(alarmService);
                Toast.MakeText(this, "定时任务被取消", ToastLength.Short).Show();


        }

        private void BtnStartAlarm_Click(object sender, EventArgs e)
        {
            Intent alarmService = new Intent(this, typeof(AlarmTestService));
            int mTime = int.Parse(etTextTime.Text);
            alarmService.PutExtra("Time", mTime * 1000);
            StartService(alarmService);
            Toast.MakeText(this, "定时任务启动", ToastLength.Short).Show();
        }
    }
}

