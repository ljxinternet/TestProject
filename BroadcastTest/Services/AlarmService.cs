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
    [IntentFilter(new string[]{ "Xamarin.Service.Alarm.YiJian" })]
    public class AlarmService : Service
    {
        private bool isRun;
        private NotificationManager _nm;
        private Notification notification;
        private int _time;
        private HttpHelper _http;
        private HttpItem _item;
        private AlarmManager alarm;
        private Intent _intent;

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
            _nm = (NotificationManager)GetSystemService(NotificationService);
            _intent = new Intent(Application.Context, typeof(MainActivity));
            ServerNotification("Alarm Running...");
        }

        void ServerNotification(string text)
        {
            PendingIntent pendingIntent=PendingIntent.GetActivity(Application.Context,0,_intent,0);
            Notification.Builder builder = new Notification.Builder(Application.Context).SetContentIntent(pendingIntent);
            builder.SetAutoCancel(false).SetContentTitle("江苏建设人才网").SetContentText(text);
            
            builder.SetTicker("前台服务开启...");
            notification = builder.Build();
            _nm.Notify(1,notification);

            

            //nm.Notify(99999, notification);

            StartForeground(99999, notification);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            isRun = false;
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
                _time = intent.GetIntExtra("Time", 5*60*1000);

                    _item = new HttpItem()
                    {
                        URL = "http://hr.jscin.gov.cn/swhyFront/zyks/",
                        Method = "GET",
                        ResultType = ResultType.String
                    };
                    try
                    {
                        HttpResult result = _http.GetHtml(_item);
                        if (result.StatusCode == HttpStatusCode.OK)
                        {
                            var client2 = new Intent("Xamarin.Broadcast.Test2");
                            client2.PutExtra("HTML", result.Html);
                            SendBroadcast(client2);
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error("TestService", e.Message);
                    }

                StopSelf(startId);
                
               
            });

            return StartCommandResult.Sticky;
        }

    }
}