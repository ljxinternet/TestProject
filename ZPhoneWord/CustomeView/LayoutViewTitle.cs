using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Zcom.ndsc.CustomeView
{
    public class LayoutViewTitle : RelativeLayout
    {
        public LayoutViewTitle(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize(context);
        }

        public LayoutViewTitle(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize(context);
        }

        private void Initialize(Context context)
        {
            LayoutInflater.From(context).Inflate(Resource.Layout.CustomeView,this);

            //TextView tvTitle = FindViewById<Button>(Resource.Id.tVTitle);
            //tvTitle.Click += (sender, e) => { Toast.MakeText(Application.Context, "Title", ToastLength.Short).Show(); };
            //TextView tvContext = FindViewById<Button>(Resource.Id.tVContent);
            //tvTitle.Click += (sender, e) => { Toast.MakeText(Application.Context, "Context", ToastLength.Short).Show(); };
            Button btnTest = FindViewById<Button>(Resource.Id.customeView_btnTest);
            btnTest.Click += (sender, e) =>
                {
                    Toast.MakeText(Application.Context,"O(∩_∩)O哈哈~",ToastLength.Short).Show();
                };
        }

        
    }
}