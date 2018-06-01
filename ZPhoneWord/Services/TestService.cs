using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Zcom.ndsc.Services
{


    using Android.Graphics;
    using Android.Util;

    using Java.Lang;



    [Service]
    [IntentFilter(new string[]{"TestServiceDemons"})]
    public class TestService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            Log.Error("TestService", "OnBind");
            //throw new NotImplementedException();
            return null;
        }

        public override void OnCreate()
        {
            Log.Error("TestService", "OnCreate");
            base.OnCreate();
        }

        public override void OnDestroy()
        {
            Log.Error("TestService", "OnDestroy");
            base.OnDestroy();
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Log.Error("TestService", "OnStartCommand Frist");
            new System.Threading.Thread(() =>
                        {

                            //Toast.MakeText(Application.Context, "(～ o ～)~zZ", ToastLength.Short).Show();
                            Log.Error("TestService", "OnStartCommand EveryOnce");
                            Thread.Sleep(3000);
                        }).Start();


            return StartCommandResult.Sticky;
            //return base.OnStartCommand(intent, flags, startId);
        }

        public override ComponentName StartService(Intent service)
        {
            Log.Error("TestService", "StartService");
            return base.StartService(service);
        }

        public override bool StopService(Intent name)
        {
            Log.Error("TestService", "StopService");
            return base.StopService(name);
        }
    }
}