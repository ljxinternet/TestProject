using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace ZCameraTest
{
    [Activity(Label = "CameraActivity")]
    public class CameraActivity : AppCompatActivity
    {
        #region Field

        private Camera _camera;

        #endregion


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        protected override void OnResume()
        {

            base.OnResume();
        }

        protected override void OnPause()
        {

            base.OnPause();
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var m1 = menu.Add(0, 1, 0, "Camera");
            m1.SetShowAsActionFlags(ShowAsAction.WithText|ShowAsAction.IfRoom);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case 1:
                    Android.Net.Uri uri = Android.Net.Uri.Parse("geo:39.9,116.3");
                    Intent intent = new Intent(Intent.ActionView, uri);
                    StartActivity(intent);
                    //var intent=new Intent();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}