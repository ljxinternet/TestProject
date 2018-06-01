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
    using Android.Support.V7.App;
    [Activity(Label = "ServiceTestActivity")]
    public class ServiceTestActivity : AppCompatActivity
    {
        #region Field

        private Button btnStartService, btnStopService;

        //private int serviceId;

        #endregion


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Activity_Service);

            //初始化
            Init();
        }

        void Init()
        {
            //初始化控件
            btnStartService = FindViewById<Button>(Resource.Id.btnStartService);
            btnStopService = FindViewById<Button>(Resource.Id.btnStopService);
            //绑定单击事件
            btnStartService.Click += BtnStartService_Click;
            btnStopService.Click += BtnStopService_Click;

            //
        }

        private void BtnStopService_Click(object sender, EventArgs e)
        {
            StopService(new Intent(this, typeof(TestService)));
            //throw new NotImplementedException();
        }

        private void BtnStartService_Click(object sender, EventArgs e)
        {
            var serviceIntent=new Intent(this,typeof(TestService));
            //serviceIntent.Extras.PutString("Test","Hello World!");
            StartService(serviceIntent);
            //throw new NotImplementedException();
        }
    }
}