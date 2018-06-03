using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using BroadcastTest.Services;
using SufeiUtil;

namespace BroadcastTest.Broadcasts
{
    [BroadcastReceiver(Enabled =true,Exported =true)]
    [IntentFilter(new string[] { "Xamarin.Broadcast.AlarmTests", "android.net.conn.CONNECTIVITY_CHANGE" })]
    public class AlarmReceiver : BroadcastReceiver
    {
        #region Field

        private HttpHelper _http;
        private HttpItem _item;
        private HttpResult _result;
        private AlarmManager _alarmManager;
        
        private int _time;

        #endregion

        public Action<string> Alert;


        public override void OnReceive(Context context, Intent intent)
        {
            _time = intent.GetIntExtra("Time",60*60*1000);
            Handler myHandler=new Handler();

            myHandler.Post(() => { Toast.MakeText(Application.Context, $"接收到广播.Time:{_time}", ToastLength.Short).Show(); });

            //var mIntent=new Intent(context,typeof(TestService));
            //mIntent.PutExtra("Time", _time);
            //context.StartService(mIntent);



            GetHtmlAsync("http://ip.chinaz.com/getip.aspx", (html) =>
                {
                    myHandler.Post(() => { Toast.MakeText(context, html, ToastLength.Short).Show(); });
                }, (msg) =>
                {
                    myHandler.Post(() => { Toast.MakeText(context, $"Error:{msg}", ToastLength.Short).Show(); });
                });

            //设定定时程序
            AlarmTask(context, intent, _time);
        }

        /// <summary>
        /// 设置定时任务
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="intent"></param>
        /// <param name="time">间隔时间(秒)</param>
        void AlarmTask(Context context, Intent intent,long time)
        {
            
            _alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            long triggerAtMills = SystemClock.ElapsedRealtime() + time;
            PendingIntent pIntent = PendingIntent.GetBroadcast(Application.Context, 0, new Intent(Android.App.Application.Context, typeof(AlarmReceiver)), 0);
            _alarmManager.Cancel(pIntent);
            _alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, triggerAtMills, pIntent);
        }

        /// <summary>
        /// 获取Html
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="OnSuccess">成功后调用,返回html</param>
        /// <param name="OnError">失败后调用,返回error信息</param>
        void GetHtmlAsync(string url,Action<string> OnSuccess,Action<string> OnError)
        {
            Task.Factory.StartNew(() =>
            {
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
            });
        }


    }
}