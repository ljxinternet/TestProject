using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

namespace Zcom.ndsc
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Net;
    using System.Threading.Tasks;

    using Android.Content;
    using Android.Graphics;
    using Android.Net;
    using Android.Util;

    using Java.Lang;
    using Java.Net;


    using Zcom.ndsc.Services;

    using Color = Android.Graphics.Color;
    using NotificationCompat =Android.Support.V4.App.NotificationCompat;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : BaseActivity
    {
        //class MyClass:Object
        //{
        //    public string HTML;
        //}

        #region StartContent
        
        public static void ActionStart(Context context, string  args)
        {
            Intent intent=new Intent(context,typeof(MainActivity));
            intent.PutExtra("arg1", args);
            context.StartActivity(intent);
        }

        #endregion


        #region Field

        private Button btnNotification,btnTestService, btnControlTest;

        //private MyClass my;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //
            Init();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void Init()
        {
            //
            btnNotification = FindViewById<Button>(Resource.Id.btnNotificationTest);
            btnNotification.Click += BtnNotificationClick;
            //
            btnTestService = FindViewById<Button>(Resource.Id.btnServiceTest);
            btnTestService.Click += BtnTestService_Click;
            //
            btnControlTest = FindViewById<Button>(Resource.Id.btnControlTest);
            btnControlTest.Click += BtnControlTest_Click;
            //
            Button btnListView = FindViewById<Button>(Resource.Id.main_btnListViewTest);
            btnListView.Click += BtnListView_Click;
        }


        private void BtnListView_Click(object sender, System.EventArgs e)
        {
            ListViewActivity.ActionStart(this,"Hello");
            //throw new System.NotImplementedException();
        }

        private void BtnControlTest_Click(object sender, System.EventArgs e)
        {
            SecondActivity.ActionStart(this,"Test");
            //throw new System.NotImplementedException();
        }

        private void BtnTestService_Click(object sender, System.EventArgs e)
        {
            var serviceIntent=new Intent(this,typeof(ServiceTestActivity));
            StartActivity(serviceIntent);
            //throw new System.NotImplementedException();
        }

        private void BtnNotificationClick(object sender, System.EventArgs e)
        {
            var client = new Intent("Xamarin.Broadcast.Test");
            SendBroadcast(client);



            //Intent intent = new Intent(this, typeof(MainActivity));

            //PendingIntent pIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.UpdateCurrent);

            //var customeView = new RemoteViews(this.PackageName, Resource.Layout.CustomeView);
            //customeView.SetOnClickPendingIntent(Resource.Id.tVContent, pIntent);
            //customeView.SetTextViewText(Resource.Id.tVTitle, "这是自定义标题");
            //customeView.SetTextViewText(Resource.Id.tVContent, "这是自定义内容");

            ////Notification.Builder builder = new Notification.Builder(this).SetAutoCancel(true).SetContentIntent(pIntent).SetCustomContentView(customeView);
            //Notification.Builder builder = new Notification.Builder(this)
            //    .SetAutoCancel(true)
            //    .SetContentIntent(pIntent)
            //    .SetContentTitle("Title")
            //    .SetContentText("Text")
            //    .SetContent(customeView);
            //    //.SetCustomContentView(customeView);
            //Notification notification = builder.Build();


            //NotificationManager manager = (NotificationManager)GetSystemService(Context.NotificationService);
            //manager.Notify(0, notification);
            ////throw new System.NotImplementedException();
        }
    }
}

