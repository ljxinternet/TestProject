using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BroadcastTest.Broadcasts
{
    [BroadcastReceiver]
    //[BroadcastReceiver(Enabled = true,Exported = true)]
    //[IntentFilter(new string[] { "android.net.conn.CONNECTIVITY_CHANGE","Xamarin.Broadcast.Test2" })]
    ////[Register("ZOM.BroadcastTest.BroadcastTest.Broadcasts.StartServiceReceiver")]
    public class StartServiceReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, "网络状态发生改变.", ToastLength.Short).Show();
        }
    }
}