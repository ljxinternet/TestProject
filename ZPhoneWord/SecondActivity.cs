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

namespace Zcom.ndsc
{
    [Activity(Label = "SecondActivity")]
    public class SecondActivity : BaseActivity
    {
        #region Field

        Button btnTest;
        TextView txtView;
        EditText etTextTest;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_Second);
            InitUI();
        }

        private void InitUI()
        {
            btnTest = FindViewById<Button>(Resource.Id.second_btnTest);
            btnTest.Click += BtnTest_Click;
            txtView = FindViewById<TextView>(Resource.Id.second_txtView);
            txtView.Click += TxtView_Click;
            etTextTest = FindViewById<EditText>(Resource.Id.second_etText);
            etTextTest.Click += EtTextTest_Click;
            //throw new NotImplementedException();
        }

        private void EtTextTest_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, etTextTest.Text, ToastLength.Short).Show();
            //throw new NotImplementedException();
        }

        private void TxtView_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, txtView.Text, ToastLength.Short).Show();
            //throw new NotImplementedException();
        }

        private void BtnTest_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, etTextTest.Text, ToastLength.Short).Show();
            ProgressDialog dialog = new ProgressDialog(this);
            dialog.SetTitle("正在进行...");
            dialog.SetMessage("请等待...");
            dialog.SetProgressStyle(ProgressDialogStyle.Horizontal);
            dialog.Show();

            //throw new NotImplementedException();
        }

        public static void ActionStart(Context context, string args)
        {
            Intent intent = new Intent(context, typeof(SecondActivity));
            intent.PutExtra("arg1", args);
            context.StartActivity(intent);
        }
    }
}