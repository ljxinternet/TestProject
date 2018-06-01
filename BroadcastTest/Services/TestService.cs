using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
//
using SufeiUtil;

namespace BroadcastTest.Services
{
    /// <summary>
    /// The test service.
    /// </summary>
    [Service(Enabled =true,Exported =true)]
    [IntentFilter(new string[]{ "Xamarin.Service.Test" })]
    public class TestService : Service
    {
        private bool isRun;
        private NotificationManager _nm;
        private Notification notification;
        private int _time;
        private HttpHelper _http;
        private HttpItem _item;
        private AlarmManager alarm;

        public override IBinder OnBind(Intent intent)
        {
            return null;
            //throw new NotImplementedException();
        }

        public override void OnCreate()
        {
            base.OnCreate();
            _http=new HttpHelper();
            isRun = true;
            ServerNotification();
            SetForeground(true);
            SetForeground(true);
            //Toast.MakeText(Application.Context,)
        }

        void ServerNotification()
        {
            var intent=new Intent(Application.Context,typeof(MainActivity));
            PendingIntent pendingIntent=PendingIntent.GetActivity(Application.Context,0,intent,0);
            Notification.Builder builder = new Notification.Builder(Application.Context).SetContentIntent(pendingIntent);
            builder.SetAutoCancel(false).SetContentTitle("BroadcastTest").SetContentText("TestService Running...");
            builder.SetTicker("前台服务开启...");
            builder.SetSmallIcon(Resource.Drawable.notification_template_icon_bg).SetOngoing(true);
            
            notification = builder.Build();

            //nm.Notify(99999, notification);

            StartForeground(99999, notification);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            isRun = false;
            //_nm.CancelAll();
            _nm = (NotificationManager)GetSystemService(NotificationService);
            _nm.Cancel(99999);
            PendingIntent pIntent = PendingIntent.GetService(Application.Context, 0, new Intent(Android.App.Application.Context, typeof(TestService)), 0);
            alarm.Cancel(pIntent);
            SetForeground(false);
            StopForeground(true);
            //Reboot
            //Intent testService = new Intent(this, typeof(TestService));
            //testService.PutExtra("Time", _time);
            //StartService(testService);

        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {

            Task.Factory.StartNew(() =>
            {
                int i = 0;
                //var http= new RestClient("http://hr.jscin.gov.cn");

                _time = intent.GetIntExtra("Time", 10000);

                //while (isRun)
                {
                    var client = new Intent("Xamarin.Broadcast.Test");
                    //client.PutExtra("msg",i);
                    Log.Error("Service", "OnStartCommand | SendBroadcast");
                    client.PutExtra("msg", i);
                    SendBroadcast(client);
                    i++;

                    _item = new HttpItem()
                    {
                        URL = "http://hr.jscin.gov.cn",
                        Method = "GET",
                        ResultType = ResultType.String
                    };
                    try
                    {
                        HttpResult result = _http.GetHtml(_item);
                        if (result.StatusCode == HttpStatusCode.OK)
                        {
                            client.PutExtra("HTML", result.Html);
                            SendBroadcast(client);
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error("TestService", e.Message);
                        //Console.WriteLine(e);
                        //throw;
                    }

                    //Thread.Sleep(_time * 1000);
                }
            });


             alarm = (AlarmManager) GetSystemService(AlarmService);
            long triggerAtMills = SystemClock.ElapsedRealtime() + 10 * 1000;
            PendingIntent pIntent=PendingIntent.GetService(Application.Context,0,new Intent(Android.App.Application.Context,typeof(TestService)), 0);
            alarm.Set(AlarmType.ElapsedRealtimeWakeup,triggerAtMills,pIntent);





            return StartCommandResult.Sticky;
        }

    }
}