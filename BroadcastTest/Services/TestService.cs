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
    [IntentFilter(new string[]{ "Xamarin.Service.Test" })]
    public class TestService : Service
    {
        #region Field

        private HttpHelper _http;
        private HttpItem _item;
        private HttpResult _result;
        private AlarmManager _alarmManager;
        private int _time;

        #endregion

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

       

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {

            Task.Factory.StartNew(() =>
            {
                _time = intent.GetIntExtra("Time", 10 * 1000);

                Handler myHandler = new Handler();
                myHandler.Post(() => { Toast.MakeText(Application.Context, "服务开始执行.", ToastLength.Short).Show(); });

                GetHtmlAsync("http://ip.chinaz.com/getip.aspx", (html) =>
                {
                    myHandler.Post(() => { Toast.MakeText(Application.Context, html, ToastLength.Short).Show(); });
                }, (msg) =>
                {
                    myHandler.Post(() => { Toast.MakeText(Application.Context, $"Error:{msg}", ToastLength.Short).Show(); });
                });

                //
                //设定定时程序
                AlarmTask(Application.Context, intent, _time);
            });
            
            return StartCommandResult.Sticky;
        }


        void GetHtmlAsync(string url, Action<string> OnSuccess, Action<string> OnError)
        {
            //Task.Factory.StartNew(() =>
            //{
            string html = null;

            _http = new HttpHelper();
            _item = new HttpItem()
            {
                URL = url,
                Method = "GET",
                ResultType = ResultType.String
            };

            try
            {
                HttpResult result = _http.GetHtml(_item);
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    html = result.Html;
                    OnSuccess(html);
                }
                else
                {
                    OnError("网络错误.HttpStatusCode!=OK");
                }
            }
            catch (Exception e)
            {
                OnError(e.Message);
            }
            //});


        }


        /// <summary>
        /// 设置定时任务
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="intent"></param>
        /// <param name="time">间隔时间(秒)</param>
        void AlarmTask(Context context, Intent intent, long time)
        {

            _alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            long triggerAtMills = SystemClock.ElapsedRealtime() + time;
            PendingIntent pIntent = PendingIntent.GetBroadcast(Application.Context, 0, new Intent(Android.App.Application.Context, typeof(AlarmReceiver)), 0);
            _alarmManager.Cancel(pIntent);
            _alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, triggerAtMills, pIntent);
        }

    }
}