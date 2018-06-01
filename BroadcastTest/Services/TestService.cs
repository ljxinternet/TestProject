using System;
using System.Collections.Generic;
using System.Linq;
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
        //private NotificationManager nm;
        private Notification notification;

        public override IBinder OnBind(Intent intent)
        {
            return null;
            //throw new NotImplementedException();
        }

        public override void OnCreate()
        {
            base.OnCreate();
            isRun = true;
            ServerNotification();
            SetForeground(true);
            SetForeground(true);
            //Toast.MakeText(Application.Context,)
        }

        void ServerNotification()
        {
            var intent=new Intent(Application.Context,typeof(MainActivity));
            PendingIntent pendingIntent=PendingIntent.GetActivity(Application.Context,0,intent,PendingIntentFlags.UpdateCurrent);
            Notification.Builder builder = new Notification.Builder(Application.Context).SetContentIntent(pendingIntent);
            builder.SetAutoCancel(false).SetContentTitle("BroadcastTest").SetContentText("TestService Running...");
            builder.SetSmallIcon(Resource.Drawable.notification_template_icon_bg).SetOngoing(true);
            
            notification = builder.Build();

            //nm = (NotificationManager)GetSystemService(NotificationService);

            //nm.Notify(99999, notification);

            StartForeground(99999, notification);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            isRun = false;
            //nm.Cancel(99999);
            SetForeground(false);
            StopForeground(true);
            //Reboot
            Intent testService = new Intent(this, typeof(TestService));
            StartService(testService);

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
                while (isRun)
                {
                    var client = new Intent("Xamarin.Broadcast.Test");
                    //client.PutExtra("msg",i);
                    Log.Error("Service", "OnStartCommand | SendBroadcast");
                    client.PutExtra("msg", i);
                    SendBroadcast(client);
                    i++;
                    Thread.Sleep(10000);
                }
            });
            //new System.Threading.Thread(() =>
            //{
                
            //}).Start();
            //var client = new Intent("Xamarin.Broadcast.Test");
            //SendBroadcast(client);

            //SetForeground(true);

            


            return StartCommandResult.Sticky;
        }
    }
}