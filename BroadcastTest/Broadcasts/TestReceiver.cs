using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace BroadcastTest.Broadcasts
{
    [BroadcastReceiver(Enabled =true,Exported =true)]
    [IntentFilter(new string[]{ "Xamarin.Broadcast.Test" })]
    public class TestReceiver : BroadcastReceiver
    {
        //private int j = 1;

        public Action<string> Alert;

        public override void OnReceive(Context context, Intent intent)
        {

           
            string html = intent.GetStringExtra("HTML");
          
                Toast.MakeText(Application.Context, html, ToastLength.Short).Show();
                Log.Error("Boradcast", $"来自服务HTML");
    

            

            //Notification.Builder builder=new Notification.Builder(Application.Context);
            //builder.SetContentTitle("标题").SetContentText("内容").SetAutoCancel(true)
            //    .SetSmallIcon(Resource.Drawable.notification_template_icon_bg).SetColor(Color.Green);
            //Notification notification = builder.Build();
            //NotificationManager nm = (NotificationManager) context.GetSystemService(Context.NotificationService);
            //nm.Notify(j, notification);
            //j++;

        }
    }
}