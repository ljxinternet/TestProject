using System;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
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
            Intent alarmService = new Intent(this, typeof(AlarmService));
            StartService(alarmService);



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
            Intent alarmService = new Intent(this, typeof(AlarmService));
            StopService(alarmService);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

        }


        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
        }

        void GetHtmlAsync(string url, Func<string> OnSuccess, Action<string> OnError)
        {
            SufeiUtil.HttpHelper http=new HttpHelper();
            HttpItem item=new HttpItem()
            {
                URL = url,
                Method = "GET",
                ResultType = ResultType.String
            };
            try
            {
                HttpResult result = http.GetHtml(item);
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    OnSuccess();
                }
                else
                {
                    OnError("网络错误.");
                }
            }
            catch (Exception e)
            {
                OnError(e.Message);
                //Console.WriteLine(e);
                //throw;
            }
           

        }

        //
        private LocationManager locationManager;

        private Location location;
        //

        void GetGPSLocation()
        {
            //获取地理位置管理器

            //locationManager = (LocationManager)GetSystemService(Context.LocationService);
            ////获取地理位置信息设置查询条件
            //if (locationManager.IsProviderEnabled(LocationManager.NetworkProvider))
            //{
            //    location = locationManager.GetLastKnownLocation(LocationManager.NetworkProvider);
               
            //}
            //else
            //{
            //    Criteria criteria=new Criteria();
            //    criteria.CostAllowed = true;
            //    criteria.AltitudeRequired = false;
            //    criteria.BearingRequired = false;
            //    locationManager.GetBestProvider(criteria,true);
            //    locationManager.RequestLocationUpdates();
            //}
            //if (location != null)
            //{
                
            //}
        }

    }
}

