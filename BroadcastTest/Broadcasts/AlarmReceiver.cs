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
using BroadcastTest.Services;

namespace BroadcastTest.Broadcasts
{
    [BroadcastReceiver(Enabled =true,Exported =true)]
    [IntentFilter(new string[] { "Xamarin.Broadcast.AlarmTests" })]
    public class AlarmReceiver : BroadcastReceiver
    {
        //private int j = 1;

        public Action<string> Alert;

        public override void OnReceive(Context context, Intent intent)
        {
            var  mIntent=new Intent(context,typeof(AlarmService));
            context.StartService(mIntent);
        }
    }
}