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
using BroadcastTest.Broadcasts;
//
using SufeiUtil;

namespace BroadcastTest.Services
{
    /// <summary>
    /// The test service.
    /// </summary>
    [Service(Enabled =true,Exported =true)]
    [IntentFilter(new string[]{ "Xamarin.Service.Alarm.YiJian" })]
    public class AlarmTestService : Service
    {
        private bool isRun;
        private NotificationManager _nm;
        private Notification notification;



        private AlarmManager alarm;
        private Intent _intent;
        private AlarmReceiver _alarmReceiver;


        public override IBinder OnBind(Intent intent)
        {
            return null;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 创建服务
        /// </summary>
        public override void OnCreate()
        {
            base.OnCreate();
            //
            isRun = true;
            _nm = (NotificationManager)GetSystemService(NotificationService);
            _intent = new Intent(Application.Context, typeof(MainActivity));
            //通知
            ServerNotification("Alarm Running...");
            //注册广播
            _alarmReceiver=new AlarmReceiver();
            IntentFilter intentFilter=new IntentFilter();
            intentFilter.AddAction("Xamarin.Broadcast.AlarmTests");
            RegisterReceiver(_alarmReceiver,intentFilter);

        }

        /// <summary>
        /// 前台通知：避免被杀
        /// </summary>
        /// <param name="text"></param>
        void ServerNotification(string text)
        {
            PendingIntent pendingIntent=PendingIntent.GetActivity(Application.Context,0,_intent,0);
            Notification.Builder builder = new Notification.Builder(Application.Context).SetContentIntent(pendingIntent);
            builder.SetAutoCancel(false).SetContentTitle("Alarm定时任务").SetContentText(text);
            builder.SetSmallIcon(Resource.Drawable.notification_template_icon_bg);
            
            notification = builder.Build();
            //_nm.Notify(1,notification);
            StartForeground(99999, notification);
            SetForeground(true);
        }

        /// <summary>
        /// 销毁服务
        /// </summary>
        public override void OnDestroy()
        {
            base.OnDestroy();
            //
            isRun = false;
            //销毁广播
            UnregisterReceiver(_alarmReceiver);
            StopForeground(true);
            SetForeground(false);
            _nm.Cancel(99999);
            
            
        }
        
        /// <summary>
        /// 执行服务
        /// </summary>
        /// <param name="intent"></param>
        /// <param name="flags"></param>
        /// <param name="startId"></param>
        /// <returns></returns>
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            var mIntent=new Intent("Xamarin.Broadcast.AlarmTests");
            int mTime = intent.GetIntExtra("Time",60*60*1000);
            mIntent.PutExtra("Time", mTime);
            SendBroadcast(mIntent);

            return StartCommandResult.Sticky;
        }

    }
}